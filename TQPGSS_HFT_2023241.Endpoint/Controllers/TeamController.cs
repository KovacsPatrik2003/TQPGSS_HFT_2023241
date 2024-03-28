using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TQPGSS_HFT_2023241.Endpoint.Services;
using TQPGSS_HFT_2023241.Logic;
using TQPGSS_HFT_2023241.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TQPGSS_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamLogic logic;
        IHubContext<SignalRHub> hub;

        public TeamController(ITeamLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<TeamController>
        [HttpGet]
        public IEnumerable<Team> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public Team Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<TeamController>
        [HttpPost]
        public void Create([FromBody] Team value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("TeamCreated", value);

        }

        // PUT api/<TeamController>/5
        [HttpPut]
        public void Update([FromBody] Team value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("TeamUpdated", value);

        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("TeamDeleted", toDelete);

        }
    }
}
