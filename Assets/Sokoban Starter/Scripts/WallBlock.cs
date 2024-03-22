using UnityEngine;

namespace Sokoban_Starter.Scripts {
    public class WallBlock : Block {
        public override bool move(Vector2Int direction, string type) {
            if (isChecked) return false;
            return false;
        }
    }
}