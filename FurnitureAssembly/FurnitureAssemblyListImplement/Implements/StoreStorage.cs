using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyListImplement.Models;

namespace FurnitureAssemblyListImplement.Implements
{
    public class StorehouseStorage : IStorehouseStorage
    {
        private readonly DataListSingleton source;
        public StorehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StorehouseViewModel> GetFullList()
        {
            List<StorehouseViewModel> result = new List<StorehouseViewModel>();
            foreach (var storehouse in source.Storehouses)
            {
                result.Add(CreateModel(storehouse));
            }
            return result;
        }
        public List<StorehouseViewModel> GetFilteredList(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<StorehouseViewModel> result = new List<StorehouseViewModel>();
            foreach (var storehouse in source.Storehouses)
            {
                if (storehouse.StorehouseName.Contains(model.StorehouseName))
                {
                    result.Add(CreateModel(storehouse));
                }
            }
            return result;
        }
        public StorehouseViewModel GetElement(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var storehouse in source.Storehouses)
            {
                if (storehouse.Id == model.Id || storehouse.StorehouseName == model.StorehouseName)
                {
                    return CreateModel(storehouse);
                }
            }
            return null;
        }
        public void Insert(StorehouseBindingModel model)
        {
            var tempStorehouse = new Storehouse
            {
                Id = 1,
                StorehouseComponents = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            foreach (var storehouse in source.Storehouses)
            {
                if (storehouse.Id >= tempStorehouse.Id)
                {
                    tempStorehouse.Id = storehouse.Id + 1;
                }
            }
            source.Storehouses.Add(CreateModel(model, tempStorehouse));
        }
        public void Update(StorehouseBindingModel model)
        {
            Storehouse tempStorehouse = null;
            foreach (var storehouse in source.Storehouses)
            {
                if (storehouse.Id == model.Id)
                {
                    tempStorehouse = storehouse;
                }
            }
            if (tempStorehouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempStorehouse);
        }
        public void Delete(StorehouseBindingModel model)
        {
            for (int i = 0; i < source.Storehouses.Count; ++i)
            {
                if (source.Storehouses[i].Id == model.Id)
                {
                    source.Storehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Storehouse CreateModel(StorehouseBindingModel model, Storehouse storehouse)
        {
            storehouse.StorehouseName = model.StorehouseName;
            storehouse.ResponsiblePerson = model.ResponsiblePerson;
            storehouse.DateCreate = model.DateCreate;
            // удаляем убранные
            foreach (var key in storehouse.StorehouseComponents.Keys.ToList())
            {
                if (!model.StorehouseComponents.ContainsKey(key))
                {
                    storehouse.StorehouseComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.StorehouseComponents)
            {
                if (storehouse.StorehouseComponents.ContainsKey(component.Key))
                {
                    storehouse.StorehouseComponents[component.Key] = model.StorehouseComponents[component.Key].Item2;
                }
                else
                {
                    storehouse.StorehouseComponents.Add(component.Key, model.StorehouseComponents[component.Key].Item2);
                }
            }
            return storehouse;
        }
        private StorehouseViewModel CreateModel(Storehouse storehouse)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> storehouseComponents = new Dictionary<int, (string, int)>();
            foreach (var wc in storehouse.StorehouseComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (wc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                storehouseComponents.Add(wc.Key, (componentName, wc.Value));
            }
            return new StorehouseViewModel
            {
                Id = storehouse.Id,
                StorehouseName = storehouse.StorehouseName,
                ResponsiblePerson = storehouse.ResponsiblePerson,
                DateCreate = storehouse.DateCreate,
                StorehouseComponents = storehouseComponents
            };
        }

    }
}
