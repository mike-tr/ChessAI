using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SBrain", menuName = "Chess brains/basic3")]
public class SBrain : ChessBrain {

    public int level = 1;
    public override void Logic (ChessBoard board) {
        var color = board.currentPlayer;
        var moves = board.GetAllPlayerMoves (color, true);
        //RateMoves (moves, board, color);
        //moves.Sort ();
        var move = RecursiveRate (moves, board, color, level);
        AcceptMove (move);
    }

    public PieceMove RecursiveRate (List<PieceMove> moves, ChessBoard initial, PlayerColor color, int level) {
        var enemy = color == PlayerColor.white ? PlayerColor.black : PlayerColor.white;
        PieceMove best = moves[0];
        float mscore = float.MinValue;
        foreach (var move in moves) {
            var nboard = move.GetNextBoard ();
            move.score = 0;
            var bs = ScoreBoard (nboard, color, enemy);
            move.score += bs * 3 * Mathf.Abs (bs);
            //move.score -= ScorePlayer (nboard, enemy) * 3;

            move.score += nboard.GetAllPlayerMoves (color, false).Count * 0.33f;
            move.score -= nboard.GetAllPlayerMoves (enemy, false).Count * 0.25f;

            if (nboard.IsChecked (enemy)) {
                move.score += 0.5f;
                var nextMoves = nboard.GetAllPlayerMoves (enemy, true);
                if (nextMoves.Count < 1) {
                    move.score = float.MaxValue;
                    best = move;
                    mscore = move.score;
                    return move;
                }
            }

            if (level > 0) {
                var nmoves = nboard.GetAllPlayerMoves (enemy, true);
                if (nmoves.Count < 1) {
                    // a.k.a enemy has no moves but he is not checked.
                    move.score = float.MinValue;
                    continue;
                }
                move.score -= RecursiveRate (nmoves, nboard, enemy, level - 1).score;
            }

            if (move.score >= mscore) {
                best = move;
                mscore = move.score;
            }
        }
        return best;
    }

    public int ScoreBoard (ChessBoard board, PlayerColor color, PlayerColor enemy) {
        return ScorePlayer (board, color) - ScorePlayer (board, enemy);
    }

    public int ScorePlayer (ChessBoard board, PlayerColor color) {
        var score = 0;
        foreach (var piece in board.pieces[color]) {
            switch (piece.type) {
                case PieceType.Bishop:
                case PieceType.Knight:
                    score += 3;
                    break;
                case PieceType.Rook:
                    score += 5;
                    break;
                case PieceType.Queen:
                    score += 10;
                    break;
                case PieceType.Pawn:
                    score += 1;
                    break;
            }
        }
        return score;
    }
}
