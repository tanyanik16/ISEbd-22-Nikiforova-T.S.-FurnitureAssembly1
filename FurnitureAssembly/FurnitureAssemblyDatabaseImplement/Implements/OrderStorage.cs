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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                return context.Orders.Include(rec => rec.Furniture)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    FurnitureId = rec.FurnitureId,
                    FurnitureName = rec.Furniture.FurnitureName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                return context.Orders.Include(rec => rec.Furniture)
                .Where(rec => rec.FurnitureId == model.FurnitureId)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    FurnitureId = rec.FurnitureId,
                    FurnitureName = rec.Furniture.FurnitureName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Order order = context.Orders.Include(rec => rec.Furniture)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    FurnitureId = order.FurnitureId,
                    FurnitureName = order.Furniture.FurnitureName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Order order = new Order
                {
                    FurnitureId = model.FurnitureId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.FurnitureId = model.FurnitureId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Furniture element = context.Furnitures.FirstOrDefault(rec => rec.Id == model.FurnitureId);
                if (element != null)
                {
                    if (element.Orders == null)
                    {
                        element.Orders = new List<Order>();
                    }
                    element.Orders.Add(order);
                    context.Furnitures.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return order;
        }
    }
}
