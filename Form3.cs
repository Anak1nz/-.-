using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Демо.Экз
{
    public partial class Form3 : Form
    {
        private List<OrderItem> orderItems;
        private string deliveryMessage;
        public Form3(List<OrderItem> items, string message)
        {
            InitializeComponent();
            orderItems = items;
            deliveryMessage = message;
            DisplayOrderItems();
            label3.Text = DateTime.Now.ToString("dd.MM.yyyy");
            Random random = new Random();
            label9.Text = random.Next(1000,2000).ToString();
            Random random2 = new Random();
            label10.Text = random2.Next(100,999).ToString();
            UpdateTotalPrice();
            label13.Text = deliveryMessage;
        }

        private void DisplayOrderItems()
        {
            dataGridView1.Rows.Clear();

            // Пример: вывод данных в DataGridView
            foreach (var item in orderItems)
            {
                // Здесь мы добавляем данные в DataGridView
                dataGridView1.Rows.Add(item.ProductID, item.ProductImage, item.ProductName, item.ProductDescription, item.ManuFacturer, item.Price, item.Quantity);
            }
            label13.Text = deliveryMessage;// кол-во дней доставки
        }
        private void UpdateTotalPrice()
        {
            int totalPrice = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Column5"].Value != null && int.TryParse(row.Cells["Column5"].Value.ToString(), out int price))
                {
                    totalPrice += price;
                }
            }

            // Обновляем текст в label
            label11.Text = $"{totalPrice} Рублей";
        }




    }
}
