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
    public partial class ExpensesForm : Form
    {
        public ExpensesForm()
        {
            InitializeComponent();
            LoadExpenses();
        }


        private void LoadExpenses()
        {
            List<Expense> expenses = DataAccess.GetExpenses();
            dataGridViewExpenses.DataSource = expenses;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new ExpenseEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadExpenses();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewExpenses.CurrentRow != null)
            {
                Expense expense = (Expense)dataGridViewExpenses.CurrentRow.DataBoundItem;
                var form = new ExpenseEditForm(expense);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadExpenses();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewExpenses.CurrentRow != null)
            {
                Expense expense = (Expense)dataGridViewExpenses.CurrentRow.DataBoundItem;
                if (MessageBox.Show($"Delete expense {expense.Category} - {expense.Description}?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Implement delete functionality
                    MessageBox.Show("Delete functionality to be implemented");
                }
            }
        }

        private System.Windows.Forms.DataGridView dataGridViewExpenses;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}
