using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.Wpfclient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class GrandPrixWindowViewModel : ObservableRecipient
    {
        public RestCollection<GrandPrix> GrandPrixs { get; set; }

        public ICommand CreateGrandPrixCommand { get; set; }
        public ICommand DeleteGrandPrixCommand { get; set; }
        public ICommand UpdateGrandPrixCommand { get; set; }

        private GrandPrix selectedGrandPrix;

        public GrandPrix SelectedGrandPrix
        {
            get { return selectedGrandPrix; }
            set
            {
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
                GrandPrixs = new RestCollection<GrandPrix>("http://localhost:18928/", "grandprix");

                CreateGrandPrixCommand = new RelayCommand(() =>
                {
                    GrandPrixs.Add(new GrandPrix()
                    {
                        Name = "Ajak",
                        Date = "Jan 1",
                        WhoWon = 1
                    });
                });

                DeleteGrandPrixCommand = new RelayCommand(() => GrandPrixs.Delete(SelectedGrandPrix.Id), () => { return selectedGrandPrix != null; });
            }

        }

    }
}
