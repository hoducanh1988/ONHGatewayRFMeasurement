using OneHomeRFMeasurement.Assets.Custom;
using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Assets.Global {
    public class myGlobal {

        public static SettingViewModel settingviewmodel = new SettingViewModel();
        public static RunAllViewModel runviewmodel = new RunAllViewModel();
        public static MainWindowViewModel mainviewmodel = new MainWindowViewModel();
        public static List<AttenuationItem> listAttenuation = null;

    }
}
