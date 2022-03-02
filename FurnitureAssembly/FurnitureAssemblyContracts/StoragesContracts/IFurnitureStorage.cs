using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;

namespace FurnitureAssemblyContracts.StoragesContracts
{
    public interface IFurnitureStorage
    {
        List<FurnitureViewModel> GetFullList();
        List<FurnitureViewModel> GetFilteredList(FurnitureBindingModel model);
        FurnitureViewModel GetElement(FurnitureBindingModel model);
        void Insert(FurnitureBindingModel model);
        void Update(FurnitureBindingModel model);
        void Delete(FurnitureBindingModel model);
    }
}
