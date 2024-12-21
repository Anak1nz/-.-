using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Демо.Экз.Properties;

namespace Демо.Экз
{
    public partial class Form1 : Form
    {
        public List<OrderItem> orderItems = new List<OrderItem>();
        private DataGridView dataGridViewOrder;
        public Form1()
        {
            InitializeComponent();
            Table();
            CreateContextMenu();
        }
       
        private void Table()
        {
            // Пример добавления строк с фото и данными товара
            dataGridView1.Rows.Add(1,null, "Хлеб", "Свеже выпеченный", "Реж-Хлеб", 50, 38);
            dataGridView1.Rows.Add(2,null, "Молоко", "Натурально", "Ирбит", 75, 20);
            dataGridView1.Rows.Add(3, null, "Кефир", "Вкусный", "Ирбит", 50, 15);
            dataGridView1.Rows.Add(4, null, "Сыр", "Твердый", "Режевская сыроварня", 200, 7);
            dataGridView1.Rows.Add(5, null, "Фарш", "Натуральный", "Мясокомбинат", 300, 4);
            dataGridView1.Rows.Add(6, null, "Филе куринное", "Куринное", "Мясокомбинат", 400, 5);

            AddImageToRow(0, @"C:\Users\0_206_3\Desktop\Новая папка\Демо.Экз\Resources\Хлеб.jpg");
            AddImageToRow(1, @"C:\Users\0_206_3\Desktop\Новая папка\Демо.Экз\Resources\Молоко.png");   // Вставляем изображение в строку 1
            AddImageToRow(2, @"C:\Users\0_206_3\Desktop\Новая папка\Демо.Экз\Resources\кефир.jpeg");  // Вставляем изображение в строку 2
            AddImageToRow(3, @"C:\Users\0_206_3\Desktop\Новая папка\Демо.Экз\Resources\сыр.jpg"); // Вставляем изображение в строку 3
            AddImageToRow(4, @"C:\Users\0_206_3\Desktop\Новая папка\Демо.Экз\Resources\фарш.jpg");   // Вставляем изображение в строку 4
            AddImageToRow(5, @"C:\Users\0_206_3\Desktop\Новая папка\Демо.Экз\Resources\филе.jpeg");
        }

        private void AddImageToRow(int rowIndex, string imageFileName)
        {
            try
            {
                Image image = Image.FromFile(imageFileName);
                // Преобразуем изображение в нужный размер (например, 50x50 пикселей)
                Image resizedImage = new Bitmap(image, new Size(50, 50));

                // Вставляем изображение в ячейку столбца "Фото" для указанной строки
                dataGridView1.Rows[rowIndex].Cells["Column1"].Value = resizedImage; // "Фото" — имя столбца для изображений
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
            }
        }
        private ContextMenuStrip contextMenu;
        private void CreateContextMenu()
        {
            contextMenu = new ContextMenuStrip();

            // Создаем пункт меню "Добавить к заказу"
            ToolStripMenuItem addToOrderMenuItem = new ToolStripMenuItem("Добавить к заказу");
            addToOrderMenuItem.Click += AddToOrderMenuItem_Click;

            // Добавляем пункт в контекстное меню
            contextMenu.Items.Add(addToOrderMenuItem);

            // Привязываем контекстное меню к DataGridView
            dataGridView1.ContextMenuStrip = contextMenu;

            // Обработчик события правого клика мыши на DataGridView
            dataGridView1.MouseUp += dataGridView1_MouseUp;


        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Получаем индекс строки, на которой был клик
                var hitTest = dataGridView1.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0) // Если клик по строке
                {
                    // Выбираем строку, по которой кликнули правой кнопкой мыши
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTest.RowIndex].Selected = true;

                    // Показываем контекстное меню
                    contextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                }
            }
        }
        private void AddToOrderMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем выбранную строку
            var selectedRow = dataGridView1.SelectedRows[0];

            // Пример добавления товара в заказ
            var productId = selectedRow.Cells["ColumnID"].Value.ToString();
            string productName = selectedRow.Cells["Column2"].Value.ToString();
            string productDescription = selectedRow.Cells["Column3"].Value.ToString();
            string ManuFacturer = selectedRow.Cells["Column4"].Value.ToString();
            string productPrice = selectedRow.Cells["Column5"].Value.ToString();
            Image  productImage  = selectedRow.Cells["Column1"].Value as Image;
          //  int productQuantity = (int)selectedRow.Cells["ColumnAll"].Value;

            bool productExistsOrder = false;
            foreach(var item in orderItems)
            {
                if(item.ProductID == productId)
                {
                    item.Quantity += 1;
                    productExistsOrder = true;
                    break;
                }
            }

            if (!productExistsOrder)
            {
                OrderItem orderItem = new OrderItem(productId, productName, productDescription, ManuFacturer, productPrice, productImage, 1);
                orderItems.Add(orderItem);   
            }
         
            button2.Visible = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
           
            Form2 form2 = new Form2(orderItems);
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isDeliveryInThreeDays = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Получаем количество товара из столбца "Кол-во" 
                if (row.Cells["ColumnAll"].Value != null && int.TryParse(row.Cells["ColumnAll"].Value.ToString(), out int quantity))
                {
                    // Проверяем, если количество товара больше 3
                    if (quantity > 3)
                    {
                        isDeliveryInThreeDays = true;
                        break;  // Если хотя бы один товар с количеством больше 3, выходим из цикла
                    }
                }
            }
            string deliveryMessage = isDeliveryInThreeDays ? "3 дня" : "6 дней";
            Form3 form3 = new Form3(orderItems, deliveryMessage);
            form3.ShowDialog();
        }
    }
}
