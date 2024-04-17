using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.Wpfclient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class WinnerClass : ObservableObject
    {
        public string Name { get; set; }
    }
    public class CircuitClass : ObservableObject
    {
        //id name date winner
        public int id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string winner { get; set; }

    }

    public class GrandPrixWindowViewModel : ObservableRecipient
    {
        public RestCollection<GrandPrix> GrandPrixs { get; set; }
        public RestService NonCrud { get; set; }
        public ObservableCollection<CircuitClass> CircuitNonCrud { get; set; }
        public ObservableCollection<WinnerClass> WinnerNonCrud { get; set; }

        public ICommand CreateGrandPrixCommand { get; set; }
        public ICommand DeleteGrandPrixCommand { get; set; }
        public ICommand UpdateGrandPrixCommand { get; set; }
        public ICommand VisszaCommand { get; set; }
        public ICommand WinnerOfTheCircuitCommand { get; set; }
        public ICommand CircuitDetalisCommand { get; set; }

        private GrandPrix selectedGrandPrix;

        public GrandPrix SelectedGrandPrix
        {
            get { return selectedGrandPrix; }
            set
            {
                //if (value != null)
                //{
                //    selectedGrandPrix = new GrandPrix()
                //    {
                //        Name = value.Name,
                //        Date = value.Date,
                //        WhoWon = value.WhoWon,
                //    };
                //    OnPropertyChanged();
                //    (DeleteGrandPrixCommand as RelayCommand).NotifyCanExecuteChanged();
                //}
                SetProperty(ref selectedGrandPrix, value);
                (DeleteGrandPrixCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public GrandPrixWindowViewModel()
        {
            
            if (!IsInDesignMode)
            {
                GrandPrixs = new RestCollection<GrandPrix>("http://localhost:18928/", "grandprix", "hub");
                NonCrud = new RestService("http://localhost:18928/");
                CircuitNonCrud = new ObservableCollection<CircuitClass>();
                WinnerNonCrud = new ObservableCollection<WinnerClass>();
                CreateGrandPrixCommand = new RelayCommand(() =>
                {
                    
                    GrandPrixs.Add(new GrandPrix()
                    {
                        Name = SelectedGrandPrix.Name,
                        Date = SelectedGrandPrix.Date,
                        WhoWon = SelectedGrandPrix.WhoWon
                    });
                });

                DeleteGrandPrixCommand = new RelayCommand(() => GrandPrixs.Delete(SelectedGrandPrix.Id), () => { return SelectedGrandPrix != null; });

                UpdateGrandPrixCommand = new RelayCommand(() => GrandPrixs.Update(SelectedGrandPrix));

                WinnerOfTheCircuitCommand = new RelayCommand(
                () =>
                {
                    var a = NonCrud.Get<string>($"GrandPrixStat/WinnerOfTheCircuit/{SelectedGrandPrix.Name}");

                    foreach (var item in a)
                    {
                        WinnerClass winner = new WinnerClass();
                        winner.Name = item;
                        WinnerNonCrud.Add(winner);
                    }
                }, () => { return SelectedGrandPrix != null; }
                );
                CircuitDetalisCommand = new RelayCommand(
                    () =>
                    {
                        var a = NonCrud.Get<CircuitClass>($"GrandPrixStat/GrandPrixDetails/{SelectedGrandPrix.Name}");
                        foreach (var item in a)
                        {

                            CircuitNonCrud.Add(item);
                        }
                    }, () => { return SelectedGrandPrix != null; }
                    );
                VisszaCommand = new RelayCommand(
                    () =>
                    {
                        while (WinnerNonCrud.Count() != 0)
                        {
                            WinnerNonCrud.Remove(WinnerNonCrud[0]);
                        }
                        while (CircuitNonCrud.Count() != 0)
                        {
                            CircuitNonCrud.Remove(CircuitNonCrud[0]);
                        }
                    }
                    );
                SelectedGrandPrix = new GrandPrix();
            }
            
            
        }

    }
}
