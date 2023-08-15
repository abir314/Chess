namespace Chess1;

public class ChessBoard
{
    public int Size;
    public BoardTile[,] Tiles;
    public ChessPiece[] Player1Pieces;
    public ChessPiece[] Player2Pieces;
    private string chessBoardHorizintalSymbol;
    private string chessBoardVerticalSymbol;
    public ChessPiece TileSpace;
    public List<string> Player1WonPieces;
    public List<string> Player2WonPieces;
    public bool NoLegalMoves;

    public ChessBoard(int size)
    {
        Size = size;
        TileSpace = new ChessPiece("  ",0);
        Player1WonPieces = new List<string>();
        Player2WonPieces = new List<string>();
        NoLegalMoves = false;
        Tiles = new BoardTile[Size , Size];
        for (var i = 0; i < Size; i++)
        {
            for (var j = 0; j < Size; j++)
            {
                Tiles[i,j] = new BoardTile(false, i,j, new ChessPiece("  ", 0,i,j));  
            }
            
        }

        chessBoardHorizintalSymbol = "+---";
        chessBoardVerticalSymbol = "|";
        Player1Pieces = new ChessPiece[] { 
            new ChessPiece("R1",0, 0,0),
            new ChessPiece("N1",0,0,1),
            new ChessPiece("B1",0,0,2),
            new ChessPiece(" Q",0,0,3),
            new ChessPiece(" K",0,0,4),
            new ChessPiece("B2",0,0,5),
            new ChessPiece("N2",0,0,6),
            new ChessPiece("R2",0,0,7),
            new ChessPiece("P1",0,1,0),
            new ChessPiece("P2",0,1,1),
            new ChessPiece("P3",0,1,2),
            new ChessPiece("P4",0,1,3),
            new ChessPiece("P5",0,1,4),
            new ChessPiece("P6",0,1,5),
            new ChessPiece("P7",0,1,6),
            new ChessPiece("P8",0,1,7)};
        
        Player2Pieces = new ChessPiece[] { 
            new ChessPiece("r1",0,7,0),
            new ChessPiece("n1",0,7,1),
            new ChessPiece("b1",0,7,2),
            new ChessPiece(" q",0,7,3),
            new ChessPiece(" k",0,7,4),
            new ChessPiece("b2",0,7,5),
            new ChessPiece("n2",0,7,6),
            new ChessPiece("r2",0,7,7),
            new ChessPiece("p1",0,6,0),
            new ChessPiece("p2",0,6,1),
            new ChessPiece("p3",0,6,2),
            new ChessPiece("p4",0,6,3),
            new ChessPiece("p5",0,6,4),
            new ChessPiece("p6",0,6,5),
            new ChessPiece("p7",0,6,6),
            new ChessPiece("p8",0,6,7)};
    }

    public void Print()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("P-pawn, R-rook, N-knight, B-Bishop, Q-queen, K-king");
        Console.WriteLine();
        Console.WriteLine("Player 1 ---");
        foreach (var player1WonPiece in Player1WonPieces)
        {
            Console.Write($"{player1WonPiece} ");
        }
        Console.WriteLine();
        Console.WriteLine("    A   B   C   D   E   F   G   H");
        for (var i = 0; i < (Size *2) +1 ; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write("  ");
                for (var j = 0; j < Size; j++)
                {
                    Console.Write(chessBoardHorizintalSymbol);
                }

                Console.Write("+\n");
            }
            else
            {
                Console.Write($"{((i-1)/2)+1} ");
                for (var j = 0; j < Size; j++)
                {
                    Console.Write(chessBoardVerticalSymbol);
                    if (Tiles[(i - 1) / 2, j].LegalNextMove)
                    {
                        if (Tiles[(i - 1) / 2, j].TilePiece.Piece != TileSpace.Piece)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            
                            if (Player2Pieces.Any(p => p.Piece == Tiles[(i - 1) / 2, j].TilePiece.Piece))
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($"{Tiles[(i - 1) / 2, j].TilePiece.Piece} ");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write($"{Tiles[(i - 1) / 2, j].TilePiece.Piece} ");
                            }
                            
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" X ");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        if (Tiles[(i - 1) / 2, j].IsSelected)
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            
                            if (Player2Pieces.Any(p => p.Piece == Tiles[(i - 1) / 2, j].TilePiece.Piece))
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($"{Tiles[(i - 1) / 2, j].TilePiece.Piece} ");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write($"{Tiles[(i - 1) / 2, j].TilePiece.Piece} ");
                            }
                            
