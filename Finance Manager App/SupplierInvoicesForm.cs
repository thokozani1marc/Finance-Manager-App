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
    public partial class SupplierInvoicesForm : Form
    {
        public SupplierInvoicesForm()
        {
            InitializeComponent();
            LoadSupplierInvoices();
            LoadSuppliers();
        }

        private void LoadSupplierInvoices()
        {
            List<SupplierInvoice> invoices = DataAccess.GetSupplierInvoices();
            dataGridViewSupplierInvoices.DataSource = invoices;
        }

        private void LoadSuppliers()
        {
            // This would be used for dropdowns in the edit form
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new SupplierInvoiceEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadSupplierInvoices();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewSupplierInvoices.CurrentRow != null)
            {
                SupplierInvoice invoice = (SupplierInvoice)dataGridViewSupplierInvoices.CurrentRow.DataBoundItem;
                var form = new SupplierInvoiceEditForm(invoice);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadSupplierInvoices();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSupplierInvoices.CurrentRow != null)
            {
                SupplierInvoice invoice = (SupplierInvoice)dataGridViewSupplierInvoices.CurrentRow.DataBoundItem;
                if (MessageBox.Show($"Delete invoice {invoice.InvoiceNumber}?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Implement delete functionality
                    MessageBox.Show("Delete functionality to be implemented");
                }
            }
        }

        private System.Windows.Forms.DataGridView dataGridViewSupplierInvoices;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}
