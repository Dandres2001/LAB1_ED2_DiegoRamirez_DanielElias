using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading.Tasks;
using LAB1_ED2_DiegoRamírez_DanielElias.Models;
using LAB1_ED2_DiegoRamírez_DanielElias.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
//using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LAB1_ED2_DiegoRamírez_DanielElias.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class MoviesController : ControllerBase
    {
        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MoviesController>/5
        [HttpGet("{recorrido}")]
        public IEnumerable<Movies> Get([FromRoute]string recorrido)
        {
            if (recorrido == "preorder")
            {

            }
            else if (recorrido == "inorder")
            {

            }
            else if (recorrido == "postorder")
            {

            }
            else
            {
                
            }
            return Singleton.Instance.MoviesList;
        }

        // POST api/<MoviesController>
        [HttpPost]
        public ActionResult SetGrado([FromBody] int grado)
        {
            try
            {
                Singleton.Instance.Grado = grado;
                return Created("", grado);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost("populate")]
        
        public ActionResult PopulateTree([FromBody] JsonElement json) 
        {

            try
            {

                


               

                var movies = JsonSerializer.Deserialize<List<Movies>>(json.GetRawText().ToString());
               
               

    
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
