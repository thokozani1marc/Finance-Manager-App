
using Finance_Manager_App.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finance_Manager_App
{
    public class DataAccess
    {
        // Supplier methods
        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Suppliers ORDER BY name";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier()
                        {
                            SupplierId = Convert.ToInt32(reader["supplier_id"]),
                            Name = reader["name"].ToString(),
                            ContactDetails = reader["contact_details"].ToString()
                        });
                    }
                }
            }

            return suppliers;
        }

        public static bool AddSupplier(Supplier supplier)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Suppliers (name, contact_details) VALUES (@name, @contact)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", supplier.Name);
                    cmd.Parameters.AddWithValue("@contact", supplier.ContactDetails);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding supplier: " + ex.Message);
                return false;
            }
        }

        // Similar methods for UpdateSupplier, DeleteSupplier, and for other entities
        // Customer methods
        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Customers ORDER BY name";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            Name = reader["name"].ToString(),
                            ContactDetails = reader["contact_details"].ToString()
                        });
                    }
                }
            }

            return customers;
        }

        public static bool AddCustomer(Customer customer)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Customers (name, contact_details) VALUES (@name, @contact)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", customer.Name);
                    cmd.Parameters.AddWithValue("@contact", customer.ContactDetails);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message);
                return false;
            }
        }

        // Supplier Invoice methods
        public static List<SupplierInvoice> GetSupplierInvoices()
        {
            List<SupplierInvoice> invoices = new List<SupplierInvoice>();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT si.*, s.name as supplier_name 
                                FROM SupplierInvoices si 
                                INNER JOIN Suppliers s ON si.supplier_id = s.supplier_id 
                                ORDER BY si.date DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new SupplierInvoice()
                        {
                            InvoiceId = Convert.ToInt32(reader["invoice_id"]),
                            SupplierId = Convert.ToInt32(reader["supplier_id"]),
                            InvoiceNumber = reader["invoice_number"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            PaymentTerms = reader["payment_terms"].ToString(),
                            Status = reader["status"].ToString(),
                            SupplierName = reader["supplier_name"].ToString()
                        });
                    }
                }
            }

            return invoices;
        }

        public static bool AddSupplierInvoice(SupplierInvoice invoice)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO SupplierInvoices 
                                    (supplier_id, invoice_number, date, amount, payment_terms, status) 
                                    VALUES (@supplierId, @invoiceNumber, @date, @amount, @terms, @status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@supplierId", invoice.SupplierId);
                    cmd.Parameters.AddWithValue("@invoiceNumber", invoice.InvoiceNumber);
                    cmd.Parameters.AddWithValue("@date", invoice.Date);
                    cmd.Parameters.AddWithValue("@amount", invoice.Amount);
                    cmd.Parameters.AddWithValue("@terms", invoice.PaymentTerms);
                    cmd.Parameters.AddWithValue("@status", invoice.Status);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding supplier invoice: " + ex.Message);
                return false;
            }
        }

        // Similar methods for CustomerInvoices, Expenses, and Income
        // Customer Invoice methods
        public static List<CustomerInvoice> GetCustomerInvoices()
        {
            List<CustomerInvoice> invoices = new List<CustomerInvoice>();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ci.*, c.name as customer_name 
                                FROM CustomerInvoices ci 
                                INNER JOIN Customers c ON ci.customer_id = c.customer_id 
                                ORDER BY ci.date DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new CustomerInvoice()
                        {
                            InvoiceId = Convert.ToInt32(reader["invoice_id"]),
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            InvoiceNumber = reader["invoice_number"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            DueDate = Convert.ToDateTime(reader["due_date"]),
                            Status = reader["status"].ToString(),
                            CustomerName = reader["customer_name"].ToString()
                        });
                    }
                }
            }

            return invoices;
        }

        public static bool AddCustomerInvoice(CustomerInvoice invoice)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO CustomerInvoices 
                                    (customer_id, invoice_number, date, amount, due_date, status) 
                                    VALUES (@customerId, @invoiceNumber, @date, @amount, @dueDate, @status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@customerId", invoice.CustomerId);
                    cmd.Parameters.AddWithValue("@invoiceNumber", invoice.InvoiceNumber);
                    cmd.Parameters.AddWithValue("@date", invoice.Date);
                    cmd.Parameters.AddWithValue("@amount", invoice.Amount);
                    cmd.Parameters.AddWithValue("@dueDate", invoice.DueDate);
                    cmd.Parameters.AddWithValue("@status", invoice.Status);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer invoice: " + ex.Message);
                return false;
            }
        }

        // Expense methods
        public static List<Expense> GetExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Expenses ORDER BY date DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expenses.Add(new Expense()
                        {
                            ExpenseId = Convert.ToInt32(reader["expense_id"]),
                            Category = reader["category"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            Description = reader["description"].ToString()
                        });
                    }
                }
            }

            return expenses;
        }

        public static bool AddExpense(Expense expense)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Expenses 
                                    (category, date, amount, description) 
                                    VALUES (@category, @date, @amount, @description)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@category", expense.Category);
                    cmd.Parameters.AddWithValue("@date", expense.Date);
                    cmd.Parameters.AddWithValue("@amount", expense.Amount);
                    cmd.Parameters.AddWithValue("@description", expense.Description);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding expense: " + ex.Message);
                return false;
            }
        }

        // Income methods
        public static List<Income> GetIncome()
        {
            List<Income> income = new List<Income>();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Income ORDER BY date DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        income.Add(new Income()
                        {
                            IncomeId = Convert.ToInt32(reader["income_id"]),
                            Source = reader["source"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            Description = reader["description"].ToString()
                        });
                    }
                }
            }

            return income;
        }

        public static bool AddIncome(Income income)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Income 
                                    (source, date, amount, description) 
                                    VALUES (@source, @date, @amount, @description)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@source", income.Source);
                    cmd.Parameters.AddWithValue("@date", income.Date);
                    cmd.Parameters.AddWithValue("@amount", income.Amount);
                    cmd.Parameters.AddWithValue("@description", income.Description);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding income: " + ex.Message);
                return false;
            }
        }

        // Report generation method
        public static DataTable GenerateIncomeStatement(DateTime startDate, DateTime endDate)
        {
            DataTable result = new DataTable();

            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();

                // Validate dates are in the past
                if (startDate >= DateTime.Today || endDate >= DateTime.Today)
                {
                    throw new Exception("Invalid time selection. Dates must be in the past.");
                }

                string query = @"
                    SELECT 'Revenue' as Category, source as Description, date, amount 
                    FROM Income 
                    WHERE date BETWEEN @startDate AND @endDate
                    UNION ALL
                    SELECT 'Cost of Goods Sold' as Category, 'Supplier Invoices' as Description, date, amount 
                    FROM SupplierInvoices 
                    WHERE date BETWEEN @startDate AND @endDate
                    UNION ALL
                    SELECT 'Expenses' as Category, category as Description, date, amount 
                    FROM Expenses 
                    WHERE date BETWEEN @startDate AND @endDate
                    ORDER BY Category, date";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(result);
            }

            return result;
        }
    }
}
