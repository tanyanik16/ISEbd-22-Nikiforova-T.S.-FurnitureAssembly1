using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using System.Collections.Generic;

namespace FurnitureAssemblyContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportFurnitureComponentViewModel> GetFurnitureComponent();
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        void SaveComponentsToWordFile(ReportBindingModel model);
        void SaveFurnitureComponentToExcelFile(ReportBindingModel model);
        void SaveOrdersToPdfFile(ReportBindingModel model);
        void SaveOrdersForAllDatesToPdfFile(ReportBindingModel model);
        void SaveStoreHouseComponentsToExcelFile(ReportBindingModel model);
        void SaveStoreHousesToWordFile(ReportBindingModel model);
        List<ReportOrdersForAllDatesViewModel> GetOrdersForAllDates();
        List<ReportStoreHouseComponentViewModel> GetStoreHouseComponents();
    }
}
