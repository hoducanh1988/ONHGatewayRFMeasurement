using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace OneHomeRFMeasurement.Assets.Protocol {
    public class RS232<T> where T : class, new() {

        SerialPort serial = null;
        public bool isConnected = false;
        string port_name = "";
        int baud_rate = 0;
        T t = null;

        public RS232(string address, int baud, T _t) {
            this.port_name = address;
            this.baud_rate = baud;
            this.t = _t;
        }

        public bool Open() {
            try {
                serial = new SerialPort();
                serial.PortName = this.port_name;
                serial.BaudRate = this.baud_rate;
                serial.DataBits = 8;
                serial.Parity = Parity.None;
                serial.StopBits = StopBits.One;
                if (serial.IsOpen == false) serial.Open();

                isConnected = serial.IsOpen;
            }
            catch (Exception ex) {
                isConnected = false;
                var log = t.GetType().GetProperty("logDut");
                string data = log.GetValue(t, null) as string;
                data += ex.ToString();
                log.SetValue(t, data, null);
            }

            return isConnected;
        }

        public bool Write(string cmd) {
            if (!isConnected) return false;
            serial.Write(cmd);
            Thread.Sleep(100);
            return true;
        }

        //public bool Write(string cmd) {
        //    if (!isConnected) return false;
        //    int count = 0;
        //RE:
        //    count++;
        //    serial.Write(cmd);
        //    PropertyInfo pi = t.GetType().GetProperty("logDut");
        //    string s = pi.GetValue(t, null) as string;
        //    s += cmd;
        //    string data = this.Read();
        //    s += data + "\n";
        //    bool r = string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data);
        //    if (r) {
        //        if (count < 3) goto RE;
        //    }

        //    pi.SetValue(t, s, null);
        //    return r;
        //}

        public bool WriteLine(string cmd) {
            return this.Write(cmd + "\n");
        }

        public bool WriteHexa(string hexa_value) {
            PropertyInfo pi = t.GetType().GetProperty("logDut");
            string s = pi.GetValue(t, null) as string;
            s += hexa_value + "\n";

            hexa_value = hexa_value.ToUpper();
            hexa_value = hexa_value.Replace("0X", "");

            try {
                List<byte> bytetosend = new List<byte>();
                bytetosend.Add(Convert.ToByte(hexa_value.Substring(0, 2), 16));
                bytetosend.Add(Convert.ToByte(hexa_value.Substring(2, 2), 16));

                serial.Write(bytetosend.ToArray(), 0, bytetosend.ToArray().Length);
                pi.SetValue(t, s, null);
                return true;
            } catch (Exception ex) {
                s += ex.ToString();
                pi.SetValue(t, s, null);
                return false;
            }
        }

        public string Query(string cmd) {
            if (!isConnected) return null;
            int count = 0;
        RE:
            count++;
            serial.Write(cmd + "\n");
            PropertyInfo pi = t.GetType().GetProperty("logDut");
            string s = pi.GetValue(t, null) as string;
            s += cmd + "\n";
            string data = this.Read();
            s += data + "\n";
            bool r = string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data);
            if (r) {
                if (count < 3) goto RE;
            }

            pi.SetValue(t, s, null);
            return data;
        }

        public string Read() {
            string data = serial.ReadExisting();
            PropertyInfo pi = t.GetType().GetProperty("logDut");
            string s = pi.GetValue(t, null) as string;
            s += data;
            pi.SetValue(t, s, null);
            return data;
        }

        public byte[] ReadByte() {
            int length = serial.BytesToRead;
            byte[] buffer = new byte[length];
            serial.Read(buffer, 0, length);
            PropertyInfo pi = t.GetType().GetProperty("logDut");
            string s = pi.GetValue(t, null) as string;
            foreach (var b in buffer) s += b + ",";
            s += "\n";
            pi.SetValue(t, s, null);
            return buffer;
        }

        public bool Close() {
            if (!isConnected) return true;
            serial.Close();
            return true;
        }


    }

}
