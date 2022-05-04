using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            //this.dbContext.Database.Log = Console.Write;
            //透過輸出去看他建立T-SQL的過程
        }


        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {

            var q = from n in dbContext.Products
                    where n.UnitPrice > 30
                    select n;

            this.dataGridView1.DataSource = q.ToList();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();
            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Sales_by_Year(new DateTime(1997, 1, 1), DateTime.Now).ToList();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //嘗試將totalprice格式化
            //發生錯誤-System.NotSupportedException: 'LINQ to Entities 無法辨識方法'
            //解決方法-在後面加上 AsEnumerable
            var q = from p in this.dbContext.Products.AsEnumerable()
                    orderby p.UnitsInStock descending, p.ProductID ascending
                    select new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        TotalPrice = $"{p.UnitsInStock * p.UnitPrice:c2}"
                    };
                  
            this.dataGridView1.DataSource = q.ToList();
            //==============================================================
            var q2 = this.dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenByDescending(p => p.ProductID);
            this.dataGridView2.DataSource = q2.ToList();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    select new
                    {p.CategoryID,p.Category.CategoryName,p.ProductName,p.UnitPrice};

            this.dataGridView3.DataSource = q.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var q = from c in this.dbContext.Categories
                    from p in c.Products
                    orderby c.CategoryID ascending
                    select new { c.CategoryID, c.CategoryName, p.ProductName, p.UnitPrice };

            this.dataGridView3.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
                var q = from p in this.dbContext.Products
                    group p by p.Category.CategoryName
                    into g
                    select new { CategoryName = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //會發現OrderDate沒辦法直接.year
            //出現Datetime?=>表示允許空值
            //OrderDate.Value
            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year
                    into g
                    select new { Year = g.Key, Count = g.Count() };

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product prod = new Product { ProductName = DateTime.Now.ToLongTimeString(), Discontinued = true };
            this.dbContext.Products.Add(prod);

            this.dbContext.SaveChanges();

        }
    }
}
