using System;
using Microsoft.Win32;
using System.Windows.Forms;
using storeroom;
using System.Drawing;

namespace liquid_crystal
{
    partial class liquid_crystal : Form
    {
        private Class1 ccc = new Class1();
        public liquid_crystal()
        {
            InitializeComponent(); 
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            string[] str = Registry.CurrentUser.GetSubKeyNames();
            foreach(string s in str)
            {
                string findstr = "OUT";
                Console.WriteLine(s);
                //if (s == findstr)
                //{
                //    MessageBox.Show("试用期结束，请联系开发人员获取使用权限！");
                //    Close();
                //}
            }
            double a1 = (double)numericUpDown1.Value;
            double a2 = -(double)numericUpDown2.Value;
            double a3 = ccc.ToAngle((double)numericUpDown3.Value);
            ccc.Init(a1, a2, a3);         
        }
        public void Pic_Click(object sender, EventArgs e)
        {            
            pic.Enabled = false;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen mypen = new Pen(Color.Black, 2);
            double a1 = (double)numericUpDown1.Value;
            double a2 = -(double)numericUpDown2.Value;
            double a3 = ccc.ToAngle((double)numericUpDown3.Value);
            ccc.Init(a1, a2, a3);
            ccc.WhatToShow();
            label_change.Text = ccc.p.Show;
            for (double i = 0; i <= 7; i = i + 0.1)
            {
                ccc.Draw(i); ;
                g.DrawLine(mypen, (float)ccc.p.X1, (float)ccc.p.Y1, (float)ccc.p.X2, (float)ccc.p.Y2);
            }
            pic.Enabled = true;
        }
        public void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {       
            ccc.p.X = (double)numericUpDown1.Value;              
        }
        public void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ccc.p.Y = -(double)numericUpDown2.Value;
        }
        public void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double a = (double)numericUpDown3.Value;
            ccc.p.Sita = ccc.ToAngle(a);    //sita为弧度值  
            label6.Text = "相位差: " + a / 180 + "π";
        }          
    }
}
