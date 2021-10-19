using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Models {
    public class LogModel : INotifyPropertyChanged {

        public LogModel() {
            isLog = false;
            isSetting = false;
            isUserGuide = false;
        }

        bool _is_log;
        public bool isLog {
            get { return _is_log; }
            set {
                _is_log = value;
                OnPropertyChanged(nameof(isLog));
            }
        }
        bool _is_setting;
        public bool isSetting {
            get { return _is_setting; }
            set {
                _is_setting = value;
                OnPropertyChanged(nameof(isSetting));
            }
        }
        bool _is_user_guide;
        public bool isUserGuide {
            get { return _is_user_guide; }
            set {
                _is_user_guide = value;
                OnPropertyChanged(nameof(isUserGuide));
            }
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
