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
    public partial class SupplierEditForm : Form
    {
        private Supplier _supplier;
        public SupplierEditForm()
        {
            InitializeComponent();
            _supplier = new Supplier();
        }

        public SupplierEditForm(Supplier supplier)
        {
            InitializeComponent();
            _supplier = supplier;
            txtName.Text = supplier.Name;
            txtContact.Text = supplier.ContactDetails;
        }


       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name");
                return;
            }

            _supplier.Name = txtName.Text;
            _supplier.ContactDetails = txtContact.Text;

            bool success;
            if (_supplier.SupplierId == 0)
                success = DataAccess.AddSupplier(_supplier);
            else
                success = false; // Update functionality to be implemented

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error saving supplier");
            }
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }

}

