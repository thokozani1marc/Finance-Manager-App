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
    public partial class CustomerInvoicesForm : Form
    {
        public CustomerInvoicesForm()
        {
            InitializeComponent();
            LoadCustomerInvoices();
            LoadCustomers();
        }

        private void LoadCustomerInvoices()
        {
            List<CustomerInvoice> invoices = DataAccess.GetCustomerInvoices();
            dataGridViewCustomerInvoices.DataSource = invoices;
        }

        private void LoadCustomers()
        {
            // This would be used for dropdowns in the edit form
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new CustomerInvoiceEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCustomerInvoices();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomerInvoices.CurrentRow != null)
            {
                CustomerInvoice invoice = (CustomerInvoice)dataGridViewCustomerInvoices.CurrentRow.DataBoundItem;
                var form = new CustomerInvoiceEditForm(invoice);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerInvoices();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomerInvoices.CurrentRow != null)
            {
                CustomerInvoice invoice = (CustomerInvoice)dataGridViewCustomerInvoices.CurrentRow.DataBoundItem;
                if (MessageBox.Show($"Delete invoice {invoice.InvoiceNumber}?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Implement delete functionality
                    MessageBox.Show("Delete functionality to be implemented");
                }
            }
        }

        private System.Windows.Forms.DataGridView dataGridViewCustomerInvoices;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}
