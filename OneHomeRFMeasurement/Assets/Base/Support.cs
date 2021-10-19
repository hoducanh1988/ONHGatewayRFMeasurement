using OneHomeRFMeasurement.Assets.Custom;
using OneHomeRFMeasurement.Assets.Global;
using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Assets.Base {
    public class Support {

        public static DataTable readExcel(string excel_file, string sheet_name) {
            try {
                string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel_file + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                using (OleDbConnection conn = new OleDbConnection(_connectionString)) {
                    DataTable dt = new DataTable();
                    conn.Open();
                    OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter($"select * from [{sheet_name}$A1:N]", conn);
                    DataSet excelDataSet = new DataSet();
                    objDA.Fill(excelDataSet);
                    dt = excelDataSet.Tables[0];

                    excelDataSet.Dispose();
                    objDA.Dispose();
                    conn.Dispose();
                    return dt;
                }
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        public static bool loadScriptCommand() {
            try {
                string f = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + myGlobal.settingviewmodel.SM.scriptFile;
                if (System.IO.File.Exists(f) == false) {
                    System.Windows.MessageBox.Show($"File script không tồn tại '{myGlobal.settingviewmodel.SM.scriptFile}'.", "Load Script File", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return false;
                }
                myGlobal.runviewmodel.collectionCommand.Clear();
                myGlobal.runviewmodel.collectionResult.Clear();
                string mode = "";
                var data = Support.readExcel(f, myGlobal.settingviewmodel.SM.sheetName);
                for (int i = 0; i < data.Rows.Count; i++) {
                    var row = data.Rows[i];
                    CommandItem ci = new CommandItem();
                    ci.ID = i + 1;
                    ci.Mode = row.ItemArray[1] as string;
                    ci.Device = row.ItemArray[2] as string;
                    ci.commandText = row.ItemArray[3] as string;
                    ci.isChecked = string.IsNullOrEmpty(row.ItemArray[4] as string) || string.IsNullOrWhiteSpace(row.ItemArray[4] as string);
                    ci.inputType = row.ItemArray[5] as string;
                    ci.retryTime = (string.IsNullOrEmpty(row.ItemArray[6] as string) || string.IsNullOrWhiteSpace(row.ItemArray[6] as string)) ? 0 : int.Parse(row.ItemArray[6] as string);
                    ci.feedBack = row.ItemArray[7] as string;
                    ci.splitChar = row.ItemArray[8] as string;
                    ci.valueIndexer = row.ItemArray[9] as string;
                    ci.LL = row.ItemArray[10] as string;
                    ci.UL = row.ItemArray[11] as string;
                    ci.Attenuation = row.ItemArray[12] as string;
                    ci.Note = row.ItemArray[13] as string;

                    if (ci.isChecked == true) {
                        myGlobal.runviewmodel.collectionCommand.Add(ci);
                    }

                    if (ci.Mode != mode) {
                        ResultItem ri = new ResultItem();
                        ri.Name = ci.Mode;
                        ri.Index = myGlobal.runviewmodel.collectionResult.Count + 1;
                        myGlobal.runviewmodel.collectionResult.Add(ri);
                        mode = ci.Mode;
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public static List<string> ListSheetInExcel() {
            if (myGlobal.settingviewmodel == null) return null;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + myGlobal.settingviewmodel.SM.scriptFile;
            bool r = string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath);
            if (r) return null;
            if (File.Exists(filePath) == false) return null;

            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filePath;
            if (Path.GetExtension(filePath).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (Path.GetExtension(filePath).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);
            List<string> listSheet = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString())) {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dtSheet.Rows) {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        listSheet.Add(drSheet["TABLE_NAME"].ToString().Replace("$", "").Replace("\n", "").Replace("\r", "").Trim());
                    }
                }
            }
            return listSheet;
        }

        public static bool loadAttenuation() {
            var setting = myGlobal.settingviewmodel.SM;
            if (File.Exists(setting.pathlossFile) == false) return false;
            myGlobal.listAttenuation = new List<AttenuationItem>();

            string raw_data = File.ReadAllText(setting.pathlossFile);
            string bh0 = raw_data.Split(new string[] { "</Path>" }, StringSplitOptions.None)[0];
            string data_list = bh0.Split(new string[] { "<DataList>" }, StringSplitOptions.None)[1];
            string[] buffer = data_list.Split('\n');
            for (int i = 0; i < buffer.Length; i++) {
                string s = buffer[i];
                if (s.Contains("<Frequency>")) {
                    AttenuationItem ai = new AttenuationItem();
                    ai.freq = s.Replace("<Frequency>", "").Replace("</Frequency>", "").Trim();
                    ai.value = buffer[i + 1].Replace("<Value>", "").Replace("</Value>", "").Trim();
                    myGlobal.listAttenuation.Add(ai);
                }
            }

            return true;
        }

        public static bool inputAttenuationToCollectionCommand() {
            if (myGlobal.runviewmodel.collectionCommand.Count == 0) return false;
            for (int i = 0; i < myGlobal.runviewmodel.collectionCommand.Count; i++) {
                var item = myGlobal.runviewmodel.collectionCommand[i];
                if (string.IsNullOrEmpty(item.Attenuation) || string.IsNullOrWhiteSpace(item.Attenuation)) continue;
                string att_value = "";
                for (int j = i; j >= 0; j--) {
                    var ref_item = myGlobal.runviewmodel.collectionCommand[j];
                    if (string.IsNullOrWhiteSpace(ref_item.commandText) || string.IsNullOrEmpty(ref_item.commandText)) continue;
                    if (ref_item.commandText.Contains(":FREQ")) {
                        string[] buffer = ref_item.commandText.Split(' ');
                        string freq = buffer[1].Substring(0, 4);
                        if (myGlobal.listAttenuation.Count == 0) att_value = "0";
                        var att = myGlobal.listAttenuation.Where(x => x.freq.Equals(freq)).FirstOrDefault();
                        if (att == null) att_value = "0";
                        else att_value = att.value;
                        break;
                    }
                }
                item.Attenuation = att_value;
            }

            return true;
        }
    }
}
