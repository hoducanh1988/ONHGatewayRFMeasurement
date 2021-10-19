using OneHomeRFMeasurement.Assets.Base;
using OneHomeRFMeasurement.Assets.Custom;
using OneHomeRFMeasurement.Assets.Global;
using OneHomeRFMeasurement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneHomeRFMeasurement {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        RunAllView rav = new RunAllView();
        SettingView sv = new SettingView();
        LogView lv = new LogView();
        HelpView hv = new HelpView();
        AboutView av = new AboutView();

        public MainWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.mainviewmodel;
            var list = Support.ListSheetInExcel();
            if (list != null) myGlobal.settingviewmodel.collectionSheetName = new CollectionView(list);
            Support.loadAttenuation();
            Support.loadScriptCommand();
            Support.inputAttenuationToCollectionCommand();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            Label lb = sender as Label;
            int idx = -1;
            UserControl uctrl = null;
            switch (lb.Tag) {
                case "run": { uctrl = rav; idx = 0; break; }
                case "setting": { uctrl = sv; idx = 1; break; }
                case "log": { uctrl = lv; idx = 2; break; }
                case "help": { uctrl = hv; idx = 3; break; }
                case "about": { uctrl = av; idx = 4; break; }
                default: break;
            }

            var main = myGlobal.mainviewmodel.mainmodel;
            main.selectedIndex = idx;
            this.grid_main.Children.Clear();
            if (uctrl != null) this.grid_main.Children.Add(uctrl);
        }

    }
}
