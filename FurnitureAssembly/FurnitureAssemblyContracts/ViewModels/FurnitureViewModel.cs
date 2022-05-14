using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using FurnitureAssemblyContracts.Attributes;

namespace FurnitureAssemblyContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    [DataContract]
    public class FurnitureViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [DisplayName("Мебель")]
        [Column(title: "Название мебели", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FurnitureName { get; set; }
        [DisplayName("Цена")]
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> FurnitureComponents { get; set; }
    }
}
