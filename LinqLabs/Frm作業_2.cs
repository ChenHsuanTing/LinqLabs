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

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(this.advDataSet1.ProductPhoto);
            LoadYearToCombobox();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.advDataSet1.ProductPhoto
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime datefrom = dateTimePicker1.Value;
            DateTime dateto = dateTimePicker2.Value;
            var q = this.advDataSet1.ProductPhoto.Where
                (p => p.ModifiedDate > datefrom && p.ModifiedDate < dateto);
            this.dataGridView1.DataSource = q.ToList();
        }

        private void LoadYearToCombobox()
        {
            var q = from p in this.advDataSet1.ProductPhoto
                    orderby p.ModifiedDate.Year ascending
                    select p.ModifiedDate.Year;
            var Myear = q.Distinct();
            foreach(var year in Myear)
            {
                this.comboBox3.Items.Add(year);
            }
            this.comboBox3.Text = "請選擇年度";
            this.comboBox2.Text = "請選擇季度";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if(comboBox3.Text== "請選擇年度")
            {
                MessageBox.Show("請選擇年度!");
            }
            else
            {
                string year = comboBox3.Text;
                var q = this.advDataSet1.ProductPhoto.Where
                    (p => p.ModifiedDate.Year.ToString() == year);
                this.dataGridView1.DataSource = q.ToList();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "請選擇年度")
            {
                MessageBox.Show("請選擇年度!");
            }
            else if(comboBox2.Text == "請選擇季度")
            {
                MessageBox.Show("請選擇季度!");
            }
            else
            {
                string q = comboBox2.Text;
                if (q == "第一季")
                {
                    Quarter(1, 3);
                }
                else if (q == "第二季")
                {
                    Quarter(4, 6);
                }
                else if (q == "第三季")
                {
                    Quarter(7, 9);
                }
                else
                {
                    Quarter(10, 12);
                }
            }
        }
        void Quarter (int start ,int end)
        {
            string year = comboBox3.Text;
            var q = this.advDataSet1.ProductPhoto.
                Where(p => p.ModifiedDate.Year.ToString() == year 
                    &&p.ModifiedDate.Month >= start 
                   && p.ModifiedDate.Month <= end);
            this.dataGridView1.DataSource = q.ToList();
            lblMaster.Text = "Master = " + q.ToList().Count + "筆";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] showphoto = (byte[])this.dataGridView1.CurrentRow.Cells[3].Value;
            pictureBox1.Image = Image.FromStream(new MemoryStream(showphoto));
        }
    }
}

