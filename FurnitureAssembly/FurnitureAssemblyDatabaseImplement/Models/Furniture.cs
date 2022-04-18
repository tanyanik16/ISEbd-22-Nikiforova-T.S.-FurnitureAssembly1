using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Models
{
   public class Furniture
    {
        public int Id { get; set; }

        [Required]
        public string FurnitureName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("FurnitureId")]
        public virtual List<FurnitureComponent> FurnitureComponents { get; set; }
        [ForeignKey("FurnitureId")]
        public virtual List<Order> Orders { get; set; }
    }
}
