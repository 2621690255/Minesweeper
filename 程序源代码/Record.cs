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

namespace 扫雷
{
    public partial class Record : Form
    {
        private int line, row, cnt, time;
        public Record()
        {
            InitializeComponent();
        }
        public Record(int _line, int _row,int _cnt, int _time)
        {
            InitializeComponent();
            line = _line; row = _row; cnt = _cnt; time = _time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty) return;
            PlayList.playList.Add(new Player(textBox1.Text, line, row, cnt, time));
            PlayList.playList.Write(); //保存记录         
            textBox1.Text = String.Empty;
            MessageBox.Show("请前往扫雷英雄榜查看记录！", "保存成功");
            PlayList.playList.Display();
           // MenuForm.menufrm.LoadRecord();
            //MenuForm menuForm = new MenuForm();
            //menuForm.LoadRecord(); menuForm.Dispose();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
