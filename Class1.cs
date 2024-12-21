using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Демо.Экз
{
    public class OrderItem
    {
        public string ProductID {  get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ManuFacturer { get; set; }
        public string Price { get; set; }
        public Image ProductImage { get; set; }
        public int Quantity { get; set; }

        // Конструктор для инициализации товара
        public OrderItem(string productID,string productName, string productDescription,string manuFacturer, string price, Image productImage, int quantity)
        {
            ProductID = productID;
            ProductImage = productImage;
            ProductName = productName;
            ProductDescription = productDescription;
            ManuFacturer = manuFacturer;
            Price = price;
            Quantity = quantity;
           
        }
    }
}
