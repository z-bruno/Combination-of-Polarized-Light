using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace liquid_crystal
{
    public partial class Form1 : Form
    {
        bool noway = false;
        public point point1 = new point();
        public point save = new point();
        double pi = Math.PI;
        public Form1()
        {            
            InitializeComponent();
            //加载界面的相关数值
            point1.X = (double)numericUpDown1.Value;
            point1.Y = -(double)numericUpDown2.Value;
            save.sita = ToAngle((double)numericUpDown3.Value);    
            //设置试用条件
            //if (File.Exists(@"D:\garbage_flage.data")) noway = true;
            //else File.Create(@"D:\garbage_flage.data");       
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (noway)
            {
                MessageBox.Show("试用期结束，请联系开发者获取使用权限！");
                Close();
            }
        }
        public class point
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public double sita { get; set; }
        }
        //角度转弧度
        public double ToAngle(double a)
        {
            label6.Text = "相位差: " + a / 180 + "π";
            return (a / 180) * Math.PI;            
        }
        private void pic_Click(object sender, EventArgs e)
        {
            pic.Enabled = false;
            int Xoffset = 300;   //偏移量
            int Yoffset = 150;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen mypen = new Pen(Color.Black, 2);
            point1.X = (double)numericUpDown1.Value;
            point1.Y = -(double)numericUpDown2.Value;
            save.sita = ToAngle((double)numericUpDown3.Value);    //加载界面的相关数值

            //判断左右旋
            int a = (int)(point1.sita / (2 * pi));
            double b = point1.sita - a * (2 * pi);
            bool s1 = (b == 0);
            bool s6 = (b == pi / 2 || b == -pi * 3 / 2);
            bool s7 = (b == pi * 3 / 2 || b == -pi / 2);
            bool s2 = (b < pi && 0 < b && b != pi / 2);
            bool s3 = (b == pi);
            bool s4 = (pi < b && b < 2 * pi);
            string show = "";
            if (s1 || s3) show = "线偏振光";
            if (s2) show = "右旋椭圆偏振光";
            if (s4) show = "左旋椭圆偏振光";
            if (s6)
            {
                if (point1.X == -point1.Y) show = "右旋圆偏振光";
                else show = "右旋椭圆偏振光";
            }
            if (s7)
            {
                if (point1.X == -point1.Y) show = "左旋圆偏振光";
                else show = "左旋椭圆偏振光";
            }            
            label_change.Text = show;                     
            for (double i = 0; i <= 45; i= i+0.01)
            {     
                point1.X1 = point1.X * Math.Cos(0 + i) + Xoffset;
                point1.Y1 = point1.Y * Math.Cos(point1.sita + i) + Yoffset;    //cos函数内需要填写弧度值
                point1.X2 = point1.X * Math.Cos(0 + i + 0.1) + Xoffset;
                point1.Y2 = point1.Y * Math.Cos(point1.sita + i + 0.1) + Yoffset;
                //Console.WriteLine("X1:{0},Y1:{1}", point1.X1, point1.Y1);
                //Console.WriteLine("X1:{0},Y1:{1}", point1.X2, point1.Y2);
                g.DrawLine(mypen, (float)point1.X1, (float)point1.Y1, (float)point1.X2, (float)point1.Y2);  //绘制图像
            }
            Thread.Sleep(200);
            pic.Enabled = true;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            save = point1;            
            save.X = (double)numericUpDown1.Value;              
            point1 = save;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            save = point1;
            save.Y = -(double)numericUpDown2.Value;
            point1 = save;
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            save = point1;
            save.sita = ToAngle((double)numericUpDown3.Value);    //sita为弧度值  
            point1 = save;
            //Console.WriteLine("sita = {0}!",point1.sita);
        }  
    }
}
