using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Assets.Custom {
    public class ResultItem : INotifyPropertyChanged {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ResultItem() {
            Index = 0;
            Name = Result = testTime = "";
        }

        int _index;
        public int Index {
            get { return _index; }
            set {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
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
        string _test_time;
        public string testTime {
            get { return _test_time; }
            set {
                _test_time = value;
                OnPropertyChanged(nameof(testTime));
            }
        }


    }
}
