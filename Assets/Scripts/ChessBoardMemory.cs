using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

namespace ChessGraphics {
    public class ChessBoardMemory {
        // This class will give us "unique" encoding for each board configuration ( mostly ).
        // then we would store each game configuration in a map, hence we wont need to recalculate all the data everystep.
        // the map will also store all boards configurations that would be at stages later then the current one.
        public const int pieces = (int)Piece.None + 1;
        public static ChessBoardMemory instance;
        private BitString[,,] encodings = new BitString[8, 8, pieces];
        private Dictionary<int, Dictionary<BitString, ChessBoard>> boardMemory;
        public ChessBoardMemory() {
            boardMemory = new Dictionary<int, Dictionary<BitString, ChessBoard>>();
            instance = this;
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    for (int p = 0; p < (int)Piece.None; p++) {
                        encodings[x, y, p] = new BitString(true);
                    }
                    encodings[x, y, pieces - 1] = new BitString(false);
                }
            }
        }

        public BitString GetEncoding(int x, int y, int type) {
            return encodings[x, y, type];
        }

        // public BitString GetEncoding(BoardCord cord, ChessBoard board) {
        //     return GetEncoding(cord.GetNode(board));
        // }

        // public BitString GetEncoding(ChessNode node) {
        //     return encodings[node.x, node.y, (int)node.GetPieceType()];
        // }

        public BitString BoardHash(ChessBoard board) {
            // BitString code = new BitString(false);
            // foreach (var piece in board.pieces[PlayerColor.white]) {
            //     int type = (int)piece.type;
            //     int x = piece.node.x;
            //     int y = piece.node.y;
            //     code *= encodings[x, y, type];
            // }
            // foreach (var piece in board.pieces[PlayerColor.black]) {
            //     int type = (int)piece.type;
            //     int x = piece.node.x;
            //     int y = piece.node.y;
            //     code *= encodings[x, y, type];
            // }
            // return code;
            return null;
        }

        public void push(ChessBoard board) {
            // Dictionary<BitString, ChessBoard> boards;
            // if (boardMemory.TryGetValue(board.movesMade, out boards)) {
            //     boards.Add(board.hash, board);
            // } else {
            //     boards = new Dictionary<BitString, ChessBoard>();
            //     boards.Add(board.hash, board);
            //     boardMemory.Add(board.movesMade, boards);
            // }
        }

        public ChessBoard GetBoard(int level, BitString hash) {
            Dictionary<BitString, ChessBoard> boards;
            if (boardMemory.TryGetValue(level, out boards)) {
                ChessBoard board;
                if (boards.TryGetValue(hash, out board)) {
                    return board;
                }
            }
            return null;
        }

        public void free(int level) {
            boardMemory.Remove(level);
        }
    }
}