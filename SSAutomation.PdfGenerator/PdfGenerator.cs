using SSAutomation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SSAutomation.PdfGenerator
{
    public class PdfGeneratorEngine
    {
        public void GenerateSalarySlipsPdfs()
        {
            if (Directory.Exists(@"c:\salarySlipsPdfs\"))
            {
                Directory.Delete(@"c:\salarySlipsPdfs\", true);
                Directory.CreateDirectory(@"c:\salarySlipsPdfs\");
            }
            else
            {
                Directory.CreateDirectory(@"c:\salarySlipsPdfs\");
            }
            var emps = new JsonReader<EmployeeInfo>().ReadJson();
            foreach (var emp in emps)
            {
                var cash = new JsonReader<EmpCashAdvances>().QueryJson(emp.EmpNumber);
                var misc = new JsonReader<EmpMiscellaneous>().QueryJson(emp.EmpNumber);
                var transfers = new JsonReader<EmpTransfers>().QueryJson(emp.EmpNumber);
                var slip = new JsonReader<Slip>().QueryJson(emp.EmpNumber).FirstOrDefault();
                CreateEmployeePdf(emp, slip, cash.ToList(), transfers.ToList(), misc.ToList());
            }

        }
        private string CreateEmployeePdf(EmployeeInfo emp, Slip slip, List<EmpCashAdvances> cash, List<EmpTransfers> transfers, List<EmpMiscellaneous> misc)
        {
            if (!Directory.Exists(@"c:\salarySlipsPdfs\"))
            {
                Directory.CreateDirectory(@"c:\salarySlipsPdfs\");
            }
            var rec = new Rectangle(600f, 800f);
            var doc = new Document(rec);
            var pdfWriter = PdfWriter.GetInstance(doc, new FileStream(@"c:\salarySlipsPdfs\" + emp.EmpNumber + ".pdf", FileMode.Create));
            rec.BackgroundColor = CMYKColor.WHITE;
            doc.Open();
            //Font brown = new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.NORMAL, new BaseColor(163, 21, 21));
            var topParag = new Paragraph("Pay Slip", new Font(Font.FontFamily.HELVETICA, 16f, Font.NORMAL, new BaseColor(163, 21, 21)));
            topParag.Alignment = 1;

            PdfPTable headerTable = new PdfPTable(4);
            headerTable.TotalWidth = 50f;
            headerTable.HorizontalAlignment = 0;

            //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //headerTable.AddCell(cell);
            var cell1 = new PdfPCell(new Phrase("A/C No   :  "));
            cell1.Border = 0;
            var cell2 = new PdfPCell(new Phrase(emp.EmpNumber));
            cell2.Border = 0;
            var cell3 = new PdfPCell(new Phrase("Name   :  "));
            cell3.Border = 0;
            var cell4 = new PdfPCell(new Phrase(emp.Name));
            cell4.Border = 0;
            var cell5 = new PdfPCell(new Phrase("Month   :  "));
            cell5.Border = 0;
            var cell6 = new PdfPCell(new Phrase(slip.Month));
            cell6.Border = 0;
            headerTable.AddCell(cell1);
            headerTable.AddCell(cell2);
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(cell3);
            headerTable.AddCell(cell4);
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(cell5);
            headerTable.AddCell(cell6);
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });

            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });

            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });
            headerTable.AddCell(new PdfPCell() { Border = 0 });

            PdfPTable mainTable = new PdfPTable(3);
            mainTable.HorizontalAlignment = 1;

            mainTable.AddCell(new PdfPCell(new Phrase("Particulars")));
            mainTable.AddCell(new PdfPCell(new Phrase("Debit")));
            mainTable.AddCell(new PdfPCell(new Phrase("Credit")));

            mainTable.AddCell(new PdfPCell(new Phrase("Balance b/f")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.BalanceBfDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.BalanceBfCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Salary this month")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.SalaryThisMonthDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.SalaryThisMonthCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Overtime")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.OvertimeDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.OvertimeCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Ticket Compensation")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.TicketCompensationDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.TicketCompensationCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("House Allowance")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.HouseAllowanceDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.HouseAllowanceCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Vacation Allowance")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.VacationAllowanceDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.VacationAllowanceCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Service Award")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.ServiceAwardDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.ServiceAwardCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Car Allowance")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.CarAllowanceDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.CarAllowanceCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Other Allowance")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.CashOtherAdvanceCredit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.CashOtherAdvanceCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Miscellaneous")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.MiscDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.MiscCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Cash/other Advance")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.CashOtherAdvanceDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.CashOtherAdvanceCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Transfers")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.TransfersDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.TransfersCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Total Movements")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.TotalMovementsDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.TotalMovementsCredit)) { HorizontalAlignment = 1 });

            mainTable.AddCell(new PdfPCell(new Phrase("Outstanding Balance")) { HorizontalAlignment = 0 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.OutstandingBalanceDebit)) { HorizontalAlignment = 1 });
            mainTable.AddCell(new PdfPCell(new Phrase(slip.OutstandingBalanceCredit)) { HorizontalAlignment = 1 });


            //var lowerThreeTables = new PdfPTable(3);
            //lowerThreeTables.TotalWidth = 500f;

            var advancementsTable = new PdfPTable(3);
            var miscTable = new PdfPTable(3);
            var transfersTable = new PdfPTable(4);

            advancementsTable.AddCell(new PdfPCell(new Phrase("Cash Advances")) { Colspan = 3, HorizontalAlignment = 1 });

            advancementsTable.AddCell(new PdfPCell(new Phrase("Date", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            advancementsTable.AddCell(new PdfPCell(new Phrase("V.No", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            advancementsTable.AddCell(new PdfPCell(new Phrase("Amount", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });

            foreach (var advance in cash)
            {
                advancementsTable.AddCell(new PdfPCell(new Phrase(advance.Date, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                advancementsTable.AddCell(new PdfPCell(new Phrase(advance.VNum, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                advancementsTable.AddCell(new PdfPCell(new Phrase(advance.Amount, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            }
            advancementsTable.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = 0 });
            advancementsTable.AddCell(new PdfPCell(new Phrase("Totals")) { HorizontalAlignment = 0 });
            advancementsTable.AddCell(new PdfPCell(new Phrase(cash.Sum(a => Convert.ToDouble(a.Amount)).ToString())) { HorizontalAlignment = 0 });

            miscTable.AddCell(new PdfPCell(new Phrase("Miscellaneous")) { Colspan = 3, HorizontalAlignment = 1 });

            miscTable.AddCell(new PdfPCell(new Phrase("V.No", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            miscTable.AddCell(new PdfPCell(new Phrase("Description", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            miscTable.AddCell(new PdfPCell(new Phrase("Amount", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });

            foreach (var mis in misc)
            {
                miscTable.AddCell(new PdfPCell(new Phrase(mis.VNum, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                miscTable.AddCell(new PdfPCell(new Phrase(mis.Description, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                miscTable.AddCell(new PdfPCell(new Phrase(mis.Amount, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            }
            miscTable.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = 0 });
            miscTable.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = 0 });
            miscTable.AddCell(new PdfPCell(new Phrase(misc.Sum(a => Convert.ToDouble(a.Amount)).ToString())) { HorizontalAlignment = 0 });


            transfersTable.AddCell(new PdfPCell(new Phrase("Transfers")) { Colspan = 4, HorizontalAlignment = 1 });

            transfersTable.AddCell(new PdfPCell(new Phrase("V.No", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            transfersTable.AddCell(new PdfPCell(new Phrase("A/C Number", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            transfersTable.AddCell(new PdfPCell(new Phrase("Amount(SAR)", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            transfersTable.AddCell(new PdfPCell(new Phrase("NOK/US$", new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });

            foreach (var transf in transfers)
            {
                transfersTable.AddCell(new PdfPCell(new Phrase(transf.VNum, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                transfersTable.AddCell(new PdfPCell(new Phrase(transf.EmpNumber, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                transfersTable.AddCell(new PdfPCell(new Phrase(transf.AmountSAR, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
                transfersTable.AddCell(new PdfPCell(new Phrase(transf.NOKUS, new Font(Font.FontFamily.UNDEFINED, 9f))) { HorizontalAlignment = 0 });
            }
            transfersTable.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = 0 });
            transfersTable.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = 0 });
            transfersTable.AddCell(new PdfPCell(new Phrase(transfers.Sum(a => Convert.ToDouble(a.AmountSAR)).ToString())) { HorizontalAlignment = 0 });
            transfersTable.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = 0 });

            //lowerThreeTables.AddCell(new PdfPCell(advancementsTable));
            //lowerThreeTables.AddCell(new PdfPCell(miscTable));
            //lowerThreeTables.AddCell(new PdfPCell(transfersTable));

            doc.Add(topParag);
            doc.Add(new Paragraph("   "));
            doc.Add(headerTable);
            doc.Add(new Paragraph("   "));
            doc.Add(mainTable);
            doc.Add(new Paragraph("   "));
            doc.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------"));
            doc.Add(new Paragraph("   "));
            doc.Add(new Paragraph("Detailed Transactions", new Font(Font.FontFamily.TIMES_ROMAN, 16f, Font.NORMAL, new BaseColor(163, 21, 21))));
            doc.Add(new Paragraph("   "));
            doc.Add(advancementsTable);
            doc.Add(new Paragraph("   "));
            doc.Add(miscTable);
            doc.Add(new Paragraph("   "));
            doc.Add(transfersTable);
            doc.Close();
            return @"c:\salarySlipsPdfs\" + emp.EmpNumber + ".pdf";
        }

    }
}
