using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriaRD2;
using System.ComponentModel.DataAnnotations;
namespace LAB1_ED2_DiegoRamírez_DanielElias.Models
{
    public class Values : IComparable
    {
        int id { get; set; }
        public int CompareTo(object obj)
        {
            var ordertree = ((Values)obj).id;
            return ordertree.CompareTo(id);
        }
    }
}
