using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace 扫雷
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }     
        //定义资源路径
        //重置按钮
        private readonly string begin = System.Environment.CurrentDirectory + "\\picture\\begin.bmp"; //开始，微笑

        private readonly string end = System.Environment.CurrentDirectory + "\\picture\\end.jpg"; //结束，眩晕
        //地雷
        private readonly string landmine = System.Environment.CurrentDirectory + "\\picture\\landmine.bmp"; //地雷
        private readonly string explode = System.Environment.CurrentDirectory + "\\picture\\explode.bmp"; //爆炸
        //标志
        private readonly string flag = System.Environment.CurrentDirectory + "\\picture\\flag.bmp"; //旗帜
        private readonly string ask = System.Environment.CurrentDirectory + "\\picture\\ask.jpg"; //问号
        //游戏音效
        private readonly string victory = System.Environment.CurrentDirectory + "\\picture\\win.wav"; //胜利
        private readonly string defeat = System.Environment.CurrentDirectory + "\\picture\\boom.wav"; //失败
     
        //计时器
        private Timer timer1 = new Timer();
        //游戏用时
        private int total_time = 0;
        //地雷数量
        private int landminecnt = 10;
        //游戏是否结束
        private bool over = false;
        //生成雷的行数
        private int hang = 9;
        //生成雷的列数
        private int lie = 9;
        //游戏中剩余地雷数
        private int landminerest;
        //生成个按钮数组 
        private List<List<LandMineButton>> button = new List<List<LandMineButton>>();
        //搜索8个方向的数组
        int[,] Nextpos = new int[8, 2];
        //游戏音效开关
        int sound_close = 0;

        public int LandMineCnt
        {
            get { return landminecnt; }
            set { landminecnt = value; }
        }
        public int Lie
        {
            get { return lie; }
            set { lie = value; }
        }
        public int Hang
        {
            get { return hang; }
            set { hang = value; }
        }


        private void GameForm_Load(object sender, EventArgs e)
        {
            landminerest = LandMineCnt;
            label1.Text = "0秒";

            groupBox1.Location = new Point(26, 26);
            groupBox1.Text = "";
            groupBox1.Size = new System.Drawing.Size(908, 488);
            //动态调整groupbox1大小
            groupBox1.Width = 30 * lie + 8; groupBox1.Height = 30 * hang + 8;
            groupBox1.FlatStyle = FlatStyle.Standard;
            //动态调整窗体大小
           // this.Height = groupBox1.Height + 70;
          //  this.Width = groupBox1.Width + 50;
            //调整panel1位置
           // panel1.po

            lei.Text = "  " + landminerest.ToString() + "颗";

            this.Location = new Point(20, 20);
            timer1.Enabled = true;

            Maplandmine(); //初始化地图
            Laylandmine(); //布雷 

            this.StartPosition = FormStartPosition.Manual;

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000;

            //改变位置的数组初始化
            Nextpos[0, 0] = -1; Nextpos[0, 1] = -1;
            Nextpos[1, 0] = -1; Nextpos[1, 1] = 0;
            Nextpos[2, 0] = -1; Nextpos[2, 1] = 1;
            Nextpos[3, 0] = 0; Nextpos[3, 1] = -1;
            Nextpos[4, 0] = 0; Nextpos[4, 1] = 1;
            Nextpos[5, 0] = 1; Nextpos[5, 1] = -1;
            Nextpos[6, 0] = 1; Nextpos[6, 1] = 0;
            Nextpos[7, 0] = 1; Nextpos[7, 1] = 1;
        }

        //自定义timer控件的Tick事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            total_time++;
            if (total_time < 60)
                label1.Text = total_time.ToString() + "秒";
            else
                label1.Text = (total_time / 60).ToString() + "分" + (total_time % 60).ToString() + "秒";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //生成雷阵
        private void Maplandmine()
        {

            for(int i = 0; i < lie; ++i)
            {
                List<LandMineButton> item = new List<LandMineButton>();
                for (int j = 0; j < hang; ++j)
                {
                    item.Add(new LandMineButton());
                    item[j].Location = new Point(6 + i * 30, 3 + j * 30);
                    item[j].X = j;
                    item[j].Y = i;
                    item[j].Is_Boom = false;
                    item[j].ABLED = true;
                    string FileName = System.Environment.CurrentDirectory + "\\picture\\button\\close.jpg";
                    item[j].Image = Image.FromFile(FileName);

                    groupBox1.Controls.Add(item[j]);
                    item[j].MouseUp += new MouseEventHandler(bt_MouseUp);
                }
                button.Add(item);
            }         
        }
        //随机布雷
        private void Laylandmine()
        {
            Random rand = new Random();
            for (int i = 0; i < landminecnt;)
            {
                int position_x = rand.Next(hang);
                int position_y = rand.Next(lie);
                if (button[position_y][position_x].Is_Boom == false)
                {
                    button[position_y][position_x].Is_Boom = true;
                    ++i;
                }
            }
        }
       
        //计算点击按钮的8个方向
        private int Getlandmine(int _row, int col)//x代表行，y代表列
        {
            int around = 0;
            for(int k = 0; k < 8; ++k)
            {
                int i = _row + Nextpos[k, 0], j = col + Nextpos[k,1];
                if (!(i >= 0 && i < hang && j >= 0 && j < lie))//判断是否在扫雷区域
                     continue;
                if (button[j][i].Is_Boom == true) around++;               
            }
            return around;
        }
        //扫雷算法，递归实现
        private void DFS(int _row, int col)
        {
            int minecnt = Getlandmine(_row, col);
            if (minecnt == 0)
            {
                button[col][_row].ABLED = false;
                for(int k = 0; k < 8; ++k)
                {
                    int m = _row + Nextpos[k, 0], n = col + Nextpos[k, 1];
                    if (!(m >= 0 && m < hang && n >= 0 && n < lie))
                        continue;
                    if (!(m == col && n == _row) && button[n][m].ABLED == true && Convert.ToInt16(button[n][m].Tag) == 0)
                        DFS(m, n);
                    //判断该处是否标记为有雷，有雷该处不作任何变化，无雷控件Enable属性变为false
                    if (Convert.ToInt16(button[n][m].Tag) == 0)
                        button[n][m].ABLED = false;
                    // button[m, n].Text = Getlandmine(m, n).ToString();

                    string FileName = System.Environment.CurrentDirectory + "\\picture\\button\\" + Getlandmine(m, n).ToString() + ".jpg"; button[n][m].Image = Image.FromFile(FileName);

                    //if (button[m, n].Text == "0")
                    //  button[m, n].Text = string.Empty;                                     
                }
            }
        }
        //鼠标单击事件
        private void bt_MouseUp(object sender, MouseEventArgs e)
        {
            if (!over)
            {
                int x, y;
                //获取被点击的Button按钮
               LandMineButton b = (LandMineButton)sender;
                if (b.ABLED == false) return;
                x = b.X;//x代表button数组的第一个索引
                y = b.Y;//y表示Button数组的第二个索引
                //判断按下的鼠标键是哪个
                switch (e.Button)
                {
                    //按下鼠标左键
                    case MouseButtons.Left:
                        //判断该方格是否被标旗，Tag=0表示方格未被标志
                        if (Convert.ToInt16(button[y][x].Tag) == 0)
                        {
                            if (button[y][x].Is_Boom == false)
                            {
                                //button[x, y].Enabled = false;
                                button[y][x].ABLED = false;
                                //button[x, y].Text = Getlandmine(x, y).ToString();

                                string FileName = System.Environment.CurrentDirectory + "\\picture\\button\\" + Getlandmine(x, y).ToString() + ".jpg"; button[y][x].Image = Image.FromFile(FileName);

                                DFS(x, y);
                                if (Win())
                                {
                                    Showlandmine();
                                    timer1.Enabled = false;
                                    SoundPlayer sound = new SoundPlayer(victory);
                                    if(sound_close == 0) sound.Play();
                                    MessageBox.Show("恭喜您排除所有地雷！", "扫雷完成");
                                    over = true;

                                    Record record = new Record(hang,lie,landminecnt,total_time);
                                    record.ShowDialog();
                                    this.Close();
                                }
                            }
                            else
                            {
                                string FileName = System.Environment.CurrentDirectory + "\\picture\\landmine.bmp";
                              //  button[y][x].Image = Image.FromFile(FileName);                               
                                SoundPlayer sound = new SoundPlayer(defeat);
                                if(sound_close == 0) sound.Play(); //sound.Dispose();
                                timer1.Enabled = false;
                                b.ABLED = false;
                                b.Image = Image.FromFile(FileName);
                                display(y, x);
                                MessageBox.Show("您点击到了地雷！", "游戏失败");
                                button1.Image = Image.FromFile(end);
                                over = true;                     
                            }
                        }
                        break;
                    case MouseButtons.Right:

                        if (Convert.ToInt16(button[y][x].Tag) == 1)
                        {
                            button[y][x].Tag = 2;
                            button[y][x].Image = Image.FromFile(ask);
                            landminerest++;
                        }
                        else if (Convert.ToInt16(button[y][x].Tag) == 2)
                        {
                            button[y][x].Tag = 0;
                            //landminerest++;
                            string FileName = System.Environment.CurrentDirectory + "\\picture\\button\\close.jpg";
                            button[y][x].Image = Image.FromFile(FileName);
                        }
                        else
                        {
                            button[y][x].Tag = 1;
                            button[y][x].Image = Image.FromFile(flag);
                            landminerest--;

                        }
                        lei.Text = "  " + landminerest.ToString() + "颗";
                        if (Win())
                        {
                            SoundPlayer sound = new SoundPlayer(victory);
                            if(sound_close == 0) sound.Play();
                            MessageBox.Show("恭喜你！你太有才了，扫雷成功", "扫雷完成");
                            timer1.Enabled = false;
                            over = true;

                            Record record = new Record(hang, lie, landminecnt,total_time);
                            record.ShowDialog();
                            this.Close();

                        }
                        break;

                }
            }
            else
                return;
        }
        //判断游戏是否结束
        private bool Win()
        {
            int cnt = 0;
            for (int i = 0; i < hang; i++)
            {
                for (int j = 0; j < lie; j++)
                {
                    if (button[j][i].Is_Boom == true && Convert.ToInt16(button[j][i].Tag) == 1)
                        cnt++;

                }

            }
            if (cnt == landminecnt && landminerest == 0)
                return true;
            else
                return false;
        }
        //显示地雷
        private void Showlandmine()
        {
            for (int i = 0; i < hang; i++)
            {
                for (int j = 0; j < lie; j++)
                    if (button[j][i].Is_Boom == true)
                    {
                        button[j][i].Image = Image.FromFile(landmine);
                    }
            }
        }
        //显示爆炸后的地雷
        private void display(int x, int y)
        {
            for (int i = 0; i < hang; i++)
            {
                for (int j = 0; j < lie; j++)           
                    if (button[j][i].Is_Boom == true)
                    {
                        if(i != y || j != x)
                            button[j][i].Image = Image.FromFile(explode);
                    }               
            }
        }
        //重置游戏
        public void ResetGame()
        {
            for (int i = 0; i < hang; i++)
            {
                for (int j = 0; j < lie; j++)
                {
                    button[j][i].Tag = 0;  //不标棋
                    button[j][i].ABLED = true; //按钮可用               
                    string FileName = System.Environment.CurrentDirectory + "\\picture\\button\\close.jpg";
                    button[j][i].Image = Image.FromFile(FileName); //按钮图案变关闭
                   // button[j][i].BackgroundImage = null;
                    if (button[j][i].Is_Boom == true)  //把雷去掉
                        button[j][i].Is_Boom = false;
                }
            }

            Laylandmine(); //重新布雷
            total_time = 0; //计时器清零  
            label1.Text = "0秒";
            timer1.Enabled = true;
            over = false; //游戏未结束

            landminerest = landminecnt; //剩余雷数变化
            lei.Text = "  " + landminerest.ToString() + "颗";

            button1.Image = Image.FromFile(begin); //按钮变笑脸
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetGame(); //重置游戏
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void 游戏菜单ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 游戏帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string messagestring = "《扫雷》是一款大众类的益智小游戏，于1992年发行。游戏目标是在最短的时间内根据点击格子出现的数字找出所有非雷格子，同时避免踩雷，踩到一个雷即全盘皆输。";
            messagestring += "\n玩法\n";
            messagestring += "1、右键红旗标记出所有的雷方块\n";
            messagestring += "2、挖开空方块，可以继续玩。\n";
            messagestring += "3、挖开数字，则表示在其周围的八个方块中共有多少个雷，可以使用该信息推断能够安全单击附近的哪些方块。";
            MessageBox.Show(messagestring, "查看帮助");
        }

        private void 联系作者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此游戏由刘广城制作！\n联系QQ：2621690255", "关于此游戏");
        }

        private void 关闭声音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
        }

        private void 游戏设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关闭音效ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sound_close = (sound_close + 1) % 2;
        }
    }
}
