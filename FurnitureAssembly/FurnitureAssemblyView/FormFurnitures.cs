using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using FurnitureAssemblyContracts.ViewModels;
using Unity;

namespace FurnitureAssemblyView
{
    public partial class FormFurnitures : Form
    {
        private readonly IFurnitureLogic _logic;
        public FormFurnitures(IFurnitureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        
        private void LoadData()
        {
            try
            {
                var list = _logic.Read(null);
                if (list != null)
                {
                    dataGridViewFurniture.DataSource = list;
                    dataGridViewFurniture.Columns[0].Visible = false;
                    dataGridViewFurniture.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormFurniture>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewFurniture.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormFurniture>();
                form.Id = Convert.ToInt32(dataGridViewFurniture.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (dataGridViewFurniture.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =
                   Convert.ToInt32(dataGridViewFurniture.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logic.Delete(new FurnitureBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void FormFurnitures_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
