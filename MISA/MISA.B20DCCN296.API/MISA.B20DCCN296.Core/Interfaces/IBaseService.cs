using MISA.B20DCCN296.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.B20DCCN296.Core.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        MessageResponse InsertService(T entity);
        MessageResponse UpdateService(T entity);
    }
}
