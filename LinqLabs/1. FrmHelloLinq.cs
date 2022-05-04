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
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //public interface IEnumerable<T>
            //    System.Collections.Generic 的成員
            //摘要:
            //公開支援指定類型集合上簡單反覆運算的列舉值。
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //syntax Sugar
            foreach(int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            
            //無法在此範圍宣告名為 'a' 的區域變數或參數，因為該名稱已用於封入區域變數範圍
            //int a = 999;
            //foreach(int a in nums)
            //{
                
            //}

            //=========================================

            this.listBox1.Items.Add("==============================");
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            List<int> list = new List<int>(){ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            //syntax sugar
            foreach(int n in list)
            {
                this.listBox1.Items.Add(n);
            }
            //==========================================
            int n1 = 100;
            int n2 = 200;
            this.listBox1.Items.Add("=============================");
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Step 1: define Data Source
            // Step 2: define query
            // Step 3: excute query
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where (n<3||n>=8)
                                 //where (n<=8&&n>=5)&&(n%2==0)
                                 select n;
            foreach(int n in q)
            {
                this.listBox1.Items.Add(n);
            }
                              
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Step 1: define Data Source
            // Step 2: define query
            // Step 3: excute query
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }
        private bool IsEven(int n)
        {
            //if (n % 2 == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return n % 2 == 0 ? true : false;
            //return n % 2 == 0;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = from n in nums
                                   where n > 5
                                   select new Point(n,n*n);
            //execute query
            foreach(Point pt in q)
            {
                this.listBox1.Items.Add(pt.X +","+ pt.Y);
            }

            //========================================
            //execute query
            List<Point> list = q.ToList();
            this.dataGridView1.DataSource = list;

            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 3;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            String[] WORDS = { "AAA", "Apple", "pineApple", "xxxapple" };

            IEnumerable<string> q = from w in WORDS
                    where w.ToUpper().Contains("APPLE")&& w.Length>5
                    select w;

            foreach(string s in q)
            {
                this.listBox1.Items.Add(s);
            }
            //==========================================
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Products;

            IEnumerable<global::LinqLabs.NWDataSet.ProductsRow> q = from n in this.nwDataSet1.Products
                    where (! n.IsUnitPriceNull() && n.UnitPrice > 30&& n.UnitPrice<50)&& n.ProductName.StartsWith("M")
                    select n;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Orders;

            var q = from n in this.nwDataSet1.Orders
                    where n.OrderDate.Year == 1997 && n.OrderDate.Month<=3
                          orderby n.OrderDate descending
                    select n;

            this.dataGridView1.DataSource = q.ToList();

                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var q = from n in nums
            //        where n > 5
            //        select n;

            //IEnumerator<int> q = nums.Where(.......delegate......) select(.....);

        }
    }
}
