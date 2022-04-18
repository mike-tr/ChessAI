namespace Chess {
    public class PlayerIndexColor {
        public readonly static PlayerIndexColor black = new PlayerIndexColor(Piece.Black);
        public readonly static PlayerIndexColor white = new PlayerIndexColor(Piece.White);
        public int index { get; private set; }
        private PlayerIndexColor(int index) {
            this.index = index;
        }

        public static bool operator ==(PlayerIndexColor p1, PlayerIndexColor p2) {
            return p1.index == p2.index;
        }

        public static bool operator !=(PlayerIndexColor p1, PlayerIndexColor p2) {
            return p1.index != p2.index;
        }

        public static PlayerIndexColor operator !(PlayerIndexColor p1) {
            return p1 == white ? black : white;
        }
        public static implicit operator int(PlayerIndexColor player) => player.index;

        public override bool Equals(object obj) {
            if (obj is PlayerIndexColor)
                return (PlayerIndexColor)obj == this;
            return false;
        }

        public override int GetHashCode() {
            return this.index;
        }

        public override string ToString() {
            return this == white ? "White" : "Black";
        }
    }
}