using Game10003;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GAME_10003_Game_Development_Foundations___2D_Game_Template__v1._2_1
{
    public class Tiles
    {
        public void DrawTile(Vector2 position, Vector2 scale)
        {
            Draw.FillColor = Game10003.Color.Black;
            Draw.Rectangle(position,scale);
        }
    }
}
