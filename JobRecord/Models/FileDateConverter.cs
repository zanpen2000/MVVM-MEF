using JobRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JobRecord
{

    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var record = (Record)value;
                string filename = record.ExcelFile.Substring(0, record.ExcelFile.Length - 12);
                string date = string.Join("", (from n in record.Date.Split('-','/') select n.PadLeft(2,'0')).ToArray());
                string result = filename + date + ".xls";
                record.LastExcelFile = result;
                return result;

            }
            catch (Exception)
            {

                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FileDateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string filename = values[0].ToString();
                string filename1 = filename.Substring(0, filename.Length - 12);

                string date = values[1].ToString();

                var s = from n in date.Split('-', '/') select n.PadLeft(2, '0');

                string filename2 = string.Join("", s.ToArray());

                return filename1 + filename2 + ".xls";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
    
}
