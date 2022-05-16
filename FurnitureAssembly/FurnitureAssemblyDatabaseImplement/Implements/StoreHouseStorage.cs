using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;


namespace FurnitureAssemblyDatabaseImplement.Implements
{
   public  class StoreHouseStorage : IStorehouseStorage
    {
        private Storehouse CreateModel(StorehouseBindingModel model, Storehouse Storehouse, FurnitureAssemblyDatabase context)
        {
            Storehouse.StorehouseName = model.StorehouseName;
            Storehouse.NameOfResponsiblePerson = model.ResponsiblePersonFCS;

            if (Storehouse.Id == 0)
            {
                Storehouse.DateCreate = DateTime.Now;
                context.Storehouses.Add(Storehouse);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                List<StorehouseComponent> StorehouseComponents = context.StorehouseComponents
                    .Where(rec => rec.StorehouseId == model.Id.Value)
                    .ToList();

                context.StorehouseComponents.RemoveRange(StorehouseComponents
                    .Where(rec => !model.StorehouseComponents.ContainsKey(rec.ComponentId))
                    .ToList());
                context.SaveChanges();

                foreach (StorehouseComponent updateComponent in StorehouseComponents)
                {
                    updateComponent.Count = model.StorehouseComponents[updateComponent.ComponentId].Item2;
                    model.StorehouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }


            foreach (KeyValuePair<int, (string, int)> StorehouseComponent in model.StorehouseComponents)
            {
                context.StorehouseComponents.Add(new StorehouseComponent
                {
                    StorehouseId = Storehouse.Id,
                    ComponentId = StorehouseComponent.Key,
                    Count = StorehouseComponent.Value.Item2
                });
                context.SaveChanges();
            }

            return Storehouse;
        }

        public List<StorehouseViewModel> GetFullList()
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                return context.Storehouses
                    .Include(rec => rec.StorehouseComponents)
                    .ThenInclude(rec => rec.Component)
                    .ToList()
                    .Select(rec => new StorehouseViewModel
                    {
                        Id = rec.Id,
                        StorehouseName = rec.StorehouseName,
                        ResponsiblePersonFCS = rec.NameOfResponsiblePerson,
                        DateCreate = rec.DateCreate,
                        StorehouseComponents = rec.StorehouseComponents
                            .ToDictionary(recStorehouseComponents => recStorehouseComponents.ComponentId,
                            recStorehouseComponents => (recStorehouseComponents.Component?.ComponentName,
                            recStorehouseComponents.Count))
                    })
                    .ToList();
            }
        }

        public List<StorehouseViewModel> GetFilteredList(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                return context.Storehouses
                    .Include(rec => rec.StorehouseComponents)
                    .ThenInclude(rec => rec.Component)
                    .Where(rec => rec.StorehouseName.Contains(model.StorehouseName))
                    .ToList()
                    .Select(rec => new StorehouseViewModel
                    {
                        Id = rec.Id,
                        StorehouseName = rec.StorehouseName,
                        ResponsiblePersonFCS = rec.NameOfResponsiblePerson,
                        DateCreate = rec.DateCreate,
                        StorehouseComponents = rec.StorehouseComponents
                            .ToDictionary(recStorehouseComponent => recStorehouseComponent.ComponentId,
                            recStorehouseComponent => (recStorehouseComponent.Component?.ComponentName,
                            recStorehouseComponent.Count))
                    })
                    .ToList();
            }
        }

        public StorehouseViewModel GetElement(StorehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Storehouse Storehouse = context.Storehouses
                    .Include(rec => rec.StorehouseComponents)
                    .ThenInclude(rec => rec.Component)
                    .FirstOrDefault(rec => rec.StorehouseName == model.StorehouseName ||
                    rec.Id == model.Id);

                return Storehouse != null ?
                    new StorehouseViewModel
                    {
                        Id = Storehouse.Id,
                        StorehouseName = Storehouse.StorehouseName,
                        ResponsiblePersonFCS = Storehouse.NameOfResponsiblePerson,
                        DateCreate = Storehouse.DateCreate,
                        StorehouseComponents = Storehouse.StorehouseComponents
                            .ToDictionary(recStorehouseComponent => recStorehouseComponent.ComponentId,
                            recStorehouseComponent => (recStorehouseComponent.Component?.ComponentName,
                            recStorehouseComponent.Count))
                    } :
                    null;
            }
        }

        public void Insert(StorehouseBindingModel model)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var stock = context.Storehouses.FirstOrDefault(x => x.StorehouseName == model.StorehouseName);
                        {
                            throw new Exception("склад есть с таким именем ");
                        }
                        CreateModel(model, new Storehouse(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }

        public void Update(StorehouseBindingModel model)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Storehouse Storehouse = context.Storehouses.FirstOrDefault(rec => rec.Id == model.Id);

                        if (Storehouse == null)
                        {
                            throw new Exception("Склад не найден");
                        }

                        CreateModel(model, Storehouse, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(StorehouseBindingModel model)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Storehouse Storehouse = context.Storehouses.FirstOrDefault(rec => rec.Id == model.Id);

                if (Storehouse == null)
                {
                    throw new Exception("Склад не найден");
                }
                context.Storehouses.Remove(Storehouse);
                context.SaveChanges();
            }
        }

        public bool WriteOffFromStorehouses(Dictionary<int, (string, int)> Components, int reinforcedCount)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (KeyValuePair<int, (string, int)> Component in Components)
                        {
                            int requiredComponentCount = Component.Value.Item2 * reinforcedCount;
                            IEnumerable<StorehouseComponent> StorehouseComponents = context.StorehouseComponents
                                .Where(Storehouse => Storehouse.ComponentId == Component.Key);
                            foreach (StorehouseComponent StorehouseComponent in StorehouseComponents)
                            {
                                if (StorehouseComponent.Count <= requiredComponentCount)
                                {
                                    requiredComponentCount -= StorehouseComponent.Count;
                                    context.StorehouseComponents.Remove(StorehouseComponent);
                                }
                                else
                                {
                                    StorehouseComponent.Count -= requiredComponentCount;
                                    requiredComponentCount = 0;
                                    break;
                                }
                            }
                            if (requiredComponentCount != 0)
                            {
                                throw new Exception("Нехватка материалов на складе");
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
    
 }

