using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BusinessLogicsContracts
{
   public interface IStorehouseLogic
    {
        List<StorehouseViewModel> Read(StorehouseBindingModel model);
        void CreateOrUpdate(StorehouseBindingModel model);
        void Delete(StorehouseBindingModel model);
        void Replenishment(StorehouseReplenishmentBindingModel model);
    }
}
