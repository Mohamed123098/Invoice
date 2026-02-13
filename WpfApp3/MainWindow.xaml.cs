using System;
using System.Collections.Generic;
using System.Windows;

namespace SimpleInvoice
{
    public partial class MainWindow : Window
    {
        List<Customer> customers;
        List<Item> items;
        List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            // Load customers
            customers = DB.GetCustomers();
            foreach (var c in customers)
                CustomerCombo.Items.Add(c.Name);

            // Load items
            items = DB.GetItems();
            foreach (var i in items)
                ItemCombo.Items.Add(i.Name + " ($" + i.Price + ")");
        }

        void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ItemCombo.SelectedIndex < 0) { MessageBox.Show("Select an item"); return; }
            if (!int.TryParse(QuantityBox.Text, out int qty) || qty <= 0) { MessageBox.Show("Enter valid quantity"); return; }

            var item = items[ItemCombo.SelectedIndex];
            invoiceItems.Add(new InvoiceItem { ItemID = item.ID, Quantity = qty, Price = item.Price });

            ItemsGrid.Items.Add(new { ItemName = item.Name, Quantity = qty, Price = item.Price, Total = qty * item.Price });
            UpdateTotal();
        }

        void UpdateTotal()
        {
            decimal total = 0;
            foreach (var ii in invoiceItems)
                total += ii.Quantity * ii.Price;
            TotalLabel.Text = total.ToString("C");
        }

        void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerCombo.SelectedIndex < 0) { MessageBox.Show("Select a customer"); return; }
            if (invoiceItems.Count == 0) { MessageBox.Show("Add items first"); return; }

            decimal total = 0;
            foreach (var ii in invoiceItems)
                total += ii.Quantity * ii.Price;

            int customerID = customers[CustomerCombo.SelectedIndex].ID;
            int invoiceID = DB.CreateInvoice(customerID, total);

            foreach (var ii in invoiceItems)
                DB.AddInvoiceItem(invoiceID, ii.ItemID, ii.Quantity, ii.Price);

            MessageBox.Show("Invoice saved!");
            invoiceItems.Clear();
            ItemsGrid.Items.Clear();
            ItemCombo.SelectedIndex = -1;
            QuantityBox.Text = "1";
            TotalLabel.Text = "$0.00";
        }

        void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            var invoices = DB.GetInvoices();
            string msg = "Invoices:\n\n";
            foreach (var inv in invoices)
                msg += $"Invoice {inv.Number}: ${inv.Total:F2}\n";
            MessageBox.Show(msg);
        }
    }
}
