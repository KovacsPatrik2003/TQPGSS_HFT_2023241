﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.Wpfclient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class TeamWindowViewModel : ObservableRecipient
    {
        public RestCollection<Team> Teams { get; set; }

		private Team selectedTeam;

        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }
        public ICommand TeamsDriversTeamCommand { get; set; }
        public ICommand AvargePointsPerGrandPrixTeamCommand { get; set; }
        public ICommand FirstAndSecondDriverTeamCommand { get; set; }
        public ICommand VisszaTeamCommand { get; set; }


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

                CreateTeamCommand = new RelayCommand(() => {
                    
                    Teams.Add(new Team()
                    {
                        Name = SelectedTeam.Name,
                        Points = SelectedTeam.Points,
                        Principal = SelectedTeam.Principal
                    });
                });
                DeleteTeamCommand = new RelayCommand(() => Teams.Delete(SelectedTeam.Id), () => { return selectedTeam != null; });

                UpdateTeamCommand = new RelayCommand(() => Teams.Update(SelectedTeam));
            }
            SelectedTeam = new Team();
        }

	}
}
