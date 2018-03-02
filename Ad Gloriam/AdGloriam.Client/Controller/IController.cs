using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ad_Gloriam.Model;

namespace Ad_Gloriam.Controller
{
    public interface IController
    {
        Board.Field[,] GetTableFields();
        void FieldClicked(int x, int y);
        Point GetSelectedCoordinates();
        List<Point> GetAvailableFieldsCoordinates(int x, int y);
        void CreateGame();
        bool JoinGame(GameItem gItem, string pwd);
        void SendMessage(string message);
        List<GameItem> FindGames();
        void EndGame();
    }
}
