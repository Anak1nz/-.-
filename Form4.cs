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
    public partial class Form4 : Form
    {
        private bool PasswordVisible = false;
        public Form4()
        {
            InitializeComponent();
            txtboxPassword.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Login = txtboxLogin.Text; // получаем значение которые вводит пользватель
            string Password = txtboxPassword.Text;
            if (txtboxLogin.Text == "" || txtboxPassword.Text == "")
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else
            {
                Form1 form1 = new Form1();
                form1.ShowDialog();
            }
        }
        private void ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (PasswordVisible)
            {
                txtboxPassword.PasswordChar = '*';
            }
            else
            {
                txtboxPassword.PasswordChar = '\0';
            }
            PasswordVisible = !PasswordVisible;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtboxLogin.Clear();
            txtboxPassword.Clear();
        }
    }
}
