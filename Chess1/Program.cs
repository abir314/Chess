using System.Data;
using System.Drawing;
using Chess1;
using static System.IO.Stream;

class Program
{
    static void Main(string[] args)
    {
        var myBoard = new ChessBoard(8);
        myBoard.InitializeBoard();
        int roundCount = 1;
        int playerNumber;
        bool winnerFound = false;
        int row = 0;
        int column = 0;
        int rowToMoveTo = 0;
        int columnToMoveTo = 0;
        while (!winnerFound)
        {
            Console.Clear();
            myBoard.ResetSelectedTile();
           
            if (roundCount % 2 != 0)
            {
                playerNumber = 1;
                bool player1ValidInput = false;
          
                while (!player1ValidInput)
                {
                    myBoard.ResetSelectedTile();
                    myBoard.SetLegalMovesToFalse();
                    myBoard.Print();
                    Console.WriteLine();
                    Console.WriteLine($"Round: {roundCount}");
                    Console.WriteLine();
                    Console.WriteLine("Player 1's turn.");
                    Console.WriteLine("Choose a piece -");
                    var userChoice = Console.ReadLine();
                    if (userChoice.Length > 0 && userChoice.Length < 3 && (userChoice.Length == 2
                            ? (("PRNB").Contains(char.ToUpper(userChoice[0])) && ("12345678").Contains(userChoice[1]))
                            : ("QK").Contains(char.ToUpper(userChoice[0]))))
                    {
                        for (int i = 0; i < myBoard.Size; i++)
                        {
                            for (int j = 0; j < myBoard.Size; j++)
                            {
                                if ((myBoard.Tiles[i, j].TilePiece.Piece == userChoice.ToUpper()) ||
                                    (myBoard.Tiles[i, j].TilePiece.Piece == (" " + userChoice.ToUpper())))
                                {
                                    row = i;
                                    column = j;
                                    break;
                                }
                            }
                        }

                        myBoard.MarkLegalNextMoves(myBoard.Tiles[row, column], 1, false);
                        myBoard.Tiles[row, column].IsSelected = true;
                        if (myBoard.NoLegalMoves)
                        {
                            Console.WriteLine("Nowhere to move, choose another piece.");
                        }

                        else
                        {
                            myBoard.Print();
                            Console.WriteLine("Input position to move-- column and row. Example - A1 or a1");
                            var positionToMove = Console.ReadLine();
                            if (positionToMove.Length == 2 &&
                                ("12345678").Contains(positionToMove[1]) &&
                                ("ABCDEFGH").Contains(char.ToUpper(positionToMove[0])))
                            {
                                char c = positionToMove[0];
                                columnToMoveTo = (char.ToUpper(c) - 64) - 1;
                                char d = positionToMove[1];
                                rowToMoveTo = (d - '0') - 1;

                                if (rowToMoveTo < 8 && columnToMoveTo < 8)
                                {
                                    if (myBoard.Tiles[rowToMoveTo, columnToMoveTo].LegalNextMove)
                                    {
                                        player1ValidInput = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("Invalid input, try again.");
                    Thread.Sleep(2000);
                }

                myBoard.Tiles[row, column].TilePiece.MoveCount++;
                var pieceToMove = myBoard.Tiles[row, column].TilePiece;
                var pieceToMoveTo = myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece;
                // myBoard.Tiles[row, column].TilePiece.CurrentRow = rowToMoveTo;
                // myBoard.Tiles[row, column].TilePiece.CurrentColumn = columnToMoveTo;
                var player1PieceToMove = myBoard.Player1Pieces.FirstOrDefault(p => p.Piece == pieceToMove.Piece);
                player1PieceToMove.CurrentRow = rowToMoveTo;
                player1PieceToMove.CurrentColumn = columnToMoveTo;
                if (CheckToCapturePiece(myBoard, pieceToMoveTo, playerNumber))
                {
                    myBoard.Player2Pieces = myBoard.Player2Pieces.Where(s => s.Piece != pieceToMoveTo.Piece).ToArray();
                    myBoard.Player1WonPieces.Add(pieceToMoveTo.Piece);

                }
                // else
                // {
                //     (myBoard.Tiles[row, column].TilePiece, myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece) = (myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece, myBoard.Tiles[row, column].TilePiece);
                // }
                myBoard.Tiles[row, column].TilePiece = myBoard.TileSpace;
                myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece = pieceToMove;
                
                myBoard.Print();
            }

            if (roundCount % 2 == 0)
            {
                playerNumber = 2;
                bool player2ValidInput = false;
          
                while (!player2ValidInput)
                {
                    myBoard.ResetSelectedTile();
                    myBoard.SetLegalMovesToFalse();
                    myBoard.Print();
                    Console.WriteLine();
                    Console.WriteLine($"Round: {roundCount}");
                    Console.WriteLine();
                    Console.WriteLine("Player 2's turn.");
                    Console.WriteLine("Choose a piece -");
                    var userChoice = Console.ReadLine();
                    if (userChoice.Length > 0 && userChoice.Length < 3 && (userChoice.Length == 2
                            ? (("prnb").Contains(char.ToLower(userChoice[0])) && ("12345678").Contains(userChoice[1]))
                            : ("qk").Contains(char.ToLower(userChoice[0]))))
                    {
                        for (int i = 0; i < myBoard.Size; i++)
                        {
                            for (int j = 0; j < myBoard.Size; j++)
                            {
                                if ((myBoard.Tiles[i, j].TilePiece.Piece == userChoice.ToLower()) ||
                                    (myBoard.Tiles[i, j].TilePiece.Piece == (" " + userChoice.ToLower())))
                                {
                                    row = i;
                                    column = j;
                                    break;
                                }
                            }
                        }

                        myBoard.MarkLegalNextMoves(myBoard.Tiles[row, column], 2, false);
                        myBoard.Tiles[row, column].IsSelected = true;
                        if (myBoard.NoLegalMoves)
                        {
                            Console.WriteLine("Nowhere to move, choose another piece.");
                        }

                        else
                        {
                            myBoard.Print();
                            Console.WriteLine("Input position to move-- column and row. Example - A1 or a1");
                            var positionToMove = Console.ReadLine();
                            if (positionToMove.Length == 2 &&
                                ("12345678").Contains(positionToMove[1]) &&
                                ("ABCDEFGH").Contains(char.ToUpper(positionToMove[0])))
                            {
                                char c = positionToMove[0];
                                columnToMoveTo = (char.ToUpper(c) - 64) - 1;
                                char d = positionToMove[1];
                                rowToMoveTo = (d - '0') - 1;

                                if (rowToMoveTo < 8 && columnToMoveTo < 8)
                                {
                                    if (myBoard.Tiles[rowToMoveTo, columnToMoveTo].LegalNextMove)
                                    {
                                        player2ValidInput = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("Invalid input, try again.");
                    Thread.Sleep(2000);
                }

                myBoard.Tiles[row, column].TilePiece.MoveCount++;
                var pieceToMove = myBoard.Tiles[row, column].TilePiece;
                var pieceToMoveTo = myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece;
                // myBoard.Tiles[row, column].TilePiece.CurrentRow = rowToMoveTo;
                // myBoard.Tiles[row, column].TilePiece.CurrentColumn = columnToMoveTo;
                var player2PieceToMove = myBoard.Player2Pieces.FirstOrDefault(p => p.Piece == pieceToMove.Piece);
                player2PieceToMove.CurrentRow = rowToMoveTo;
                player2PieceToMove.CurrentColumn = columnToMoveTo;
                if (CheckToCapturePiece(myBoard, pieceToMoveTo, playerNumber))
                {
                    myBoard.Player1Pieces = myBoard.Player1Pieces.Where(s => s.Piece != pieceToMoveTo.Piece).ToArray();
                    myBoard.Player2WonPieces.Add(pieceToMoveTo.Piece);
                }
                // else
                // {
                //     (myBoard.Tiles[row, column].TilePiece, myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece) = (myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece, myBoard.Tiles[row, column].TilePiece);
                // }
                myBoard.Tiles[row, column].TilePiece = myBoard.TileSpace;
                myBoard.Tiles[rowToMoveTo, columnToMoveTo].TilePiece = pieceToMove;
                
                myBoard.Print();
            }

            roundCount++;
            winnerFound = Findwinner(myBoard, out var winner);
            if (winnerFound)
            {
                Console.WriteLine($"Game over and the winner is {winner}");
            }
        }

    }
    

    private static bool CheckToCapturePiece(ChessBoard myBoard, ChessPiece pieceToMoveTo, int playerNumber)
    {
        if (playerNumber == 1)
        {
            if (myBoard.Player2Pieces.Any(player2Piece => player2Piece.Piece == pieceToMoveTo.Piece))
            {
                return true;
            }
        }
        if (playerNumber == 2)
        {
            if (myBoard.Player1Pieces.Any(player1Piece => player1Piece.Piece == pieceToMoveTo.Piece))
            {
                return true;
            }
        }
        
        return false;
    }

    private static bool Findwinner(ChessBoard myBoard, out string winner)
    {
        winner = "";
        
        if (myBoard.Player1Pieces.All(player1Piece => player1Piece.Piece != " K"))
        {
            winner = "Player 2";
            return true;
        }
        
        if (myBoard.Player2Pieces.All(player2Piece => player2Piece.Piece != " k"))
        {
            winner = "Player 1";
            return true;
        }
        
        return false;
    }
}