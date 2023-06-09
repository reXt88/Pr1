using Pr1.ModelEF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Pr1.ModelEF;

namespace Pr1
{
    public partial class FormShowMot : Form
    {
        public FormShowMot()
        {
            InitializeComponent();
        }
        public static Model1 db = new Model1();
        private void FormShowMot_Load(object sender, EventArgs e)
        {
            tableMotorbikeBindingSource.DataSource  = db.Table_Motorbike.ToList();
            if (db.Table_Motorbike.ToList().Count == 0) return;
            int ID = (int)dataGridView2.CurrentRow.Cells[0].Value;
            pictureBox1.Image = Image.FromFile($@"Pictures\{db.Table_Motorbike.Find(ID).Picture}");
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            Forms.FormAddMot f = new Forms.FormAddMot();
            this.Visible = false;
            f.Show();
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            if (db.Table_Motorbike.ToList().Count == 0)
            {
                MessageBox.Show("Данные отсутствуют");
                return;
            }
            Table_Motorbike curMoto = db.Table_Motorbike.Find((int)dataGridView2.CurrentRow.Cells[0].Value);
            DialogResult res = MessageBox.Show($@"Вы действительно хотите удалить объект с ID - {curMoto.ID}", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                try
                {
                    db.Table_Motorbike.Remove(curMoto);
                    db.SaveChanges();
                    File.Delete($@"Pictures\{curMoto.Picture}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    tableMotorbikeBindingSource.DataSource = db.Table_Motorbike.ToList();
                    pictureBox1.Image = null;
                }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.Table_Motorbike.ToList().Count == 0) return;
                int ID = (int)dataGridView2.CurrentRow.Cells[0].Value;
                pictureBox1.Image = Image.FromFile($@"Pictures\{db.Table_Motorbike.Find(ID).Picture}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
