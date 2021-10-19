using OneHomeRFMeasurement.Assets.Base;
using OneHomeRFMeasurement.Commands;
using OneHomeRFMeasurement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using UtilityPack.IO;

namespace OneHomeRFMeasurement.ViewModels {
    public class SettingViewModel : INotifyPropertyChanged {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public SettingViewModel() {

            //load setting file
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "setting.xml") == false) _sm = new SettingModel();
            else _sm = XmlHelper<SettingModel>.FromXmlFile(AppDomain.CurrentDomain.BaseDirectory + "setting.xml");

            _collection_instr_name = new CollectionView(new List<string>() { "E6640A", "MT8870A" });
            _collection_baud_rate = new CollectionView(new List<int>() { 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 });
            
            List<string> listCom = new List<string>();
            for (int i = 1; i <= 99; i++) listCom.Add($"COM{i}");
            _collection_serial_port = new CollectionView(listCom);

            SaveCommand = new SettingSaveCommand(this);
            BrowseScriptCommand = new SettingBrowseScriptCommand(this);
            BrowsePathlossCommand = new SettingBrowsePathlossCommand(this);
        }


        SettingModel _sm;
        public SettingModel SM {
            get => _sm;
        }

        CollectionView _collection_instr_name;
        public CollectionView collectionInstrName {
            get => _collection_instr_name;
        }
        CollectionView _collection_baud_rate;
        public CollectionView collectionBaudRate {
            get => _collection_baud_rate;
        }
        CollectionView _collection_serial_port;
        public CollectionView collectionSerialPort {
            get => _collection_serial_port;
        }
        CollectionView _collection_sheet_name;
        public CollectionView collectionSheetName {
            get { return _collection_sheet_name; }
            set {
                _collection_sheet_name = value;
                OnPropertyChanged(nameof(collectionSheetName));
            }
        }

        //command
        public ICommand SaveCommand {
            get;
            private set;
        }
        public ICommand BrowseScriptCommand {
            get;
            private set;
        }
        public ICommand BrowsePathlossCommand {
            get;
            private set;
        }
    }
}
