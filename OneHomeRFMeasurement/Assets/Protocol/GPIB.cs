using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Assets.Protocol {

    public class GPIB<T> where T : class, new() {

        MessageBasedSession mbSession = null;
        string gpib_address = "";
        public bool isConnected = false;
        T t = null;

        public GPIB(string address, T _t) {
            this.gpib_address = address;
            this.t = _t;
        }

        public bool Open() {
            try {
                mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(this.gpib_address);
                isConnected = true;
            }
            catch (Exception ex) {
                isConnected = false;
                var log = this.t.GetType().GetProperty("logInstrument");
                string data = (string)log.GetValue(this.t, null);
                data += ex.ToString();
                log.SetValue(this.t, data, null);
            }
            return isConnected;
        }

        public bool WriteLine(string cmd) {
            if (!isConnected) return false;
            return this.Write($"{cmd}\n");
        }

        public string Query(string cmd) {
            if (!isConnected) return null;
            PropertyInfo pi = t.GetType().GetProperty("logInstrument");
            string s = pi.GetValue(t, null) as string;
            s += cmd + "\n";
            string data = mbSession.Query(cmd + "\n");
            s += data + "\n";
            pi.SetValue(t, s, null);
            return data;
        }

        public bool Write(string cmd) {
            if (!isConnected) return false;
            int count = 0;
        RE:
            count++;
            mbSession.Write(cmd);
            PropertyInfo pi = t.GetType().GetProperty("logInstrument");
            string s = pi.GetValue(t, null) as string;
            s += cmd;

            mbSession.Write(":SYSTem:ERRor?\n");
            s += ":SYSTem:ERRor?\n";
            string data = this.Read();
            s += data + "\n";
            bool r = data.ToLower().Contains("no error");
            if (!r) {
                if (count < 3) goto RE;
            }

            pi.SetValue(t, s, null);
            return r;
        }

        public string Read() {
            string data = mbSession.ReadString();
            PropertyInfo pi = t.GetType().GetProperty("logInstrument");
            string s = pi.GetValue(t, null) as string;
            s += data;
            pi.SetValue(t, s, null);
            return data;
        }

        public bool Close() {
            if (!isConnected) return true;
            mbSession.Dispose();
            return true;
        }
    }

}
