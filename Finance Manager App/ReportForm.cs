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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }


     

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable report = DataAccess.GenerateIncomeStatement(dtpStartDate.Value, dtpEndDate.Value);
                dataGridViewReport.DataSource = report;

                // Calculate totals
                decimal totalRevenue = 0;
                decimal totalExpenses = 0;

                foreach (DataRow row in report.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["amount"]);
                    string category = row["Category"].ToString();

                    if (category == "Revenue")
                        totalRevenue += amount;
                    else
                        totalExpenses += amount;
                }

                MessageBox.Show($"Total Revenue: {totalRevenue:C}\nTotal Expenses: {totalExpenses:C}\nNet Income: {totalRevenue - totalExpenses:C}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewReport.DataSource != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Simple CSV export implementation
                    DataTable dt = (DataTable)dataGridViewReport.DataSource;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName);

                    // Write headers
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(dt.Columns[i]);
                        if (i < dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();

                    // Write rows
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sw.Write(row[i].ToString());
                            if (i < dt.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();
                    }

                    sw.Close();
                    MessageBox.Show("Exported successfully");
                }
            }
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.Button btnExport;
    }
}

