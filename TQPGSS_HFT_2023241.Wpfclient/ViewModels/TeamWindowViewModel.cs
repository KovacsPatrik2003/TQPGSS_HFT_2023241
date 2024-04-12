using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.Wpfclient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class TeamsDriverClass
    {
        public string Names { get; set; }
    }
    public class AvaragePointTeamClass
    {
        public double Points { get; set; }
        public string TeamName { get; set; }
    }
    public class FirstAndSecondClass
    {
        public string name { get; set; }
        public int driver1 { get; set; }
        public int driver2 { get; set; }
        public int driver1Points { get; set; }
        public int driver2Points { get; set; }
        public int first { get; set; }
        public int second { get; set; }
        public string driver1name { get; set; }
        public string driver2name { get; set; }

    }
    public class TeamWindowViewModel : ObservableRecipient
    {
        public RestCollection<Team> Teams { get; set; }
        public RestCollection<Driver> Drivers { get; set; }
        public RestService NonCrud { get; set; }
        public ObservableCollection<TeamsDriverClass> TeamsDriversNonCrud { get; set; }
        public ObservableCollection<AvaragePointTeamClass> AvaragesPointsNonCrud { get; set; }
        public ObservableCollection<FirstAndSecondClass> FirstAndSecondNonCrud { get; set; }


        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }
        public ICommand TeamsDriversTeamCommand { get; set; }
        public ICommand AvargePointsPerGrandPrixTeamCommand { get; set; }
        public ICommand FirstAndSecondDriverTeamCommand { get; set; }
        public ICommand VisszaTeamCommand { get; set; }


        private Team selectedTeam;

        public Team SelectedTeam
		{
			get { return selectedTeam; }
			set
			{

                //if (value != null)
                //{
                //    selectedTeam = new Team()
                //    {
                //        Name = value.Name,
                //        Points = value.Points,
                //        Principal = value.Principal,
                //    };
                //    OnPropertyChanged();
                //    (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                //}
                SetProperty(ref selectedTeam, value);
                (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public TeamWindowViewModel()
		{
            if (!IsInDesignMode)
            {
                
                Teams = new RestCollection<Team>("http://localhost:18928/", "team", "hub");
                Drivers = new RestCollection<Driver>("http://localhost:18928/", "driver", "hub");
                NonCrud = new RestService("http://localhost:18928/");
                TeamsDriversNonCrud = new ObservableCollection<TeamsDriverClass>();
                AvaragesPointsNonCrud = new ObservableCollection<AvaragePointTeamClass>();
                FirstAndSecondNonCrud = new ObservableCollection<FirstAndSecondClass>();

                CreateTeamCommand = new RelayCommand(() => {

                    Teams.Add(new Team()
                    {
                        Name = SelectedTeam.Name,
                        Points = SelectedTeam.Points,
                        Principal = SelectedTeam.Principal
                    });
                });
                DeleteTeamCommand = new RelayCommand( () => {Teams.Delete(SelectedTeam.Id); }, () => { return SelectedTeam != null; });
                SelectedTeam = new Team();
                UpdateTeamCommand = new RelayCommand(() => Teams.Update(SelectedTeam));

                TeamsDriversTeamCommand = new RelayCommand(
                    () =>
                    {
                        var a = NonCrud.Get<string>($"TeamStat/TeamsDrivers/{SelectedTeam.Name}");
                        foreach (var item in a)
                        {
                            TeamsDriverClass firstAndSecondClass = new TeamsDriverClass();
                            firstAndSecondClass.Names = item;
                            TeamsDriversNonCrud.Add(firstAndSecondClass);
                        }
                    }, () => { return SelectedTeam != null; }
                    );
                AvargePointsPerGrandPrixTeamCommand = new RelayCommand(
                    () =>
                    {
                        var a = NonCrud.Get<double>("TeamStat/AvaragePointsPerGrandPrixByTeams");
                        List<string> list = new List<string>();
                        foreach (var item in Teams)
                        {
                            list.Add(item.Name);
                        }
                        int i = 0;
                        foreach (var item in a)
                        {
                            AvaragePointTeamClass avaragePointClass = new AvaragePointTeamClass();
                            avaragePointClass.Points = item;
                            avaragePointClass.TeamName = list[i];
                            AvaragesPointsNonCrud.Add(avaragePointClass);
                            i++;
                        }
                    }
                    );
                FirstAndSecondDriverTeamCommand = new RelayCommand(
                    () =>
                    {
                        var a = NonCrud.Get<FirstAndSecondClass>("TeamStat/FirstAndSecondDriverByPoints");
                        
                        int i = 0;
                        List<string> name = new List<string>();
                        foreach (var item in Teams)
                        {
                            name.Add(item.Name);
                        }
                        List<Driver> drivers = new List<Driver>();
                        foreach (var item in Drivers)
                        {
                            drivers.Add(item);
                        }
                        foreach (var item in name)
                        {
                            FirstAndSecondClass f = new FirstAndSecondClass();
                            f = a[i];
                            f.name = item;
                            int j = 0;
                            bool find1 = false;
                            bool find2 = false;
                            while (j<drivers.Count() && (!find1 || !find2))
                            {
                                if (f.driver1 == drivers[j].Id)
                                {
                                    f.driver1name = drivers[j].Name;
                                    find1 = true;
                                }
                                if (f.driver2== drivers[j].Id)
                                {
                                    f.driver2name = drivers[j].Name;
                                    find2=true;
                                }
                                j++;
                            }
                            if (f.driver1Points < f.driver2Points)
                            {
                                ;
                                FirstAndSecondClass seged = new FirstAndSecondClass();
                                seged = f;
                                int p1 = f.driver1Points;
                                int p2 = f.driver2Points;
                                string n1=f.driver1name;
                                string n2 = f.driver2name;
                                seged.driver1Points = p2;
                                seged.driver1name = n2;
                                seged.driver2Points = p1;
                                seged.driver2name = n1;

                                FirstAndSecondNonCrud.Add(seged);
                            }
                            else
                            {
                                FirstAndSecondNonCrud.Add(f);
                            }

                            i++;
                        }
                        
                        
                    }
                    );
                VisszaTeamCommand = new RelayCommand(
                    () =>
                    {
                        while (TeamsDriversNonCrud.Count() != 0)
                        {
                            TeamsDriversNonCrud.Remove(TeamsDriversNonCrud[0]);
                        }
                        while (AvaragesPointsNonCrud.Count() != 0)
                        {
                            AvaragesPointsNonCrud.Remove(AvaragesPointsNonCrud[0]);
                        }
                        while (FirstAndSecondNonCrud.Count() != 0)
                        {
                            FirstAndSecondNonCrud.Remove(FirstAndSecondNonCrud[0]);
                        }
                    }
                    );
                
            }
            SelectedTeam = new Team();

        }

	}
}
