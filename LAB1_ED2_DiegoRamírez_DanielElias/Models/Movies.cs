using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB1_ED2_DiegoRamírez_DanielElias.Models
{
    public class Movies : IComparable
    {
        public string director { get; set; }

        public double imdbRating { get; set; }

        public string genre { get; set; }

        public string releaseDate { get; set; }

        public double rottenTomatoesRating { get; set; }
        public string title { get; set; }
        
        
       
        
        



        public int CompareTo(object obj)
        {
            var ordertree = ((Movies)obj).title;
            return title.CompareTo(ordertree);
        }

    }
}
