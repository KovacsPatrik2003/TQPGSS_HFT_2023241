using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand driverWindowCommand { get; set; }
        public ICommand teamWindowCommand { get; set; }
        public ICommand grandPrixWindowCommand { get; set; }


        public MainWindowViewModel()
        {
            driverWindowCommand = new RelayCommand(() => new DriverWindow().Show());
            teamWindowCommand = new RelayCommand(() => new TeamWindow().Show());
            grandPrixWindowCommand = new RelayCommand(() => new GrandPrixWindow().Show());
        }

    }
}
