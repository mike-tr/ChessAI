using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

namespace ChessGraphics {
    public class BoardDrawer : MonoBehaviour {
        public delegate void OnBoardChange();
        public OnBoardChange OnChangeCallBack;
        public string fileName;
        public SquareHandler[] squareHandlers { get; private set; } = new SquareHandler[64];
        public ChessBoard board { get; private set; }
        public SquareHandler handlerPrefab;

        public Vector2 boardOffset;
        public Vector2 tileOffset;
        private Transform holder;
        public BoardCursor cursor;
        private Camera cam;

        private void Start() {
            PrecomputedMoves.Initialize();
            ChessAssetsLoader.Initialize();
            ChessBoardMemory memory = new ChessBoardMemory();

            cam = Camera.main;
            var sp = GetComponent<SpriteRenderer>();
            Debug.Log(sp.bounds.size);
            tileOffset.x = sp.bounds.size.x / 8;
            tileOffset.y = sp.bounds.size.y / 8;

            board = new ChessBoard();
            //print(memory.BoardHash(board));


            holder = new GameObject("holder").transform;
            holder.parent = transform;
            holder.localPosition = boardOffset;

            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    squareHandlers[y * 8 + x] = Instantiate(handlerPrefab);
                    squareHandlers[y * 8 + x].Initialize(this, holder, x, y, tileOffset);
                }
            }
            SwitchBoard(board);
            //print(memory.BoardHash(board));
        }

        public int CurrentTurn() {
            return board.CurrentPlayer;
        }

        public void SwitchBoard(ChessBoard newBoard) {
            board = newBoard;
            ChessBoardMemory.instance.free(newBoard.MovesMade);
            for (int i = 0; i < 64; i++) {
                squareHandlers[i].UpdateBoard();
            }
            // for (int x = 0; x < 8; x++) {
            //     for (int y = 0; y < 8; y++) {
            //         //tiles[x, y].SetNode(newBoard.nodes[x, y]);
            //     }
            // }
            if (OnChangeCallBack != null) {
                OnChangeCallBack.Invoke();
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                var pos = cam.ScreenToWorldPoint(Input.mousePosition);
                cursor.SetPos(pos);

            }
        }

        public void RefreshBoard() {
            for (int i = 0; i < 64; i++) {
                squareHandlers[i].ResetTile();
            }
        }
    }
}