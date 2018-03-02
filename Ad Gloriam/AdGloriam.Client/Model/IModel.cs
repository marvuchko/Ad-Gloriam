using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ad_Gloriam.Model
{
    interface IModel
    {
        void ResetTable();
        void ResetPoints();
        void SetField(int x, int y);
        void ReleaseField(int x, int y);
        void SetSelected(int x, int y);
        Point GetSelectedCoordinates();
        void SetPointsPlayer1(int value);
        void SetPointsPlayer2(int value);
        int GetPointsPlayer1();
        int GetPointsPlayer2();
        Board.Field[,] GetTable();
        Board.Field GetField(int x, int y);
        void NextPlayer();
        int GetPlayer();
        Game GetGame();
    }
}
