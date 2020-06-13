using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCBrain : ChessBrain {
    public override void Logic (ChessBoard board) {
        // A.k.a the human brain is simple just update the cursour to give controll to this player.
        handler.cursor.player = board.currentPlayer;
    }
}
