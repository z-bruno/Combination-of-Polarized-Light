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

            //试用期设置
            //RegistryKey RootKey, RegKey;
            ////项名为：HKEY_CURRENT_USERSoftware
            //RootKey = Registry.CurrentUser.OpenSubKey("Software", true);

            ////打开子项：HKEY_CURRENT_USERSoftwareMyRegDataApp
            //if ((RegKey = RootKey.OpenSubKey("MyRegDataApp", true)) == null)
            //{
            //    RootKey.CreateSubKey("MyRegDataApp");//不存在，则创建子项
            //    RegKey = RootKey.OpenSubKey("MyRegDataApp", true);
            //    RegKey.SetValue("UseTime", (object)10);    //创建键值，存储可使用次数
            //    MessageBox.Show("您可以免费使用本软件10次！", "感谢您首次使用");
            //}
            //object usetime = RegKey.GetValue("UseTime");//读取键值，可使用次数
            //MessageBox.Show("你还可以使用本软件 :" + usetime.ToString() + "次！", "确认", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //int newtime = Int32.Parse(usetime.ToString()) - 1;
            //if (newtime < 0)
            //{
            //    if (MessageBox.Show("继续使用，请购买本软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            //{
            //    RegKey.SetValue("UseTime", (object)newtime);//更新键值，可使用次数减1
            //}


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
