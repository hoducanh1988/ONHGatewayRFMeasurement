using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using OneHomeRFMeasurement.Assets.Base;

namespace OneHomeRFMeasurement.Commands {
    public class SettingBrowseScriptCommand : ICommand {
        private SettingViewModel svm;
        public SettingBrowseScriptCommand(SettingViewModel _svm) {
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
            dlg.Filter = "*.xlsx|*.xlsx";
            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Scripts";
            if (dlg.ShowDialog() == true) {
                svm.SM.scriptFile = dlg.SafeFileName;
                var list = Support.ListSheetInExcel();
                svm.collectionSheetName = new System.Windows.Data.CollectionView(list);
            }
        }

        #endregion
    }
}
