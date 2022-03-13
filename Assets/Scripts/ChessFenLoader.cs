namespace Chess {
    using System.Collections.Generic;
    public static class FenUtility {


    }

    public class LoadedPositionInfo {
        public int[] squares;
        public bool whiteToMove;
        public bool whiteCastleKingside;
        public bool whiteCastleQueenside;
        public bool blackCastleKingside;
        public bool blackCastleQueenside;
        public int enPassant;
        public int plyCount;

        public LoadedPositionInfo() {
            squares = new int[64];
        }
    }
}