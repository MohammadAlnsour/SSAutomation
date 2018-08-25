using SSAutomation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SSAutomation.ExcelParser
{
    internal static class EmployeeJsonSerializer
    {
        private static string slipsJsonFile = @"c:\ssAutomation\slips.json";
        private static string cashJsonFile = @"c:\ssAutomation\cash.json";
        private static string transfersJsonFile = @"c:\ssAutomation\transfers.json";
        private static string miscJsonFile = @"c:\ssAutomation\misc.json";
        private static string empsJsonFile = @"c:\ssAutomation\emps.json";

        internal static void ClearJsonFilesContent()
        {
            if (!File.Exists(slipsJsonFile)) File.Create(slipsJsonFile);
            if (!File.Exists(cashJsonFile)) File.Create(cashJsonFile);
            if (!File.Exists(transfersJsonFile)) File.Create(transfersJsonFile);
            if (!File.Exists(miscJsonFile)) File.Create(miscJsonFile);
            if (!File.Exists(empsJsonFile)) File.Create(empsJsonFile);

            File.WriteAllText(slipsJsonFile, string.Empty);
            File.WriteAllText(cashJsonFile, string.Empty);
            File.WriteAllText(transfersJsonFile, string.Empty);
            File.WriteAllText(miscJsonFile, string.Empty);
            File.WriteAllText(empsJsonFile, string.Empty);
        }
        internal static void SerializeObjectToFile<T>(T t)
        {
            if (t == null) throw new ArgumentNullException("t", "t connot be null.");

            var json = JsonConvert.SerializeObject(t);

            if (t.GetType() == typeof(EmpCashAdvances))
            {
                var sw = File.AppendText(cashJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (t.GetType() == typeof(Slip))
            {
                var sw = File.AppendText(slipsJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (t.GetType() == typeof(EmpTransfers))
            {
                var sw = File.AppendText(transfersJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (t.GetType() == typeof(EmpMiscellaneous))
            {
                var sw = File.AppendText(miscJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (t.GetType() == typeof(EmployeeInfo))
            {
                var sw = File.AppendText(empsJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
        }
        internal static T SearchDeserializeJson<T>(string empNumber) where T : ModelObjectBase
        {
            if (string.IsNullOrEmpty(empNumber)) throw new ArgumentNullException("empNumber", "employee slip connot be null.");

            var properJsonFile = string.Empty;
            if (typeof(T) == typeof(EmpCashAdvances))
            {
                properJsonFile = cashJsonFile;
            }
            if (typeof(T) == typeof(Slip))
            {
                properJsonFile = slipsJsonFile;
            }
            if (typeof(T) == typeof(EmpMiscellaneous))
            {
                properJsonFile = miscJsonFile;
            }
            if (typeof(T) == typeof(EmpTransfers))
            {
                properJsonFile = transfersJsonFile;
            }
            if (typeof(T) == typeof(EmployeeInfo))
            {
                properJsonFile = empsJsonFile;
            }

            if (!File.Exists(properJsonFile)) throw new FileNotFoundException("json file does not exist therefor can't deserialize the object");

            try
            {
                var fileJson = File.ReadAllText(properJsonFile);
                var empsList = JsonConvert.DeserializeObject<List<T>>(fileJson);
                var emp = empsList.FirstOrDefault(e => e.EmpNumber.Trim() == empNumber.Trim());

                return emp;
            }
            catch (Exception ex)
            {
                //log
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        internal static void SerializeListObjectsToFile<T>(List<T> list)
        {
            if (list == null) throw new ArgumentNullException("list", "list connot be null.");

            var json = JsonConvert.SerializeObject(list);
            if (list.GetType() == typeof(List<EmpCashAdvances>))
            {
                var sw = File.AppendText(cashJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (list.GetType() == typeof(List<Slip>))
            {
                var sw = File.AppendText(slipsJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (list.GetType() == typeof(List<EmpTransfers>))
            {
                var sw = File.AppendText(transfersJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (list.GetType() == typeof(List<EmpMiscellaneous>))
            {
                var sw = File.AppendText(miscJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
            else if (list.GetType() == typeof(List<EmployeeInfo>))
            {
                var sw = File.AppendText(empsJsonFile);
                sw.Write(json);
                sw.Close();
                sw.Dispose();
            }
        }

    }

}
