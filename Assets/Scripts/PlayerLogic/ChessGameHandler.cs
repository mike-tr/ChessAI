using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameHandler : MonoBehaviour {
    // Start is called before the first frame update
    public BoardCursor cursor;
    public BoardDrawer board;
    public ChessBrain whitePlayer;
    public ChessBrain blackPlayer;
    private Dictionary<TeamColor, ChessBrain> player = new Dictionary<TeamColor, ChessBrain> ();
    void Start () {
        board = GetComponent<BoardDrawer> ();
        board.OnChangeCallBack += NextTurn;
        if (whitePlayer == null) {
            whitePlayer = new HumanCBrain ();
        }
        if (blackPlayer == null) {
            blackPlayer = new HumanCBrain ();
        }
        whitePlayer.Init (board, this, TeamColor.white);
        blackPlayer.Init (board, this, TeamColor.black);
        player.Add (TeamColor.white, whitePlayer);
        player.Add (TeamColor.black, blackPlayer);
    }

    public void NextTurn () {
        player[board.CurrentTurn ()].Play ();
    }
}
