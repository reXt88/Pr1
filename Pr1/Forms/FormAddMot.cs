using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Pr1.ModelEF;

namespace Pr1.Forms
{
    public partial class FormAddMot : Form
    {
        public FormAddMot()
        {
            InitializeComponent();
        }
        private string pictureName;
        private List<Table_Motorbike> vsTable_MotorBike = FormShowMot.db.Table_Motorbike.ToList();

        private int FLplus1()
        {
            int max = 0;
            foreach (Table_Motorbike tb in vsTable_MotorBike)
                if (max < tb.ID) max = tb.ID;
            return ++max;
        }
        private void FormAddMot_Load(object sender, EventArgs e)
        {
            List<string> dictBrand = new List<string>();
            foreach (Table_Motorbike tb in vsTable_MotorBike)
                dictBrand.Add(tb.Brand);
            dictBrand = dictBrand.Distinct().ToList();
            comboBox1.DataSource = dictBrand;
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            FormAddMot f = new FormAddMot();
            f.Visible = false;
            this.Close();
            FormShowMot w = new FormShowMot();
            w.Visible = true;
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            FLplus1();
            if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            try
            {
                Convert.ToInt32(textBox4.Text);
                Convert.ToInt32(textBox5.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("В полях Л/С и пробег могут быть только целочисленные данные");
                return;
            }
            try
            {
                Convert.ToSingle(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("В поле Цена, могут быть только числа с плавающей точкой");
            }
            if (!File.Exists(pictureName))
            {
                MessageBox.Show("Невозможно найти файл");
                return;
            }
            Table_Motorbike moto = new Table_Motorbike();
            moto.ID = FLplus1();
            moto.Brand = comboBox1.Text;
            moto.Model = textBox2.Text;
            moto.Price = Convert.ToSingle(textBox3.Text);
            moto.Horsepower = Convert.ToInt32(textBox4.Text);
            moto.Mileage = Convert.ToInt32(textBox5.Text);
            moto.Picture = $@"P{FLplus1()}{Path.GetExtension(pictureName)}";
            File.Copy(pictureName, $@"Pictures\P{FLplus1()}{Path.GetExtension(pictureName)}");
            try
            {
                FormShowMot.db.Table_Motorbike.Add(moto);
                FormShowMot.db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Данные успешно добавлены");
            FormShowMot f = new FormShowMot();
            f.Visible = true;
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Фалйы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            DialogResult result = openFileDialog.ShowDialog();
            if (DialogResult.OK == result)
            {
                pictureName = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //    if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != ',') ;
            //    e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46) ;
            //e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46) ;
            //e.Handled = true;
        }
    }
}
