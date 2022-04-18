using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

namespace ChessAI {
    [CreateAssetMenu(fileName = "AIBrain", menuName = "Chess brains/basic")]
    public class AiBrain : ChessBrain {
        public override void Logic(ChessBoard board) {
            var color = board.CurrentPlayer;
            var moves = board.GetAllPlayerMoves(color, true);
            RateMoves(moves, board, color);
            moves.Sort();
            //Debug.Log (moves[0].score + " , " + moves[1].score);
            AcceptMove(moves[0]);
        }

        public void RateMoves(List<PieceMove> moves, ChessBoard initial, PlayerIndexColor color) {
            var enemyColor = !color;
            var score = ScorePlayer(initial, enemyColor);
            foreach (var move in moves) {
                var nboard = board.ApplyMove(move);
                //var nboard = board.ApplyMove(move);
                move.Score += (score - ScorePlayer(nboard, enemyColor)) * 3;
                move.Score += nboard.GetAllPlayerMoves(color, false).Count;
                move.Score += nboard.GetAllPlayerMoves(enemyColor, true).Count > 0 ? 0 : float.MaxValue;
            }
        }

        public int ScorePlayer(ChessBoard board, int color) {
            var score = 0;
            // foreach (var piece in board.pieces[color]) {
            //     switch (piece.type) {
            //         case PieceType.Bishop:
            //         case PieceType.Knight:
            //             score += 3;
            //             break;
            //         case PieceType.Rook:
            //             score += 5;
            //             break;
            //         case PieceType.Queen:
            //             score += 10;
            //             break;
            //         case PieceType.Pawn:
            //             score += 1;
            //             break;
            //     }
            // }
            return score;
        }
    }
}