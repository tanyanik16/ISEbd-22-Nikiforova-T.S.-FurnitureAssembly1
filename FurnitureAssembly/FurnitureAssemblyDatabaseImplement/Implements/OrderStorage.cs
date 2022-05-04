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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new FurnitureAssemblyDatabase();
            return context.Orders.Include(rec => rec.Furniture)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                  FurnitureId = rec.FurnitureId,
                    FurnitureName = rec.Furniture.FurnitureName,
                    Count = rec.Count,
                    ClientFIO = rec.Client.ClientFIO,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    ImplementerFIO = rec.Implementer.ImplementerFIO,
                })
                .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new FurnitureAssemblyDatabase();
            return context.Orders
            .Include(rec => rec.Furniture)
             .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue &&
            rec.DateCreate.Date == model.DateCreate.Date) ||
             (model.DateFrom.HasValue && model.DateTo.HasValue &&
            rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <=
            model.DateTo.Value.Date) ||
             (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
            (model.SearchStatus.HasValue && model.SearchStatus.Value ==
            rec.Status) ||
            (model.ImplementerId.HasValue && rec.ImplementerId ==
            model.ImplementerId && model.Status == rec.Status)).
            Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                ClientFIO = rec.Client.ClientFIO,
                ImplementerId = rec.ImplementerId,
                ImplementerFIO = rec.Implementer.ImplementerFIO,
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
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                Order order = context.Orders.Include(rec => rec.Furniture)
                 .Include(rec => rec.Implementer)
                 .Include(rec => rec.Client)
                 .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    ImplementerId = order.ImplementerId,
                    ClientId = order.ClientId,
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
                    ImplementerId = model.ImplementerId,
                    Count = model.Count,
                    ClientId = (int)model.ClientId,
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
                element.ImplementerId = model.ImplementerId;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new FurnitureAssemblyDatabase();
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
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (FurnitureAssemblyDatabase context = new FurnitureAssemblyDatabase())
            {
                
                Implementer impl = context.Implementers.FirstOrDefault(rec => rec.Id == model.ImplementerId);
                if (impl != null)
                {
                    if (impl.Orders == null)
                    {
                        impl.Orders = new List<Order>();
                        context.Implementers.Update(impl);
                        context.SaveChanges();
                    }
                    impl.Orders.Add(order);
                }
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

