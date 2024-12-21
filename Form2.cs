using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Демо.Экз
{
    public partial class Form2 : Form
    {
        private List<OrderItem> orderItems;

        // Конструктор, принимающий список заказов
        public Form2(List<OrderItem> items)
        {
            InitializeComponent();
            this.orderItems = items; // Сохраняем переданные данные
            DisplayOrderItems(); // Метод для отображения данных на форме
        }


        private void DisplayOrderItems()
        {
            dataGridView1.Rows.Clear();

            // Пример: вывод данных в DataGridView
            foreach (var item in orderItems)
            {
                // Здесь мы добавляем данные в DataGridView
                dataGridView1.Rows.Add(item.ProductID,item.ProductImage, item.ProductName, item.ProductDescription, item.ManuFacturer, item.Price, item.Quantity);
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем индекс строки, которую нужно удалить
                var selectedRow = dataGridView1.SelectedRows[0];

                // Получаем ID товара из выбранной строки
                var productId = selectedRow.Cells["ColumnID"].Value.ToString();

                // Находим товар в списке orderItems и удаляем его
                var itemToRemove = orderItems.FirstOrDefault(item => item.ProductID == productId);
                if (itemToRemove != null)
                {
                    orderItems.Remove(itemToRemove); // Удаляем товар из корзины
                }

                // Удаляем строку из DataGridView
                dataGridView1.Rows.Remove(selectedRow);

                // Возможно, нужно обновить отображение товаров в корзине
                DisplayOrderItems();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.");
            }
        }
    }
}
