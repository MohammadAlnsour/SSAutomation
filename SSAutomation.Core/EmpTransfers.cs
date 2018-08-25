using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAutomation.Core
{
    public class EmpTransfers : ModelObjectBase
    {
        public override string EmpNumber { get; set; }
        public string VNum { get; set; }
        public string AmountSAR { get; set; }
        public string NOKUS { get; set; }

        // public EmployeeSlip EmpSlip { get; set; }



    }
}
