using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureAssemblyContracts.ViewModels
{
  
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string ClientFIO { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
