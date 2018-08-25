using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace SSAutomation.ExcelParser
{
    internal class OleDbParser
    {
        private readonly string Excel2003 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES"";";
        private readonly string Excel2007 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES"";";
        private readonly string spreadsheetPath;

        public OleDbParser(string spreadsheetPath)
        {
            this.spreadsheetPath = spreadsheetPath;
        }
        private string GetExtension()
        {
            return Path.GetExtension(spreadsheetPath);
        }
        public string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(spreadsheetPath))
                {
                    var ext = GetExtension();
                    return ext == ".xls" ? string.Format(Excel2003, spreadsheetPath) : string.Format(Excel2007, spreadsheetPath);
                }
                return string.Empty;
            }
        }
        public List<string> GetSheetsNames()
        {
            List<string> listSheet = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(this.ConnectionString))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    listSheet.Add(drSheet["TABLE_NAME"].ToString());
                }
            }
            return listSheet;
        }
        public DataTable GetSheetData(string sheetName)
        {
            DataTable dtSheet;
            using (OleDbConnection conn = new OleDbConnection(this.ConnectionString))
            {
                conn.Open();
                var command = new OleDbCommand() {
                    CommandText = "select * from [" + sheetName + "$]",
                    Connection = conn
                };

                DataSet ds = new DataSet();
                var adapter = new OleDbDataAdapter(command);
                adapter.Fill(ds);
                dtSheet = ds.Tables[0];
            }

            return dtSheet;
        }

    }
}
