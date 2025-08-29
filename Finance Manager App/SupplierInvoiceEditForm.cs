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
    public partial class SupplierInvoiceEditForm : Form
    {
        private SupplierInvoice _invoice;
        private List<Supplier> _suppliers;
        public SupplierInvoiceEditForm()
        {
            InitializeComponent();
            _invoice = new SupplierInvoice();
            LoadSuppliers();
        }

        public SupplierInvoiceEditForm(SupplierInvoice invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            LoadSuppliers();

            // Set form values from the invoice
            cmbSupplier.SelectedValue = invoice.SupplierId;
            txtInvoiceNumber.Text = invoice.InvoiceNumber;
            dtpDate.Value = invoice.Date;
            txtAmount.Text = invoice.Amount.ToString();
            txtPaymentTerms.Text = invoice.PaymentTerms;
            cmbStatus.Text = invoice.Status;
        }

        private void LoadSuppliers()
        {
            _suppliers = DataAccess.GetSuppliers();
            cmbSupplier.DataSource = _suppliers;
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "SupplierId";
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

            _invoice.SupplierId = (int)cmbSupplier.SelectedValue;
            _invoice.InvoiceNumber = txtInvoiceNumber.Text;
            _invoice.Date = dtpDate.Value;
            _invoice.Amount = amount;
            _invoice.PaymentTerms = txtPaymentTerms.Text;
            _invoice.Status = cmbStatus.Text;

            bool success;
            if (_invoice.InvoiceId == 0)
                success = DataAccess.AddSupplierInvoice(_invoice);
            else
                success = false; // Update functionality to be implemented

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error saving supplier invoice");
            }
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPaymentTerms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
