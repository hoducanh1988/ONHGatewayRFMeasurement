using OneHomeRFMeasurement.Assets.Base;
using OneHomeRFMeasurement.Models;
using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityPack.IO;

namespace OneHomeRFMeasurement.Commands {
    public class SettingSaveCommand : ICommand {

        private SettingViewModel _svm;
        public SettingSaveCommand(SettingViewModel svm) {
            _svm = svm;
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
            XmlHelper<SettingModel>.ToXmlFile(_svm.SM, AppDomain.CurrentDomain.BaseDirectory + "setting.xml");
            Support.loadAttenuation();
            Support.loadScriptCommand();
            Support.inputAttenuationToCollectionCommand();
            System.Windows.MessageBox.Show("Sucess!", "Save Setting", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        #endregion

    }
}
