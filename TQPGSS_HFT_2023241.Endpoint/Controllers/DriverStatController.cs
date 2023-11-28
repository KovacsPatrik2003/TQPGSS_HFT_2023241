using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using TQPGSS_HFT_2023241.Logic;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TQPGSS_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DriverStatController : ControllerBase
    {
        IDriverLogic logic;
        public DriverStatController(IDriverLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet("{name}")]
        public IEnumerable DriverWins(string name)
        {
            return this.logic.driverWins(name);
        }
        [HttpGet]
        public IEnumerable AvaragePointPerGrandPrixByDrivers()
        {
            return this.logic.avaragePointPerGrandPrix();
        }
        [HttpGet]
        public IEnumerable WhoWonTheMost()
        {
            return this.logic.whoWonTheMost();
        }
    }
}
