using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;

namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
    public class StorehouseLogic : IStorehouseLogic
    {
        private readonly IStorehouseStorage _storehouseStorage;
        private readonly IComponentStorage _componentStorage;

        public StorehouseLogic(IStorehouseStorage storehouseStorage, IComponentStorage componentStorage)
        {
            _storehouseStorage = storehouseStorage;
            _componentStorage = componentStorage;
        }
        public List<StorehouseViewModel> Read(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return _storehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StorehouseViewModel> { _storehouseStorage.GetElement(model) };
            }
            return _storehouseStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StorehouseBindingModel model)
        {
            var element = _storehouseStorage.GetElement(new StorehouseBindingModel
            {
                StorehouseName = model.StorehouseName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Склад с таким названием уже есть");
            }
            if (model.Id.HasValue)
            {
                _storehouseStorage.Update(model);
            }
            else
            {
                _storehouseStorage.Insert(model);
            }
        }
        public void Delete(StorehouseBindingModel model)
        {
            var element = _storehouseStorage.GetElement(new StorehouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Склад не найден");
            }
            _storehouseStorage.Delete(model);
        }
        public void Replenishment(StorehouseReplenishmentBindingModel model)
        {
            var storehouse = _storehouseStorage.GetElement(new StorehouseBindingModel
            {
                Id = model.StorehouseId
            });
            if (storehouse == null)
            {
                throw new Exception("Склад не найден");
            }
            var component = _componentStorage.GetElement(new ComponentBindingModel
            {
                Id = model.ComponentId
            });
            if (component == null)
            {
                throw new Exception("Компонент не найден");
            }
            if (storehouse.StorehouseComponents.ContainsKey(model.ComponentId))
            {
                storehouse.StorehouseComponents[model.ComponentId] =
                (component.ComponentName, storehouse.StorehouseComponents[model.ComponentId].Item2 + model.Count);
            }
            else
            {
                storehouse.StorehouseComponents.Add(component.Id, (component.ComponentName, model.Count));
            }
            _storehouseStorage.Update(new StorehouseBindingModel
            {
                Id = storehouse.Id,
                StorehouseName = storehouse.StorehouseName,
                ResponsiblePerson = storehouse.ResponsiblePerson,
                DateCreate = storehouse.DateCreate,
                StorehouseComponents = storehouse.StorehouseComponents
            });
        }
    }
   
}

