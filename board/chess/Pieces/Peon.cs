using board;

namespace chess
{
    public class Peon : Piece
    {   
        private ChessGame Match;
        public Peon(Color color, Board board, Position position) : base(color, board, position){}
        public Peon(Color color, Board board, ChessGame match) : base(color, board){
            Match = match;
        }

        private bool existsEnemy(Position pos){
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        private bool FreePosition(Position pos){
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Rows, Board.Cols];
            
            Position pos;
            if(Color == Color.WHITE){
                pos = new Position(Position.Row-1, Position.Col);
                if(Board.IsValidPosition(pos) && FreePosition(pos)){
                    mat[pos.Row, pos.Col] = true;
                }

                pos = new Position(Position.Row-2, Position.Col);
                if(Board.IsValidPosition(pos) && FreePosition(pos) && QtdMoviments == 0){
                    mat[pos.Row, pos.Col] = true;
                }

                pos = new Position(Position.Row-1, Position.Col-1);
                if(Board.IsValidPosition(pos) && existsEnemy(pos)){
                    mat[pos.Row, pos.Col] = true;
                }

                pos = new Position(Position.Row-1, Position.Col+1);
                if(Board.IsValidPosition(pos) && existsEnemy(pos)){
                    mat[pos.Row, pos.Col] = true;
                }

                if(Position.Row == 3){
                    Position leftPosition = new Position(Position.Row, Position.Col-1);
                    if(Board.IsValidPosition(leftPosition) && existsEnemy(leftPosition) && Board.GetPiece(leftPosition) == Match.EmPassantVulnerable){
                        mat[leftPosition.Row-1, leftPosition.Col] = true;
                    }
                    Position rightPosition = new Position(Position.Row, Position.Col-1);
                    if(Board.IsValidPosition(rightPosition) && existsEnemy(rightPosition) && Board.GetPiece(rightPosition) == Match.EmPassantVulnerable){
                        mat[rightPosition.Row-1, rightPosition.Col] = true;
                    }
                }
            }else{
                pos = new Position(Position.Row+1, Position.Col);
                if(Board.IsValidPosition(pos) && FreePosition(pos)){
                    mat[pos.Row, pos.Col] = true;
                }

                pos = new Position(Position.Row+2, Position.Col);
                if(Board.IsValidPosition(pos) && FreePosition(pos) && QtdMoviments == 0){
                    mat[pos.Row, pos.Col] = true;
                }

                pos = new Position(Position.Row+1, Position.Col-1);
                if(Board.IsValidPosition(pos) && existsEnemy(pos)){
                    mat[pos.Row, pos.Col] = true;
                }

                pos = new Position(Position.Row+1, Position.Col+1);
                if(Board.IsValidPosition(pos) && existsEnemy(pos)){
                    mat[pos.Row, pos.Col] = true;
                }
            }

            // Special move en passant
            if(Position.Row == 3){
                Position leftPosition = new Position(Position.Row, Position.Col-1);
                if(Board.IsValidPosition(leftPosition) && existsEnemy(leftPosition) && Board.GetPiece(leftPosition) == Match.EmPassantVulnerable){
                    mat[leftPosition.Row+1, leftPosition.Col] = true;
                }
                Position rightPosition = new Position(Position.Row, Position.Col-1);
                if(Board.IsValidPosition(rightPosition) && existsEnemy(rightPosition) && Board.GetPiece(rightPosition) == Match.EmPassantVulnerable){
                    mat[rightPosition.Row+1, rightPosition.Col] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}