using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SAIBrain", menuName = "Chess brains/basic2")]
public class SAIBrain : ChessBrain {

    public int recursions = 1;
    public override void Play (ChessBoard board) {
        var color = board.currentPlayer;
        var moves = board.GetAllPlayerMoves (color, true);
        //RateMoves (moves, board, color);
        //moves.Sort ();
        var move = RecursiveRate (moves, board, color, recursions);
        AcceptMove (move);
    }

    public PieceMove RecursiveRate (List<PieceMove> moves, ChessBoard initial, PlayerColor color, int recursions) {
        var enemy = color == PlayerColor.white ? PlayerColor.black : PlayerColor.white;
        var score = ScorePlayer (initial, enemy);

        PieceMove best = null;
        float mscore = float.MinValue;
        foreach (var move in moves) {
            var nboard = move.GetNextBoard ();
            move.score += (score - ScorePlayer (nboard, enemy)) * 3;
            move.score += nboard.GetAllPlayerMoves (color, false).Count;

            var nmoves = nboard.GetAllPlayerMoves (enemy, true);
            if (nmoves.Count > 0) {
                if (recursions > 0) {
                    move.score -= RecursiveRate (nmoves, nboard, enemy, recursions - 1).score;
                }
            } else {
                move.score = float.MaxValue;
                return move;
            }

            if (move.score >= mscore) {
                best = move;
                mscore = move.score;
            }
        }
        return best;
    }

    public void RateMoves (List<PieceMove> moves, ChessBoard initial, PlayerColor color) {
        var enemy = color == PlayerColor.white ? PlayerColor.black : PlayerColor.white;
        var score = ScorePlayer (initial, enemy);
        foreach (var move in moves) {
            var nboard = move.GetNextBoard ();
            move.score += (score - ScorePlayer (nboard, enemy)) * 3;
            move.score += nboard.GetAllPlayerMoves (color, false).Count;
            move.score += nboard.GetAllPlayerMoves (enemy, true).Count > 0 ? 0 : float.MaxValue;
        }
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
