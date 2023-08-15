namespace Chess1;

public class ChessPiece
{
    public string Piece;
    public int MoveCount;
    public List<BoardTile> ListOfTilesThatItCanMoveTo;
    public int CurrentRow;
    public int CurrentColumn;

    public ChessPiece(string piece, int moveCount, int currentRow, int currentColumn)
    {
        Piece = piece;
        MoveCount = moveCount;
        CurrentRow = currentRow;
        CurrentColumn = currentColumn;
        ListOfTilesThatItCanMoveTo = new List<BoardTile>();
    }

    public ChessPiece(string piece, int moveCount)
    {
        Piece = piece;
        MoveCount = moveCount;
    }
}