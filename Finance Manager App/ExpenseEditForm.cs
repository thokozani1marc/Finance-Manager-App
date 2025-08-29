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
    public partial class ExpenseEditForm : Form
    {
        private Expense _expense;
        public ExpenseEditForm()
        {
            InitializeComponent();
            _expense = new Expense();
        }

        public ExpenseEditForm(Expense expense)
        {
            InitializeComponent();
            _expense = expense;

            // Set form values from the expense
            txtCategory.Text = expense.Category;
            dtpDate.Value = expense.Date;
            txtAmount.Text = expense.Amount.ToString();
            txtDescription.Text = expense.Description;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                MessageBox.Show("Please enter a category");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount");
                return;
            }

            _expense.Category = txtCategory.Text;
            _expense.Date = dtpDate.Value;
            _expense.Amount = amount;
            _expense.Description = txtDescription.Text;

            bool success;
            if (_expense.ExpenseId == 0)
                success = DataAccess.AddExpense(_expense);
            else
                success = false; // Update functionality to be implemented

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error saving expense");
            }
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
