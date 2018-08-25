using MahApps.Metro.Controls;
using SSAutomation.EmailComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SSAutomation.ExcelUI
{
    /// <summary>
    /// Interaction logic for OperationReport.xaml
    /// </summary>
    public partial class OperationReport : MetroWindow
    {
        public OperationReport(OperationReportContext context)
        {
            InitializeComponent();
            //var context = this.DataContext as OperationReportContext;
            lblTotal.Content = (context.Success.Count + context.Errors.Count).ToString();
            lblSuccess.Content = context.Success.Count.ToString();
            lblFailed.Content = context.Errors.Count.ToString();

            GridSuccessEmps.ItemsSource = context.Success;
            GridFailedEmps.ItemsSource = context.Errors;

            var overallBreakdown = new List<OverallBreakdownData>();
            overallBreakdown.AddRange(context.Success.Select(s => new OverallBreakdownData()
            {
                EmpEmail = s,
                OperationDate = DateTime.Now,
                Status = "Success",
                Reason = string.Empty
            }));
            overallBreakdown.AddRange(context.Errors.Select(f => new OverallBreakdownData()
            {
                EmpEmail = f.Email,
                OperationDate = DateTime.Now,
                Status = "Failed",
                Reason = f.ErrorMessage
            }));

            GridBreakdown.ItemsSource = overallBreakdown;
        }

    }

    public class OverallBreakdownData
    {
        public string EmpEmail { get; set; }
        public string Status { get; set; }
        public DateTime OperationDate { get; set; }
        public string Reason { get; set; }

    }

}
