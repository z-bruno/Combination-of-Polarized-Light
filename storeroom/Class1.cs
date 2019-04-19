using System;
using Microsoft.Win32;


namespace storeroom
{

    public class Class1
    {
     
        const string userRoot = "HKEY_CURRENT_USER";
        const string sbkey = "OUT";
        public string keyName = userRoot + "\\" + sbkey;

        public int Xoffset = 300;   //偏移量
        public int Yoffset = 150;
        public double pi = Math.PI;
        public point  p = new point();
        public struct point   //结构体在库中，实例化后可在库中赋值然后Form处调用，适用于方法在库中。
                              
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public double Sita { get; set; }
            public string Show { get; set; }
        }
        public void Init(double a1, double a2, double a3)
        {
            p.X = a1;
            p.Y = a2;
            p.Sita = a3;
        }
        public void WhatToShow()
        {
            //判断左右旋
            int a = (int)(p.Sita / (2 * pi));
            double b = p.Sita - a * (2 * pi);
            bool s1 = (b == 0);
            bool s6 = (b == pi / 2 || b == -pi * 3 / 2);
            bool s7 = (b == pi * 3 / 2 || b == -pi / 2);
            bool s2 = (b < pi && 0 < b && b != pi / 2);
            bool s3 = (b == pi);
            bool s4 = (pi < b && b < 2 * pi);
            p.Show = "";
            if (s1 || s3) p.Show = "线偏振光";
            if (s2) p.Show = "右旋椭圆偏振光";
            if (s4) p.Show = "左旋椭圆偏振光";
            if (s6)
            {
                if (p.X == -p.Y) p.Show = "右旋圆偏振光";
                else p.Show = "右旋椭圆偏振光";
            }
            if (s7)
            {
                if (p.X == -p.Y) p.Show = "左旋圆偏振光";
                else p.Show = "左旋椭圆偏振光";
            }
            p.Show = "张云淞 2016050205015       " + p.Show;
        }
        public void Draw(double i)
        {
            p.X1 = p.X * Math.Cos(0 + i) + Xoffset;
            p.Y1 = p.Y * Math.Cos(p.Sita + i) + Yoffset;  //cos函数内需要填写弧度值
            p.X2 = p.X * Math.Cos(0 + i + 0.1) + Xoffset;
            p.Y2 = p.Y * Math.Cos(p.Sita + i + 0.1) + Yoffset;
            Registry.SetValue(keyName, "OUT", 1);
        }
        //角度转弧度
        public double ToAngle(double a)
        {
            return (a / 180) * Math.PI;
        }        

    }
    public struct Parameter  //结构体在storeroom命名空间下，与Class1库平行，在Form处赋值并调用，适用于方法在库外
    {
        public double Ex;
        public double Ey;
        public double Delta;
        public string x;
    }
}
