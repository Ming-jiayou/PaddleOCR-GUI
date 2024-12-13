using Microsoft.Win32;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaddleOCR_GUI.ViewModels
{
    public class Python_NET_MethodViewModel : BindableBase
    {
        public IContainerProvider Container;
        public Python_NET_MethodViewModel(IContainerProvider container)
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImageCommand);
            OCRCommand = new DelegateCommand(async () => await ExecuteOCRCommand());
            //OCRCommand = new DelegateCommand(ExecuteOCRCommand2);
            Container = container;
            PaddleOCRSettingsViewModel = container.Resolve<PaddleOCRSettingsViewModel>();

            if (PaddleOCRSettingsViewModel.PythonDLLPath == null || PaddleOCRSettingsViewModel.PythonHomePath == null || PaddleOCRSettingsViewModel.PythonPath == null)
            {
                return;
            }

            Runtime.PythonDLL = PaddleOCRSettingsViewModel.PythonDLLPath;

            PythonEngine.PythonHome = PaddleOCRSettingsViewModel.PythonHomePath;
            PythonEngine.PythonPath = PaddleOCRSettingsViewModel.PythonPath;
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
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

                if (PaddleOCRSettingsViewModel.PythonDLLPath == null || PaddleOCRSettingsViewModel.PythonHomePath == null || PaddleOCRSettingsViewModel.PythonPath == null)
                {
                    return;
                }

                try
                {
                    using (Py.GIL())
                    {
                        dynamic example = Py.Import("test4");
                        //var mod = PyModule.Import("test4");
                        if (SelectedFilePath == null)
                        {
                            return;
                        }
                        string image_path = SelectedFilePath;
                        string selected_language = selectedLanguage;
                        string result = example.use_paddleocr(image_path, selected_language);
                        OCRText = result;
                    }
                }
                catch (Exception ex)
                {
                    // 处理其他异常
                    Console.WriteLine($"General exception: {ex.Message}");
                }
            });
        }

        private void ExecuteOCRCommand2()
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

            if (PaddleOCRSettingsViewModel.PythonDLLPath == null || PaddleOCRSettingsViewModel.PythonHomePath == null || PaddleOCRSettingsViewModel.PythonPath == null)
            {
                return;
            }

            try
            {
                using (Py.GIL())
                {
                    dynamic example = Py.Import("test4");

                    if (SelectedFilePath == null)
                    {
                        return;
                    }

                    string image_path = SelectedFilePath;
                    string selected_language = selectedLanguage;
                    string result = example.use_paddleocr(image_path, selected_language);
                    OCRText = result;
                }
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"General exception: {ex.Message}");
            }
        }
        private void test()
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

            if (PaddleOCRSettingsViewModel.PythonDLLPath == null || PaddleOCRSettingsViewModel.PythonHomePath == null || PaddleOCRSettingsViewModel.PythonPath == null)
            {
                return;
            }

            PythonEngine.PythonHome = PaddleOCRSettingsViewModel.PythonHomePath;
            PythonEngine.PythonPath = PaddleOCRSettingsViewModel.PythonPath;
            PythonEngine.Initialize();

            try
            {
                using (Py.GIL())
                {
                    dynamic example = Py.Import("test4");
                    //var mod = PyModule.Import("test4");
                    if (SelectedFilePath == null)
                    {
                        return;
                    }
                    string image_path = SelectedFilePath;
                    string selected_language = selectedLanguage;
                    //PythonEngine.Exec($"use_paddleocr({image_path},{selected_language})");
                    string result = example.use_paddleocr(image_path, selected_language);
                    OCRText = result;
                }
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"General exception: {ex.Message}");
            }
        }
    }
}
