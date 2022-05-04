using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //設定來源
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            //設定查詢
            //IEnumerable<IGrouping<int,int>> q = from n in nums
                                                //group n by (n%2);//僅顯示出key

            IEnumerable<IGrouping<string, int>> q = from n in nums
                                                    group n by (n % 2==0)?"偶數":"奇數";//僅顯示出key
            //var q = from n in nums
            //        group n by (n % 2);
            //執行查循
            this.dataGridView1.DataSource = q.ToList();

            //====================================================
            //Treeview
            foreach(var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add(group.Key.ToString());
                foreach(var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            //=====================================================
            //Listview
            foreach(var group in q)
            {
                ListViewGroup lvg = this.listView1.Groups.Add(group.Key.ToString(), group.Key.ToString());
                foreach(var item in group)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                    //內迴圈的值回傳定義的lvg
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = from n in nums
                    group n by (n % 2 == 0) ? "偶數" : "奇數" //僅顯示出key
                    into g
                    select new
                    {
                        Mykey = g.Key,
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyAvg = g.Average(),
                        MyCount = g.Count(),
                        MyGroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();
            ///===========================================
            foreach(var group in q)
            {
                string s = $"{group.Mykey}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.Mykey.ToString(),s);
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=============================================
            foreach (var group in q)
            {
                string s = $"{group.Mykey}({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.Mykey.ToString(),s);
                foreach (var item in group.MyGroup)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                    //內迴圈的值回傳定義的lvg
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = from n in nums
                    group n by  MyKey(n)
                    into g
                    select new
                    {
                        Mykey = g.Key,
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyAvg = g.Average(),
                        MyCount = g.Count(),
                        MyGroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();
            ///===========================================
            foreach (var group in q)
            {
                string s = $"{group.Mykey}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.Mykey.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=============================================
            foreach (var group in q)
            {
                string s = $"{group.Mykey}({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.Mykey.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                    //內迴圈的值回傳定義的lvg
                }
            }
            //=============================================
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "Mykey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "Mykey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private string MyKey(int n)
        {
            if (n < 5)
                return "Small";
            else if (n < 10)
                return "Medium";
            else
                return "Large";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files;

            var q = from n in files
                    group n by n.Extension into g
                    orderby g.Count() descending
                    select new
                    {
                        副檔名 = g.Key,
                        MyCount = g.Count(),
                        //Mygroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            var q = from o in this.nwDataSet1.Orders
                    group o by o.OrderDate.Year into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count()
                    };
            this.dataGridView1.DataSource = q.ToList();

            //===========================================
            int count = (from o in this.nwDataSet1.Orders
                     where o.OrderDate.Year == 1997
                     select o).Count();
            MessageBox.Show("Count 1997= " +count);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files;

            int count = (from n in files
                         let s=n.Extension
                         where s==".exe"
                         //where n.Extension == ".exe"
                     select n).Count();

            MessageBox.Show("Count .exe = " + count);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "This is a book. This is a pen. This is an apple.";

            char[] chars = { ' ', ',', '?', '.' };
            string[] words= s.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            //StringSplitOptions.RemoveEmptyEntries 可以移除字串中的空值

            var q = from w in words
                    group w by w/*.ToUpper()*/
                    into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count()
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 3, 5, 7, 9, 11, 1 };
            int[] nums2 = { 1, 2, 3, 4, 5, 6, 8 };

            //集合運算子 Distinct/Union/Intersect/Except
            //========================================
            IEnumerable<int> q;
            q = nums1.Intersect(nums2);//交集
            q = nums1.Union(nums2);//聯集
            q = nums1.Distinct();//消除相同

            //切割運算子 Take/Skip
            //========================================

            //數量運算子 Any/All/Contains
            //========================================
            bool result;
            result = nums1.Any(n => n > 3);
            result = nums1.All(n => n >= 1);

            //單一元素運算子:
            //First/Last/Single/ElementAt/
            //FirstOrDefault/LastOrDefault/ElementAtOrDefault
            int n1;
            n1 = nums1.First();
            n1 = nums1.Last();
            //n1 = nums1.ElementAt(13);超過索引值會失敗
            n1 = nums1.ElementAtOrDefault(13);

            //產生作業 Range/Repeat
            var q1 = Enumerable.Range(1, 1000).Select(n=>new { N=n });//todo
            this.dataGridView1.DataSource = q1.ToList();

            //==========================================
            var q2 = Enumerable.Repeat(60, 1000).Select(n => new { n });
            this.dataGridView2.DataSource = q2.ToList();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q = from p in this.nwDataSet1.Products
                    group p by p.CategoryID
                    into g
                    select new
                    {
                        CategoryID = g.Key,
                        MyAvg = $"{g.Average(p => p.UnitPrice):c1}"
                    };
            this.dataGridView1.DataSource = q.ToList();

            //=======================================================
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            var q1 = from c in this.nwDataSet1.Categories
                     join p in this.nwDataSet1.Products
                     on c.CategoryID equals p.CategoryID
                     group p by c.CategoryName
                    into g
                     select new
                     {
                         CategoryName = g.Key,
                         MyAvg =$"{g.Average(p => p.UnitPrice):c2}"
                     };
            this.dataGridView2.DataSource = q1.ToList();

            
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }
    }
}
