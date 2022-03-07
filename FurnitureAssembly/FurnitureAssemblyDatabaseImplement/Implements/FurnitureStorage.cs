using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Implements
{
    public class FurnitureStorage : IFurnitureStorage
    {
        public List<FurnitureViewModel> GetFullList()
        {
            using var context = new FurnitureAssemblyDatabase();
            return context.Furnitures
            .Include(rec => rec.FurnitureComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<FurnitureViewModel> GetFilteredList(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            return context.Furnitures
            .Include(rec => rec.FurnitureComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.FurnitureName.Contains(model.FurnitureName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public FurnitureViewModel GetElement(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            var furniture = context.Furnitures
            .Include(rec => rec.FurnitureComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.FurnitureName == model.FurnitureName ||
            rec.Id == model.Id);
            return furniture != null ? CreateModel(furniture) : null;
        }
        public void Insert(FurnitureBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Furnitures.Add(CreateModel(model, new Furniture(),
                context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(FurnitureBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Furnitures.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(FurnitureBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
            Furniture element = context.Furnitures.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Furnitures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Furniture CreateModel(FurnitureBindingModel model, Furniture furniture,
       FurnitureAssemblyDatabase context)
        {
            furniture.FurnitureName = model.FurnitureName;
            furniture.Price = model.Price;
            if (model.Id.HasValue)
            {
                var furnitureComponents = context.FurnitureComponents.Where(rec =>
               rec.FurnitureId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.FurnitureComponents.RemoveRange(furnitureComponents.Where(rec =>
               !model.FurnitureComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in furnitureComponents)
                {
                    updateComponent.Count =
                   model.FurnitureComponents[updateComponent.ComponentId].Item2;
                    model.FurnitureComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.FurnitureComponents)
            {
                context.FurnitureComponents.Add(new FurnitureComponent
                {
                    FurnitureId = furniture.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return furniture;
        }
        private static FurnitureViewModel CreateModel(Furniture furniture)
        {
            return new FurnitureViewModel
            {
                Id = furniture.Id,
                FurnitureName = furniture.FurnitureName,
                Price = furniture.Price,
                FurnitureComponents = furniture.FurnitureComponents
            .ToDictionary(recPC => recPC.ComponentId,
            recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }
}

