
namespace board
{
    public abstract class Piece
    {
        public Color Color { get; protected set; }
        public int QtdMoviments { get; protected set; }
        protected Board Board { get; set;}
        public Position Position { get; set; }
    
        public Piece(Color color, Board board){
            Color = color;
            Board = board;
            QtdMoviments = 0;
        }

        public Piece(Color color, Board board, Position position) : this(color, board){
            Position = position;
        }

        public void IncreaseMoviment(){
            QtdMoviments ++;
        }

        public void DecreaseMoviment(){
            QtdMoviments --;
        }

        public bool AreThereAvailableMoviments(){
            bool[,] possibleMoviments = PossibleMoviments();
            
            for (int i = 0; i < Board.Rows; i++){
                for (int j = 0; j < Board.Cols; j++){
                    if(possibleMoviments[i, j]){
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos){
            return PossibleMoviments()[pos.Row, pos.Col];
        }

        public abstract bool[,] PossibleMoviments();
    }
}