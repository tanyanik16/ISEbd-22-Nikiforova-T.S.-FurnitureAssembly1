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
    public partial class FormFurniture : Form
    {
        public int Id { set { id = value; } }
        private readonly IFurnitureLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> furnitureComponents;

        public FormFurniture(IFurnitureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormFurniture_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    FurnitureViewModel view = _logic.Read(new FurnitureBindingModel
                    {
                        Id =id.Value })?[0];
                    if (view != null)
                    {
                        textBoxPrice.Text = view.Price.ToString();
                        textBoxName.Text = view.FurnitureName;
                        furnitureComponents = view.FurnitureComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else

            {
                furnitureComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (furnitureComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in furnitureComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,pc.Value.Item2 });
                    }
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
            
           var form = Program.Container.Resolve<FormFurnitureComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (furnitureComponents.ContainsKey(form.Id))
                {
                    furnitureComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else
                {
                    furnitureComponents.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        furnitureComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
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

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormFurnitureComponent>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = furnitureComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    furnitureComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (furnitureComponents == null || furnitureComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new FurnitureBindingModel
                {
                    Id = id,
                    FurnitureName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    FurnitureComponents = furnitureComponents
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
