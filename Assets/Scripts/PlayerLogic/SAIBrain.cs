using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Chess;

namespace ChessAI {
    [CreateAssetMenu(fileName = "SAIBrain", menuName = "Chess brains/basic2")]
    public class SAIBrain : ChessBrain {

        public int level = 1;
        public override void Logic(ChessBoard board) {
            var color = board.CurrentPlayer;
            var moves = board.GetAllPlayerMoves(color, true);
            //RateMoves (moves, board, color);
            //moves.Sort ();
            var move = RecursiveRate(moves, board, color, level);
            AcceptMove(move);
        }

        public PieceMove RecursiveRate(List<PieceMove> moves, ChessBoard initial, int color, int level) {
            var enemy = color == PlayerColor.white ? PlayerColor.black : PlayerColor.white;
            var score = ScorePlayer(initial, enemy);

            PieceMove best = moves[0];
            float mscore = float.MinValue;
            foreach (var move in moves) {
                //var nboard = move.GetNextBoard();
                var nboard = board.ApplyMove(move);
                move.Score += (score - ScorePlayer(nboard, enemy)) * 3;
                move.Score += nboard.GetAllPlayerMoves(color, false).Count * 0.5f;
                move.Score += nboard.GetAllPlayerMoves(enemy, false).Count * 0.25f;

                if (nboard.IsChecked(enemy)) {
                    move.Score += 0.5f;
                    var nextMoves = nboard.GetAllPlayerMoves(enemy, true);
                    if (nextMoves.Count < 1) {
                        move.Score = float.MaxValue;
                        best = move;
                        mscore = move.Score;
                        return move;
                    }
                    //Debug.Log (move.score + " : " + move.id + " level : " + level);
                    //Debug.Log();
                }

                if (level > 0) {
                    var nmoves = nboard.GetAllPlayerMoves(enemy, true);
                    if (nmoves.Count < 1) {
                        // a.k.a enemy has no moves but he is not checked.
                        move.Score = float.MinValue;
                        continue;
                    }
                    move.Score -= RecursiveRate(nmoves, nboard, enemy, level - 1).Score;
                }

                if (move.Score >= mscore) {
                    best = move;
                    mscore = move.Score;
                }
            }
            return best;
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