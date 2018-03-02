using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ad_Gloriam.Model;

namespace Ad_Gloriam.View
{
    interface IView
    {
        void GameOver(int winner);
        void UpdatePoints(int points1, int points2);
        void DisplayMessage(string message);
        void Redraw();
        void UpdatePlayerTurn(int player);
    }
}
