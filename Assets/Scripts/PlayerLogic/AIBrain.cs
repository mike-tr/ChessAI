using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : ChessBrain {
    public AIBrain (BoardDrawer board, ChessGameHandler handler, TeamColor color) : base (board, handler, color) { }
    // Update is called once per frame  
    public override void Play () {
        // A.k.a the human brain is simple just update the cursour to give controll to this player.
        //handler.cursor.player = color;

        var moves = drawer.board.GetAllPlayerMoves (color, true);
        MakeAMove (moves[0]);
    }
}
