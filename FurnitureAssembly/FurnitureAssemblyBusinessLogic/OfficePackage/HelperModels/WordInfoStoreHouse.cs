using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels
{
   public class WordInfoStoreHouse
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<StorehouseViewModel> StoreHouses { get; set; }
    }
}
