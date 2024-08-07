using Dapper;
using MISA.B20DCCN296.Core;
using MISA.B20DCCN296.Core.Entities;
using MISA.B20DCCN296.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.B20DCCN296.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        IDbConnection _connection;
        public DepartmentRepository()
        {
            _connection = new MySqlConnection(Common.DatabaseString);
        }

        public int Delete(string id)
        {
            return 0;
        }

        public int DeleteAny(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           _connection.Dispose();
        }

        public List<Department> Get()
        {
            var sql = $"SELECT * FROM Department";
            var res = _connection.Query<Department>(sql);
            return res.ToList();
        }

        public Department? Get(string id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Department entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
