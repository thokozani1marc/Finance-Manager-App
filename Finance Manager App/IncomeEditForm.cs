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
    public partial class IncomeEditForm : Form
    {
        private Income _income;
        public IncomeEditForm()
        {
            InitializeComponent();
            _income = new Income();
        }

        public IncomeEditForm(Income income)
        {
            InitializeComponent();
            _income = income;

            // Set form values from the income
            txtSource.Text = income.Source;
            dtpDate.Value = income.Date;
            txtAmount.Text = income.Amount.ToString();
            txtDescription.Text = income.Description;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSource.Text))
            {
                MessageBox.Show("Please enter a source");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount");
                return;
            }

            _income.Source = txtSource.Text;
            _income.Date = dtpDate.Value;
            _income.Amount = amount;
            _income.Description = txtDescription.Text;

            bool success;
            if (_income.IncomeId == 0)
                success = DataAccess.AddIncome(_income);
            else
                success = false; // Update functionality to be implemented

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error saving income");
            }
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
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
