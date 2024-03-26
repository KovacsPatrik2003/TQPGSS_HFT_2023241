using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.Wpfclient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class DriverWindowViewModel : ObservableRecipient
    {
        public RestCollection<Driver> Drivers { get; set; }


        public ICommand CreateDriverCommand { get; set; }
        public ICommand DeleteDriverCommand { get; set; }
        public ICommand UpdateDriverCommand { get; set; }

        private Driver selectedDriver;

        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set
            {
                SetProperty(ref selectedDriver, value);
                (DeleteDriverCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public DriverWindowViewModel()
        {
            Drivers = new RestCollection<Driver>("http://localhost:18928/", "driver");

            CreateDriverCommand = new RelayCommand(() =>
            {
                Drivers.Add(new Driver()
                {
                    Name = "Kovacs Patrik"
                });
            });
            DeleteDriverCommand = new RelayCommand(() => Drivers.Delete(SelectedDriver.Id), () => { return selectedDriver != null; });
        }
    }
}
