using Finance_Manager_App.Models;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class CustomerInvoiceEditForm : Form
    {
        private CustomerInvoice _invoice;
        private List<Customer> _customers;
        public CustomerInvoiceEditForm()
        {
            InitializeComponent();
            _invoice = new CustomerInvoice();
            LoadCustomers();
        }

        public CustomerInvoiceEditForm(CustomerInvoice invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            LoadCustomers();

            // Set form values from the invoice
            cmbCustomer.SelectedValue = invoice.CustomerId;
            txtInvoiceNumber.Text = invoice.InvoiceNumber;
            dtpDate.Value = invoice.Date;
            txtAmount.Text = invoice.Amount.ToString();
            dtpDueDate.Value = invoice.DueDate;
            cmbStatus.Text = invoice.Status;
        }

        private void LoadCustomers()
        {
            _customers = DataAccess.GetCustomers();
            cmbCustomer.DataSource = _customers;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "CustomerId";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                MessageBox.Show("Please enter an invoice number");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount");
                return;
            }

            _invoice.CustomerId = (int)cmbCustomer.SelectedValue;
            _invoice.InvoiceNumber = txtInvoiceNumber.Text;
            _invoice.Date = dtpDate.Value;
            _invoice.Amount = amount;
            _invoice.DueDate = dtpDueDate.Value;
            _invoice.Status = cmbStatus.Text;

            bool success;
            if (_invoice.InvoiceId == 0)
                success = DataAccess.AddCustomerInvoice(_invoice);
            else
                success = false; // Update functionality to be implemented

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error saving customer invoice");
            }
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
