using MongoDbOrder.Entities;
using MongoDbOrder.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MongoDbOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        OrderTransaction orderTransaction = new OrderTransaction();

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var order = new Order
            {
                CustomerName = txtAd.Text,
                District = txtIlce.Text,
                City = txtSehir.Text,
                TotalPrice = Convert.ToDecimal(txtToplamFiyat.Text)
            };

            orderTransaction.AddOrder(order);
            MessageBox.Show("Eklendi.","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            List<Order> orders = orderTransaction.GetAllOrders();
            dataGridView1.DataSource = orders;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string orderId = txtId.Text;
            orderTransaction.DeleteOrder(orderId);
            MessageBox.Show("silindi.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            var order = new Order
            {
                OrderId = txtId.Text,
                CustomerName = txtAd.Text,
                District = txtIlce.Text,
                City = txtSehir.Text,
                TotalPrice = Convert.ToDecimal(txtToplamFiyat.Text)
            };

            orderTransaction.UpdateOrder(order);
            MessageBox.Show("Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            string orderId = txtId.Text;
            Order order = orderTransaction.GetOrder(orderId);
            if (order == null)
            {
                MessageBox.Show("Şipariş Bulunamadı", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                List<Order> orders = new List<Order>();
                orders.Add(order);
                dataGridView1.DataSource = orders;
            }
           
        }
    }
}
