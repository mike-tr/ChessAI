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
            whitePlayer = new HumanCBrain (board, this, TeamColor.white);
        }
        if (blackPlayer == null) {
            //blackPlayer = new HumanCBrain (board, this, TeamColor.black);
            blackPlayer = new AIBrain (board, this, TeamColor.black);
        }
        player.Add (TeamColor.white, whitePlayer);
        player.Add (TeamColor.black, blackPlayer);
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.I)) {
            Debug.Log ("show every possible move!");
            board.RefreshBoard ();
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    var c = new Color (Random.value, Random.value, Random.value, 1);
                    c.a = 1f;
                    var tile = board.tiles[x, y];
                    var xy = x * y / 64f;

                    tile.DrawAllMoves (board.CurrentTurn ());
                }
            }
        }
    }

    public void NextTurn () {
        player[board.CurrentTurn ()].Play ();
    }
}
