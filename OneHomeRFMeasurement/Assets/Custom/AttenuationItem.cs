using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHomeRFMeasurement.Assets.Custom {
    public class AttenuationItem {
        public string freq { get; set; }
        public string value { get; set; }
        public override string ToString() {
            return $"freq={freq}, value={value}";
        }
    }
}
