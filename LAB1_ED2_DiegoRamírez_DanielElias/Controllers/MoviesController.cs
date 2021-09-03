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
using LibreriaRD2;

//using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LAB1_ED2_DiegoRamírez_DanielElias.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        
        public static bool ordenSeteado = false;
        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MoviesController>/5
        [HttpGet("{recorrido}")]
        public string Get([FromRoute]string recorrido)
        {
            string returnJson = "";
            if (recorrido == "preorder")
            {
                Singleton.Instance.bTree.traversal.Clear();
                Singleton.Instance.bTree.preorden(Singleton.Instance.bTree.root);
                for (int i = 0; i < Singleton.Instance.bTree.traversal.Count; i++)
                {
                    returnJson += JsonSerializer.Serialize<Movies>(Singleton.Instance.bTree.traversal.ElementAt(i));
                }
                
            }
            else if (recorrido == "inorder")
            {
                Singleton.Instance.bTree.traversal.Clear();
                Singleton.Instance.bTree.inorder(Singleton.Instance.bTree.root);
                for (int i = 0; i < Singleton.Instance.bTree.traversal.Count; i++)
                {
                    returnJson += JsonSerializer.Serialize<Movies>(Singleton.Instance.bTree.traversal.ElementAt(i));
                }
            }
            else if (recorrido == "postorder")
            {
                Singleton.Instance.bTree.traversal.Clear();
                Singleton.Instance.bTree.postorden(Singleton.Instance.bTree.root);
                for (int i = 0; i < Singleton.Instance.bTree.traversal.Count; i++)
                {
                    returnJson += JsonSerializer.Serialize<Movies>(Singleton.Instance.bTree.traversal.ElementAt(i));
                }
            }
            else
            {
                return "Recorrido no válido";
                
            }

           
            return returnJson;
        }

        // POST api/<MoviesController>
        [HttpPost]
        public ActionResult SetGrado([FromBody] int grado)
        {
           
            try
            {

                Singleton.Instance.Grado = grado;
                ordenSeteado = true;
                Singleton.Instance.bTree = null;
                Singleton.Instance.bTree = new BTree<Movies>(grado);
               
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
            Singleton.Instance.MoviesList.Clear();
            try
            {

                if (ordenSeteado == false)
                {
                    return BadRequest();
                }


             

                Singleton.Instance.MoviesList = JsonSerializer.Deserialize<List<Movies>>(json.GetRawText().ToString());

                for (int i = 0; i < Singleton.Instance.MoviesList.Count; i++)
                {
                    Singleton.Instance.bTree.insert(Singleton.Instance.MoviesList.ElementAt(i));
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("populate/{title}")]
        public ActionResult DeleteNode(string title)
        {

         

            var aux = new Movies();
            aux.title = title;
            try
            {
                if (Singleton.Instance.bTree.searchbydata(Singleton.Instance.bTree.root, aux, null).title == null)
                {
                    return BadRequest();
                }
                else
                {
                    Singleton.Instance.bTree.delete(Singleton.Instance.bTree.searchbydata(Singleton.Instance.bTree.root, aux, null));
                    return Ok();
                }

            }
            catch
            {
                return BadRequest();
            }
           
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete]
        public void Delete()
        {
            Singleton.Instance.bTree = null;
        }

        

    }
}