                            Console.ResetColor();
                        }
                        else
                        {
                            if (Player2Pieces.Any(p => p.Piece == Tiles[(i - 1) / 2, j].TilePiece.Piece))
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($"{Tiles[(i - 1) / 2, j].TilePiece.Piece} ");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write($"{Tiles[(i - 1) / 2, j].TilePiece.Piece} ");
                            }
                        }
                    }
                }

                Console.Write($"| {((i-1)/2)+1}\n");
            }
        }
        Console.WriteLine("    A   B   C   D   E   F   G   H");
        Console.WriteLine();
        Console.WriteLine("Player 2 ---");
        foreach (var player2WonPiece in Player2WonPieces)
        {
            Console.Write($"{player2WonPiece} "); 
        }

        Console.WriteLine();
    }

    public void SetLegalMovesToFalse()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Tiles[i, j].LegalNextMove = false;
            }
        }
    }

    public void InitializeBoard()
    {
        Tiles[0, 0].TilePiece = Player1Pieces[0];
        Tiles[0, 1].TilePiece = Player1Pieces[1];
        Tiles[0, 2].TilePiece = Player1Pieces[2];
        Tiles[0, 3].TilePiece = Player1Pieces[3];
        Tiles[0, 4].TilePiece = Player1Pieces[4];
        Tiles[0, 5].TilePiece = Player1Pieces[5];
        Tiles[0, 6].TilePiece = Player1Pieces[6];
        Tiles[0, 7].TilePiece = Player1Pieces[7];
        Tiles[1, 0].TilePiece = Player1Pieces[8];
        Tiles[1, 1].TilePiece = Player1Pieces[9];
        Tiles[1, 2].TilePiece = Player1Pieces[10];
        Tiles[1, 3].TilePiece = Player1Pieces[11];
        Tiles[1, 4].TilePiece = Player1Pieces[12];
        Tiles[1, 5].TilePiece = Player1Pieces[13];
        Tiles[1, 6].TilePiece = Player1Pieces[14];
        Tiles[1, 7].TilePiece = Player1Pieces[15];
        Tiles[7, 0].TilePiece = Player2Pieces[0];
        Tiles[7, 1].TilePiece = Player2Pieces[1];
        Tiles[7, 2].TilePiece = Player2Pieces[2];
        Tiles[7, 3].TilePiece = Player2Pieces[3];
        Tiles[7, 4].TilePiece = Player2Pieces[4];
        Tiles[7, 5].TilePiece = Player2Pieces[5];
        Tiles[7, 6].TilePiece = Player2Pieces[6];
        Tiles[7, 7].TilePiece = Player2Pieces[7];
        Tiles[6, 0].TilePiece = Player2Pieces[8];
        Tiles[6, 1].TilePiece = Player2Pieces[9];
        Tiles[6, 2].TilePiece = Player2Pieces[10];
        Tiles[6, 3].TilePiece = Player2Pieces[11];
        Tiles[6, 4].TilePiece = Player2Pieces[12];
        Tiles[6, 5].TilePiece = Player2Pieces[13];
        Tiles[6, 6].TilePiece = Player2Pieces[14];
        Tiles[6, 7].TilePiece = Player2Pieces[15];
    }

    public void MarkLegalNextMoves(BoardTile currentTile, int playerNumber, bool checkingKingsPossibleMoves)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Tiles[i, j].LegalNextMove = false;
            }
        }

        ChessPiece currentPiece;
        NoLegalMoves = false;

        if (playerNumber == 1)
        {
            currentPiece = Player1Pieces.FirstOrDefault(p => p.Piece == currentTile.TilePiece.Piece);
            switch (currentTile.TilePiece.Piece)
            {
                case "N1":
                case "N2":
                    
                    if (IsSafe(currentTile.RowNumber + 2, currentTile.ColumnNumber + 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber + 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber + 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 2, currentTile.ColumnNumber + 1));
                    }
                    if (IsSafe(currentTile.RowNumber + 2, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber - 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 2, currentTile.ColumnNumber - 1));
                    } 
                    if (IsSafe(currentTile.RowNumber - 2, currentTile.ColumnNumber + 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber + 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber + 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber - 2, currentTile.ColumnNumber + 1));
                    }
                    if (IsSafe(currentTile.RowNumber - 2, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber - 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber - 2, currentTile.ColumnNumber - 1));
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 1, currentTile.ColumnNumber + 2));
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber -2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 1, currentTile.ColumnNumber -2));
                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber +2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber + 2));
                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber -2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber -2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber -2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber -2));
                    }
                    bool checkKnight = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkKnight = boardTile.LegalNextMove || checkKnight;
                    }
                    if(!checkKnight)
                    {
                        NoLegalMoves = true;
                    }

                    break;
                
                case " K":
                    
                    if (IsSafe(currentTile.RowNumber + 0, currentTile.ColumnNumber + 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber + 1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 0, currentTile.ColumnNumber + 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber + 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber + 0, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 0, currentTile.ColumnNumber - 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber - 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber - 1, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber - 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber - 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber - 1, currentTile.ColumnNumber +0) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber + 0));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 0],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber - 1, currentTile.ColumnNumber +1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber + 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber + 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +0) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber + 0));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 0],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber -1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber -1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber - 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                    }
                    bool checkKing = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkKing = boardTile.LegalNextMove || checkKing;
                    }
                    if(!checkKing)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case "R1":
                case "R2":
                    
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));
                            
                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i+1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i-1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i-1].TilePiece, playerNumber)) break;
                        }
                        
                    }
                    
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                            
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i-1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i-1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }
                    
                    bool checkRook = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkRook = boardTile.LegalNextMove || checkRook;
                    }
                    if(!checkRook)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case "B1":
                case "B2":
                    
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    
                    bool checkBishop = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkBishop = boardTile.LegalNextMove || checkBishop;
                    }
                    if(!checkBishop)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case " Q":
                    
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));
                            
                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i+1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i-1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i-1].TilePiece, playerNumber)) break;
                        }
                        
                    }
                    
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                            
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i-1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i-1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }
                    
                    bool checkQueen = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkQueen = boardTile.LegalNextMove || checkQueen;
                    }
                    if(!checkQueen)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case "P1":
                case "P2":
                case "P3":
                case "P4":
                case "P5":
                case "P6":
                case "P7":
                case "P8":
                    
                    if (IsSafe(currentTile.RowNumber +2, currentTile.ColumnNumber +0) && currentTile.TilePiece.MoveCount == 0 && Tiles[currentTile.RowNumber +2, currentTile.ColumnNumber +0].TilePiece.Piece == TileSpace.Piece)
                    {
                        Tiles[currentTile.RowNumber +2, currentTile.ColumnNumber +0].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +2, currentTile.ColumnNumber +0));

                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +0) && Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +0].TilePiece.Piece == TileSpace.Piece)
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +0].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber +0));

                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +1].TilePiece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber +1));

                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber -1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -1].TilePiece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber -1));
                    }
                    bool checkPawn = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkPawn = boardTile.LegalNextMove || checkPawn;
                    }
                    if(!checkPawn)
                    {
                        NoLegalMoves = true;
                    }
                    break;
                
                default:
                    break;
            }
        }
        
        if (playerNumber == 2)
        {
            currentPiece = Player2Pieces.FirstOrDefault(p => p.Piece == currentTile.TilePiece.Piece);
            switch (currentTile.TilePiece.Piece)
            {
                case "n1":
                case "n2":
                    
                    if (IsSafe(currentTile.RowNumber + 2, currentTile.ColumnNumber + 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber + 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber + 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 2, currentTile.ColumnNumber + 1));
                    }
                    if (IsSafe(currentTile.RowNumber + 2, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber + 2, currentTile.ColumnNumber - 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 2, currentTile.ColumnNumber - 1));
                    } 
                    if (IsSafe(currentTile.RowNumber - 2, currentTile.ColumnNumber + 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber + 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber + 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber - 2, currentTile.ColumnNumber + 1));
                    }
                    if (IsSafe(currentTile.RowNumber - 2, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber - 2, currentTile.ColumnNumber - 1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber - 2, currentTile.ColumnNumber - 1));
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 1, currentTile.ColumnNumber + 2));
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber -2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 1, currentTile.ColumnNumber -2));
                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber +2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber + 2));
                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber -2) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber -2].TilePiece.Piece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber -2].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber -2));
                    }
                    bool checkKnight = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkKnight = boardTile.LegalNextMove || checkKnight;
                    }
                    if(!checkKnight)
                    {
                        NoLegalMoves = true;
                    }

                    break;
                
                case " k":
                    
                    if (IsSafe(currentTile.RowNumber + 0, currentTile.ColumnNumber + 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber + 1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 0, currentTile.ColumnNumber + 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber + 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber + 0, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber + 0, currentTile.ColumnNumber - 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber - 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 0, currentTile.ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber - 1, currentTile.ColumnNumber - 1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber - 1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber - 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber - 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber - 1, currentTile.ColumnNumber +0) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber + 0));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 0],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber - 1, currentTile.ColumnNumber +1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber + 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber - 1, currentTile.ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber + 1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber +0) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber + 0));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 0],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            }
                        }
                    }
                    if (IsSafe(currentTile.RowNumber +1, currentTile.ColumnNumber -1) && CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +1, currentTile.ColumnNumber -1].TilePiece.Piece, playerNumber))
                    {
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +1, currentTile.ColumnNumber -1));
                        if (!checkingKingsPossibleMoves)
                        {
                            if (CheckIfWillNotGetChecked(Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber - 1],
                                    playerNumber))
                            {
                                Tiles[currentTile.RowNumber + 1, currentTile.ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                    }
                    bool checkKing = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkKing = boardTile.LegalNextMove || checkKing;
                    }
                    if(!checkKing)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case "r1":
                case "r2":
                    
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));
                            
                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i+1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i-1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i-1].TilePiece, playerNumber)) break;
                        }
                        
                    }
                    
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                            
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i-1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i-1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }
                    
                    bool checkRook = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkRook = boardTile.LegalNextMove || checkRook;
                    }
                    if(!checkRook)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case "b1":
                case "b2":
                    
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    
                    bool checkBishop = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkBishop = boardTile.LegalNextMove || checkBishop;
                    }
                    if(!checkBishop)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case " q":
                    
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber - i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber - i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber -i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber -i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber -i-1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (int i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber -i, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -i, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -i-1, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));
                            
                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i+1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i+1].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + i))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber + i].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +0, currentTile.ColumnNumber +i));

                            if(IsSafe(currentTile.RowNumber +0, currentTile.ColumnNumber + (i-1)) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +0, currentTile.ColumnNumber +i-1].TilePiece, playerNumber)) break;
                        }
                        
                    }
                    
                    for (var i = 1; i < Size; i++)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                            
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i+1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i+1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }

                    for (int i = -1; i > -Size; i--)
                    {
                        if (IsSafe(currentTile.RowNumber +i, currentTile.ColumnNumber + 0))
                        {
                            if(!CheckIfDoesNotContainsSamePlayersPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece.Piece, playerNumber)) break;
                        
                            Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber + 0].LegalNextMove = true;
                            currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber +i, currentTile.ColumnNumber +0));

                            if(IsSafe(currentTile.RowNumber +i-1, currentTile.ColumnNumber + 0) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i, currentTile.ColumnNumber +0].TilePiece, playerNumber) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber +i-1, currentTile.ColumnNumber +0].TilePiece, playerNumber)) break;
                        }
                    }
                    
                    bool checkQueen = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkQueen = boardTile.LegalNextMove || checkQueen;
                    }
                    if(!checkQueen)
                    {
                        NoLegalMoves = true;
                    }
                    
                    break;
                
                case "p1":
                case "p2":
                case "p3":
                case "p4":
                case "p5":
                case "p6":
                case "p7":
                case "p8":
                    
                    if (IsSafe(currentTile.RowNumber -2, currentTile.ColumnNumber +0) && currentTile.TilePiece.MoveCount == 0 && Tiles[currentTile.RowNumber -2, currentTile.ColumnNumber +0].TilePiece.Piece == TileSpace.Piece)
                    {
                        Tiles[currentTile.RowNumber -2, currentTile.ColumnNumber +0].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -2, currentTile.ColumnNumber +0));

                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber +0) && Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +0].TilePiece.Piece == TileSpace.Piece)
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +0].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber +0));

                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber +1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +1].TilePiece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber +1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber +1));

                    }
                    if (IsSafe(currentTile.RowNumber -1, currentTile.ColumnNumber -1) && CheckIfContainsOpponentsPiece(Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber -1].TilePiece, playerNumber))
                    {
                        Tiles[currentTile.RowNumber -1, currentTile.ColumnNumber -1].LegalNextMove = true;
                        currentPiece?.ListOfTilesThatItCanMoveTo.Add(new BoardTile(currentTile.RowNumber -1, currentTile.ColumnNumber -1));
                    }
                    bool checkPawn = false;
                    foreach (var boardTile in Tiles)
                    {
                        checkPawn = boardTile.LegalNextMove || checkPawn;
                    }
                    if(!checkPawn)
                    {
                        NoLegalMoves = true;
                    }
                    break;
                
                default:
                    break;
            }
        }
    }

    private bool CheckIfWillNotGetChecked(BoardTile tile, int playersKing)
    {
        int playerNumber;
        if (playersKing == 1)
        {
            playerNumber = 2;
            foreach (var player2Piece in Player2Pieces)
            {
               MarkLegalNextMoves(Tiles[player2Piece.CurrentRow, player2Piece.CurrentColumn], playerNumber, true); 
            }

            foreach (var player2Piece in Player2Pieces)
            {
                if (player2Piece.ListOfTilesThatItCanMoveTo.Any(boardTile => boardTile.RowNumber == tile.RowNumber && boardTile.ColumnNumber == tile.ColumnNumber))
                {
                    return false;
                }
            }
            return true;
        }
        
        if (playersKing == 2)
        {
            playerNumber = 1;
            foreach (var player1Piece in Player1Pieces)
            {
               MarkLegalNextMoves(Tiles[player1Piece.CurrentRow, player1Piece.CurrentColumn], playerNumber, true); 
            }

            foreach (var player1Piece in Player1Pieces)
            {
                if (player1Piece.ListOfTilesThatItCanMoveTo.Any(boardTile => boardTile.RowNumber == tile.RowNumber && boardTile.ColumnNumber == tile.ColumnNumber))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckIfContainsOpponentsPiece(ChessPiece tilePiece, int playerNumber)
    {
        if (playerNumber == 1)
        {
            foreach (var player2Piece in Player2Pieces)
            {
                if (player2Piece.Piece == tilePiece.Piece) return true;
            }
        }

        if (playerNumber == 2)
        {
            foreach (var player1Piece in Player1Pieces)
            {
                if (player1Piece.Piece == tilePiece.Piece) return true;
            }
        }

        return false;
    }

    private bool CheckIfDoesNotContainsSamePlayersPiece(string currentLocationPiece, int playerNumber)
    {
        if (playerNumber == 1)
        {
            foreach (var player1Piece in Player1Pieces)
            {
                if (player1Piece.Piece == currentLocationPiece) return false;
            }
        }
        if (playerNumber == 2)
        {
            foreach (var player2Piece in Player2Pieces)
            {
                if (player2Piece.Piece == currentLocationPiece) return false;
            }
        }

        return true;
    }

    public bool IsSafe(int row, int column)
    {
        return row > -1 && row < Size && column > -1 && column < Size;
    }

    public void ResetSelectedTile()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Tiles[i, j].IsSelected = false;
            }
        }
    }
}