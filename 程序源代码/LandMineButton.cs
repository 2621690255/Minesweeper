using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 扫雷
{
    public class LandMineButton : Button
    {
        private int x, y; //按钮所在点（x, y）
        private bool is_boom; //true有雷
        private bool abled; //控件是否可用
        public LandMineButton()
        {
            Tag = 0; //0表示未被标记，继承自Button
            Size = new System.Drawing.Size(30, 30); //设置大小
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public bool Is_Boom
        {
            get { return is_boom; }
            set { is_boom = value; }
        }
        public bool ABLED
        {
            get { return abled; }
            set { abled = value; }
        }
            
    }
}
