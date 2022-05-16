using FurnitureAssemblyContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using FurnitureAssemblyContracts.Attributes;



namespace FurnitureAssemblyContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        public int FurnitureId { get; set; }
        public int ClientId { get; set; }
        [Column(title: "Мебель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FurnitureName { get; set; }
        public int? ImplementerId { get; set; }
        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }
        [Column(title: "Клиент", width: 100)]
        public string ClientFIO { get; set; }
        [Column(title: "Исполнитель", width: 100)]
        public string ImplementerFIO { get; set; }
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }
    }
}
