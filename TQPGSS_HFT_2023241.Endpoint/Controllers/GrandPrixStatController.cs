using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TQPGSS_HFT_2023241.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TQPGSS_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GrandPrixStatController : ControllerBase
    {
        IGrandPrixLogic logic;
        public GrandPrixStatController(IGrandPrixLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet("{name}")]
        public IEnumerable WinnerOfTheCircuit(string name)
        {
            return this.logic.winnerOfTheCircuit(name);
        }
        [HttpGet("{name}")]
        public IEnumerable GrandPrixDetails(string name)
        {
            return this.logic.grandPrixDetails(name);
        }
        
    }
}
