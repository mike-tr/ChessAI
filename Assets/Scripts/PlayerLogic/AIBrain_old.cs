using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Chess;

namespace ChessAI {
    [CreateAssetMenu(fileName = "AIBrain", menuName = "Chess brains/Vesic")]
    public class AIBrain_old : ChessBrain {
        public override void Logic(ChessBoard board) {
            var color = board.CurrentPlayer;
            var moves = board.GetAllPlayerMoves(color, true);
            AcceptMove(moves[0]);
        }
    }
}