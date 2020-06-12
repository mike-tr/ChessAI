using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ChessBrain : ScriptableObject {
    public new string name = "random";
    protected BoardDrawer drawer;
    protected ChessGameHandler handler;
    public void LinkBrain (BoardDrawer drawer, ChessGameHandler handler) {
        this.handler = handler;
        this.drawer = drawer;

        if (name.Contains ("random")) {
            PickAName ();
        }
    }

    public virtual void PickAName () {
        name = "brain" + GetInstanceID ();
    }

    public abstract void Play (ChessBoard board);

    protected void AcceptMove (PieceMove move) {
        if (move.IsPartOf (drawer.board)) {
            drawer.SwitchBoard (move.GetNextBoard ());
        }
    }
}
