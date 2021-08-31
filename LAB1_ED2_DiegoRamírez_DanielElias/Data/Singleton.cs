using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB1_ED2_DiegoRamírez_DanielElias.Models;

namespace LAB1_ED2_DiegoRamírez_DanielElias.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Movies> MoviesList { get; set; }
        public int Grado { get; set; }
        private Singleton()
        {
            MoviesList = new List<Movies>();
            Grado = 0;

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
