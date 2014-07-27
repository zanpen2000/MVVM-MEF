using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRecord.Models
{
    using Microsoft.Practices.Prism.Mvvm;

    public class Record : BindableBase
    {
        private string excelFile;

        public string ExcelFile
        {
            get { return excelFile; }
            set
            {
                excelFile = value;
                this.OnPropertyChanged("ExcelFile");
            }
        }

        private string lastExcelFile;

        public string LastExcelFile
        {
            get { return lastExcelFile; }
            set
            {
                lastExcelFile = value;
                this.OnPropertyChanged("LastExcelFile");
            }
        }

        private string mailUser;

        public string MailUser
        {
            get { return mailUser; }
            set
            {
                mailUser = value;
                this.OnPropertyChanged("MailUser");
            }
        }

        private string mailPwd;

        public string MailPassword
        {
            get { return mailPwd; }
            set
            {
                mailPwd = value;
                this.OnPropertyChanged("MailPassword");
            }
        }

        private string mailTo;

        public string MailTo
        {
            get { return mailTo; }
            set
            {
                mailTo = value;
                this.OnPropertyChanged("MailTo");
            }
        }


        private string contentCell;

        public string ContentCell
        {
            get { return contentCell; }
            set
            {
                contentCell = value;
                this.OnPropertyChanged("ContentCell");
            }
        }



        private string name;
        public string PersonName
        {
            get { return name; }
            set
            {
                name = value;
                this.OnPropertyChanged("PersonName");
            }
        }

        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                this.OnPropertyChanged("Id");
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                this.OnPropertyChanged("Date");
            }
        }

        private string depart;
        public string Department
        {
            get { return depart; }
            set
            {
                depart = value;
                this.OnPropertyChanged("Department");
            }
        }

        private string company;
        public string Company
        {
            get { return company; }
            set
            {
                company = value;
                this.OnPropertyChanged("Company");
            }
        }

        private string diaryContent;
        public string DiaryContent
        {
            get { return diaryContent; }
            set
            {
                diaryContent = value;
                this.OnPropertyChanged("DiaryContent");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
