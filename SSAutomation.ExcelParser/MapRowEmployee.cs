using SSAutomation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SSAutomation.ExcelParser
{
    internal static class MapRowData
    {
        internal static Slip MapRowToSlipObject(DataRow row)
        {
            var empSlip = new Slip();
            empSlip.EmpNumber = row[0].ToString();
            empSlip.Month = row[1].ToString();
            empSlip.BalanceBfDebit = row[2].ToString();
            empSlip.BalanceBfCredit = row[3].ToString();
            empSlip.SalaryThisMonthDebit = row[4].ToString();
            empSlip.SalaryThisMonthCredit = row[5].ToString();
            empSlip.OvertimeDebit = row[6].ToString();
            empSlip.OvertimeCredit = row[7].ToString();
            empSlip.TicketCompensationDebit = row[8].ToString();
            empSlip.TicketCompensationCredit = row[9].ToString();
            empSlip.HouseAllowanceDebit = row[10].ToString();
            empSlip.HouseAllowanceCredit = row[11].ToString();
            empSlip.VacationAllowanceDebit = row[12].ToString();
            empSlip.VacationAllowanceCredit = row[13].ToString();
            empSlip.ServiceAwardDebit = row[14].ToString();
            empSlip.ServiceAwardCredit = row[15].ToString();
            empSlip.CarAllowanceDebit = row[16].ToString();
            empSlip.CarAllowanceCredit = row[17].ToString();
            empSlip.MiscDebit = row[18].ToString();
            empSlip.MiscCredit = row[19].ToString();
            empSlip.CashOtherAdvanceDebit = row[20].ToString();
            empSlip.CashOtherAdvanceCredit = row[21].ToString();
            empSlip.TransfersDebit = row[22].ToString();
            empSlip.TransfersCredit = row[23].ToString();
            empSlip.TotalMovementsDebit = row[24].ToString();
            empSlip.TotalMovementsCredit = row[25].ToString();
            empSlip.OutstandingBalanceDebit = row[26].ToString();
            empSlip.OutstandingBalanceCredit = row[27].ToString();
            return empSlip;
        }
        internal static EmpCashAdvances MapRowToCashAdvance(DataRow row)
        {
            var cashAdvance = new EmpCashAdvances();
            cashAdvance.EmpNumber = row[0].ToString();
            cashAdvance.Date = row[1].ToString();
            cashAdvance.VNum = row[2].ToString();
            cashAdvance.Amount = row[3].ToString();
            return cashAdvance;
        }
        internal static EmpMiscellaneous MapRowToMisc(DataRow row)
        {
            var misc = new EmpMiscellaneous();
            misc.EmpNumber = row[0].ToString();
            misc.VNum = row[1].ToString();
            misc.Description = row[2].ToString();
            misc.Amount = row[3].ToString();
            return misc;
        }
        internal static EmpTransfers MapRowToTransfers(DataRow row)
        {
            var transfer = new EmpTransfers();
            transfer.EmpNumber = row[0].ToString();
            transfer.VNum = row[1].ToString();
            transfer.AmountSAR = row[2].ToString();
            transfer.NOKUS = row[3].ToString();
            return transfer;
        }
        internal static EmployeeInfo MapRowToEmployee(DataRow row)
        {
            var empInfo = new EmployeeInfo();
            empInfo.EmpNumber = row[0].ToString();
            empInfo.Name = row[1].ToString();
            empInfo.Email = row[2].ToString();
            return empInfo;
        }

    }
}
