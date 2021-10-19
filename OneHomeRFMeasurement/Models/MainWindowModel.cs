using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Models {
    public class MainWindowModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public MainWindowModel() {
            appInfo = $"Version: ONH001VN0U0001 - Build time: 08/09/2021 08:00 - Copyright of VNPT Technology 2021";
            productName = "ONE HOME";
            stationName = "Test Tín Hiệu RF (Bluetooth, ZigBee, Wlan ...)";
            selectedIndex = -1;
        }

        string _app_info;
        public string appInfo {
            get { return _app_info; }
            set {
                _app_info = value;
                OnPropertyChanged(nameof(appInfo));
            }
        }
        string _product_name;
        public string productName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(productName));
            }
        }
        string _station_name;
        public string stationName {
            get { return _station_name; }
            set {
                _station_name = value;
                OnPropertyChanged(nameof(stationName));
            }
        }
        int _selected_index;
        public int selectedIndex {
            get { return _selected_index; }
            set {
                _selected_index = value;
                OnPropertyChanged(nameof(selectedIndex));
            }
        }
    }
}
