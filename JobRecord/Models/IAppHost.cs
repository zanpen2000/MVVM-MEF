using Microsoft.Win32;

namespace JobRecord
{
    public interface IAppHost
    {
        void ShowMessage(string msg);

        string OpenFileDialog(OpenFileDialog dlg);
    }


}
