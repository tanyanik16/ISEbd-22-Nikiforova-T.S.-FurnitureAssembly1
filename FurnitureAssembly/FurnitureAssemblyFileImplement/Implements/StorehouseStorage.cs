using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyFileImplement.Models;
using FurnitureAssemblyFileImplement;

namespace FurnitureAssemblyFileImplement.Implements
{
    public class StorehouseStorage : IStorehouseStorage
    {
        private readonly FileDataListSingleton source;
        public StorehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorehouseViewModel> GetFullList()
        {
            return source.Storehouses
            .Select(CreateModel)
            .ToList();
        }
        public List<StorehouseViewModel> GetFilteredList(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Storehouses
            .Where(rec => rec.StorehouseName.Contains(model.StorehouseName))
            .Select(CreateModel)
            .ToList();
        }
        public StorehouseViewModel GetElement(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var storehouse = source.Storehouses
            .FirstOrDefault(rec => rec.StorehouseName == model.StorehouseName || rec.Id
           == model.Id);
            return storehouse != null ? CreateModel(storehouse) : null;
        }
        public void Insert(StorehouseBindingModel model)
        {
            int maxId = source.Storehouses.Count > 0 ? source.Storehouses.Max(rec => rec.Id) : 0;
            var element = new Storehouse
            {
                Id = maxId + 1,
                StorehouseComponents = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            source.Storehouses.Add(CreateModel(model, element));
        }
        public void Update(StorehouseBindingModel model)
        {
            var element = source.Storehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(StorehouseBindingModel model)
        {
            Storehouse element = source.Storehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Storehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Storehouse CreateModel(StorehouseBindingModel model, Storehouse storehouse)
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
                    storehouse.StorehouseComponents[component.Key] =
                   model.StorehouseComponents[component.Key].Item2;
                }
                else
                {
                    storehouse.StorehouseComponents.Add(component.Key,
                   model.StorehouseComponents[component.Key].Item2);
                }
            }
            return storehouse;
        }
        private StorehouseViewModel CreateModel(Storehouse storehouse)
        {
            return new StorehouseViewModel
            {
                Id = storehouse.Id,
                StorehouseName = storehouse.StorehouseName,
                ResponsiblePerson = storehouse.ResponsiblePerson,
                DateCreate = storehouse.DateCreate,
                StorehouseComponents = storehouse.StorehouseComponents
                            .ToDictionary(recPC => recPC.Key, recPC =>
                        (source.Components.FirstOrDefault(recC => recC.Id ==
                 recPC.Key)?.ComponentName, recPC.Value))
            };
        }
        public bool WriteOffFromStorehouses(Dictionary<int, (string, int)> components, int writeOffCount)
        {
            foreach (var component in components)
            {
                int count = source.Storehouses.Where(comp => comp.StorehouseComponents.ContainsKey(component.Key)).Sum(comp => comp.StorehouseComponents[component.Key]);

                if (count < component.Value.Item2 * writeOffCount)
                {
                    return false;
                }
            }
            foreach (var component in components)
            {
                int count = component.Value.Item2 * writeOffCount;
                IEnumerable<Storehouse> storehouses = source.Storehouses.Where(comp => comp.StorehouseComponents.ContainsKey(component.Key));

                foreach (Storehouse storehouse in storehouses )
                {
                    if (storehouse.StorehouseComponents[component.Key] <= count)
                    {
                        count -= storehouse.StorehouseComponents[component.Key];
                        storehouse.StorehouseComponents.Remove(component.Key);
                    }
                    else
                    {
                        storehouse.StorehouseComponents[component.Key] -= count;
                        count = 0;
                    }
                    if (count == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }
    }
}
