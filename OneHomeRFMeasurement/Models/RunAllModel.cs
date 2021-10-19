using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Models {

    public class RunAllModel : INotifyPropertyChanged {

        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set {
                _log_system = value;
                OnPropertyChanged(nameof(logSystem));
            }
        }
        string _log_dut;
        public string logDut {
            get { return _log_dut; }
            set {
                _log_dut = value;
                OnPropertyChanged(nameof(logDut));
            }
        }
        string _log_instrument;
        public string logInstrument {
            get { return _log_instrument; }
            set {
                _log_instrument = value;
                OnPropertyChanged(nameof(logInstrument));
            }
        }
        string _product_id;
        public string productID {
            get { return _product_id; }
            set {
                _product_id = value;
                OnPropertyChanged(nameof(productID));
            }
        }
        string _id;
        public string ID {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
        string _total_time;
        public string totalTime {
            get { return _total_time; }
            set {
                _total_time = value;
                OnPropertyChanged(nameof(totalTime));
            }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        bool _is_focused;
        public bool isFocused {
            get { return _is_focused; }
            set {
                _is_focused = value;
                OnPropertyChanged(nameof(isFocused));
            }
        }
        int _current_index;
        public int currentIndex {
            get { return _current_index; }
            set {
                _current_index = value;
                OnPropertyChanged(nameof(currentIndex));
            }
        }
        
        public void Init() {
            logSystem = logDut = logInstrument = productID = "";
            totalTime = "00:00:00";
            totalResult = "-";
            isFocused = true;
            currentIndex = -1;
        }

        public void Waiting() {
            productID = ID;
            totalResult = "Waiting...";
            isFocused = false;
        }

        public bool Passed() {
            ID = "";
            totalResult = "Passed";
            isFocused = true;
            return true;
        }

        public bool Failed() {
            ID = "";
            totalResult = "Failed";
            isFocused = true;
            return true;
        }

        public RunAllModel() {
            productID = "";
            ID = "";
            Init();
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
