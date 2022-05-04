using LinqLabs;
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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        NorthwindEntities dbcontext = new NorthwindEntities();
        private void button4_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView2.Columns.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };


            foreach (int i in nums)
            {
                string n = LargeSmall(i);

                if (treeView1.Nodes[n] == null)
                {
                    TreeNode node = null;
                    node = treeView1.Nodes.Add(n, n);
                    node.Nodes.Add(i.ToString());
                }
                else
                {
                    treeView1.Nodes[n].Nodes.Add(i.ToString());
                }
            }
        }

        private string LargeSmall(int i)
        {
            if (i < 5)
            {
                return "small";
            }
            else if (i < 9)
            {
                return "medium";
            }
            else
            {
                return "large";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by MyKey(f.Length)
                    into g
                    orderby g.Key ascending
                    select new
                    {
                        Size = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = files;
            //=========================================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Size}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.Size.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    string s2 = $"{item}({"Size = " + item.Length})";
                    node.Nodes.Add(item.ToString(), s2);
                }
            }
        }

        private string MyKey(long length)
        {
            if (length < 100000)
            {
                return "Small";
            }
            else if (length < 1000000)
            {
                return "Medium";
            }
            else
            {
                return "Large";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by f.CreationTime.Year
                    into g
                    orderby g.Key descending
                    select new
                    {
                        Year = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = files;
            //===============================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Year}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in dbcontext.Products.AsEnumerable()
                    group p by MyPrice(p.UnitPrice)
                    into g
                    select new
                    {
                        Mode = g.Key,
                        Count = g.Count(),
                        Group = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = this.dbcontext.Products.ToList();
            //================================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Mode}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Mode.ToString(), s);
                foreach (var item in group.Group)
                {
                    string s1 = $"{item.ProductName}({item.UnitPrice:c2})";
                    node.Nodes.Add(s1);
                }
            }
        }

        private string MyPrice(decimal? unitPrice)
        {
            if (unitPrice < 20)
            {
                return "LowerPrice";
            }
            else if (unitPrice < 50)
            {
                return "MediumPrice";
            }
            else
            {
                return "HigherPrice";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var q = from o in dbcontext.Orders
                    group o by o.OrderDate.Value.Year
                    into g
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = dbcontext.Orders.ToList();
            //===============================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Year}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    string s1 = $"{"OrderID =" + item.OrderID}";
                    node.Nodes.Add(s1);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = from o in dbcontext.Orders
                    group o by new { o.OrderDate.Value.Year, o.OrderDate.Value.Month }
                    into g
                    select new
                    {
                        YearMonth = g.Key.Year + "年" + g.Key.Month + "月",
                        //Year =g.Key.Year,
                        //Month =g.Key.Month,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = dbcontext.Orders.ToList();
            //====================================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.YearMonth}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.YearMonth.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    string s1 = $"{"OrderID =" + item.OrderID}";
                    node.Nodes.Add(s1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView2.Columns.Clear();
            var q = from p in dbcontext.Order_Details.AsEnumerable()
                    orderby p.UnitPrice ascending
                    select new
                    {
                        p.UnitPrice,
                        p.Quantity,
                        p.Discount,
                        TotalPrice= p.UnitPrice*p.Quantity*(decimal)(1-p.Discount) 
                    };

            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = this.dbcontext.Order_Details.ToList();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView2.Columns.Clear();
            var q = from or in this.dbcontext.Order_Details.AsEnumerable()
                    group or by
                    new { or.Order.EmployeeID, or.Order.Employee.FirstName, or.Order.Employee.LastName }
                    into g
                    orderby g.Sum(n => n.UnitPrice * n.Quantity * (decimal)(1 - n.Discount)) descending
                    select new
                    {
                        Name = g.Key.FirstName + g.Key.LastName,
                        TotalPrice = $"{g.Sum(n => n.UnitPrice * n.Quantity * (decimal)(1 - n.Discount)):c2}"
                    };

            this.dataGridView1.DataSource = q.Take(5).ToList();
            this.dataGridView2.DataSource = this.dbcontext.Order_Details.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView2.Columns.Clear();
            var q = from p in this.dbcontext.Products
                     orderby p.UnitPrice descending
                    select new
                    {
                        p.Category.CategoryName,
                        p.UnitPrice
                    };
            this.dataGridView1.DataSource = q.Take(5).ToList();
            this.dataGridView2.DataSource = this.dbcontext.Products.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = from p in dbcontext.Products
                    select p.UnitPrice;
            bool result;
            result = q.Any(n => n > 300);
            MessageBox.Show("results=" + result);
        }
    }
}
