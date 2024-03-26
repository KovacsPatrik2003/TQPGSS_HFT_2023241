using MovieDbApp.Wpfclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Wpfclient.ViewModels
{
    public class DriverWindowViewModel
    {
        public RestCollection<Driver> Drivers { get; set; }
        public DriverWindowViewModel()
        {
            Drivers = new RestCollection<Driver>("http://localhost:18928/", "driver");
        }
    }
}
