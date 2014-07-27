using JobRecord.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JobRecord.Models
{
    public class RecordInformation : DependencyObject
    {



        public string ExcelFile
        {
            get { return (string)GetValue(ExcelFileProperty); }
            set { SetValue(ExcelFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExcelFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExcelFileProperty =

            DependencyProperty.Register("ExcelFile", typeof(string), typeof(RecordInformation));



        public string LastExcelFile
        {
            get { return (string)GetValue(LastExcelFileProperty); }
            set { SetValue(LastExcelFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastExcelFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastExcelFileProperty =
            DependencyProperty.Register("LastExcelFile", typeof(string), typeof(RecordInformation));



        public string MailUser
        {
            get { return (string)GetValue(MailUserProperty); }
            set { SetValue(MailUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MailUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MailUserProperty =
            DependencyProperty.Register("MailUser", typeof(string), typeof(RecordInformation));





        public string MailPassword
        {
            get { return (string)GetValue(MailPasswordProperty); }
            set { SetValue(MailPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MailPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MailPasswordProperty =
            DependencyProperty.Register("MailPassword", typeof(string), typeof(RecordInformation));



        public string MailTo
        {
            get { return (string)GetValue(MailToProperty); }
            set { SetValue(MailToProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MailTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MailToProperty =
            DependencyProperty.Register("MailTo", typeof(string), typeof(RecordInformation));




        public string ContentCell
        {
            get { return (string)GetValue(ContentCellProperty); }
            set { SetValue(ContentCellProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentCell.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentCellProperty =
            DependencyProperty.Register("ContentCell", typeof(string), typeof(RecordInformation));

        public string PersonName
        {
            get { return (string)GetValue(PersonNameProperty); }
            set { SetValue(PersonNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PersonName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PersonNameProperty =
            DependencyProperty.Register("PersonName", typeof(string), typeof(RecordInformation));



        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(string), typeof(RecordInformation));




        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Date.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(string), typeof(RecordInformation));



        public string Department
        {
            get { return (string)GetValue(DepartmentProperty); }
            set { SetValue(DepartmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Department.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepartmentProperty =
            DependencyProperty.Register("Department", typeof(string), typeof(RecordInformation));



        public string Company
        {
            get { return (string)GetValue(CompanyProperty); }
            set { SetValue(CompanyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Company.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompanyProperty =
            DependencyProperty.Register("Company", typeof(string), typeof(RecordInformation));



        public string DiaryContent
        {
            get { return (string)GetValue(DiaryContentProperty); }
            set { SetValue(DiaryContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DiaryContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiaryContentProperty =
            DependencyProperty.Register("DiaryContent", typeof(string), typeof(RecordInformation));



    }
}
