using System;

namespace board
{
    public class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(){}
        
        public Position(int row, int col){
            Row = row;
            Col = col;
        }
    
        public override string ToString()
        {
            return $"({Row}, {Col})";
        }
    }
}