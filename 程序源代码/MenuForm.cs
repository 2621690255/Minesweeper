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
    public partial class MenuForm : Form
    {
        public static MenuForm menufrm;
        public MenuForm()
        {
            InitializeComponent();
            menufrm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center; //文本居中显示
            richTextBox1.SelectionFont = new Font("宋体", 13, FontStyle.Bold);
            PlayList.playList.Load();
            PlayList.playList.Display();
        }

        public void LoadRecord(string s)
        {
            if (s == "") richTextBox1.Clear();
            else richTextBox1.Text += s; 
          /*  richTextBox1.Clear();
            richTextBox1.Text = "姓名\t行数\t列数\t雷数\t\t用时\n";
            FileStream aFile = new FileStream("Record.dat", FileMode.Open, FileAccess.Read);
            BinaryReader myReader = new BinaryReader(aFile);
            //获取的下一个字符不是EOF
            while (myReader.PeekChar() != -1)
            {
                //将文件流类型变为字符型输出
                richTextBox1.Text += Convert.ToString(myReader.ReadString()) + '\t';
                richTextBox1.Text += Convert.ToString(myReader.ReadString()) + '\t';
                richTextBox1.Text += Convert.ToString(myReader.ReadString()) + '\t';
                richTextBox1.Text += Convert.ToString(myReader.ReadString()) + "\t";
                int costtime = Convert.ToInt32(myReader.ReadString());
                richTextBox1.Text +=  " " + (costtime / 60).ToString() + "分" + (costtime % 60).ToString() + "秒\n" ;
            }
            myReader.Close();
            aFile.Close();
            */
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {     
           // this.Hide(); //调用Hide方法隐藏当前窗体
            GameForm gameForm = new GameForm(); //实例化GameForm
            gameForm.Lie = 9;gameForm.Hang = 9;
            gameForm.LandMineCnt = 10;
            gameForm.Show(); //调用Show方法打开窗体         
            //this.Show();
         //   LoadRecord();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(); //实例化GameForm
            gameForm.Lie = 16; gameForm.Hang = 16;
            gameForm.LandMineCnt = 40;
            gameForm.Show(); //调用Show方法打开窗体
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(); //实例化GameForm
            gameForm.Lie = 30; gameForm.Hang = 16;
            gameForm.LandMineCnt = 99;
            gameForm.Show(); //调用Show方法打开窗体
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CustomGame customGame = new CustomGame();
            customGame.Show();
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            //LoadRecord();
        }

        private void 使用手册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string messagestring = "通过点击开始游戏下方四个按钮,可选择游戏难度！\n\n";          
            messagestring += "扫雷英雄榜显示玩家排名，规则如下：\n";
            messagestring += "1、首先按用时升序排序；\n";
            messagestring += "2、用时相同时，按游戏难度（行x列）降序排序。\n";
            MessageBox.Show(messagestring, "程序介绍");
        }

        private void 联系作者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此游戏由刘广城制作！\n学号：541807010119\n班级：计算机科学与技术1801\n联系QQ：2621690255", "关于此游戏");
        }

        private void 清空记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayList.playList.Clear();
            MessageBox.Show("扫雷英雄榜已重置！", "清空记录");
            PlayList.playList.Display();
        }
    }
}
