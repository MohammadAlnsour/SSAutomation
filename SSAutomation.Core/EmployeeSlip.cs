using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAutomation.Core
{
    public class Slip : ModelObjectBase
    {
        //public string EmpNumber { get; set; }
        public override string EmpNumber { get; set; }
        //public string EmpName { get; set; }
        //public string EmpEmail { get; set; }
        public string Month { get; set; }
        public string BalanceBfDebit { get; set; }
        public string BalanceBfCredit { get; set; }
        public string SalaryThisMonthDebit { get; set; }
        public string SalaryThisMonthCredit { get; set; }
        public string OvertimeDebit { get; set; }
        public string OvertimeCredit { get; set; }
        public string TicketCompensationDebit { get; set; }
        public string TicketCompensationCredit { get; set; }
        public string HouseAllowanceDebit { get; set; }
        public string HouseAllowanceCredit { get; set; }
        public string VacationAllowanceDebit { get; set; }
        public string VacationAllowanceCredit { get; set; }
        public string ServiceAwardDebit { get; set; }
        public string ServiceAwardCredit { get; set; }
        public string CarAllowanceDebit { get; set; }
        public string CarAllowanceCredit { get; set; }
        public string MiscDebit { get; set; }
        public string MiscCredit { get; set; }
        public string CashOtherAdvanceDebit { get; set; }
        public string CashOtherAdvanceCredit { get; set; }
        public string TransfersDebit { get; set; }
        public string TransfersCredit { get; set; }
        public string TotalMovementsDebit { get; set; }
        public string TotalMovementsCredit { get; set; }
        public string OutstandingBalanceDebit { get; set; }
        public string OutstandingBalanceCredit { get; set; }

        public List<EmpCashAdvances> EmpCashAdvances { get; set; }
        public List<EmpTransfers> EmpTransfers { get; set; }
        public List<EmpMiscellaneous> EmpMiscellaneous { get; set; }


    }
}
