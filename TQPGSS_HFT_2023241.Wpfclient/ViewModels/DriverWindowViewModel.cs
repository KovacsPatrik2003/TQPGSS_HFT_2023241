using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.Wpfclient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class AvaragePointClass : ObservableObject
    {
        public double Points { get; set; }
        public string Name { get; set; }

    }
    public class DriverWinsClass : ObservableObject
    {
        public string Circuits { get; set; }  
    }
    public class MostWinsClass : ObservableObject
    {
        public string Name { get; set; }
    }
    public class DriverWindowViewModel : ObservableRecipient
    {
        
        public RestCollection<Driver> Drivers { get; set; }
        public RestService DriversNonCrud { get; set; }
        public ObservableCollection<AvaragePointClass> NonCrudListAvg { get; set; }
        public ObservableCollection<DriverWinsClass> NonCrudListWins { get; set; }
        public ObservableCollection<MostWinsClass> NonCrudListMostwins { get; set; }
        

        public ICommand CreateDriverCommand { get; set; }
        public ICommand DeleteDriverCommand { get; set; }
        public ICommand UpdateDriverCommand { get; set; }
        public ICommand DriverWinsCommand { get; set; }
        public ICommand AvaragePointsPerGrandPrixesDriverCommand { get; set; }
        public ICommand VisszaCommand { get; set; }
        public ICommand WhoWonTheMostCommand { get; set; }

        private Driver selectedDriver;

        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set
            {
                //if (value != null)
                //{
                //    selectedDriver = new Driver()
                //    {
                //        Name = value.Name,
                //        Points = value.Points,
                //    };
                //    OnPropertyChanged();
                //    (DeleteDriverCommand as RelayCommand).NotifyCanExecuteChanged();
                //}
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
            
            if (!IsInDesignMode)
            {
                Drivers = new RestCollection<Driver>("http://localhost:18928/", "driver", "hub");
                DriversNonCrud = new RestService("http://localhost:18928/");
                NonCrudListAvg = new ObservableCollection<AvaragePointClass>();
                NonCrudListWins = new ObservableCollection<DriverWinsClass>();
                NonCrudListMostwins = new ObservableCollection<MostWinsClass>();
                CreateDriverCommand = new RelayCommand(() =>
                {
                    Drivers.Add(new Driver()
                    {
                        Name = SelectedDriver.Name,
                        Points=SelectedDriver.Points
                    });
                });
                DeleteDriverCommand = new RelayCommand(() => Drivers.Delete(SelectedDriver.Id), () => { return SelectedDriver != null; });
                UpdateDriverCommand = new RelayCommand(() => Drivers.Update(SelectedDriver));
                AvaragePointsPerGrandPrixesDriverCommand = new RelayCommand(
                    () => {
                        var a  = DriversNonCrud.Get<double>("DriverStat/AvaragePointPerGrandPrixByDrivers");
                        List<string> list = new List<string>();
                        foreach (var driver in Drivers)
                        {
                            list.Add(driver.Name);
                        }
                        int i = 0;
                        foreach (var item in a)
                        {
                            AvaragePointClass avg = new AvaragePointClass();
                            avg.Points = item;
                            avg.Name = list[i];
                            NonCrudListAvg.Add(avg);
                            i++;
                        }
                        
                        }
                    );
                DriverWinsCommand = new RelayCommand(
                    () =>
                    {
                       
                        
                        var a = DriversNonCrud.Get<string>($"DriverStat/DriverWins/{SelectedDriver.Name}");
                        foreach (var item in a)
                        {
                            DriverWinsClass wins = new DriverWinsClass();
                            wins.Circuits = item;
                            NonCrudListWins.Add(wins);
                        }
                    }, () => { return SelectedDriver != null; }
                    );
                VisszaCommand = new RelayCommand(
                    () =>
                    {
                        
                        while (NonCrudListWins.Count()!=0)
                        {
                            NonCrudListWins.Remove(NonCrudListWins[0]);
                        }
                        while (NonCrudListAvg.Count()!=0)
                        {
                            NonCrudListAvg.Remove(NonCrudListAvg[0]);
                        }
                        while (NonCrudListMostwins.Count() != 0)
                        {
                            NonCrudListMostwins.Remove(NonCrudListMostwins[0]);
                        }
                    }
                    );
                WhoWonTheMostCommand = new RelayCommand(
                    () =>
                    {
                        var a = DriversNonCrud.Get<string>("DriverStat/WhoWonTheMost");
                        
                        foreach (var item in a)
                        {
                            MostWinsClass wins = new MostWinsClass();
                            wins.Name = item;
                            NonCrudListMostwins.Add(wins);
                        }
                    }
                    );
            }
            SelectedDriver=new Driver();
        }
    }
}
