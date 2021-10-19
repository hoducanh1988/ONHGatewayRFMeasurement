using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;
using OneHomeRFMeasurement.Assets.Global;

namespace OneHomeRFMeasurement.Assets.IO {
    public class Log<T> where T: class, new() {
        T t = null;
        string dir = "";
        string product_id = "";
        string result = "";

        public Log(T _t) {
            this.t = _t;
            product_id = t.GetType().GetProperty("productID").GetValue(t, null) as string;
            result = t.GetType().GetProperty("totalResult").GetValue(t, null) as string;

            dir = $"{AppDomain.CurrentDomain.BaseDirectory}Log\\{DateTime.Now.ToString("yyyy-MM-dd")}\\{product_id}_{DateTime.Now.ToString("HHmmss")}";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }


        public bool systemToFile() {
            try {
                string f = $"{dir}\\{product_id}_{DateTime.Now.ToString("HHmmss")}_system_{result}.txt";
                string data = t.GetType().GetProperty("logSystem").GetValue(t, null) as string;
                File.WriteAllText(f, data);
                return true;
            }
            catch { return false; }
        }

        public bool instrumentToFile() {
            try {
                string f = $"{dir}\\{product_id}_{DateTime.Now.ToString("HHmmss")}_instr_{result}.txt";
                string data = t.GetType().GetProperty("logInstrument").GetValue(t, null) as string;
                File.WriteAllText(f, data);
                return true;
            }
            catch { return false; }
        }

        public bool dutToFile() {
            try {
                string f = $"{dir}\\{product_id}_{DateTime.Now.ToString("HHmmss")}_dut_{result}.txt";
                string data = t.GetType().GetProperty("logDut").GetValue(t, null) as string;
                File.WriteAllText(f, data);
                return true;
            }
            catch { return false; }
        }

        public bool commandToFile() {
            try {
                string f = $"{dir}\\{product_id}_{DateTime.Now.ToString("HHmmss")}_command_{result}.txt";
                using (StreamWriter sw = new StreamWriter(f, true, Encoding.Unicode)) {
                    sw.WriteLine("\"ID\",\"Mode\",\"commandText\",\"inputType\",\"isChecked\",\"retryTime\",\"feedBack\",\"splitChar\",\"valueIndexer\",\"LL\",\"Value\",\"UL\",\"Attenuation\",\"Result\",\"Note\"");
                    foreach (var item in myGlobal.runviewmodel.collectionCommand) {
                        sw.WriteLine(item.ToString());
                    }
                }
                return true;
            } catch { return false; }
            
        }


    }
}
