using System.Collections.Generic;
using static System.Math;
using UnityEngine;

namespace Chess {
    public static class ChessBoardReoresentation {
        public static string IndexToRepresentation(int index) {
            int x = index % 8;
            int y = index / 8;
            return "" + (char)('a' + x) + (char)('1' + y);
        }

        public static int RepresentationToIndex(string representation) {
            int y = representation[0] - 'a';
            int x = representation[1] - '1';
            return y * 8 + x;
        }
    }
}
