using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using FurnitureAssemblyContracts.Attributes;

namespace FurnitureAssemblyContracts.ViewModels
{
  
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }
        [Column(title: "Логин", width: 100)]
        public string Email { get; set; }
        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}
