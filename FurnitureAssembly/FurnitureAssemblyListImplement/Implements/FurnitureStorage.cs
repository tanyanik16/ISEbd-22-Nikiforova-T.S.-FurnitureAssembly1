﻿using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureAssemblyListImplement.Implements
{
    public class FurnitureStorage : IFurnitureStorage
    {
        private readonly DataListSingleton source;
        public FurnitureStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<FurnitureViewModel> GetFullList()
        {
            var result = new List<FurnitureViewModel>();
            foreach (var component in source.Furnitures)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<FurnitureViewModel> GetFilteredList(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<FurnitureViewModel>();
            foreach (var furniture in source.Furnitures)
            {
                if (furniture.FurnitureName.Contains(model.FurnitureName))
                {
                    result.Add(CreateModel(furniture));
                }
            }
            return result;
        }
        public FurnitureViewModel GetElement(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var furniture in source.Furnitures)
            {
                if (furniture.Id == model.Id || furniture.FurnitureName ==
                model.FurnitureName)
                {
                    return CreateModel(furniture);
                }
            }
            return null;
        }
        public void Insert(FurnitureBindingModel model)
        {
            var tempFurniture = new Furniture
            {
                Id = 1,
                FurnitureComponents = new
            Dictionary<int, int>()
            };
            foreach (var furniture in source.Furnitures)
            {
                if (furniture.Id >= tempFurniture.Id)
                {
                    tempFurniture.Id = furniture.Id + 1;
                }
            }
            source.Furnitures.Add(CreateModel(model, tempFurniture));
        }
        public void Update(FurnitureBindingModel model)
        {
            Furniture tempFurniture = null;
            foreach (var furniture in source.Furnitures)
            {
                if (furniture.Id == model.Id)
                {
                    tempFurniture = furniture;
                }
            }
            if (tempFurniture == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempFurniture);
        }
        public void Delete(FurnitureBindingModel model)
        {
            for (int i = 0; i < source.Furnitures.Count; ++i)
            {
                if (source.Furnitures[i].Id == model.Id)
                {
                    source.Furnitures.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Furniture CreateModel(FurnitureBindingModel model, Furniture
        furniture)
        {
            furniture.FurnitureName = model.FurnitureName;
            furniture.Price = model.Price;
            // удаляем убранные
            foreach (var key in furniture.FurnitureComponents.Keys.ToList())
            {
                if (!model.FurnitureComponents.ContainsKey(key))
                {
                    furniture.FurnitureComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.FurnitureComponents)
            {
                if (furniture.FurnitureComponents.ContainsKey(component.Key))
                {
                    furniture.FurnitureComponents[component.Key] =
                    model.FurnitureComponents[component.Key].Item2;
                }
                else
                {
                    furniture.FurnitureComponents.Add(component.Key,
                    model.FurnitureComponents[component.Key].Item2);
                }
            }
            return furniture;
        }
        private FurnitureViewModel CreateModel(Furniture furniture)
        {
            // требуется дополнительно получить список компонентов для изделия названиями и их количество
            var furnitureComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in furniture.FurnitureComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                furnitureComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new FurnitureViewModel
            {
                Id = furniture.Id,
                FurnitureName = furniture.FurnitureName,
                Price = furniture.Price,
                FurnitureComponents = furnitureComponents
            };
        }
    }
}
