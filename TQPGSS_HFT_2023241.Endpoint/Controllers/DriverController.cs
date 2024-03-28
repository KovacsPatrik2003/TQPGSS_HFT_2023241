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
    public class DriverController : ControllerBase
    {
        IDriverLogic logic;
        IHubContext<SignalRHub> hub;
        public DriverController(IDriverLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: api/<DriverController>


        [HttpGet]
        public IEnumerable<Driver> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<DriverController>/5
        [HttpGet("{id}")]
        public Driver Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<DriverController>
        [HttpPost]
        public void Create([FromBody] Driver value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("DriverCreated",value);
        }

        // PUT api/<DriverController>/5
        [HttpPut]
        public void Update([FromBody] Driver value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("DriverUpdated", value);

        }

        // DELETE api/<DriverController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("DriverDeleted", toDelete);

        }
    }
}
