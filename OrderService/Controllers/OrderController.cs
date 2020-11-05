using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderService.Models;
using OrderService.PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProducerConfig config;
        public OrderController(ProducerConfig config)
        {
            this.config = config;
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> PostAync([FromForm] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string serializedOrder = JsonConvert.SerializeObject(order);
            var producer = new ProducherWrapper(config, "orders");
            await producer.WriteMEssage(serializedOrder);

            return Created("TxnID", "Order is processing...");
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
