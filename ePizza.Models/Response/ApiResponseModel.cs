using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Models.Response
{
    public class ApiResponseModel<T>
    {
        public bool success { get; set; }
        public T Data { get; set; }
        public string message { get; set; }
    }
}
