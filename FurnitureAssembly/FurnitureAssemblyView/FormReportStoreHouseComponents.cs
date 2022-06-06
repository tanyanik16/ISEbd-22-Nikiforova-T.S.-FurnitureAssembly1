using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace FurnitureAssemblyView
{
    public partial class FormReportStoreHouseComponents : Form
    {
        public new IUnityContainer Container { get; set; }

        private readonly IReportLogic logic;
        public FormReportStoreHouseComponents(IReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReportStoreHouseComponents_Load(object sender, EventArgs e)
        {
            try
            {
                var storeHouseMaterials = logic.GetStoreHouseComponents();
                if (storeHouseMaterials != null)
                {
                    dataGridViewStoreHouse.Rows.Clear();

                    foreach (var wareHouse in storeHouseMaterials)
                    {
                        dataGridViewStoreHouse.Rows.Add(new object[] { wareHouse.StoreHouseName, "", "" });

                        foreach (var component in wareHouse.Components)
                        {
                            dataGridViewStoreHouse.Rows.Add(new object[] { "", component.Item1, component.Item2 });
                        }

                        dataGridViewStoreHouse.Rows.Add(new object[] { "Итого", "", wareHouse.TotalCount });
                        dataGridViewStoreHouse.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveStoreHouseComponentsToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
