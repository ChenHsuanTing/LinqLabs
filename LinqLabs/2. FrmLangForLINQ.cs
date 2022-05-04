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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);

            Swap(ref n1, ref n2);
            

            MessageBox.Show(n1 + "," + n2);
            //====================================
            string s1, s2;
            s1 = "aaaaa";
            s2 = "bbbbb";
            MessageBox.Show(s1 + "," + s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);

        }
        void Swap(ref int n1,ref int n2)
        {
            int temp = n1;
            n1 = n2;
            n2 = temp;
        }
        void Swap(ref string n1, ref string n2)
        {
            string temp = n1;
            n1 = n2;
            n2 = temp;
        }
        //void Swap(ref string n1, ref string n2)
        //{
        //    string temp = n1;
        //    n1 = n2;
        //    n2 = temp;
        //}

        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);
            //Swapanytype<int>(ref n1, ref n2);
            //自動推斷型別
            Swapanytype(ref n1, ref n2);

            MessageBox.Show(n1 + "," + n2);
            //==================================
            string s1, s2;
            s1 = "aaaaa";
            s2 = "ccccc";

            MessageBox.Show(s1 + "," + s2);
            Swapanytype(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
            //=================================
            string t1, t2;
            t1 = "wwwww";
            t2 = "hhhhh";
            MessageBox.Show(t1 + "," + t2);
            Swapanytypee(ref t1, ref t2);
            MessageBox.Show(t1 + "," + t2);
        }
        void Swapanytypee<T>(ref T n1,ref T n2)
        {
            T temp = n1;
            n1 = n2;
            n2 = temp;
        }

        void Swapanytype<T>(ref T n1, ref T n2)
        {
            //僅僅改型別就好
            T temp = n1;
            n1 = n2;
            n2 = temp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //'ButtonX_Click' 沒有任何多載符合委派 'EventHandler
            //如果把內容取消掉
            //this.buttonX.Click += ButtonX_Click;


            //演變流程
            //C#1.0 具名方法
            this.buttonX.Click += new EventHandler(aaa);
            //syntax sugar
            this.buttonX.Click += bbb;//可以不用new的方法

            //C#2.0 匿名方法
            //sender/e不要相同
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
              {
                  MessageBox.Show("C#2.0 匿名方法");
              };

            //C#3.0 匿名方法
            //lambda運算式=>goes to
            this.buttonX.Click += (object sender1, EventArgs e1) =>
            {
                MessageBox.Show("C#3.0 匿名方法");
            };

        }

        private void ButtonX_Click(/*object sender, EventArgs e*/)
        {
            MessageBox.Show("Button_Click");
        }
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }
        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        bool Test(int n)
        {
            //if (n > 5)
            //    return true;
            //else
            //    return false;

            return n > 5;
        }
        bool Test1(int n)
        {
            return n % 2 == 0;
        }
        bool Test2(int n)
        {
            return n % 2 == 1;
        }

        //step 1 : create delegate型別
        //step 2 : create delegate object(new)
        //step 3 : invoke/call method

        //Create delegate
        delegate bool MyDelegate(int n);
        delegate bool MyDelegatenew(int n);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(7);
            MessageBox.Show("result=" + result);

            //bool results = Test2(5);
            //MessageBox.Show("result=" + results);

            //=====================================

            //MyDelegatenew delegatenew = new MyDelegatenew(Test2);
            //result = delegatenew(9);
            //MessageBox.Show("result=" + result);

            MyDelegate delegateobj = new MyDelegate(Test);
            result = delegateobj.Invoke(7);//Invoke可用可不用//Call方法
            MessageBox.Show("result=" + result);

            //=====================================

            delegateobj = Test1;//syntax sugar
            result = delegateobj(3);
            MessageBox.Show("result=" + result);

            //=====================================
            //C#2.0 匿名方法
            delegateobj = delegate (int n)
             {
                 return n > 5;
             };
            result = delegateobj(6);
            MessageBox.Show("result=" + result);

            MyDelegatenew delegatenew = new MyDelegatenew(Test);
            delegatenew = delegate (int n)
            {
                return n > 5;
            };
            result = delegatenew(5);
            MessageBox.Show("result=" + result);

            //=====================================
            //C#3.0 匿名方法 lambda運算式=>goes to
            //委派名稱=>參數=>敘述

            delegatenew = n => n < 7;
            result = delegatenew(6);
            delegateobj = n => n > 5;
            result = delegateobj(6);
            MessageBox.Show("result=" + result);
        }

        
        
        List<int> MyWhere(int[] nums, MyDelegate delegateobj)
        {
            List<int> list = new List<int>();
            //...........
            foreach (int n in nums)
            {
                if (delegateobj(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            //this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //List<int> LargeList = MyWhere(nums, Test1/*可替換方法*/);
            ////
            //foreach(int n in LargeList)
            //{
            //    this.listBox1.Items.Add(n);
            //}
            //======================================
            List<int> list1 = MyWhere(nums, n => n > 5);
            List<int> oddList = MyWhere(nums, n => n % 2 == 1);
            List<int> evenList = MyWhere(nums, n => n % 2 == 0);

            foreach (int n in oddList)
            {
                this.listBox1.Items.Add(n);
            }
            this.listBox1.Items.Add("======================");
            foreach (int n in evenList)
            {
                this.listBox1.Items.Add(n);
            }

        }


        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateobj)
        {
            foreach (int n in nums)
            {
                if (delegateobj(n)) //call method
                {
                    yield return n;
                }
            }

        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = MyIterator(nums, n => n > 5);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q = from n in nums
            //        where n > 5
            //        select n;
            IEnumerable<int> q = nums.Where(n => n > 5);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
            //============================================
            string[] words = { "aaa", "bbbbb", "ccccccc" };
            IEnumerable<string> q1 = words.Where<string>(w => w.Length > 3);

            foreach (string w in q1)
            {
                this.listBox2.Items.Add(w);
            }
            this.dataGridView1.DataSource = q1.ToList();
            //=============================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q2 = nwDataSet1.Products.Where(h => h.UnitPrice > 30);

            this.dataGridView2.DataSource = q2.ToList();

        }

        private void button45_Click(object sender, EventArgs e)
        {
            //如果知道型別盡量習慣寫出來
            //var通常用於匿名型別
            var n = 100;//int32
            var s = "aaa";//UTF-16
            //S.LENGTH
            var Q = new Point(100, 100);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint pt1 = new MyPoint(); //Constructor
            pt1.P1 = 100;   //set
            int w = pt1.P1; //get

            pt1.P2 = 200;
            //MessageBox.Show(pt1.p1.ToString()); //get
            List<MyPoint> list = new List<MyPoint>();
            list.Add(new MyPoint());
            list.Add(new MyPoint(100));
            list.Add(new MyPoint(99, 99));
            list.Add(new MyPoint("xxxxx"));

            //=========================================
            //物件初始化
            list.Add(new MyPoint { P1 = 100, P2 = 300, field1 = "AAA", field2 = "BBB" });
            list.Add(new MyPoint { P1 = 100 });
            list.Add(new MyPoint { P1 = 200, P2 = 500 });

            this.dataGridView1.DataSource = list;
            //=========================================
            List<MyPoint> list2 = new List<MyPoint>
            {
                new MyPoint{P1=1,P2=2,field1="aaa"},
                new MyPoint{P1=11,P2=2,field1="aaa" },
                new MyPoint { P1 = 111, P2 = 2, field1 = "aaa" },
                new MyPoint { P1 = 1111, P2 = 2, field1 = "aaa" },

        };
            this.dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //匿名型別
            var x = new { P1 = 99, P2 = 88, P3 = 77 };
            var y = new { P1 = 99, P2 = 88, P3 = 77 };
            var z = new { P1 = 99, P2 = 88, P3 = 77 };

            this.listBox1.Items.Add(x.GetType());
            this.listBox1.Items.Add(y.GetType());
            this.listBox1.Items.Add(z.GetType());
            //============================================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, Square = n * n, Cube = n * n * n };

            var q = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });
            this.dataGridView1.DataSource = q.ToList();
            //============================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q2 = from p in nwDataSet1.Products
                     where p.UnitPrice > 30
                     select new
                     {
                         ProductID = p.ProductID,
                         ProductName = p.ProductName,
                         p.UnitPrice,
                         p.UnitsInStock,
                         Total = $"{p.UnitPrice * p.UnitsInStock:c2}"
                     };
            this.dataGridView1.DataSource = q2.ToList();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            //擴充方法
            string s = "abcdefg";
            int n = s.WordCount();
            MessageBox.Show("WordCount=" + n);
            //==================================
            string s1 = "123456789";
            n = s1.WordCount();
            MessageBox.Show("WordCount=" + n);

            MyStringExtension.WordCount(s1);
            //===================================
            char ch = s1.Char(3);
            MessageBox.Show("ch=" + ch);    


        }
    }
}
public static class MyStringExtension
{
    public static int WordCount(this string s)//用this開頭
    {
        //使用static類別的擴充方法
        return s.Length;
    }
    public static char Char(this string s,int index)
    {
        return s[index];
    }
}
public class MyPoint
{
    public MyPoint()
    {

    }
    public MyPoint(int p1)
    {
        P1 = p1;
    }
    //P1是屬性p1是參數
    public MyPoint(int p1, int p2)
    {
        P1 = p1;
        P2 = p2;
    }
    public MyPoint(string field1)
    {

    }

    private int m_p1;
    public string field1 = "xxx", field2 = "yyy";
    public int P1
    {
        get
        {
            //logic...
            return m_p1;
        }
        set
        {
            //logic...
            m_p1 = value;
        }


    }
    public int P2 { get; set; }

}
