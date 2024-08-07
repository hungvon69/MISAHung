using Dapper;
using MISA.B20DCCN296.Core;
using MISA.B20DCCN296.Core.Entities;
using MISA.B20DCCN296.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.B20DCCN296.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRespository, IDisposable
    {
        IDbConnection _connection;
        public EmployeeRepository()
        {
            _connection = new MySqlConnection(Common.DatabaseString);
        }

        public bool CheckCodeDuplicate(string EmployeeCode)
        {
            var sql = "SELECT EmployeeCode FROM Employee e WHERE e.EmployeeCode = @EmployeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", EmployeeCode);
            var res = _connection.QueryFirstOrDefault(sql, parameters);
            return res != null;
        }

        public int Delete(string id)
        {
            var sql = $"DELETE FROM Employee WHERE EmployeeId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = _connection.Execute(sql, parameters);
            return res;
        }

        public int DeleteAny(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public List<Employee> Get()
        {
            var sql = $"SELECT * FROM Employee ORDER BY EmployeeCode DESC";
            var res = _connection.Query<Employee>(sql);
            return res.ToList();
        }

        public Employee? Get(string EmployeeId)
        {
            var sql = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeId", EmployeeId);
            var res = _connection.QueryFirstOrDefault<Employee>(sql, parameters);
            return res;
        }

        public string GetNewEmployeeCode()
        {
            var sql = $"SELECT * FROM Employee ORDER BY EmployeeCode DESC";
            var res = _connection.QueryFirstOrDefault<Employee>(sql);
            if (res == null)
            {
                return "EMP-000001";
            }

            string numberPart = res.EmployeeCode.Substring(4);

            // Chuyển phần số sang số nguyên
            int number = int.Parse(numberPart);

            // Tăng giá trị số lên 1
            number++;

            // Định dạng lại thành chuỗi với số mới
            string newNumberPart = number.ToString("D6"); // Đảm bảo có 6 chữ số với các số 0 dẫn đầu
            return $"EMP-{newNumberPart}";
        }

        public int Insert(Employee entity)
        {
            var className = "Employee";
            var propListName = "";
            var propListValue = "";
            // lấy ra tất cả các props của entity
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            // duyệt từng props
            foreach (var prop in props)
            {
                // lấy ra tên của prop
                var propname = prop.Name; // EmployeeId
                var val = prop.GetValue(entity);

                // lấy ra values của prop
                propListName += $"{propname},";
                propListValue += $"@{propname},";
                parameters.Add($"@{propname}", val);
            }
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);

            // Build câu lệnh sql
            var sqlInsert = $"INSERT {className}({propListName}) VALUES ({propListValue});";
            // thực thi
            var res = _connection.Execute(sqlInsert, parameters);
            return res;
        }

        public int Update(Employee entity)
        {
            var className = "Employee";
            var setClause = "";
            var keyPropertyName = className + "Id";
            var keyPropertyValue = "";

            // lấy ra tất cả các props của entity
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();

            // duyệt từng props
            foreach (var prop in props)
            {
                // lấy ra tên của prop
                var propname = prop.Name;
                var val = prop.GetValue(entity);
                if (propname == keyPropertyName)
                {
                    keyPropertyValue = val?.ToString() ?? "";
                }

                // xây dựng SET clause, bỏ qua khóa chính
                if (propname != keyPropertyName)
                {
                    setClause += $"{propname} = @{propname}, ";
                    parameters.Add($"@{propname}", val);
                }
            }

            // loại bỏ dấu phẩy và khoảng trắng cuối cùng
            setClause = setClause.Substring(0, setClause.Length - 2);

            // thêm khóa chính vào parameters
            parameters.Add($"@{keyPropertyName}", keyPropertyValue);

            // Build câu lệnh sql
            var sqlUpdate = $"UPDATE {className} SET {setClause} WHERE {keyPropertyName} = @{keyPropertyName};";

            // thực thi
            var res = _connection.Execute(sqlUpdate, parameters);
            return res;
        }
    }
}
