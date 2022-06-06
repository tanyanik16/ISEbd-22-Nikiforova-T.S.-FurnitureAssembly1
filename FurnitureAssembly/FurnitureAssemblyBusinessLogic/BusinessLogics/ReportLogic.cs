using FurnitureAssemblyBusinessLogic.OfficePackage;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IFurnitureStorage _FurnitureStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IStorehouseStorage _storeHouseStorage;
        private readonly FurnitureSaveToExcel _saveToExcel;
        private readonly FurnitureSaveToWord _saveToWord;
        private readonly FurnitureSaveToPdf _saveToPdf;
        public ReportLogic(IFurnitureStorage FurnitureStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage,
        FurnitureSaveToExcel saveToExcel, FurnitureSaveToWord saveToWord,
       FurnitureSaveToPdf saveToPdf, IStorehouseStorage storeHouseStorage)
        {
            _FurnitureStorage = FurnitureStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _storeHouseStorage = storeHouseStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        public List<ReportFurnitureComponentViewModel> GetFurnitureComponent()
        {
            var Furnitures = _FurnitureStorage.GetFullList();
            var list = new List<ReportFurnitureComponentViewModel>();
            foreach (var Furniture in Furnitures)
            {
                var record = new ReportFurnitureComponentViewModel
                {
                    FurnitureName = Furniture.FurnitureName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in Furniture.FurnitureComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom =
           model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                FurnitureName = x.FurnitureName,
                Count = x.Count,
                Sum = x.Sum,
                Status = ((OrderStatus)Enum.Parse(typeof(OrderStatus), x.Status.ToString())).ToString()
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Furnitures = _FurnitureStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveFurnitureComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                FurnitureComponents = GetFurnitureComponent()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
        public List<ReportStoreHouseComponentViewModel> GetStoreHouseComponents()
        {
            var components = _componentStorage.GetFullList();
            var storeHouses = _storeHouseStorage.GetFullList();
            var records = new List<ReportStoreHouseComponentViewModel>();
            foreach (var storeHouse in storeHouses)
            {
                var record = new ReportStoreHouseComponentViewModel
                {
                    StoreHouseName = storeHouse.StorehouseName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (storeHouse.StorehouseComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(
                            component.ComponentName, storeHouse.StorehouseComponents[component.Id].Item2));

                        record.TotalCount += storeHouse.StorehouseComponents[component.Id].Item2;
                    }
                }
                records.Add(record);
            }
            return records;
        }
        public List<ReportOrdersForAllDatesViewModel> GetOrdersForAllDates()
        {
            return _orderStorage.GetFullList()
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new ReportOrdersForAllDatesViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }
        public void SaveStoreHousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDocStoreHouse(new WordInfoStoreHouse
            {
                FileName = model.FileName,
                Title = "Список складов",
                StoreHouses = _storeHouseStorage.GetFullList()
            });
        }

        public void SaveStoreHouseComponentsToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateDocStoreHouse(new ExcelInfoStoreHouse
            {
                FileName = model.FileName,
                Title = "Список складов",
                StoreHouseComponents = GetStoreHouseComponents()
            });
        }

        public void SaveOrdersForAllDatesToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocOrdersForAllDates(new PdfInfoOrdersForAllDates
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrdersForAllDates()
            });
        }

    }
}
