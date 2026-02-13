using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SimpleInvoice
{
    // SIMPLE CLASSES - Just what we need
    public class Customer { public int ID { get; set; } public string Name { get; set; } }
    public class Item { public int ID { get; set; } public string Name { get; set; } public decimal Price { get; set; } }
    public class Invoice { public int ID { get; set; } public string Number { get; set; } public int CustomerID { get; set; } public decimal Total { get; set; } }
    public class InvoiceItem { public int ID { get; set; } public int ItemID { get; set; } public int Quantity { get; set; } public decimal Price { get; set; } }

    // DATABASE HELPER
    public class DB
    {
        const string CS = "server=.;database=SimpleInvoice;trusted_connection=true;trustservercertificate=true";
        
        public static List<Customer> GetCustomers()
        {
            var list = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Customers", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(new Customer { ID = (int)dr[0], Name = dr[1].ToString() });
            }
            return list;
        }

        public static List<Item> GetItems()
        {
            var list = new List<Item>();
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name, Price FROM Items", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(new Item { ID = (int)dr[0], Name = dr[1].ToString(), Price = (decimal)dr[2] });
            }
            return list;
        }

        public static int CreateInvoice(int customerID, decimal total)
        {
            int invoiceNum = GetNextInvoiceNumber();
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    $"INSERT INTO Invoices(Number, CustomerID, Total) VALUES('INV-{invoiceNum}', {customerID}, {total}); SELECT SCOPE_IDENTITY();", 
                    conn);
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public static void AddInvoiceItem(int invoiceID, int itemID, int qty, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    $"INSERT INTO InvoiceItems(InvoiceID, ItemID, Quantity, Price) VALUES({invoiceID}, {itemID}, {qty}, {price})", 
                    conn);
                cmd.ExecuteNonQuery();
            }
        }

        static int GetNextInvoiceNumber()
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ID), 0) + 1 FROM Invoices", conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public static List<Invoice> GetInvoices()
        {
            var list = new List<Invoice>();
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Number, CustomerID, Total FROM Invoices ORDER BY ID DESC", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(new Invoice { ID = (int)dr[0], Number = dr[1].ToString(), CustomerID = (int)dr[2], Total = (decimal)dr[3] });
            }
            return list;
        }
    }
}
