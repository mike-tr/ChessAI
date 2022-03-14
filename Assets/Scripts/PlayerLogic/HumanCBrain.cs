using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

namespace ChessAI {
    [CreateAssetMenu(fileName = "HumanBrain", menuName = "Chess brains/Human")]
    public class HumanCBrain : ChessBrain {
        public override void Logic(ChessBoard board) {
            // A.k.a the human brain is simple just update the cursour to give controll to this player.
            handler.cursor.player = board.CurrentPlayer;
        }
    }
}