using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    public class PsuedoLegalMoves {
        Dictionary<int, List<PieceMove>> Moves = new Dictionary<int, List<PieceMove>>();
        private ChessBoard board;
        public PsuedoLegalMoves(ChessBoard board) {
            this.board = board;
        }

        public List<PieceMove> GetMoves(int SquareId) {
            // Already calculated.
            if (Moves.ContainsKey(SquareId)) {
                return Moves[SquareId];
            }

            // Otherwise
            var moves = new List<PieceMove>();
            int piece = board.Squares[SquareId];
            int type = Piece.GetPieceType(piece);
            if (type == Piece.None) {
                Moves.Add(SquareId, moves);
                return moves;
            }

            if (type == Piece.Knight) {
                for (int i = 0; i < PrecomputedMoves.KnightMoves[SquareId].Length; i++) {
                    int nextSqaure = PrecomputedMoves.KnightMoves[SquareId][i];
                    PieceMove nm = new PieceMove(SquareId, nextSqaure);
                    Debug.Log(ChessBoardReoresentation.IndexToRepresentation(nextSqaure));
                    moves.Add(nm);
                }
            }
            return moves;
        }

    }
}