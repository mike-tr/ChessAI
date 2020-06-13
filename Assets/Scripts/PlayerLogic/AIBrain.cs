using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AIBrain", menuName = "Chess brains/basic")]
public class AiBrain : ChessBrain {
    public override void Logic (ChessBoard board) {
        var color = board.currentPlayer;
        var moves = board.GetAllPlayerMoves (color, true);
        RateMoves (moves, board, color);
        moves.Sort ();
        //Debug.Log (moves[0].score + " , " + moves[1].score);
        AcceptMove (moves[0]);
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
