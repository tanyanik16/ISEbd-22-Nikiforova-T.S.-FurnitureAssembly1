using FurnitureAssemblyContracts.Enums;
using FurnitureAssemblyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FurnitureAssemblyFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string FurnitureFileName = "Furniture.xml";
        private readonly string StorehouseFileName = "Storehouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Furniture> Furnitures { get; set; }
        public List<Storehouse> Storehouses { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Furnitures = LoadFurnitures();
            Storehouses = LoadStorehouses();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        public static void SaveAll()
        {
            GetInstance().SaveComponents();
            GetInstance().SaveOrders();
            GetInstance().SaveFurnitures();
            GetInstance().SaveStorehouses();
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveFurnitures();
            SaveStorehouses();
        }
        private List<Storehouse> LoadStorehouses()
        {
            var list = new List<Storehouse>();
            if (File.Exists(StorehouseFileName))
            {
                var xDocument = XDocument.Load(StorehouseFileName);
                var xElements = xDocument.Root.Elements("Storehouse").ToList();
                foreach (var elem in xElements)
                {
                    var CompComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("StorehouseComponents").Elements("StorehouseComponent").ToList())
                    {
                        CompComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Storehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StorehouseName = elem.Element("StorehouseName").Value,
                        ResponsiblePersonFCS = elem.Element("ResponsiblePersonFCS").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        StorehouseComponents = CompComp
                    });
                }
            }
            return list;
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();

                foreach (var elem in xElements)
                {
                    OrderStatus status = 0;
                    switch (elem.Element("Status").Value)
                    {
                        case "Принят":
                            status = OrderStatus.Принят;
                            break;
                        case "Выполняется":
                            status = OrderStatus.Выполняется;
                            break;
                        case "Готов":
                            status = OrderStatus.Готов;
                            break;
                        case "Оплачен":
                            status = OrderStatus.Выдан;
                            break;
                    }

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FurnitureId = Convert.ToInt32(elem.Element("FurnitureId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = status,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = !string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? Convert.ToDateTime(elem.Element("DateImplement").Value) : (DateTime?)null
                    });
                }
            }
            return list;
        }
        private List<Furniture> LoadFurnitures()
        {
            var list = new List<Furniture>();
            if (File.Exists(FurnitureFileName))
            {
                var xDocument = XDocument.Load(FurnitureFileName);
                var xElements = xDocument.Root.Elements("Furniture").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("FurnitureComponents").Elements("FurnitureComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Furniture
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FurnitureName = elem.Element("FurnitureName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        FurnitureComponents = prodComp
                    });
                }
            }
            return list;
        }
        private void SaveStorehouses()
        {
            if (Storehouses != null)
            {
                var xElement = new XElement("Storehouses");
                foreach (var storehouse in Storehouses)
                {
                    var storehouseElement = new XElement("StorehouseComponents");
                    foreach (var component in storehouse.StorehouseComponents)
                    {
                        storehouseElement.Add(new XElement("StorehouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Storehouse",
                     new XAttribute("Id", storehouse.Id),
                     new XElement("StorehouseName", storehouse.StorehouseName),
                     new XElement("ResponsiblePersonFCS", storehouse.ResponsiblePersonFCS),
                     new XElement("DateCreate", storehouse.DateCreate),
                     storehouseElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(StorehouseFileName);
            }
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("FurnitureId", order.FurnitureId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));

                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveFurnitures()
        {
            if (Furnitures != null)
            {
                var xElement = new XElement("Furnitures");
                foreach (var furniture in Furnitures)
                {
                    var compElement = new XElement("FurnitureComponents");
                    foreach (var component in furniture.FurnitureComponents)
                    {
                        compElement.Add(new XElement("FurnitureComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Furniture",
                     new XAttribute("Id", furniture.Id),
                     new XElement("FurnitureName", furniture.FurnitureName),
                     new XElement("Price", furniture.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(FurnitureFileName);
            }
        }
    }
}
            
