using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 扫雷
{
    class PlayList
    {
        private List<Player> players = new List<Player>();
        public static PlayList playList = new PlayList();
        public PlayList()
        {
            players.Clear();
            FileStream aFile = new FileStream("Record.dat", FileMode.Append, FileAccess.Write);
            //二进制写入流
            aFile.Close();
            playList = this;
        }
        public void Add(Player x)
        {
            players.Add(x);
        }
        public void Sort()
        {
            int cmp(Player a, Player b)
            {
                if (a.TIME == b.TIME)
                    return a.LINE * a.ROW - b.LINE * b.ROW;
                else
                    return a.TIME - b.TIME;
            }
            players.Sort(cmp);
        }
        public void Load()
        {
            players.Clear();
            FileStream aFile = new FileStream("Record.dat", FileMode.Open, FileAccess.Read);
            BinaryReader myReader = new BinaryReader(aFile);
            //获取的下一个字符不是EOF
            while (myReader.PeekChar() != -1)
            {
                Player p = new Player();
                //将文件流类型变为字符型输出
                p.NAME = Convert.ToString(myReader.ReadString());
                p.LINE = Convert.ToInt32(myReader.ReadString());
                p.ROW = Convert.ToInt32(myReader.ReadString());
                p.LAND = Convert.ToInt32(myReader.ReadString());
                p.TIME = Convert.ToInt32(myReader.ReadString());
                players.Add(p);
            }
            myReader.Close();
            aFile.Close();
        }
        public void Write()
        {          
            //System.IO
            //同一工程目录下，添加形式，写入
            FileStream aFile = new FileStream("Record.dat", FileMode.Create, FileAccess.Write);
            //二进制写入流
            BinaryWriter myWriter = new BinaryWriter(aFile);
            foreach(Player p in players)
            {
                myWriter.Write(p.NAME);
                myWriter.Write(p.LINE.ToString());
                myWriter.Write(p.ROW.ToString());
                myWriter.Write(p.LAND.ToString());
                myWriter.Write(p.TIME.ToString());
            }            
            myWriter.Close();
            aFile.Close();
        }
        public void Display()
        {
            MenuForm.menufrm.LoadRecord("");
            MenuForm.menufrm.LoadRecord("姓名\t行数\t列数\t雷数\t\t用时\n");
            Sort();
            int cnt = 0;
            foreach (Player p in players)
            {
                string s; ++cnt;
                s = cnt.ToString() + ". " + p.NAME + "\t"; s += p.LINE.ToString() + "\t"; s += p.ROW.ToString() + "\t";
                s += p.LAND.ToString() + "\t";
                s += " " + (p.TIME / 60).ToString() + "分" + (p.TIME % 60).ToString() + "秒\n";
                MenuForm.menufrm.LoadRecord(s) ;
            }
        }
        public void Clear()
        {
            players.Clear();
            Write();
        }
    }
}
