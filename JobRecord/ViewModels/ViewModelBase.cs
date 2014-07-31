using JobRecord.Models;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;

namespace JobRecord.ViewModels
{
    public abstract class ViewModelBase : DependencyObject, IAppHost, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isbusy;
        public bool IsBusy
        {
            get
            {
                return isbusy;
            }
            set
            {
                isbusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        private string busyContent;
        public string BusyContent
        {
            get
            {
                return busyContent;
            }
            set
            {
                busyContent = value;
                RaisePropertyChanged("BusyContent");
            }
        }


        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ShowMessage(string msg)
        {
            iwm.ShowMessage(msg);
        }

        public string OpenFileDialog(Microsoft.Win32.OpenFileDialog dlg)
        {
            return iwm.OpenFileDialog(dlg);
        }

        [Import(typeof(IWindowManager))]
        public IWindowManager iwm = null;

        private CompositionContainer _container;

        public ViewModelBase()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);
        }
    }
}
