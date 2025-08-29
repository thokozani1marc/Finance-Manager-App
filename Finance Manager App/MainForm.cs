using MySql.Data.MySqlClient;
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
    public partial class MainForm : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dashboardToolStripMenuItem;
        private ToolStripMenuItem suppliersToolStripMenuItem;
        private ToolStripMenuItem customersToolStripMenuItem;
        private ToolStripMenuItem supplierInvoicesToolStripMenuItem;
        private ToolStripMenuItem customerInvoicesToolStripMenuItem;
        private ToolStripMenuItem expensesToolStripMenuItem;
        private ToolStripMenuItem incomeToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem incomeStatementToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabDashboard;
        private DataGridView dataGridViewRecentTransactions;
        private Label lblNetIncome;
        private Label label5;
        private Label lblTotalExpenses;
        private Label label3;
        private Label lblTotalRevenue;
        private Label label1;

        public MainForm()
        {
            InitializeComponent();
            LoadDashboard();
        }

        


        private void LoadDashboard()
        {
            // Load summary data
            decimal totalRevenue = CalculateTotalRevenue();
            decimal totalExpenses = CalculateTotalExpenses();
            decimal netIncome = totalRevenue - totalExpenses;

            lblTotalRevenue.Text = totalRevenue.ToString("C");
            lblTotalExpenses.Text = totalExpenses.ToString("C");
            lblNetIncome.Text = netIncome.ToString("C");

            // Load recent transactions
            LoadRecentTransactions();
        }

        private decimal CalculateTotalRevenue()
        {
            decimal total = 0;
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT SUM(amount) FROM Income", conn);
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        total = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating revenue: " + ex.Message);
            }
            return total;
        }

        private decimal CalculateTotalExpenses()
        {
            decimal total = 0;
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new MySqlCommand(
                        "SELECT SUM(amount) FROM (SELECT amount FROM SupplierInvoices UNION ALL SELECT amount FROM Expenses) AS all_expenses",
                        conn);
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        total = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating expenses: " + ex.Message);
            }
            return total;
        }

        private void LoadRecentTransactions()
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    var query = @"
                        SELECT 'Income' as Type, source as Description, date, amount 
                        FROM Income 
                        UNION ALL
                        SELECT 'Supplier Invoice' as Type, invoice_number as Description, date, amount 
                        FROM SupplierInvoices 
                        UNION ALL
                        SELECT 'Expense' as Type, category as Description, date, amount 
                        FROM Expenses 
                        ORDER BY date DESC 
                        LIMIT 50";

                    var adapter = new MySqlDataAdapter(query, conn);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewRecentTransactions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transactions: " + ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Test database connection
            if (!DBConnection.TestConnection())
            {
                MessageBox.Show("Cannot connect to database. Please check your connection settings.");
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabDashboard;
            LoadDashboard();
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SuppliersForm();
            form.ShowDialog();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = new CustomersForm();
            //form.ShowDialog();
        }

        private void supplierInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = new SupplierInvoicesForm();
            //form.ShowDialog();
        }

        private void customerInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = new CustomerInvoicesForm();
            //form.ShowDialog();
        }

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = new ExpensesForm();
            //form.ShowDialog();
        }

        private void incomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = new IncomeForm();
            //form.ShowDialog();
        }

        private void incomeStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ReportForm();
            form.ShowDialog();
        }
    }

}
