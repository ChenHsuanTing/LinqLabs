using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList arrlist = new System.Collections.ArrayList();
            arrlist.Add(1);
            arrlist.Add(2);
            arrlist.Add(3);
            arrlist.Add(4);

            var q = from n in arrlist.Cast<int>()
                    where n > 2
                    select new { 比2大 = n };//or{n}就好了
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q = (from n in this.nwDataSet1.Products
                     orderby n.UnitsInStock descending
                     select n).Take(5);

            this.dataGridView1.DataSource = q.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.listBox1.Items.Add("Sum=" + nums.Sum());
            this.listBox1.Items.Add("Max=" + nums.Max());
            this.listBox1.Items.Add("Min=" + nums.Min());
            this.listBox1.Items.Add("Avg=" + nums.Average());
            this.listBox1.Items.Add("Count=" + nums.Count());
            this.listBox1.Items.Add("EvenMin=" + nums.Where(n => n % 2 == 0).Min());
            this.listBox1.Items.Add("EvenMax=" + nums.Where(n => n % 2 == 0).Max());
            //====================================================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.listBox1.Items.Add("Sum UnitInStock = " + this.nwDataSet1.Products.Sum(p => p.UnitsInStock));
            this.listBox1.Items.Add("Max UnitInStock = " + this.nwDataSet1.Products.Max(p => p.UnitsInStock));
            this.listBox1.Items.Add("Min UnitInStock = " + this.nwDataSet1.Products.Min(p => p.UnitsInStock));
            this.listBox1.Items.Add("Average UnitInStock = " + $"{this.nwDataSet1.Products.Average(p => p.UnitsInStock):c2}");


        }
    }
}