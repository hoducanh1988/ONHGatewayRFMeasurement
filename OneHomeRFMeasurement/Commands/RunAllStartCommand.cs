using OneHomeRFMeasurement.Assets.Custom;
using OneHomeRFMeasurement.Assets.Global;
using OneHomeRFMeasurement.Assets.IO;
using OneHomeRFMeasurement.Assets.Protocol;
using OneHomeRFMeasurement.Models;
using OneHomeRFMeasurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using UtilityPack.Converter;

namespace OneHomeRFMeasurement.Commands {
    public class RunAllStartCommand : ICommand {

        RunAllViewModel ravm;
        RunAllModel testing = null;
        SettingModel setting = null;

        public RunAllStartCommand(RunAllViewModel _ravm) {
            this.ravm = _ravm;
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
            Thread t = new Thread(new ThreadStart(() => {
                bool r = MeasureParameter();
            }));
            t.IsBackground = true;
            t.Start();

            Thread z = new Thread(new ThreadStart(() => {
                int d = 0;
            RE:
                d++;
                if (t.IsAlive == true) {
                    myGlobal.runviewmodel.RAM.totalTime = myConverter.intToTimeSpan(d * 500);
                    Thread.Sleep(500);
                    goto RE;
                }
            }));
            z.IsBackground = true;
            z.Start();
        }

        #endregion

        #region Excute

        private bool MeasureParameter() {
            bool r = false;
            GPIB<RunAllModel> instr = null;
            RS232<RunAllModel> dut = null;
            try {
                testing = myGlobal.runviewmodel.RAM;
                setting = myGlobal.settingviewmodel.SM;
                instr = new GPIB<RunAllModel>(setting.instrAddress, testing);
                dut = new RS232<RunAllModel>(setting.dutAddress, setting.dutBaudRate, testing);
                if (myGlobal.runviewmodel.collectionCommand == null || myGlobal.runviewmodel.collectionCommand.Count == 0) goto END;

                testing.Init();
                testing.Waiting();
                foreach (var item in myGlobal.runviewmodel.collectionCommand) {
                    item.Result = "";
                    item.Value = "";
                }

                //connect instr
                r = instr.Open(); if (!r) goto END;

                //connect dut
                r = dut.Open(); if (!r) goto END;

                //excute command
                string mode_name = "";
                foreach (var item in myGlobal.runviewmodel.collectionCommand) {
                    myGlobal.runviewmodel.RAM.currentIndex++;
                    if (item.isChecked == false) continue;
                    var mode = myGlobal.runviewmodel.collectionResult.Where(x => x.Name.Equals(item.Mode)).FirstOrDefault();
                    if (mode.Name != mode_name) mode_name = mode.Name;

                    item.Result = "Waiting...";
                    try {
                        switch (item.Device) {
                            case var a when item.Device.ToLower().Equals("dut"): { //DUT
                                    r = dut_parameter(item, dut);
                                    if (!r) { mode.Result = "Failed"; goto END; }
                                    break;
                                }
                            case var b when item.Device.ToLower().Equals("sleep"): { //SLEEP
                                    r = sleep_parameter(item); if (!r) { mode.Result = "Failed"; goto END; }
                                    break;
                                }
                            default: { //INSTRUMENT
                                    r = instrument_parameter(item, instr); if (!r) { mode.Result = "Failed"; goto END; }
                                    break;
                                }
                        }
                    }
                    catch (Exception ex) {
                        testing.logSystem += ex.ToString();
                        r = false;
                        item.Result = "Failed";
                        mode.Result = "Failed";
                        goto END;
                    }
                }
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }

        END:
            myGlobal.runviewmodel.RAM.currentIndex = 0;
            instr.Close();
            dut.Close();
            bool _ = r ? testing.Passed() : testing.Failed();
            Log<RunAllModel> log = new Log<RunAllModel>(testing);
            log.instrumentToFile();
            log.dutToFile();
            log.systemToFile();
            log.commandToFile();
            return r;
        }

        private bool dut_parameter(CommandItem item, RS232<RunAllModel> dut) {
            int count = 0;
            bool r = false;
            string data = "";
            if (string.IsNullOrEmpty(item.inputType) == true || string.IsNullOrWhiteSpace(item.inputType) == true) { // writeline
                dut.WriteLine(item.commandText);
                Thread.Sleep(250);
                item.Result = "Passed";

                r = true;
            }
            else { //query
                count = 0;
                int time_out = 15000;
                int delay_time = 250;
                int count_max = time_out / delay_time;
                data = "";
                r = dut.WriteLine(item.commandText);
            RDQ:
                count++;
                Thread.Sleep(delay_time);
                data += dut.Read();
                item.Value = data;
                r = item.Value.ToUpper().Contains(item.feedBack.ToUpper());

                if (!r) {
                    if (count < count_max) {
                        goto RDQ;
                    }
                    else {
                        item.Result = "Failed";
                        goto END;
                    }
                }
                item.Result = "Passed";
            }

        END:
            return r;
        }

        private bool sleep_parameter(CommandItem item) {
            Thread.Sleep(int.Parse(item.commandText));
            item.Result = "Passed";
            return true;
        }

        private bool instrument_parameter(CommandItem item, GPIB<RunAllModel> instr) {
            int count = 0;
            bool r = false;
            if (string.IsNullOrEmpty(item.inputType) == true) { // writeline
                count = 0;
            RIW:
                count++;
                r = instr.WriteLine(item.commandText);
                if (!r) {
                    if (count < item.retryTime) {
                        goto RIW;
                    }
                    else {
                        item.Result = "Failed";
                        goto END;
                    }
                }
                item.Result = "Passed";
            }
            else { //query
                count = 0;
            RIQ:
                count++;
                string data = instr.Query(item.commandText);
                r = !(string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data));
                if (!r) {
                    if (count < item.retryTime) {
                        goto RIQ;
                    }
                    else {
                        item.Result = "Failed";
                        goto END;
                    }
                }

                //get value
                if (string.IsNullOrEmpty(item.splitChar) || string.IsNullOrWhiteSpace(item.splitChar)) {
                    item.Value = data;
                }
                else {
                    string[] buffer = data.Split(new string[] { item.splitChar }, StringSplitOptions.None);
                    string value = buffer[int.Parse(item.valueIndexer)];
                    try {
                        item.Value = Math.Round(Convert.ToDouble(value), 2).ToString();
                    }
                    catch { item.Value = $"{-100}"; }
                }

                //compare with standard
                double att = 0;
                switch (item.Attenuation) {
                    case var a when string.IsNullOrWhiteSpace(item.Attenuation) == true:
                    case var b when string.IsNullOrEmpty(item.Attenuation) == true:
                    case var c when double.TryParse(item.Attenuation, out att) == false: { att = 0; break; }
                    default: { att = double.Parse(item.Attenuation); break; }
                }

                bool r1 = (double.Parse(item.Value) + att) >= double.Parse(item.LL);
                bool r2 = (double.Parse(item.Value) + att) <= double.Parse(item.UL);
                r = r1 && r2;
                if (!r) {
                    if (count < item.retryTime) {
                        goto RIQ;
                    }
                    else {
                        item.Result = "Failed";
                        goto END;
                    }
                }
                item.Result = "Passed";
            }

        END:
            return r;
        }

        #endregion


    }
}
