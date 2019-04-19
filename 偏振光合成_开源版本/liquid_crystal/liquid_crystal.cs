using System;
using Microsoft.Win32;
using System.Windows.Forms;
using storeroom;
using System.Drawing;

namespace liquid_crystal
{
    partial class liquid_crystal : Form
    {
        private Class1 CTF = new Class1();
        private Parameter Para = new Parameter(); //实例化结构体
       
        public liquid_crystal()
        {
            InitializeComponent();
            GetParaFromUI();
            CTF.Init(Para.Ex, Para.Ey, Para.Delta);
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            string[] str = Registry.CurrentUser.GetSubKeyNames();
            foreach(string s in str)
            {
                Console.WriteLine(s);
                //if (s == Para.x)
                //{
                //    MessageBox.Show("试用期结束，请联系开发人员获取使用权限！");
                //    Close();
                //}
            }      
        }
        public void Pic_Click(object sender, EventArgs e)
        {            
            pic.Enabled = false;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen mypen = new Pen(Color.Black, 2);

            GetParaFromUI();
            CTF.Init(Para.Ex, Para.Ey, Para.Delta);
            CTF.WhatToShow();
            label_change.Text = CTF.p.Show;
            for (double i = 0; i <= 7; i = i + 0.1)
            {
                CTF.Draw(i); ;
                g.DrawLine(mypen, (float)CTF.p.X1, (float)CTF.p.Y1, (float)CTF.p.X2, (float)CTF.p.Y2);
            }
            pic.Enabled = true;
        }
        public void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {       
            CTF.p.X = (double)numericUpDown1.Value;              
        }
        public void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CTF.p.Y = -(double)numericUpDown2.Value;
        }
        public void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double a = (double)numericUpDown3.Value;
            CTF.p.Sita = CTF.ToAngle(a);    //sita为弧度值  
            label6.Text = "相位差: " + a / 180 + "π";
        }    
        public void GetParaFromUI()
        {
            Para.Ex = (double)numericUpDown1.Value;
            Para.Ey = -(double)numericUpDown2.Value;
            Para.Delta = CTF.ToAngle((double)numericUpDown3.Value);
            Para.x = label_try.Text;
        }
    }
}
