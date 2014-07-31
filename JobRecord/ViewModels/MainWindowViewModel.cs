using System;

namespace JobRecord.ViewModels
{
    using JobRecord.Models;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Win32;
    using System.Configuration;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;

    public class MainWindowViewModel : ViewModelBase
    {
        #region Propertys
        public RecordInformation RecordInfo
        {
            get { return (RecordInformation)GetValue(RecordInfoProperty); }
            set { SetValue(RecordInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecordInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecordInfoProperty =
            DependencyProperty.Register("RecordInfo", typeof(RecordInformation), typeof(MainWindowViewModel)); 
        #endregion

        #region Commands
        public DelegateCommand BrowseCommand { get; set; }
        public DelegateCommand ReadContentCommand { get; set; }
        public DelegateCommand SaveAsCommand { get; set; }
        public DelegateCommand MailSendCommand { get; set; }

        #endregion

        public MainWindowViewModel()
        {

            RecordInfo = new RecordInformation();

            SetCommand();
            LoadConfig();
            SetNewFileName();
        }

        private void SetNewFileName()
        {
            MultiBinding mb = new MultiBinding();
            mb.Bindings.Add(new Binding("ExcelFile") { Source = RecordInfo });
            mb.Bindings.Add(new Binding("Date") { Source = RecordInfo });
            mb.Converter = new FileDateConverter();

            BindingOperations.SetBinding(RecordInfo, RecordInformation.LastExcelFileProperty, mb);
        }

        private void SetCommand()
        {
            this.RecordInfo.PropertyChanged += RecordInfo_PropertyChanged;

            this.BrowseCommand = new DelegateCommand(BrowserExcelFile);
            this.ReadContentCommand = new DelegateCommand(ReadContent, CanReadContent);
            this.SaveAsCommand = new DelegateCommand(SaveExcelAs, CanSaveExcelAs);
            this.MailSendCommand = new DelegateCommand(MailSend, CanMailSend);

        }

        void RecordInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ExcelFile":
                    this.LoadExcel(this.RecordInfo.ExcelFile);
                    this.SaveAsCommand.RaiseCanExecuteChanged();
                    break;
                case "Date":
                    this.SaveAsCommand.RaiseCanExecuteChanged();
                    break;
                case "LastExcelFile"://永远无法触发？
                    this.SaveAsCommand.RaiseCanExecuteChanged();
                    break;
                case "ContentCell":
                    this.ReadContentCommand.RaiseCanExecuteChanged();
                    break;
                case "MailUser":
                    this.MailSendCommand.RaiseCanExecuteChanged();
                    break;
                case "MailTo":
                    this.MailSendCommand.RaiseCanExecuteChanged();
                    break;
                case "MailPassword":
                    this.MailSendCommand.RaiseCanExecuteChanged();
                    break;
                default:
                    break;
            }

        }


        private void MailSend()
        {
            SaveExcelAs();

            string filename = RecordInfo.ExcelFile;

            Task.Factory.StartNew(() =>
            {
                IsBusy = true;
                BusyContent = "正在发送日志...";
                using (ExcelUnit excel = new ExcelUnit(filename))
                {
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        if (excel.SaveAs(RecordInfo, RecordInfo.LastExcelFile))
                        {
                            Email email = new Email();
                            email.host = "smtp.qq.com";
                            email.port = 587;
                            email.mailFrom = RecordInfo.MailUser;
                            email.mailPwd = RecordInfo.MailPassword;
                            email.mailSubject = System.IO.Path.GetFileNameWithoutExtension(RecordInfo.LastExcelFile) + " " + RecordInfo.PersonName;
                            email.mailToArray = RecordInfo.MailTo.Split(';');

                            email.attachmentsPath = new string[] { RecordInfo.LastExcelFile };

                            email.SendAsync(new System.Net.Mail.SendCompletedEventHandler((obj, ee) =>
                            {
                                if (ee.Error != null)
                                {
                                    BusyContent = "发送失败:\r\n" + ee.Error.Message;
                                }
                                else
                                {
                                    BusyContent = "发送成功";
                                }

                            }));
                        }
                    }

                        ).Completed += (o1, o2) =>
                        {
                            IsBusy = false;
                            iwm.ShowMessage(BusyContent);
                        };
                }

            });


        }

