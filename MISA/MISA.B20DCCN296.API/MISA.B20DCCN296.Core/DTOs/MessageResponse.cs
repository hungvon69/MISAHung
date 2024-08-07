using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.B20DCCN296.Core.DTOs
{
    public class MessageResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<String> Errors { get; set; } = new List<string>();
    }
}
