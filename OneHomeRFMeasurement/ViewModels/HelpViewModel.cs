using OneHomeRFMeasurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.ViewModels {

    public class HelpViewModel {

        public HelpViewModel() {
            _hm = new HelpModel();
        }

        HelpModel _hm;
        public HelpModel HM {
            get => _hm;
        }

    }
}
