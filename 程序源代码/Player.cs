using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 扫雷
{
    //玩家记录
    class Player
    {
        private string name; //姓名
        private int line, row, land, time; //行，列，雷，时间
        public Player()
        {
            name = ""; line = 0; row = 0; land = 0; time = 0;
        }
        public Player(string _name, int _line, int _row, int _land, int _time)
        {
            name = _name; line = _line; row = _row; land = _land; time = _time;
        }
        public string NAME
        {
            get { return name; }
            set { name = value; }
        }
        public int LINE
        {
            get { return line; }
            set { line = value; }
        }
        public int ROW
        {
            get { return row; }
            set { row = value; }
        }
        public int LAND
        {
            get { return land; }
            set { land = value; }
        }
        public int TIME
        {
            get { return time; }
            set { time = value; }
        }      
    }
}
