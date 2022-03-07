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
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;

namespace FurnitureAssemblyView
{
    public partial class FormStorehouse : Form
    {
        public int Id { set { id = value; } }
        private readonly IStorehouseLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> storehouseComponents;
        public FormStorehouse(IStorehouseLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormStorehouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StorehouseViewModel view = _logic.Read(new StorehouseBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.StorehouseName;
                        textBox_Person.Text = view.ResponsiblePerson;
                        storehouseComponents = view.StorehouseComponents;
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
                storehouseComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (storehouseComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var wc in storehouseComponents)
                    {
                        dataGridView.Rows.Add(new object[] { wc.Key, wc.Value.Item1, wc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(textBox_Person.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new StorehouseBindingModel
                {
                    Id = id,
                    StorehouseName = textBoxName.Text,
                    ResponsiblePerson = textBox_Person.Text,
                    StorehouseComponents = storehouseComponents,
                    DateCreate = DateTime.Now
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
