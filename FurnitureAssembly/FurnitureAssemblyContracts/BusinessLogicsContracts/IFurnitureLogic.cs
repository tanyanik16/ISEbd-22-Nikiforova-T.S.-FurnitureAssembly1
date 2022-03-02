using System;
using System.Collections.Generic;
using System.Text;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;

namespace FurnitureAssemblyContracts.BusinessLogicsContracts
{
    public interface IFurnitureLogic
    {
        List<FurnitureViewModel> Read(FurnitureBindingModel model);
        void CreateOrUpdate(FurnitureBindingModel model);
        void Delete(FurnitureBindingModel model);
    }
}
