using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB1_ED2_DiegoRamírez_DanielElias.Models;
using LibreriaRD2;

namespace LAB1_ED2_DiegoRamírez_DanielElias.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Movies> MoviesList { get; set; }
        public BTree<Movies> bTree { get; set; }
        public int Grado { get; set; }
        private Singleton()
        {
            Grado = 0;
            MoviesList = new List<Movies>();

            
           

        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
