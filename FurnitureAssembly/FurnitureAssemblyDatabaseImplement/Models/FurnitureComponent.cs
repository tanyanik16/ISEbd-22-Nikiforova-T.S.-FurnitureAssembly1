using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FurnitureAssemblyDatabaseImplement.Models
{
     public class FurnitureComponent
    {
        public int Id { get; set; }
        public int FurnitureId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Furniture Furniture { get; set; }
    }

}
