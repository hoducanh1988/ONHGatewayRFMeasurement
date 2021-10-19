using OneHomeRFMeasurement.Assets.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OneHomeRFMeasurement.Views {
    /// <summary>
    /// Interaction logic for RunAllView.xaml
    /// </summary>
    public partial class RunAllView : UserControl {
        DispatcherTimer timer = null;

        public RunAllView() {
            InitializeComponent();
            this.DataContext = myGlobal.runviewmodel;
            timer = new DispatcherTimer();
            int delay_time = 500;
            timer.Interval = TimeSpan.FromMilliseconds(delay_time);
            timer.Tick += (s, e) => {
                if (myGlobal.runviewmodel.RAM.currentIndex != -1) {
                    if (myGlobal.runviewmodel.RAM.totalResult.Equals("Waiting...")) {
                        this.dg_command.Items.Refresh();
                        this.dg_command.ScrollIntoView(this.dg_command.Items.GetItemAt(myGlobal.runviewmodel.RAM.currentIndex));
                    }
                }
            };
            timer.Start();
        }

        ~RunAllView() {
            timer.Stop();
            timer = null;
        }

    }
}
