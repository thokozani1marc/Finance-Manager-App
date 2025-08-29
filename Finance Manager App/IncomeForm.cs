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
    public partial class IncomeForm : Form
    {
        public IncomeForm()
        {
            InitializeComponent();
            LoadIncome();
        }

        private void LoadIncome()
        {
            List<Income> income = DataAccess.GetIncome();
            dataGridViewIncome.DataSource = income;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new IncomeEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadIncome();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewIncome.CurrentRow != null)
            {
                Income income = (Income)dataGridViewIncome.CurrentRow.DataBoundItem;
                var form = new IncomeEditForm(income);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadIncome();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewIncome.CurrentRow != null)
            {
                Income income = (Income)dataGridViewIncome.CurrentRow.DataBoundItem;
                if (MessageBox.Show($"Delete income record {income.Source} - {income.Description}?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Implement delete functionality
                    MessageBox.Show("Delete functionality to be implemented");
                }
            }
        }

        private System.Windows.Forms.DataGridView dataGridViewIncome;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}
