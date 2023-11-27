using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using TQPGSS_HFT_2023241.Logic;
using TQPGSS_HFT_2023241.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TQPGSS_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrandPrixController : ControllerBase
    {
        IGrandPrixLogic logic;
        public GrandPrixController(IGrandPrixLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<GrandPrixController>/5
        [HttpPut]
        public void Update([FromBody] GrandPrix value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<GrandPrixController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
