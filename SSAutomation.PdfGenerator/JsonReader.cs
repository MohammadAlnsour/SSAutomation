using SSAutomation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SSAutomation.PdfGenerator
{
    internal class JsonReader<T> where T : ModelObjectBase
    {
        private string jsonFilePath;
        private string GetJsonPath()
        {
            if (typeof(T) ==  typeof(EmpCashAdvances))
            {
                return jsonFilePath = @"c:\ssAutomation\cash.json";
            }
            else if (typeof(T) ==  typeof(Slip))
            {
                return jsonFilePath = @"c:\ssAutomation\slips.json";
            }
            else if (typeof(T) ==  typeof(EmpMiscellaneous))
            {
                return jsonFilePath = @"c:\ssAutomation\misc.json";
            }
            else if (typeof(T) ==  typeof(EmpTransfers))
            {
                return jsonFilePath = @"c:\ssAutomation\transfers.json";
            }
            else if (typeof(T) == typeof(EmployeeInfo))
            {
                return jsonFilePath = @"c:\ssAutomation\emps.json";
            }
            return string.Empty;
        }
        internal IEnumerable<T> ReadJson()
        {
            GetJsonPath();
            if (!string.IsNullOrEmpty(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                var data = JsonConvert.DeserializeObject<List<T>>(json);
                return data;
            }
            return null;
        }
        internal IEnumerable<T> QueryJson(string empNumber)
        {
            GetJsonPath();
            if (!string.IsNullOrEmpty(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                var data = JsonConvert.DeserializeObject<List<T>>(json);
                var sec = data.Where(e => e.EmpNumber.Trim() == empNumber.Trim());
                return sec;
            }
            return null;
        }

    }
}
