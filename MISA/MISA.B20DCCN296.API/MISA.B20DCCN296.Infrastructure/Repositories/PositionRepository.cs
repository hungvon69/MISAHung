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
using System.Threading.Tasks;

namespace MISA.B20DCCN296.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository, IDisposable
    {
        IDbConnection _connection;
        public PositionRepository()
        {
            _connection = new MySqlConnection(Common.DatabaseString);
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public int DeleteAny(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public List<Position> Get()
        {
            var sql = $"SELECT * FROM Position";
            var res = _connection.Query<Position>(sql);
            return res.ToList();
        }

        public Position? Get(string id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Position entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Position entity)
        {
            throw new NotImplementedException();
        }
    }
}
