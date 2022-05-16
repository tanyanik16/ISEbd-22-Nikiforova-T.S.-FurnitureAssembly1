using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FurnitureAssemblyContracts.BindingModels;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BusinessLogicsContracts
{
    public interface IBackUpLogic
    {
        void CreateBackUp(BackUpSaveBinidngModel model);
    }

}
