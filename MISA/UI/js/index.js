let currentPage = 1;
let rowsPerPage = 10;
let employees = [];
let currentDelete;
let shouldFetchData = true;
let port = 7129;
let currentId = "0";
let totalPage = 0;

// Hàm để lấy dữ liệu nhân viên từ API
function fetchEmployees() {
    // if (!shouldFetchData) return;

    try {
        fetch(`https://localhost:${port}/api/v1/Employees`)
            .then((res) => res.json())
            .then((data) => {
                console.log(data);
                employees = data;
                renderTable();
                totalPage = employees.length % rowsPerPage == 0 ? employees.length / rowsPerPage : employees.length / rowsPerPage + 1;
                currentPage = 1;
                renderSelect();
                shouldFetchData = false;
            });
    } catch (error) {
        console.error(error);
    }
}

function renderSelect(){
    let select = document.getElementById("recordsPerPage");
    let options = ``;
    for(let i=1; i<= totalPage; i++){
        options += `<option value="${i}">${i}</option>`;
    }
    select.innerHTML = options;
}

// Hàm để hiển thị bảng nhân viên
function renderTable() {
    const tableBody = document.querySelector("#employeeTableBody");
    tableBody.innerHTML = '';

    const start = (currentPage - 1) * rowsPerPage;
    const end = start + rowsPerPage;
    const paginatedItems = employees.slice(start, end);

    let i = start + 1;
    for (const item of paginatedItems) {
        let tr = document.createElement("tr");
        tr.innerHTML = `<td>${i++}</td>
                        <td>${item.employeeCode}</td>
                        <td>${item.fullName}</td>
                        <td>${item.gender == 1 ? "Nam" : "Nữ" ?? ""}</td>
                        <td>${item.dateOfBirth?.substring(0, 10) ?? ""}</td>    
                        <td>${item.email ?? ""}</td>
                        <td style="position: relative;">
                            ${item.address ?? ""}
                            <div>
                                <button data-id="${item.employeeId}" class="button-edit"><img src="../assets/icon/info-48.png" alt=""></button>
                                <button data-id="${item.employeeId}" class="button-delete"><img src="../assets/icon/delete-48.png" alt=""></button>
                            </div>
                        </td>`;
        tableBody.appendChild(tr);
    }

    updatePaginationButtons();
    addDeleteButtonEvents();
    addEditButtonEvents();
}

// Hàm để cập nhật nút phân trang
function updatePaginationButtons() {
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');

    prevBtn.disabled = currentPage === 1;
    nextBtn.disabled = currentPage === Math.ceil(employees.length / rowsPerPage);
}

// Hàm để chuyển đến trang trước
function prevPage() {
    if (currentPage > 1) {
        currentPage--;
        renderTable();
        document.getElementById("recordsPerPage").value = currentPage;

    }
}

// Hàm để chuyển đến trang sau
function nextPage() {
    if (currentPage < Math.ceil(employees.length / rowsPerPage)) {
        currentPage++;
        document.getElementById("recordsPerPage").value = currentPage;
        renderTable();
    }
}

// Hàm để thay đổi số hàng mỗi trang
function changeRowsPerPage() {
    // rowsPerPage = parseInt(document.getElementById('recordsPerPage').value, 10);
    currentPage = document.getElementById("recordsPerPage").value;
    renderTable();
}

function addEditButtonEvents(){
    let buttons = document.querySelectorAll(".button-edit")
    for (const button of buttons) {
        button.addEventListener('click', function(event){
            let currentIdEdit = button.getAttribute('data-id');
            nextPageEmployee(currentIdEdit);
        });
    }
}

// Hàm để thêm sự kiện cho các nút xóa
function addDeleteButtonEvents() {
    let buttons = document.querySelectorAll(".button-delete");
    for (const button of buttons) {
        button.addEventListener('click', function (event) {
            console.log("oaiwehfowiehfoeiw")
            let employeeRow = event.target.closest('tr');
            let employeeCode = employeeRow.querySelector('td:nth-child(2)').textContent;

            let popup = document.querySelector("#popup");
            popup.querySelector(".popup-header").firstElementChild.innerHTML = "Bạn muốn xóa?";
            popup.querySelector(".popup-body").innerHTML = `<p>Bạn có chắc chắn muốn xóa nhân viên có mã ${employeeCode}?</p>`;
            popup.style.display = "block";
            currentDelete = button.getAttribute('data-id');
            console.log(`Current ID set to: ${currentDelete}`);
        });
    }
}

// Hàm để xóa một nhân viên
function deleteEmployee(employeeId) {
    console.log('Deleting employee with ID:', employeeId);
    // Kiểm tra employeeId trước khi gọi API
    if (!employeeId) {
        console.error('Employee ID is invalid:', employeeId);
        alert('Mã nhân viên không hợp lệ');
        return;
    }

    const requestOptions = {
        method: "DELETE",
        redirect: "follow"
    };

    fetch(`https://localhost:${port}/api/v1/Employees/${currentDelete}`, requestOptions)
        .then((response) => response.json())
        .then((result) => {
            if (result.success) {
                alert("Xóa thành công!");
                fetchEmployees();
            }
        })
        .catch((error) => console.error(error));
}

// Hàm tìm kiếm nhân viên
function searchEmployees() {
    const searchInput = document.querySelector('.search-input').value.toLowerCase();
    const rows = document.querySelectorAll("#employeeTableBody tr");

    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        if (text.includes(searchInput)) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });

    updateTotalRecords();
}

// Hàm để làm mới bảng nhân viên
function refreshTable() {
    currentPage = 1;
    renderTable();
    shouldFetchData = true;
    fetchEmployees();
}

function nextPageEmployee(id){
    const newPageUrl = `http://127.0.0.1:5500/UI/pages/employee.html?id=${id}`;
    window.location.href = newPageUrl;
}

// Khởi tạo bảng
fetchEmployees();

// Gán sự kiện click cho nút refresh
document.getElementById('refresh-button').addEventListener('click', () => {
    console.log("Refresh")
    fetchEmployees();
});

document.querySelector('.close-btn').onclick = function () {
    popup.style.display = 'none';
};

document.querySelector('.close-popup-btn').addEventListener('click', () => {
    deleteEmployee(currentDelete);
    popup.style.display = 'none';
})

// Thêm sự kiện cho ô tìm kiếm
document.querySelector('.search-input').addEventListener('input', searchEmployees);

document.querySelector(".btn_add").addEventListener("click", () => {
    nextPageEmployee("0");
})