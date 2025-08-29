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
    public partial class CustomerEditForm : Form
    {
        private Customer _customer;
        public CustomerEditForm()
        {
            InitializeComponent();
            _customer = new Customer();
        }

        public CustomerEditForm(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            txtName.Text = customer.Name;
            txtContact.Text = customer.ContactDetails;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name");
                return;
            }

            _customer.Name = txtName.Text;
            _customer.ContactDetails = txtContact.Text;

            bool success;
            if (_customer.CustomerId == 0)
                success = DataAccess.AddCustomer(_customer);
            else
                success = false; // Update functionality to be implemented

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error saving customer");
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
