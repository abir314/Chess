namespace Chess1;

public class BoardTile
{
    public string Color;
    public bool IsSelected;
    public ChessPiece TilePiece;
    public int RowNumber;
    public int ColumnNumber;
    public bool LegalNextMove;
    

    public BoardTile(bool isSelected, int rowNumber, int columnNumber, ChessPiece tilePiece)
    {
        IsSelected = isSelected;
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        TilePiece = tilePiece;
    }

    public BoardTile(int rowNumber, int columnNumber)
    {
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
    }
    
    
}