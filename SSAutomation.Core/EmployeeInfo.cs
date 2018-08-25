using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAutomation.Core
{
    public class EmployeeInfo : ModelObjectBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public override string EmpNumber { get; set; }

    }
}
