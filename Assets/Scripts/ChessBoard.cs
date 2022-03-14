using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    public class ChessBoard {
        public const int white = PlayerColor.white;
        public const int black = PlayerColor.black;

        public int[] Squares = new int[64];
        public List<int> Pieces { get; private set; } = new List<int>();
        public int CurrentPlayer { get; private set; } = PlayerColor.white;
        public int EnemyColor { get { return CurrentPlayer == PlayerColor.white ? PlayerColor.black : PlayerColor.white; } }
        public int MovesMade { get; private set; }

        public ChessBoard() {
            // Create a plain new Board
            this.MovesMade = 0;

            var position = ChessFenUtility.LoadFenPosition(ChessFenUtility.startFen);
            // Square[0] = Piece.Queen | Piece.White;
            // Square[7] = Piece.Rook | Piece.White;
            // Square[7 * 8] = Piece.King | Piece.Black;
            // pieces.Add (white, new List<ChessPiece> ());
            // pieces.Add (black, new List<ChessPiece> ());
            // for (int y = 0; y < 8; y++) {
            //     for (int x = 0; x < 8; x++) {
            //         nodes[x, y] = new ChessNode(this, x, y);
            //     }
            // }
            //Squares = position.Squares;
            for (int i = 0; i < 64; i++) {
                Squares[i] = position.Squares[i];
                //nodes[i, 6].InitializePiece(PieceType.Pawn, black, this);
                //nodes[i, 1].InitializePiece(PieceType.Pawn, white, this);
            }

            // initSide (7, black);
            // initSide (0, white);
            // hash = ChessBoardMemory.instance.BoardHash(this);
        }

        public ChessBoard Copy() {
            return new ChessBoard(this);
        }

        private ChessBoard(ChessBoard other) {
            // Generate a copy of a board
            this.CurrentPlayer = other.CurrentPlayer;
            this.MovesMade = other.MovesMade;
            for (int i = 0; i < 64; i++) {
                this.Squares[i] = other.Squares[i];
            }
            this.Pieces = new List<int>(other.Pieces);
        }

        private List<PieceMove>[] PlayerMoves = new List<PieceMove>[4];
        private int GetIndex(int color, bool validated) {
            int v = validated ? 1 : 0;
            return color == black ? v : 2 + v;
        }

        public List<PieceMove> GetAllPlayerMoves(int color, bool validated) {
            // int index = GetIndex(color, validated);
            // if (PlayerMoves[index] != null) {
            //     return PlayerMoves[index];
            // }
            // List<PieceMove> moves = new List<PieceMove>();
            // // for each of the current player units calculate all possible moves.
            // foreach (var piece in Pieces) {
            //     if (!validated) {
            //         // get any move.
            //         moves.AddRange(piece.GetMoves());
            //     } else {
            //         // get only valid moves.
            //         moves.AddRange(piece.GetValidMoves());
            //     }
            // }
            // // a.k.a its possible we might actually try to draw the same moves multiple times per turn,
            // // so why not save them?, hopefully it wont cause memory leak.
            // PlayerMoves[index] = moves;
            // return moves;
            return null;
        }

        public bool IsChecked(int color) {
            // var enemyColor = color == PlayerColor.white ? PlayerColor.black : PlayerColor.white;
            // ChessNode node = kings[color].node;
            // foreach (PieceType type in System.Enum.GetValues (typeof (PieceType))) {
            //     if (type == PieceType.none) {
            //         continue;
            //     }
            //     var piece = new ChessPiece (node, type, color, true);
            //     foreach (var move in piece.GetMoves ()) {
            //         var current = GetPieceAt (move.end.x, move.end.y);
            //         if (current != null && current.type == piece.type && current.color == enemyColor) {
            //             return true;
            //         }
            //     }
            // }
            return false;
        }

        public void ChangeTurn() {
            // if (currentPlayer == PlayerColor.black)
            //     currentPlayer = PlayerColor.white;
            // else
            //     currentPlayer = PlayerColor.black;
        }

        // public ChessPiece GetPieceAt(int x, int y) {
        //     return nodes[x, y].piece;
        // }

        public void initSide(int y, int player) {
            // nodes[0, y].InitializePiece(PieceType.Rook, player, this);
            // nodes[7, y].InitializePiece(PieceType.Rook, player, this);
            // nodes[1, y].InitializePiece(PieceType.Knight, player, this);
            // nodes[6, y].InitializePiece(PieceType.Knight, player, this);
            // nodes[2, y].InitializePiece(PieceType.Bishop, player, this);
            // nodes[5, y].InitializePiece(PieceType.Bishop, player, this);
            // nodes[4, y].InitializePiece(PieceType.Queen, player, this);
            // nodes[3, y].InitializePiece(PieceType.King, player, this);
        }

        public ChessBoard ApplyMove(PieceMove move) {
            // var finalBoard = this.Copy();
            // var end_node = move.end.GetNode(finalBoard);
            // // Remove hash of prev piece position
            // ChessBoardMemory memory = ChessBoardMemory.instance;
            // BitString encoding = memory.GetEncoding(move.start, this);
            // finalBoard.hash *= encoding;
            // // Remove hash of next piece position ( if there was unit on it )
            // encoding = memory.GetEncoding(end_node);
            // finalBoard.hash *= encoding;

            // // Apply new turn.
            // finalBoard.ChangeTurn();
            // end_node.ReplaceWithAnother(move.start.GetNode(finalBoard));

            // // if its a pawn and it got to the "end" convert it to a queen.
            // var piece = end_node.piece;
            // piece.moved = true;
            // if (piece.type == PieceType.Pawn) {
            //     if (piece.color == PlayerColor.black) {
            //         if (end_node.y == 0) {
            //             piece.type = PieceType.Queen;
            //         }
            //     } else if (end_node.y == 7) {
            //         piece.type = PieceType.Queen;
            //     }
            // }
            // // Add hash of new position.
            // encoding = memory.GetEncoding(end_node);
            // finalBoard.hash *= encoding;
            // finalBoard.movesMade++;

            // // ChessBoard saved = memory.GetBoard(finalBoard.movesMade, finalBoard.hash);
            // // if(saved == null){
            // //     saved = finalBoard;
            // //     memory.push(saved);
            // // }

            // //Debug.Log(finalBoard.hash);
            // //Debug.Log(memory.BoardHash(finalBoard));
            // return finalBoard;
            return null;
        }

        public bool CheckRealMove(PieceMove move) {
            // check that the move passed is actuall move.
            return true;
        }
    }
}