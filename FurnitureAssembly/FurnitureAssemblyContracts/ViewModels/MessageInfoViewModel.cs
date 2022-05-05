using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
   public class MessageInfoViewModel
    {
        public string MessageId { get; set; }
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        public string Subject { get; set; }
        [DisplayName("Текст")]
        public string Body { get; set; }
    }
}
