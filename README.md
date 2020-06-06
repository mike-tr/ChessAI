# ChessAI
trying to build my own chess bot.

building chess AI is the go too i guess.
so i wanna challange my self.

but first i need to build the chess game.



My implementation is as follows:

For startes ill make a 8x8 node map, a.k.a the game board, it would hold every tile on the board.
this tile will hold data, mostly what piece is on it.

a piece, will have all the piece properties, it will be able to generate a "possible move" list,
so the piece should hold reference to the node, and the node to the "Board", so it can see what is on other nodes.

the PossibleMove object, should have an apply method, and by hitting apply i will generate a new copy, of the board.
with the given move applied, A.k.a i dont actually override the original board, so with that i can work on trying to "see" what are
my best moves, at any given point, and not worry about changing the actuall board.

so the ApplyMove will return a new copy of the board, with the change made.

As for the "king" logic, i will draw all the possible moves, from the opposite player.
and see if any of them can "kill" the king, if it can, we need to protect the king.

if the king is trying to move, we will "see" if he can be killed on that new position, if he can.
then we block that move.

A.k.a if the king is "attacked" and all the units, cannot move.
the player lost.



THE UI:
For short, ill create a BoardDrawer object, it will to generate a 8x8 tiles object,
but in this case, all those tiles would simply be interactble with the player.

The board drawer, will simply do Update visual, it will take a data from a given Board, and pass it to the tiles,
thus we will show the current state of the borad.

A.k.a if the tile is being clicked we would highlight it, and show all the given piece moves. ( if we control it and the piece exist,
and its our turn ).

second click on a "possible" move will override the board drawer data, by getting the ApplyMove, and setting it a the BoardDrawer board.
on changing a board, we will also change who is controlling the units, and pass the turn to the currect player "Brain"



The player brain:
would be responsible, to either draw the next move, either by setting the ui be responsive to the human player,
or simple calculate the next move ( if its a computer ).


i guess that all!
