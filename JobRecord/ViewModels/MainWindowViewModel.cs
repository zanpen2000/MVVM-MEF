
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRecord.ViewModels
{
    using JobRecord.Models;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Win32;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Configuration;
    using System.Reflection;
    using System.Windows.Data;

    public class MainWindowViewModel : IAppHost
    {
        [Import(typeof(IWindowManager))]
        public IWindowManager iwm = null;

        private CompositionContainer _container;

        public Record RecordInfo { get; set; }


        public DelegateCommand BrowseCommand { get; set; }
        public DelegateCommand ReadContentCommand { get; set; }
        public DelegateCommand SaveAsCommand { get; set; }

        public DelegateCommand MailSendCommand { get; set; }

        public MainWindowViewModel()
        {
            RecordInfo = new Record();

            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);

            LoadConfig();

            SetCommand();

            //SetNewFileName();

            //ServiceLocator.AddService<IAppHost>(this);
        }

        private void SetNewFileName()
        {

            var c = new FileNameConverter();
            RecordInfo.LastExcelFile = (string)c.Convert(RecordInfo, null, null, null);
        }

        private void SetCommand()
        {
            this.BrowseCommand = new DelegateCommand(BrowserExcelFile);
            this.ReadContentCommand = new DelegateCommand(ReadContent, CanReadContent);
            this.SaveAsCommand = new DelegateCommand(SaveExcelAs, CanSaveExcelAs);
            this.MailSendCommand = new DelegateCommand(MailSend, CanMailSend);
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

        private bool CanMailSend()
        {
            return string.IsNullOrEmpty(RecordInfo.MailTo) ||
                string.IsNullOrEmpty(RecordInfo.MailUser) ||
                string.IsNullOrEmpty(RecordInfo.MailPassword) ||
                !System.IO.File.Exists(RecordInfo.LastExcelFile)
                ? false : true;
        }

        private void SaveExcelAs()
        {
            //保存信息，个人信息，日志内容，时间
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Title = "选择保存路径";
            sDialog.Filter = "文件（.xls）|*.xls";//文件扩展名
            sDialog.FileName = RecordInfo.LastExcelFile;
            if ((bool)sDialog.ShowDialog().GetValueOrDefault())
            {
                using (ExcelUnit excel = new ExcelUnit(RecordInfo.LastExcelFile))
                {
                    string msg = excel.SaveAs(RecordInfo, sDialog.FileName) ? "保存成功" : "保存失败";
                    iwm.ShowMessage(msg);
                }
            }

            SaveConfig();

        }

        private bool CanSaveExcelAs()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(RecordInfo.ExcelFile) && !string.IsNullOrEmpty(RecordInfo.LastExcelFile))
            {
                if (RecordInfo != null && !String.IsNullOrEmpty(RecordInfo.Date))
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
            if (!System.IO.File.Exists(excelFilename)) return;
            using (ExcelUnit excel = new ExcelUnit(excelFilename))
            {
                RecordInfo = excel.Read(RecordInfo);
                RecordInfo.Date = System.DateTime.Now.ToShortDateString();
                //SetNewFileName();
            }
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
