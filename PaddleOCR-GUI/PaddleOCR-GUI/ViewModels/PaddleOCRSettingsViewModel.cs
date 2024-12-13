using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaddleOCR_GUI.ViewModels
{
    public class PaddleOCRSettingsViewModel : BindableBase
    {
        public PaddleOCRSettingsViewModel()
        {
            PythonScriptPath = @"D:\Learning\PaddleOCR\test.py";
            PythonExecutablePath = @"D:\Learning\PaddleOCR\PaddleOCRVENV\Scripts\python.exe";
            PythonDLLPath = @"C:\Users\25398\AppData\Local\Programs\Python\Python312\python312.dll";
            PythonHomePath = @"D:\Learning\PaddleOCR\PaddleOCRVENV\Scripts\python.exe";
            PythonPath = @"D:\Learning\PaddleOCR\;D:\Learning\PaddleOCR\PaddleOCRVENV\Lib;D:\Learning\PaddleOCR\PaddleOCRVENV\Lib\site-packages;C:\Users\25398\AppData\Local\Programs\Python\Python312\Lib;C:\Users\25398\AppData\Local\Programs\Python\Python312\Lib\site-packages;C:\Users\25398\AppData\Local\Programs\Python\Python312\DLLs";
        }

        private string? _pythonScriptPath;
        public string? PythonScriptPath
        {
            get { return _pythonScriptPath; }
            set { SetProperty(ref _pythonScriptPath, value); }
        }

        private string? _pythonExecutablePath;
        public string? PythonExecutablePath
        {
            get { return _pythonExecutablePath; }
            set { SetProperty(ref _pythonExecutablePath, value); }
        }

        private string? _pythonDLLPath;
        public string? PythonDLLPath
        {
            get { return _pythonDLLPath; }
            set { SetProperty(ref _pythonDLLPath, value); }
        }

        private string? _pythonHomePath;
        public string? PythonHomePath
        {
            get { return _pythonHomePath; }
            set { SetProperty(ref _pythonHomePath, value); }
        }

        private string? _pythonPath;
        public string? PythonPath
        {
            get { return _pythonPath; }
            set { SetProperty(ref _pythonPath, value); }
        }
    }
}
