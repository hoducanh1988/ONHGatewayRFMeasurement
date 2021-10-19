using OneHomeRFMeasurement.Assets.Global;
using OneHomeRFMeasurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.ViewModels {
    public class MainWindowViewModel {

        public MainWindowViewModel() {
            _mwm = new MainWindowModel();
            _sm = myGlobal.settingviewmodel.SM;
        }

        MainWindowModel _mwm;
        public MainWindowModel mainmodel {
            get => _mwm;
        }

        SettingModel _sm;
        public SettingModel SM {
            get => _sm;
        }
    }
}
