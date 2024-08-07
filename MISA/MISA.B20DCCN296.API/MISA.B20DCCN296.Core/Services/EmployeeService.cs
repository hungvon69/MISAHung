using MISA.B20DCCN296.Core.DTOs;
using MISA.B20DCCN296.Core.Entities;
using MISA.B20DCCN296.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.B20DCCN296.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRespository _employeeRepository;
        public EmployeeService(IEmployeeRespository employeeRespository)
        {
            _employeeRepository = employeeRespository;
        }


        public MessageResponse InsertService(Employee entity)
        {
            var isDuplicate = _employeeRepository.CheckCodeDuplicate(entity.EmployeeCode);
            if (isDuplicate) 
            {
                var mess  =  new MessageResponse
                {
                    Success =  false,
                    StatusCode = 400
                };
                mess.Errors.Add("Mã nhân viên đã tồn tại");
                return mess; 

            }
            entity.EmployeeId = Guid.NewGuid();
            var res = _employeeRepository.Insert(entity);
            return new MessageResponse
            {
                Success = true,
                StatusCode = 200
            };
        }

        public MessageResponse UpdateService(Employee entity)
        {
            var res = _employeeRepository.Update(entity);
            return new MessageResponse
            {
                Success = true,
                StatusCode = 200
            };
        }

    }
}
