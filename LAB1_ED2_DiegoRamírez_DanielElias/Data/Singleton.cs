using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB1_ED2_DiegoRamírez_DanielElias.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        private Singleton()
        {

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
