using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Models {

    public class SettingModel : INotifyPropertyChanged {

        string _instr_addr;
        public string instrAddress {
            get { return _instr_addr; }
            set {
                _instr_addr = value;
                OnPropertyChanged(nameof(instrAddress));
            }
        }
        string _dut_addr;
        public string dutAddress {
            get { return _dut_addr; }
            set {
                _dut_addr = value;
                OnPropertyChanged(nameof(dutAddress));
            }
        }
        int _dut_baud_rate;
        public int dutBaudRate {
            get { return _dut_baud_rate; }
            set {
                _dut_baud_rate = value;
                OnPropertyChanged(nameof(dutBaudRate));
            }
        }

        string _dut_user;
        public string dutLogUser {
            get { return _dut_user; }
            set {
                _dut_user = value;
                OnPropertyChanged(nameof(dutLogUser));
            }
        }
        string _dut_password;
        public string dutLogPassword {
            get { return _dut_password; }
            set {
                _dut_password = value;
                OnPropertyChanged(nameof(dutLogPassword));
            }
        }
        string _script_file;
        public string scriptFile {
            get { return _script_file; }
            set {
                _script_file = value;
                OnPropertyChanged(nameof(scriptFile));
            }
        }
        string _sheet_name;
        public string sheetName {
            get { return _sheet_name; }
            set {
                _sheet_name = value;
                OnPropertyChanged(nameof(sheetName));
            }
        }
        string _pathloss_file;
        public string pathlossFile {
            get { return _pathloss_file; }
            set {
                _pathloss_file = value;
                OnPropertyChanged(nameof(pathlossFile));
            }
        }


        public SettingModel() {
            instrAddress = "TCPIP0::192.168.1.5::inst0::INSTR";
            dutAddress = "";
            dutBaudRate = 115200;
            dutLogUser = "";
            dutLogPassword = "";
            scriptFile = "";
            sheetName = "";
            pathlossFile = "";
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
