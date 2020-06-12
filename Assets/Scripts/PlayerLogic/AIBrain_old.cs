using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AIBrain", menuName = "Chess brains/Vesic")]
public class AIBrain_old : ChessBrain {
    public override void Play (ChessBoard board) {
        var color = board.currentPlayer;
        var moves = board.GetAllPlayerMoves (color, true);
        AcceptMove (moves[0]);
    }
}
