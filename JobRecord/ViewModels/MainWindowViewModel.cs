using System;

namespace JobRecord.ViewModels
{
    using JobRecord.Models;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Win32;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Configuration;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;

    public class MainWindowViewModel : DependencyObject, IAppHost
    {
        [Import(typeof(IWindowManager))]
        public IWindowManager iwm = null;

        private CompositionContainer _container;



        public RecordInformation RecordInfo
        {
            get { return (RecordInformation)GetValue(RecordInfoProperty); }
            set { SetValue(RecordInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecordInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecordInfoProperty =
            DependencyProperty.Register("RecordInfo", typeof(RecordInformation), typeof(MainWindowViewModel));




        public DelegateCommand BrowseCommand { get; set; }
        public DelegateCommand ReadContentCommand { get; set; }
        public DelegateCommand SaveAsCommand { get; set; }

        public DelegateCommand MailSendCommand { get; set; }

        public MainWindowViewModel()
        {
            RecordInfo = new RecordInformation();

            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);

            LoadConfig();

            SetCommand();

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
            this.BrowseCommand = new DelegateCommand(BrowserExcelFile);
            this.ReadContentCommand = new DelegateCommand(ReadContent, CanReadContent);
            this.SaveAsCommand = new DelegateCommand(SaveExcelAs, CanSaveExcelAs);
            this.MailSendCommand = new DelegateCommand(MailSend, CanMailSend);

            this.RecordInfo.PropertyChanged += RecordInfo_PropertyChanged;
        }

        void RecordInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ExcelFile":
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

        private void LoadConfig()
        {
            RecordInfo.MailUser = ConfigurationManager.AppSettings.Get("mailuser");
            RecordInfo.MailTo = ConfigurationManager.AppSettings.Get("mailto");
            RecordInfo.ExcelFile = ConfigurationManager.AppSettings.Get("lastfilename");
            RecordInfo.ContentCell = ConfigurationManager.AppSettings.Get("contentcell");
        }

        private void MailSend()
        {
            using (ExcelUnit excel = new ExcelUnit(RecordInfo.ExcelFile))
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
                            iwm.ShowMessage("发送失败:\r\n" + ee.Error.Message);
                        }
                        else
                        {
                            iwm.ShowMessage("发送成功");
                        }

                    }));
                }
            }
        }

        private bool CanMailSend()
        {
            return !string.IsNullOrEmpty(RecordInfo.MailTo) &&
                !string.IsNullOrEmpty(RecordInfo.MailUser) &&
                !string.IsNullOrEmpty(RecordInfo.MailPassword) ? true : false;


        }

        private void Save()
        {
            using (ExcelUnit excel = new ExcelUnit(RecordInfo.ExcelFile))
            {
                string msg = excel.SaveAs(RecordInfo, RecordInfo.LastExcelFile) ? "保存成功" : "保存失败";
                iwm.ShowMessage(msg);
            }
        }

        private void SaveExcelAs()
        {
            this.Dispatcher.Invoke(() =>
            {
                iwm.ShowBusyForm(SaveConfig, "正在保存配置文件，请稍候...");
                iwm.ShowBusyForm(Save, "正在保存文档，请稍候...");
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
            return string.IsNullOrEmpty(RecordInfo.ContentCell) ? false : true;
        }

        private void ReadContent()
        {
            using (ExcelUnit excel = new ExcelUnit(RecordInfo.ExcelFile))
            {
                RecordInfo.DiaryContent = excel.ReadCell(RecordInfo.ContentCell);
            }
        }

        private void BrowserExcelFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择日志模板文件";
            openFileDialog.Filter = "文件（.xls）|*.xls";//文件扩展名

            RecordInfo.ExcelFile = this.OpenFileDialog(openFileDialog);
            if (!string.IsNullOrEmpty(RecordInfo.ExcelFile))
                LoadExcel(RecordInfo.ExcelFile);
        }

        private void LoadExcel(string excelFilename)
        {
            Action ac = () =>
            {
                if (!System.IO.File.Exists(excelFilename)) return;
                using (ExcelUnit excel = new ExcelUnit(excelFilename))
                {
                    RecordInfo = excel.Read(RecordInfo);
                    RecordInfo.Date = System.DateTime.Now.ToShortDateString();
                    //SetNewFileName();
                }
            };
            this.Dispatcher.Invoke(() =>
            {
                iwm.ShowBusyForm(ac, "正在读取...");
            });

        }



        private void SaveConfig()
        {
            ConfigurationManager.AppSettings["lastfilename"] = RecordInfo.LastExcelFile;
            ConfigurationManager.AppSettings.Set("mailuser", RecordInfo.MailUser);
            ConfigurationManager.AppSettings.Set("mailto", RecordInfo.MailTo);
            ConfigurationManager.AppSettings.Set("contentcell", RecordInfo.ContentCell);
        }

        public void ShowMessage(string msg)
        {
            iwm.ShowMessage(msg);
        }

        public string OpenFileDialog(OpenFileDialog dlg)
        {
            return iwm.OpenFileDialog(dlg);
        }


    }
}
