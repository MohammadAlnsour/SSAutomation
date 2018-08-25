using SSAutomation.EmailComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAutomation.ExcelUI
{
    public class SendEmailsFinishedEventArgs
    {
        public List<string> SuccessEmails { get; set; }
        public List<EmailError> FailedEmails { get; set; }

    }
}
