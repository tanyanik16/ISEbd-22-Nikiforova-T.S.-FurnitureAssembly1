using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyListImplement.Models
{
     public class Storehouse
    {
        public int Id { get; set; }

        public string StorehouseName { get; set; }

        public string ResponsiblePerson { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> StorehouseComponents { get; set; }
    }
}
