using Microsoft.Win32;
using OneHomeRFMeasurement.Assets.Base;
using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneHomeRFMeasurement.Commands {
    public class SettingBrowsePathlossCommand : ICommand {
        private SettingViewModel svm;
        public SettingBrowsePathlossCommand(SettingViewModel _svm) {
            svm = _svm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button save setting
        public bool CanExecute(object parameter) {
            return true;
        }

        //save setting
        public void Execute(object parameter) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xml|*.xml";
            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (dlg.ShowDialog() == true) {
                svm.SM.pathlossFile = dlg.SafeFileName;
            }
        }

        #endregion
    }
}
