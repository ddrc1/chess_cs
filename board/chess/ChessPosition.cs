using board;

namespace chess
{
    public class ChessPosition : Position
    {
        public ChessPosition(char col, int row) : base(){
            Col = char.ToLower(col) - 'a';
            Row = 8 - row;
        }
    }
}