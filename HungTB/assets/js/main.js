document.addEventListener('DOMContentLoaded', function () {
  document.querySelectorAll('.btn-warning').forEach(button => {
    button.addEventListener('click', function () {
      alert('Edit button clicked');
    });
  });

  document.querySelectorAll('.btn-danger').forEach(button => {
    button.addEventListener('click', function () {
      alert('Delete button clicked');
    });
  });

  document.getElementById('toggle-btn').addEventListener('click', function () {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('minimized');
    const icon = this.querySelector('i');
    if (sidebar.classList.contains('minimized')) {
      icon.classList.remove('fa-angle-double-left');
      icon.classList.add('fa-angle-double-right');
    } else {
      icon.classList.remove('fa-angle-double-right');
      icon.classList.add('fa-angle-double-left');
    }
  });
});

function openModal() {
  $('#employeeModal').modal('show');
}

function closeModal() {
  $('#employeeModal').modal('hide');
}