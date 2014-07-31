using System.ComponentModel;
using System.Windows;

namespace JobRecord.Models
{
    public class RecordInformation : DependencyObject, INotifyPropertyChanged
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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        public string LastExcelFile
        {
            get { return (string)GetValue(LastExcelFileProperty); }
            set
            {
                SetValue(LastExcelFileProperty, value);
                this.OnPropertyChanged("LastExcelFile");
            }
        }

        // Using a DependencyProperty as the backing store for LastExcelFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastExcelFileProperty =
            DependencyProperty.Register("LastExcelFile", typeof(string), typeof(RecordInformation));

     

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


        private string mailPassword;

        public string MailPassword
        {
            get { return mailPassword; }
            set
            {
                mailPassword = value;
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


        private string personName;

        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
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




        private string department;

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
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
