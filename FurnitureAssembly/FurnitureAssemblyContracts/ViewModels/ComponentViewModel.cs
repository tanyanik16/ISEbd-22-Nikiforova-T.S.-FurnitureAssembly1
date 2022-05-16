using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.Attributes;

namespace FurnitureAssemblyContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        [Column(title: "Наименование компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }
}
