using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRecord
{
    public interface IAppHost
    {
        void ShowMessage(string msg);

        string OpenFileDialog(OpenFileDialog dlg);
    }


}
