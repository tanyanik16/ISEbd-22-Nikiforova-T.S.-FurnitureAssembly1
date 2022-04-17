using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;


namespace FurnitureAssemblyContracts.StoragesContracts
{
    public interface IImplementerStorage
    {
        List<ImplementerViewModel> GetFullList();
        List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model);
        ImplementerViewModel GetElement(ImplementerBindingModel model);
        void Insert(ImplementerBindingModel model);
        void Update(ImplementerBindingModel model);
        void Delete(ImplementerBindingModel model);
    }
}
