using board;

namespace chess
{
    public class Tower : Piece
    {   
        public Tower(Color color, Board board, Position position) : base(color, board, position){}
        public Tower(Color color, Board board) : base(color, board){}


        private bool CanMove(Position pos){
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoviments(){
            bool[,] mat = new bool[Board.Rows, Board.Cols];
            
            //N
            Position pos = new Position(Position.Row-1, Position.Col);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Row = pos.Row - 1;
            }

            //L
            pos = new Position(Position.Row, Position.Col+1);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Col = pos.Col + 1;
            }

            //S
            pos = new Position(Position.Row+1, Position.Col);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Row = pos.Row + 1;
            }

            //O
            pos = new Position(Position.Row, Position.Col-1);
            while(Board.IsValidPosition(pos) && CanMove(pos)){
                mat[pos.Row, pos.Col] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color){
                    break;
                }
                pos.Col = pos.Col - 1;
            }
             
            return mat;
        }
 

        public override string ToString()
        {
            return "T";
        }
    }
}