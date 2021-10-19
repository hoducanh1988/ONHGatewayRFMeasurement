using OneHomeRFMeasurement.Assets.Custom;
using OneHomeRFMeasurement.Commands;
using OneHomeRFMeasurement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneHomeRFMeasurement.ViewModels {

    public class RunAllViewModel : INotifyPropertyChanged {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public RunAllViewModel() {
            _ram = new RunAllModel();
            _collection_command = new ObservableCollection<CommandItem>();
            _collection_result = new ObservableCollection<ResultItem>();
            StartCommand = new RunAllStartCommand(this);
        }

        RunAllModel _ram;
        public RunAllModel RAM {
            get => _ram;
        }

        ObservableCollection<CommandItem> _collection_command;
        public ObservableCollection<CommandItem> collectionCommand {
            get { return _collection_command; }
            set {
                _collection_command = value;
                OnPropertyChanged(nameof(collectionCommand));
            }
        }

        ObservableCollection<ResultItem> _collection_result;
        public ObservableCollection<ResultItem> collectionResult {
            get { return _collection_result; }
            set {
                _collection_result = value;
                OnPropertyChanged(nameof(collectionResult));
            }
        }


        //command
        public ICommand StartCommand {
            get;
            private set;
        }

    }

}
