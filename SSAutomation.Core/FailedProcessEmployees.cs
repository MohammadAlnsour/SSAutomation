using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAutomation.Core
{
    public class FailedProcessEmployees : ModelObjectBase
    {
        public override string EmpNumber { get; set; }
        public string ErrorReason { get; set; }
        public string ErrorLocation { get; set; }

    }
}
