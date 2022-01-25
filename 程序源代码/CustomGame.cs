using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 扫雷
{
    public partial class CustomGame : Form
    {
        public CustomGame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            trackBar3.Maximum = (trackBar1.Value - 1) * (trackBar2.Value - 1);

            label6.Text = trackBar3.Value.ToString();
        }

        private int min(int a, int b)
        {
            return a < b ? a : b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(); //实例化GameForm
            gameForm.Lie = trackBar2.Value; gameForm.Hang = trackBar1.Value;
            gameForm.LandMineCnt = trackBar3.Value;
            gameForm.Show(); //调用Show方法打开窗体 
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar1.Value.ToString();
            trackBar3.Maximum = (trackBar1.Value - 1) * (trackBar2.Value - 1);
            trackBar3.Value = min(trackBar3.Maximum, trackBar3.Value);
            label6.Text = trackBar3.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar2.Value.ToString();
            trackBar3.Maximum = (trackBar1.Value - 1) * (trackBar2.Value - 1);
            trackBar3.Value = min(trackBar3.Maximum, trackBar3.Value);
            label6.Text = trackBar3.Value.ToString();
        }
    }
}
