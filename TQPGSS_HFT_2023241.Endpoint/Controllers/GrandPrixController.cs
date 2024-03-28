using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using TQPGSS_HFT_2023241.Endpoint.Services;
using TQPGSS_HFT_2023241.Logic;
using TQPGSS_HFT_2023241.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TQPGSS_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GrandPrixController : ControllerBase
    {
        IGrandPrixLogic logic;
        IHubContext<SignalRHub> hub;
        public GrandPrixController(IGrandPrixLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<GrandPrixController>
        [HttpGet]
        public IEnumerable<GrandPrix> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<GrandPrixController>/5
        [HttpGet("{id}")]
        public GrandPrix Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<GrandPrixController>
        [HttpPost]
        public void Create([FromBody] GrandPrix value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("GrandPrixCreated", value);
        }

        // PUT api/<GrandPrixController>/5
        [HttpPut]
        public void Update([FromBody] GrandPrix value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GrandPrixUpdated", value);

        }

        // DELETE api/<GrandPrixController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GrandPrixDeleted", toDelete);

        }
    }
}
