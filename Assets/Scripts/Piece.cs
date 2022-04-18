using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    public static class Piece {
        public const int None = 0;
        public const int King = 1 << 1;
        public const int Pawn = 2 << 1;
        public const int Knight = 3 << 1;
        public const int Bishop = 5 << 1;
        public const int Rook = 6 << 1;
        public const int Queen = 7 << 1;
        public const int White = 1; // after mask
        public const int Black = 0; // after mask

        const int typeMask = 0b001110;
        const int whiteMask = 0b00001;
        const int colorMask = whiteMask;

        public static bool IsColour(int piece, PlayerIndexColor colour) {
            return (piece & colorMask) == colour.index;
        }

        public static PlayerIndexColor GetColor(int piece) {
            return (piece & whiteMask) == White ? PlayerIndexColor.white : PlayerIndexColor.black;
        }

        public static int GetPieceType(int piece) {
            return piece & typeMask;
        }

        public static bool IsRookOrQueen(int piece) {
            return (piece & 0b110) == 0b110;
        }

        public static bool IsBishopOrQueen(int piece) {
            return (piece & 0b101) == 0b101;
        }

        public static bool IsSlidingPiece(int piece) {
            return (piece & 0b100) != 0;
        }

        // public static string 
    }
}