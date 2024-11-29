using Game10003;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME_10003_Game_Development_Foundations___2D_Game_Template__v1._2_1
{
    public class Control_Screen
    {
        public void ControlScreen(bool Show)
        {
            if (Show == true)
            {
                Text.Color = Color.Black;
                Text.Draw("A = Left",10,10);
                Text.Draw("D = Right",10,40);
                Text.Draw("W = Jump",10,70);
                Text.Draw("S = Slow",10,100);
                Text.Draw("Left Shift = Sprint",10,130);
                Text.Draw("C = Controls Screen",10,160);
            }
        }
    }
}
