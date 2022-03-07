using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;

namespace FurnitureAssemblyContracts.StoragesContracts
{
   public interface IStorehouseStorage
    {
        List<StorehouseViewModel> GetFullList();
        List<StorehouseViewModel> GetFilteredList(StorehouseBindingModel model);
        StorehouseViewModel GetElement(StorehouseBindingModel model);
        void Insert(StorehouseBindingModel model);
        void Update(StorehouseBindingModel model);
        void Delete(StorehouseBindingModel model);
    }
}
