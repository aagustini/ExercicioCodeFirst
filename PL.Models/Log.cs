using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime OperationDate { get; set; }
        public string Action { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
    }
}
