using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Xls2Pdf.Domain;
using Xls2Pdf.Infrastructure;

namespace Xls2Pdf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ExcelFileInfos> FilesToConvertToPdf { get; private set; }

        private readonly IDialogsManager _dialogManager;
        private readonly IExcelToPdfConverter _excelToPdfConverter;

        public ICommand SelectFilesToConvertToPdfCommand { get; private set; }
        public ICommand ConvertToPdfCommand { get; private set; }
        public ICommand RestartCommand { get; private set; }

        public MainWindowViewModel(
           IDialogsManager dialogsManager,
           IExcelToPdfConverter excelToPdfConverter)
        {
            FilesToConvertToPdf = new ObservableCollection<ExcelFileInfos>();
            _dialogManager = dialogsManager ?? throw new ArgumentNullException(nameof(dialogsManager));
            _excelToPdfConverter = excelToPdfConverter ?? throw new ArgumentNullException(nameof(excelToPdfConverter));

            SelectFilesToConvertToPdfCommand = new RelayCommand(
                () =>
                {
                    var filesToConvertToPdf = _dialogManager.SelectExcelFilesToConvertToPdf();
                    if (filesToConvertToPdf == null)
                        return;

                    foreach (var filePath in filesToConvertToPdf)
                        FilesToConvertToPdf.Add(new ExcelFileInfos(filePath));
                },
                () => ProgressBarVisibility != Visibility.Visible);

            ConvertToPdfCommand = new RelayCommand(
                async () =>
                {
                    try
                    {
                        ProgressBarVisibility = Visibility.Visible;
                        await _excelToPdfConverter.ConvertToPdf(FilesToConvertToPdf);
                        _dialogManager.ShowInformation("Operation completed");
                    }
                    catch (Exception ex)
                    {
                        _dialogManager.ShowError(ex.Message);
                    }
                    finally
                    {
                        ProgressBarVisibility = Visibility.Hidden;
                    }
                },
                () => FilesToConvertToPdf.Any() && ProgressBarVisibility != Visibility.Visible);

            RestartCommand = new RelayCommand(
            () =>
            {
                FilesToConvertToPdf.Clear();
                ProgressBarVisibility = Visibility.Collapsed;
            },
            () => FilesToConvertToPdf.Any() && ProgressBarVisibility != Visibility.Visible);
        }

        private Visibility _progressBarVisibility = Visibility.Collapsed;
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                if (_progressBarVisibility == value)
                    return;

                _progressBarVisibility = value;
                RaisePropertyChanged(nameof(ProgressBarVisibility));
            }
        }
    }
}
