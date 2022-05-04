using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        bool flag = true;
        int TAKE_OLD;
        int SKIP = 0;
        public Frm作業_1()
        {
            InitializeComponent();

            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            LoadToCombobox();

        }
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (SKIP >= this.nwDataSet1.Products.Rows.Count)
            {
                return;
            }
            if (flag == false)
            {
                flag = true;
                SKIP += TAKE_OLD;
            }
            int TAKE_NOW = int.Parse(textBox1.Text);
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(SKIP).Take(TAKE_NOW).ToList();

            SKIP += TAKE_NOW;
            TAKE_OLD = TAKE_NOW;

            //Top 10 Skip(10)
            //Distinct()
        }
        private void button12_Click(object sender, EventArgs e)
        {
            int TAKE_NOW = int.Parse(textBox1.Text);
            if (flag == true)
            {
                flag = false;
                SKIP -= TAKE_OLD;
            }
            TAKE_OLD = TAKE_NOW;
            SKIP -= TAKE_NOW;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(SKIP).Take(TAKE_NOW).ToList();
            if (SKIP < 0)
            {
                SKIP = 0;
            }

            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            //this.dataGridView1.DataSource = files;
            var q = from n in files
                    where n.Extension == ".log"
                    select n;
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    where n.CreationTime.Year == 2019
                    orderby n.CreationTime
                    select n;

            this.dataGridView1.DataSource = q.ToList();

            //this.dataGridView1.DataSource = files;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            //大檔案:>50000
            var q = from n in files
                    where n.Length > 50000
                    orderby n.Length
                    select n;
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            this.dataGridView2.DataSource = this.nwDataSet1.Order_Details;

            ////todo
            ////IEnumerable<global::LinqLabs.NWDataSet.OrdersRow>
            //var q = from n in this.nwDataSet1.Orders
            //        select n;
            //this.dataGridView1.DataSource = q.ToList();
        }

        private void LoadToCombobox()
        {
            comboBox1.Text = "請選擇年份";

            var q = from n in this.nwDataSet1.Orders
                    select n.OrderDate.Year;

            foreach (int n in q)
            {
                if (comboBox1.Items.Contains(n))
                {
                    continue;
                }
                else
                {
                    this.comboBox1.Items.Add(n);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "請選擇年份")
            {
                MessageBox.Show("請選擇年份!");
            }
            else
            {
                var q = from n in this.nwDataSet1.Orders
                        where n.OrderDate.Year == (int)comboBox1.SelectedItem
                        select n;
                this.dataGridView1.DataSource = q.ToList();

                var v = from Or in this.nwDataSet1.Orders
                        join Ord in this.nwDataSet1.Order_Details
                        on Or.OrderID equals Ord.OrderID
                        where Or.OrderDate.Year.ToString() == comboBox1.Text
                        select Or;
                this.dataGridView2.DataSource = v.ToList();
            }
        }
    }
}
