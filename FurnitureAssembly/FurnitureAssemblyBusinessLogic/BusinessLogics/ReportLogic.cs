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
        private readonly IFurnitureStorage _furnitureStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly FurnitureSaveToExcel _saveToExcel;
        private readonly FurnitureSaveToWord _saveToWord;
        private readonly FurnitureSaveToPdf _saveToPdf;
        public ReportLogic(IFurnitureStorage furnitureStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage,
        FurnitureSaveToExcel saveToExcel, FurnitureSaveToWord saveToWord,
       FurnitureSaveToPdf saveToPdf)
        {
            _furnitureStorage = furnitureStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportFurnitureComponentViewModel> GetFurnitureComponent()
        {
            var components = _componentStorage.GetFullList();
            var furnitures = _furnitureStorage.GetFullList();
            var list = new List<ReportFurnitureComponentViewModel>();
            foreach (var furniture in furnitures)
            {
                var record = new ReportFurnitureComponentViewModel
                {
                    FurnitureName = furniture.FurnitureName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (furniture.FurnitureComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(component.ComponentName, furniture.FurnitureComponents[component.Id].Item2));
                        record.TotalCount +=
                       furniture.FurnitureComponents[component.Id].Item2;
                    }
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
               Furnitures = _furnitureStorage.GetFullList()
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
                Title = "Список компонент",
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
    }
}
