using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChessGameHandler : MonoBehaviour {
    // Start is called before the first frame update
    public BoardCursor cursor;
    public BoardDrawer board;
    public ChessBrain whitePlayer;
    public ChessBrain blackPlayer;
    private Dictionary<PlayerColor, ChessBrain> player = new Dictionary<PlayerColor, ChessBrain> ();
    public float w8Time = 50;

    public Text text;

    void Start () {
        board = GetComponent<BoardDrawer> ();
        board.OnChangeCallBack += NextTurn;
        if (whitePlayer == null) {
            whitePlayer = new HumanCBrain ();
            //whitePlayer = new AIBrain (board, this, TeamColor.white);
        }
        if (blackPlayer == null) {
            blackPlayer = new HumanCBrain ();
            //blackPlayer = new SAIBrain (board, this, TeamColor.black);
        }
        whitePlayer.LinkBrain (board, this);
        blackPlayer.LinkBrain (board, this);
        player.Add (PlayerColor.white, whitePlayer);
        player.Add (PlayerColor.black, blackPlayer);

        NextTurn ();
    }

    private ChessBoard next = null;
    private float time;
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
        if (next != null) {
            Debug.Log ("Player " + board.CurrentTurn () + " has made a move in : " + (Time.timeSinceLevelLoad - time) * 1000 + " ms");
            board.SwitchBoard (next);
            next = null;
        }
    }

    public void ApplyBoard (ChessBoard board) {
        next = board;
    }

    public void NextTurn () {
        time = Time.timeSinceLevelLoad;
        if (board.board.GetAllPlayerMoves (board.CurrentTurn (), true).Count < 1) {
            string op = "Draw " + board.CurrentTurn () + " has no possible moves.";
            if (board.board.IsChecked (board.CurrentTurn ())) {
                var other = board.CurrentTurn () == PlayerColor.black ? PlayerColor.white : PlayerColor.black;
                op = player[other].name + "won as " + other;
            }
            Debug.Log (op);
            if (text) {
                text.transform.parent.gameObject.SetActive (true);
                text.text = op;
            }
            StartCoroutine (ResetGame (3f));
            return;
        }
        if (w8Time > 0) {
            StartCoroutine (SwitchTurn ());
        } else {
            player[board.CurrentTurn ()].Play (board.board);
        }
    }

    IEnumerator SwitchTurn () {
        yield return new WaitForSeconds (w8Time * 0.001f);
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
        sw.Start ();
        player[board.CurrentTurn ()].Play (board.board);
        sw.Stop ();
        //Debug.Log ("Player " + board.CurrentTurn () + " has made a move in : " + sw.ElapsedMilliseconds + " ms");
    }

    IEnumerator ResetGame (float delay) {
        yield return new WaitForSeconds (delay);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        text.transform.parent.gameObject.SetActive (false);
    }

}
