using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using TQPGSS_HFT_2023241.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TQPGSS_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TeamStatController : ControllerBase
    {
        ITeamLogic logic;
        public TeamStatController(ITeamLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet]
        public IEnumerable TeamsDrivers(string name)
        {
            return this.logic.teamsDrivers(name);
        }
        [HttpGet]
        public IEnumerable AvaragePointsPerGrandPrixByTeams()
        {
            return this.logic.avaragePointsPerGrandPrix();
        }
        [HttpGet]
        public IEnumerable FirstAndSecondDriverByPoints()
        {
            return this.logic.firstAndSecondDriverByPoints();
        }
    }
}
