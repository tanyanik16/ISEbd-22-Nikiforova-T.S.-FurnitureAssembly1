using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using System.Threading.Tasks;

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
        bool WriteOffFromStorehouses(Dictionary<int, (string, int)> components, int writeOffCount);
    }
}
