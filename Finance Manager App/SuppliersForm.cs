using Finance_Manager_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finance_Manager_App
{
    public partial class SuppliersForm : Form
    {
        public SuppliersForm()
        {
            InitializeComponent();
            LoadSuppliers();
        }


      
        private void LoadSuppliers()
        {
            List<Supplier> suppliers = DataAccess.GetSuppliers();
            dataGridViewSuppliers.DataSource = suppliers;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new SupplierEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadSuppliers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewSuppliers.CurrentRow != null)
            {
                Supplier supplier = (Supplier)dataGridViewSuppliers.CurrentRow.DataBoundItem;
                var form = new SupplierEditForm(supplier);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadSuppliers();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSuppliers.CurrentRow != null)
            {
                Supplier supplier = (Supplier)dataGridViewSuppliers.CurrentRow.DataBoundItem;
                if (MessageBox.Show($"Delete supplier {supplier.Name}?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Implement delete functionality
                    MessageBox.Show("Delete functionality to be implemented");
                }
            }
        }

        private System.Windows.Forms.DataGridView dataGridViewSuppliers;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}

