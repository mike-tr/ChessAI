using UnityEngine;

namespace Chess {
    using System.Collections.Generic;
    public static class ChessFenUtility {
        public const string startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public static Dictionary<char, int> CharToPiece = new Dictionary<char, int>() {
            ['r'] = Piece.Rook,
            ['k'] = Piece.King,
            ['b'] = Piece.Bishop,
            ['q'] = Piece.Queen,
            ['n'] = Piece.Knight,
            ['p'] = Piece.Pawn
        };
        public static LoadedPositionInfo LoadFenPosition(string fen) {
            var position = new LoadedPositionInfo();
            string[] sections = fen.Split(' ');
            var rank = 7;
            var x = 0;
            foreach (var c in sections[0]) {
                if (c == '/') {
                    rank--;
                    x = 0;
                } else if (char.IsDigit(c)) {
                    x += (int)char.GetNumericValue(c);
                } else {
                    int piece = CharToPiece[char.ToLower(c)];
                    if (char.IsUpper(c)) {
                        piece = piece | Piece.White;
                    } else {
                        piece = piece | Piece.Black;
                    }
                    position.Squares[rank * 8 + x] = piece;
                    x++;
                }
            }
            return position;
        }

    }

    public class LoadedPositionInfo {
        public int[] Squares;
        public bool WhiteToMove;
        public bool WhiteCastleKingside;
        public bool WhiteCastleQueenside;
        public bool BlackCastleKingside;
        public bool BlackCastleQueenside;
        public int EnPassant;
        public int PlyCount;

        public LoadedPositionInfo() {
            Squares = new int[64];
        }
    }
}