        private bool CanMailSend()
        {
            return !string.IsNullOrEmpty(RecordInfo.MailTo) &&
                !string.IsNullOrEmpty(RecordInfo.MailUser) &&
                !string.IsNullOrEmpty(RecordInfo.MailPassword) ? true : false;
        }

        private void SaveExcelAs()
        {
            SaveConfig();
            string filename = RecordInfo.ExcelFile;

            Task.Factory.StartNew(() =>
            {
                IsBusy = true;
                BusyContent = "正在保存文档...";
                using (ExcelUnit excel = new ExcelUnit(filename))
                {
                    string msg = "";
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        msg = excel.SaveAs(RecordInfo, RecordInfo.LastExcelFile) ? "保存成功" : "保存失败";

                    }).Completed += (o1, o2) =>
                    {
                        BusyContent = msg;
                        IsBusy = false;

                    };
                }
            });


        }

        private bool CanSaveExcelAs()
        {
            bool result = false;
            if (RecordInfo != null)
            {
                if (!string.IsNullOrEmpty(RecordInfo.ExcelFile) && !string.IsNullOrEmpty(RecordInfo.LastExcelFile) && !string.IsNullOrEmpty(RecordInfo.Date))
                {
                    result = DateTime.Parse(RecordInfo.Date).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"));
                }
            }
            return result;
        }

        private bool CanReadContent()
        {
            return string.IsNullOrEmpty(RecordInfo.ContentCell) || 
                string.IsNullOrEmpty(RecordInfo.ExcelFile) ? false : true;
        }

        private void ReadContent()
        {
            string filename = RecordInfo.ExcelFile;
            Task.Factory.StartNew(() =>
            {
                IsBusy = true;
                BusyContent = "正在加载文档...";
                using (ExcelUnit excel = new ExcelUnit(filename))
                {
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        RecordInfo.DiaryContent = excel.ReadCell(RecordInfo.ContentCell);
                    }).Completed += (o1, o2) =>
                    {
                        IsBusy = false;
                    };
                }
            });
        }

        private void BrowserExcelFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择日志模板文件";
            openFileDialog.Filter = "文件（.xls）|*.xls";//文件扩展名
            RecordInfo.ExcelFile = this.OpenFileDialog(openFileDialog);
        }

        private void LoadExcel(string excelFilename)
        {
            if (!System.IO.File.Exists(excelFilename)) return;

            Task.Factory.StartNew(() =>
            {
                IsBusy = true;
                BusyContent = "正在加载文档...";
                using (ExcelUnit excel = new ExcelUnit(excelFilename))
                {
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        RecordInfo = excel.Read(RecordInfo);
                        RecordInfo.Date = System.DateTime.Now.ToShortDateString();
                    }).Completed += (o1, o2) =>
                    {
                        IsBusy = false;
                    };
                }
            });

        }

        private void LoadConfig()
        {
            RecordInfo.MailUser = ConfigurationManager.AppSettings.Get("mailuser");
            RecordInfo.MailTo = ConfigurationManager.AppSettings.Get("mailto");
            RecordInfo.ExcelFile = ConfigurationManager.AppSettings.Get("lastfilename");
            RecordInfo.ContentCell = ConfigurationManager.AppSettings.Get("contentcell");
        }

        private void SaveConfig()
        {
            var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings["lastfilename"].Value = RecordInfo.LastExcelFile;
            conf.AppSettings.Settings["mailuser"].Value = RecordInfo.MailUser;
            conf.AppSettings.Settings["mailto"].Value = RecordInfo.MailTo;
            conf.AppSettings.Settings["contentcell"].Value = RecordInfo.ContentCell;
            conf.Save();
        }
    }
}
