using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAutomation.Core
{
    public class EmpMiscellaneous : ModelObjectBase
    {
        public override string EmpNumber { get; set; }
        public string VNum { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }

       // public EmployeeSlip EmpSlip { get; set; }


    }
}
