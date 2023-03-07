using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ВычМат_Лаба1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="" || textBox2.Text=="") { MessageBox.Show("Заполните поля", "Ошибка"); return; }
            float a = (float)Convert.ToDouble(textBox1.Text);
            float b = (float)Convert.ToDouble(textBox2.Text);
            if(a>b)
            {
                float c = a;
                a = b;
                b = c;
            }
            if (radioButton1.Checked==true)
            {
                float solution = FindSolutions.half_div_method(a, b);

                label3.Text = $"Корень уравнения равен {solution}";
            }
            else if (radioButton3.Checked == true)
            {
                float solution = FindSolutions.newton_method(a, b);

                label3.Text = $"Корень уравнения равен \n{solution}";
            }
            else
            {
                float solution = FindSolutions.simple_iter_method(a, b);

                label3.Text = $"Корень уравнения равен {solution}";
                //MessageBox.Show($"{FindSolutions.e}");

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FindSolutions.e /= 10;
        }
    }
}
