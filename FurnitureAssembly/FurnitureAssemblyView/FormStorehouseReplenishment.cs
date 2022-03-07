using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyContracts.BindingModels;

namespace FurnitureAssemblyView
{
    public partial class FormStorehouseReplenishment : Form
    {
        private readonly IStorehouseLogic _logicStoreh;

        private readonly IComponentLogic _logicComp;
        public FormStorehouseReplenishment(IStorehouseLogic logicStoreh, IComponentLogic logicComp)
        {
            InitializeComponent();
            _logicStoreh = logicStoreh;
            _logicComp = logicComp;
        }
        private void FormStorehouseReplenishment_Load(object sender, EventArgs e)
        {
            try
            {
                List<StorehouseViewModel> listStorehouse = _logicStoreh.Read(null);
                if (listStorehouse != null)
                {
                    comboBoxStorehouse.DisplayMember = "StorehouseName";
                    comboBoxStorehouse.ValueMember = "Id";
                    comboBoxStorehouse.DataSource = listStorehouse;
                    comboBoxStorehouse.SelectedItem = null;
                }

                List<ComponentViewModel> listComponent = _logicComp.Read(null);
                if (listComponent != null)
                {
                    comboBoxComponent.DisplayMember = "ComponentName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = listComponent;
                    comboBoxComponent.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorehouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<ComponentViewModel> listComponent = _logicComp.Read(null);
                _logicStoreh.Replenishment(new StorehouseReplenishmentBindingModel
                {
                    StorehouseId = Convert.ToInt32(comboBoxStorehouse.SelectedValue),
                    ComponentId = listComponent[comboBoxComponent.SelectedIndex].Id,
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
