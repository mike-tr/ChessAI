using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    public class ChessBoard {
        public const int EnPassantNO = ChessFenUtility.LoadedPositionInfo.NoEnPessant;
        public readonly static PlayerIndexColor white = PlayerIndexColor.white;
        public readonly static PlayerIndexColor black = PlayerIndexColor.black;

        public int[] Squares = new int[64];
        public Dictionary<PlayerIndexColor, List<int>> Pieces { get; private set; } = new Dictionary<PlayerIndexColor, List<int>>();
        public PlayerIndexColor CurrentPlayer { get; private set; } = PlayerIndexColor.white;
        public PlayerIndexColor EnemyColor { get; private set; } = PlayerIndexColor.black;
        public int MovesMade { get; private set; }
        public int EnPassant { get; private set; } = EnPassantNO;

        public bool WhiteCastleKingside { get; private set; }
        public bool WhiteCastleQueenside { get; private set; }
        public bool BlackCastleKingside { get; private set; }
        public bool BlackCastleQueenside { get; private set; }

        public PsuedoLegalMoves PLM;
        public ChessBoard() {
            // Create a plain new Board
            this.MovesMade = 0;

            var position = ChessFenUtility.LoadFenPosition(ChessFenUtility.startFen);
            this.Pieces = new Dictionary<PlayerIndexColor, List<int>>();
            this.Pieces.Add(PlayerIndexColor.white, new List<int>());
            this.Pieces.Add(PlayerIndexColor.black, new List<int>());
            for (int i = 0; i < 64; i++) {
                int piece = position.Squares[i];
                Squares[i] = piece;
                if (piece != Piece.None) {
                    PlayerIndexColor index = Piece.GetColor(piece);
                    this.Pieces[index].Add(piece);
                }
            }
            PLM = new PsuedoLegalMoves(this);
            this.CurrentPlayer = position.WhiteToMove ? PlayerIndexColor.white : PlayerIndexColor.black;
            this.EnemyColor = !this.CurrentPlayer;
            this.EnPassant = position.EnPassant;
            this.BlackCastleQueenside = position.BlackCastleQueenside;
            this.BlackCastleKingside = position.BlackCastleKingside;
            this.WhiteCastleKingside = position.WhiteCastleKingside;
            this.WhiteCastleQueenside = position.WhiteCastleQueenside;
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
            this.Pieces = new Dictionary<PlayerIndexColor, List<int>>();
            //this.Pieces.Add(PlayerIndexColor.white, new List<int>());
            //this.Pieces.Add(PlayerIndexColor.black, new List<int>());
            foreach (var key in other.Pieces.Keys) {
                this.Pieces.Add(key, new List<int>(other.Pieces[key]));
            }

            for (int i = 0; i < 64; i++) {
                this.Squares[i] = other.Squares[i];
            }
            this.CurrentPlayer = other.CurrentPlayer;
            this.EnemyColor = other.EnemyColor;
            this.BlackCastleKingside = other.BlackCastleKingside;
            this.BlackCastleQueenside = other.BlackCastleQueenside;
            this.WhiteCastleKingside = other.WhiteCastleKingside;
            this.WhiteCastleQueenside = other.WhiteCastleQueenside;
            this.EnPassant = other.EnPassant;
            this.MovesMade = other.MovesMade;

            PLM = new PsuedoLegalMoves(this);
            //this.Pieces = new List<int>(other.Pieces);
        }

        private List<PieceMove>[] PlayerMoves = new List<PieceMove>[4];
        private int GetIndex(int color, bool validated) {
            int v = validated ? 1 : 0;
            return color == black ? v : 2 + v;
        }

        public List<PieceMove> GetPieceMoves(int Square) {
            return PLM.GetMoves(Square);
        }


        public List<PieceMove> GetAllPlayerMoves(PlayerIndexColor color, bool validated) {
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

        public bool IsChecked(PlayerIndexColor color) {
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
            CurrentPlayer = EnemyColor;
            EnemyColor = !EnemyColor;
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
            // we just need to swap the pieces and put empty piece on the old position.
            // remove the piece that was eaten, from all references.
            var finalBoard = this.Copy();
            var targetSqaurePiece = finalBoard.Squares[move.TargetSquare];
            if (targetSqaurePiece != Piece.None) {
                finalBoard.Pieces[Piece.GetColor(targetSqaurePiece)].Remove(targetSqaurePiece);
            }
            var pieceToMove = finalBoard.Squares[move.StartSquare];
            finalBoard.Squares[move.StartSquare] = Piece.None;
            finalBoard.Squares[move.TargetSquare] = pieceToMove;
            finalBoard.ChangeTurn();
            finalBoard.MovesMade++;
            Debug.Log(finalBoard.MovesMade + " " + this.MovesMade);
            return finalBoard;
        }

        public bool CheckRealMove(PieceMove move) {
            // check that the move passed is actuall move.
            return true;
        }
    }
}