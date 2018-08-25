using MahApps.Metro.Controls;
using System.Windows.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using SSAutomation.EmailComponent;

namespace SSAutomation.ExcelUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Excel files (*.xls)|*.xls|Excel files (*.xlsx)|*.xlsx";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TxtFilePath.Text = ofd.FileName;
                BtnStartProcess.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(@"c:\ssAutomation\"))
            {
                Directory.CreateDirectory(@"c:\ssAutomation\");
            }
            ProgressBar1.Visibility = Visibility.Visible;
            var progress = new Progress<double>(val => { ProgressBar1.Value = val; });
            var filePath = TxtFilePath.Text;

            TxtFilePath.IsEnabled = false;
            BtnBrowse.IsEnabled = false;
            BtnStartProcess.IsEnabled = false;

            Task.Factory.StartNew(() => BeginProcess(filePath, progress));
        }

        private void BeginProcess(string spreadSheetPath, IProgress<double> progress)
        {
            var process = new ProcessController(spreadSheetPath);
            process.OnProgressChanged += (d) =>
            {
                progress?.Report(d);
                ProgressBar1.Invoke((Action)(() => ProgressBar1.Value = d));
                return d;
            };
            process.OnProgressFinished += (s, e) =>
            {
                TxtFilePath.Invoke((Action)(() => TxtFilePath.IsEnabled = true));
                BtnBrowse.Invoke((Action)(() => BtnBrowse.IsEnabled = true));
                BtnStartProcess.Invoke((Action)(() => BtnStartProcess.IsEnabled = true));
                System.Windows.MessageBox.Show("Operation Done Successfully.");

                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var report = new OperationReport(new OperationReportContext()
                    {
                        Success = e.SuccessEmails,
                        Errors = e.FailedEmails
                    });
                    report.ShowDialog();
                }));
            };
            process.BeginProcessing();
        }

    }

    public class OperationReportContext
    {
        public List<string> Success { get; set; }
        public List<EmailError> Errors { get; set; }
    }
}
