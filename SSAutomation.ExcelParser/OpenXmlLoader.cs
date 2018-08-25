using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSAutomation.Core;
using System.IO;
using System.Data;

namespace SSAutomation.ExcelParser
{
    public delegate void LoadCompleteHandler();
    public delegate double ReportLoadPercentageHandler(double percentage);


    public class OpenXmlLoader
    {
        public event ReportLoadPercentageHandler OnLoadPercentageChanged;
        public event LoadCompleteHandler OnLoadComplete;

        private readonly string spreadSheetDocumentPath;
        public OpenXmlLoader(string spreadSheetDocumentPath)
        {
            if (string.IsNullOrEmpty(spreadSheetDocumentPath)) throw new ArgumentNullException("spreadSheetDocumentPath", "spread sheet path is not exists.");
            this.spreadSheetDocumentPath = spreadSheetDocumentPath;
        }
        public void LoadExcelDataToJsonFile()
        {
            if (!File.Exists(spreadSheetDocumentPath)) throw new FileNotFoundException("spread sheet file cannot be found.");
            EmployeeJsonSerializer.ClearJsonFilesContent();

            var oledbParser = new OleDbParser(spreadSheetDocumentPath);
            var emps = oledbParser.GetSheetData("Emps_Info");
            var slips = oledbParser.GetSheetData("Slips");
            var advances = oledbParser.GetSheetData("CashAdvances");
            var transfers = oledbParser.GetSheetData("Transfers");
            var misc = oledbParser.GetSheetData("Miscellaneous");

            LoadObjectToJson<EmployeeInfo>(emps);
            OnLoadPercentageChanged?.Invoke(30);
            LoadObjectToJson<Slip>(slips);
            OnLoadPercentageChanged?.Invoke(40);
            LoadObjectToJson<EmpCashAdvances>(advances);
            OnLoadPercentageChanged?.Invoke(50);
            LoadObjectToJson<EmpTransfers>(transfers);
            OnLoadPercentageChanged?.Invoke(70);
            LoadObjectToJson<EmpMiscellaneous>(misc);
            OnLoadPercentageChanged?.Invoke(100);
            OnLoadComplete?.Invoke();
        }
        private void LoadObjectToJson<T>(DataTable dataTable)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable", "dataTable should not be null");

            if (typeof(T) == typeof(EmployeeInfo))
            {
                List<EmployeeInfo> objList = dataTable.AsEnumerable().Select(dr => MapRowData.MapRowToEmployee(dr)).ToList();
                EmployeeJsonSerializer.SerializeListObjectsToFile(objList);
            }
            if (typeof(T) == typeof(Slip))
            {
                List<Slip> objList = dataTable.AsEnumerable().Select(dr => MapRowData.MapRowToSlipObject(dr)).ToList();
                EmployeeJsonSerializer.SerializeListObjectsToFile(objList);

            }
            if (typeof(T) == typeof(EmpCashAdvances))
            {
                List<EmpCashAdvances> objList = dataTable.AsEnumerable().Select(dr => MapRowData.MapRowToCashAdvance(dr)).ToList();
                EmployeeJsonSerializer.SerializeListObjectsToFile(objList);
            }
            if (typeof(T) == typeof(EmpMiscellaneous))
            {
                List<EmpMiscellaneous> objList = dataTable.AsEnumerable().Select(dr => MapRowData.MapRowToMisc(dr)).ToList();
                EmployeeJsonSerializer.SerializeListObjectsToFile(objList);
            }
            if (typeof(T) == typeof(EmpTransfers))
            {
                List<EmpTransfers> objList = dataTable.AsEnumerable().Select(dr => MapRowData.MapRowToTransfers(dr)).ToList();
                EmployeeJsonSerializer.SerializeListObjectsToFile(objList);
            }

            //foreach (var row in dataTable.Rows)
            //{
            //    ModelObjectBase model;
            //    if (typeof(T) == typeof(EmployeeInfo))
            //    {
            //        model = MapRowData.MapRowToEmployee((DataRow)row);
            //        EmployeeJsonSerializer.SerializeObjectToFile(model);
            //    }
            //    if (typeof(T) == typeof(Slip))
            //    {
            //        model = MapRowData.MapRowToSlipObject((DataRow)row);
            //        EmployeeJsonSerializer.SerializeObjectToFile(model);
            //    }
            //    if (typeof(T) == typeof(EmpCashAdvances))
            //    {
            //        model = MapRowData.MapRowToCashAdvance((DataRow)row);
            //        EmployeeJsonSerializer.SerializeObjectToFile(model);
            //    }
            //    if (typeof(T) == typeof(EmpMiscellaneous))
            //    {
            //        model = MapRowData.MapRowToMisc((DataRow)row);
            //        EmployeeJsonSerializer.SerializeObjectToFile(model);
            //    }
            //    if (typeof(T) == typeof(EmpTransfers))
            //    {
            //        model = MapRowData.MapRowToTransfers((DataRow)row);
            //        EmployeeJsonSerializer.SerializeObjectToFile(model);
            //    }
            //}
        }
    }

}
