using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF[] pts;
        int Max = 100, Min = 100;
        PointF drawSize = new PointF(400, 400);
        PointF offSet = new PointF(10, 10);


        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] input_arr = new string[0];
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    input_arr = File.ReadAllLines(ofd.FileName);
                    button2.Enabled = true;
                }
            }

            pts = Array.ConvertAll(input_arr, (s) =>
            {
                int[] arr = Array.ConvertAll(s.Split(','), int.Parse);
                return new PointF(arr[0], arr[1]);
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointF center = new PointF(drawSize.X / 2f, drawSize.Y / 2f);
            //計算縮放比,注意Y軸縮放是負的,要讓座標軸相反
            PointF scale = new PointF(drawSize.X / (Max + Min), -drawSize.Y / (Max + Min));

            // 偏移繪圖座標至中心點
            g.TranslateTransform(center.X + offSet.X, center.Y + offSet.Y);

            // X軸
            g.DrawLine(Pens.Black, -drawSize.X / 2, 0, drawSize.X / 2, 0);
            for (float x = -Min; x <= Max; x += 10)
            {
                if (x % 20 == 0)
                    g.DrawString(x.ToString(), new Font("consolas", 8), Brushes.Black, x * scale.X - 6, 0);
                g.FillRectangle(Brushes.Black, x * scale.X - 1, -3, 2, 6);
            }

            // Y軸
            g.DrawLine(Pens.Black, 0, -drawSize.Y / 2, 0, drawSize.Y / 2);
            for (float y = -Min; y <= Max; y += 10)
            {
                if (y % 20 == 0 && y != 0)
                    g.DrawString(y.ToString(), new Font("consolas", 8), Brushes.Black, 0, y * scale.Y - 6);
                g.FillRectangle(Brushes.Black, -3, y * scale.Y - 1, 6, 2);
            }

            // 縮放所有座標
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X *= scale.X;
                pts[i].Y *= scale.Y;
            }

            // 繪出
            g.DrawLines(Pens.Red, pts);

        }
    }
}
