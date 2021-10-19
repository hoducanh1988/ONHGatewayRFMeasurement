using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

namespace OneHomeRFMeasurement.Commands {
    public class LogGoCommand : ICommand {

        LogViewModel lvm = null;
        public LogGoCommand(LogViewModel _lvm) {
            this.lvm = _lvm;
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
            if (lvm.LM.isLog == true) {
                string dir = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                System.Diagnostics.Process.Start(dir);
            }
            else if (lvm.LM.isSetting == true) {
                string f = $"{AppDomain.CurrentDomain.BaseDirectory}setting.xml";
                if (File.Exists(f)) System.Diagnostics.Process.Start(f);
            }
            else if (lvm.LM.isUserGuide == true) {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
            else {
                System.Windows.MessageBox.Show("Sai");
            }
        }

        #endregion

    }
}
