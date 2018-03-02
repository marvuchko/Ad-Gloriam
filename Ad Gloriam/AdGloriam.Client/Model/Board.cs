using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad_Gloriam.Model
{
    public class Board
    {
        public enum Field
        {
            Invalid = -1,
            Free,
            Player1,
            Player2
        }

        private Field[,] _fields;

        public Field[,] Fields { get => _fields; set => _fields = value; }

        public Board()
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            Fields = new Field[8, 8];

            for(int i=0; i<8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Fields[i, j] = Field.Free;
                }
            }

            Fields[0, 0] = Field.Player1;
            Fields[7, 0] = Field.Player1;
            Fields[0, 7] = Field.Player2;
            Fields[7, 7] = Field.Player2;
        }

        public void SetOwner(int x, int y, Field owner)
        {
            if (x < 0 || y < 0 || x > 7 || y > 7) return;
            Fields[x, y] = owner;
        }

    }
}
