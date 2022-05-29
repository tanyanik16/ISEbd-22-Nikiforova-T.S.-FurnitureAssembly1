using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;

namespace FurnitureAssemblyBusinessLogic.OfficePackage
{
    public abstract class FurnitureSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
                WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var firniture in info.Furnitures)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {
                        (firniture.FurnitureName + ": ", new WordTextProperties {
                        Size = "24",
                        Bold = true
                        }),
                        (Convert.ToInt32(firniture.Price).ToString(), new WordTextProperties {
                        Size = "24"
                        })},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }
        public void CreateDocStoreHouse(WordInfoStoreHouse info)
        {
            CreateWord(new WordInfo()
            {
                FileName = info.FileName
            });
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
                WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(new List<string>() { "Название", "ФИО ответственного", "Дата создания" });

            foreach (var storeHouse in info.StoreHouses)
            {
                CreateRow(new WordRowParametrs()
                {
                    Texts = new List<string>()
                  {
                      storeHouse.StorehouseName,
                      storeHouse.ResponsiblePersonFCS,
                      storeHouse.DateCreate.ToShortDateString()
                  }
                });
            }
            SaveWord(new WordInfo()
            {
                FileName = info.FileName
            });
        }
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(WordRowParametrs wordRow);
        protected abstract void CreateWord(WordInfo info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void SaveWord(WordInfo info);
    }
}
