using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail_Sender.Validation
{
    public class ValidationResult
    {
        public bool result { get; set; }
        public List<string> errorList { get; set; }
    }
}
