using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaddleOCR_GUI.ViewModels
{
    public class Process_MethodViewModel : BindableBase
    {
        public Process_MethodViewModel(IContainerProvider container)
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImageCommand);
            OCRCommand = new DelegateCommand(async () => await ExecuteOCRCommand());
            PaddleOCRSettingsViewModel = container.Resolve<PaddleOCRSettingsViewModel>();
        }

        private string? _selectedFilePath;
        public string? SelectedFilePath
        {
            get { return _selectedFilePath; }
            set { SetProperty(ref _selectedFilePath, value); }
        }

        private string? _selectedLanguage;
        public string? SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { SetProperty(ref _selectedLanguage, value); }
        }

        private string? _ocrText;
        public string? OCRText
        {
            get { return _ocrText; }
            set { SetProperty(ref _ocrText, value); }
        }

        public List<string> LanguageOptions { get; set; } = new List<string> { "中文", "英文" };

        public ICommand SelectImageCommand { get; private set; }
        public ICommand OCRCommand { get; private set; }

        public PaddleOCRSettingsViewModel PaddleOCRSettingsViewModel { get; private set; }

        private void ExecuteSelectImageCommand()
        {
            // 创建 OpenFileDialog 实例
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*",
                Title = "选择图片文件"
            };

            // 显示对话框
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取选中的文件路径
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        private Task ExecuteOCRCommand()
        {
            return Task.Run(() =>
            {
                string selectedLanguage;
                switch (SelectedLanguage)
                {
                    case "中文":
                        selectedLanguage = "ch";
                        break;
                    case "英文":
                        selectedLanguage = "en";
                        break;
                    default:
                        selectedLanguage = "ch";
                        break;
                }
                if (PaddleOCRSettingsViewModel.PythonScriptPath == null || PaddleOCRSettingsViewModel.PythonExecutablePath == null)
                {
                    return;
                }
                string pythonScriptPath = PaddleOCRSettingsViewModel.PythonScriptPath; // 替换为你的Python脚本路径
                string pythonExecutablePath = PaddleOCRSettingsViewModel.PythonExecutablePath; // 替换为你的Python解释器路径

                if (SelectedFilePath == null)
                {
                    return;
                }

                string arguments = SelectedFilePath; // 替换为你要传递的参数                                                                                                                                                                         

                // 创建一个 ProcessStartInfo 实例
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = pythonExecutablePath;
                start.Arguments = $"\"{pythonScriptPath}\" {arguments} {selectedLanguage}";
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = true;

                // 创建并启动进程
                using (Process process = Process.Start(start))
                {
                    using (System.IO.StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        OCRText = result;
                    }
                }
            });
        }
    }
}
