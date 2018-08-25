using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSAutomation.EmailComponent;
using SSAutomation.ExcelParser;
using SSAutomation.PdfGenerator;

namespace SSAutomation.ExcelUI
{
    public delegate double OverallProgressChangedHandler(double percentage);
    public delegate void ProgressFinishedHandler(object sender, SendEmailsFinishedEventArgs e);

    public class ProcessController
    {

        public event OverallProgressChangedHandler OnProgressChanged;
        public event ProgressFinishedHandler OnProgressFinished;

        private readonly string spreadSheetFilePath;
        public ProcessController(string spreadSheetFilePath)
        {
            if (string.IsNullOrEmpty(spreadSheetFilePath)) throw new ArgumentNullException("spreadSheetFilePath");
            this.spreadSheetFilePath = spreadSheetFilePath;
        }

        public void BeginProcessing()
        {
            OnProgressChanged?.Invoke(0);
            //Read and load excel to json objects
            var reader = new OpenXmlLoader(spreadSheetFilePath);
            reader.LoadExcelDataToJsonFile();
            OnProgressChanged?.Invoke(20);

            var pdf = new PdfGeneratorEngine();
            pdf.GenerateSalarySlipsPdfs();
            OnProgressChanged?.Invoke(50);

            var poster = new EmailPoster();
            poster.OnProgressChanged += (d) =>
            {
                var progress = 50 + d;
                OnProgressChanged?.Invoke(progress);
            };
            poster.SendEmails();
            OnProgressFinished?.Invoke(this, new SendEmailsFinishedEventArgs() { FailedEmails = poster.ErrorEmails, SuccessEmails = poster.SuccessEmails });
        }

    }
}
