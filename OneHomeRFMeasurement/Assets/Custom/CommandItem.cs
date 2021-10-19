using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Assets.Custom {
    public class CommandItem : INotifyPropertyChanged {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public CommandItem() {
            ID = retryTime = 0;
            Mode = Device = commandText = inputType = splitChar = valueIndexer = LL = UL = Result = Value = feedBack = Note = Attenuation = "";
            isChecked = true;
        }

        int _id;
        public int ID {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
        string _mode;
        public string Mode {
            get { return _mode; }
            set {
                _mode = value;
                OnPropertyChanged(nameof(Mode));
            }
        }
        string _device;
        public string Device {
            get { return _device; }
            set {
                _device = value;
                OnPropertyChanged(nameof(Device));
            }
        }
        string _command_text;
        public string commandText {
            get { return _command_text; }
            set {
                _command_text = value;
                OnPropertyChanged(nameof(commandText));
            }
        }
        bool _is_checked;
        public bool isChecked {
            get { return _is_checked; }
            set {
                _is_checked = value;
                OnPropertyChanged(nameof(isChecked));
            }
        }
        string _input_type;
        public string inputType {
            get { return _input_type; }
            set {
                _input_type = value;
                OnPropertyChanged(nameof(inputType));
            }
        }
        int _retry_time;
        public int retryTime {
            get { return _retry_time; }
            set {
                _retry_time = value;
                OnPropertyChanged(nameof(retryTime));
            }
        }
        string _feed_back;
        public string feedBack {
            get { return _feed_back; }
            set {
                _feed_back = value;
                OnPropertyChanged(nameof(feedBack));
            }
        }
        string _split_char;
        public string splitChar {
            get { return _split_char; }
            set {
                _split_char = value;
                OnPropertyChanged(nameof(splitChar));
            }
        }
        string _value_indexer;
        public string valueIndexer {
            get { return _value_indexer; }
            set {
                _value_indexer = value;
                OnPropertyChanged(nameof(valueIndexer));
            }
        }
        string _ll;
        public string LL {
            get { return _ll; }
            set {
                _ll = value;
                OnPropertyChanged(nameof(LL));
            }
        }
        string _ul;
        public string UL {
            get { return _ul; }
            set {
                _ul = value;
                OnPropertyChanged(nameof(UL));
            }
        }
        string _att;
        public string Attenuation {
            get { return _att; }
            set {
                _att = value;
                OnPropertyChanged(nameof(Attenuation));
            }
        }
        string _value;
        public string Value {
            get { return _value; }
            set {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        string _result;
        public string Result {
            get { return _result; }
            set {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        string _note;
        public string Note {
            get { return _note; }
            set {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
        //ID = retryTime = 0;
        //Mode = Device = commandText = inputType = splitChar = valueIndexer = LL = UL = Result = Value = feedBack = Note = "";
        //isChecked = true;

        public override string ToString() {
            return $"\"{ID}\",\"{Mode}\",\"{commandText}\",\"{inputType}\",\"{isChecked}\",\"{retryTime}\",\"{feedBack}\",\"{splitChar}\",\"{valueIndexer}\",\"{LL}\",\"{Value}\",\"{UL}\",\"{Attenuation}\",\"{Result}\",\"{Note}\"";
        }

    }
}
