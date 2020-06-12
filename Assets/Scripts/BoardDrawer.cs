using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDrawer : MonoBehaviour {

    public delegate void OnBoardChange ();
    public OnBoardChange OnChangeCallBack;
    public string fileName;
    public Dictionary<PlayerColor, Dictionary<PieceType, Sprite>> sprites { get; private set; } = new Dictionary<PlayerColor, Dictionary<PieceType, Sprite>> ();
    public TileHandler[, ] tiles { get; private set; } = new TileHandler[8, 8];
    public ChessBoard board { get; private set; }
    public TileHandler handlerPrefab;

    public Vector2 boardOffset;
    public Vector2 tileOffset;
    private Transform holder;
    public BoardCursor cursor;
    private Camera cam;

    private void Start () {
        cam = Camera.main;
        var sp = GetComponent<SpriteRenderer> ();
        Debug.Log (sp.bounds.size);
        tileOffset.x = sp.bounds.size.x / 8;
        tileOffset.y = sp.bounds.size.y / 8;

        board = new ChessBoard ();
        sprites.Add (PlayerColor.black, new Dictionary<PieceType, Sprite> ());
        sprites.Add (PlayerColor.white, new Dictionary<PieceType, Sprite> ());
        holder = new GameObject ("holder").transform;
        holder.parent = transform;
        holder.localPosition = boardOffset;
        Sprite[] load = Resources.LoadAll<Sprite> (fileName);
        foreach (var current in load) {
            PlayerColor color = PlayerColor.white;
            if (current.name.Contains ("B_")) {
                color = PlayerColor.black;
            }
            PieceType type = PieceType.Pawn;
            foreach (PieceType t in System.Enum.GetValues (typeof (PieceType))) {
                if (current.name.Contains (t.ToString ())) {
                    type = t;
                }
            }
            sprites[color].Add (type, current);
        }
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                tiles[x, y] = Instantiate (handlerPrefab);
                tiles[x, y].Initialize (this, holder, x, y, tileOffset);
            }
        }
        SwitchBoard (board);
    }

    public PlayerColor CurrentTurn () {
        return board.currentPlayer;
    }

    public void SwitchBoard (ChessBoard newBoard) {
        board = newBoard;
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                tiles[x, y].SetNode (newBoard.nodes[x, y]);
            }
        }
        if (OnChangeCallBack != null) {
            OnChangeCallBack.Invoke ();
        }
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.Mouse0)) {
            var pos = cam.ScreenToWorldPoint (Input.mousePosition);
            cursor.SetPos (pos);

        }
    }

    public void RefreshBoard () {
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                tiles[x, y].ResetTile ();
            }
        }
    }
}
