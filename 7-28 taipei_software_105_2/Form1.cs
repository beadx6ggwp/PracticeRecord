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

namespace taipei_software_105_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int col, row, count = 0;
        int[][] map;
        //x=[0],y[1]
        int[,] way = new int[,] {
            { 0, -1 },
            { 1, -1 },
            { 1, 0 },
            { 1, 1 },
            { 0, 1 },
            { -1, 1 },
            { -1, 0 },
            { -1, -1 }
        };

        private void button1_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                ofd.Filter = "txt files|*.txt|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    path = ofd.FileName;
                }
            }
            sourceText.Text = OpenTxt(path);

            // select file
            string[] temp = sourceText.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            map = new int[temp.Length][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = Array.ConvertAll(temp[i].Split('\t'), s => int.Parse(s));
            }
            // calc
            row = map.Length;
            col = map[0].Length;

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    if (map[r][c] == 255)
                    {
                        count++;
                        DFS(r, c, count);
                    }
                }
            }
            //show
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    resultText.Text += map[r][c] + "\t";
                }
                resultText.Text += "\r\n";
            }
        }

        private void DFS(int r,int c,int count)
        {
            int tx, ty;
            map[r][c] = count;
            for (int k = 0; k < 8; k++)
            {
                tx = c + way[k, 0];
                ty = r + way[k, 1];
                if (tx < 0 || tx > col - 1 || ty < 0 || ty > row - 1)
                    continue;
                if (map[ty][tx] == 255)
                {
                    DFS(ty, tx, count);
                }
            }

            return;
        }

        private string OpenTxt(string path)
        {
            string result = string.Empty;
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}
