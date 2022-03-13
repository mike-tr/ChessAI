using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public abstract class ChessBrain : ScriptableObject {
    public new string name = "random";
    protected BoardDrawer drawer;
    protected ChessGameHandler handler;
    protected ChessBoard board;
    public void LinkBrain(BoardDrawer drawer, ChessGameHandler handler) {
        this.handler = handler;
        this.drawer = drawer;

        if (name.Contains("random")) {
            PickAName();
        }
    }

    public virtual void PickAName() {
        name = "brain" + GetInstanceID();
    }

    public abstract void Logic(ChessBoard board);

    public void Play(ChessBoard board) {
        var th = new Thread(() => Logic(board));
        th.Start();
    }

    protected void AcceptMove(PieceMove move) {


        if (drawer.board.CheckRealMove(move)) {
            //drawer.SwitchBoard (move.GetNextBoard ());
            handler.ApplyBoard(drawer.board.ApplyMove(move));
        }
    }
}
