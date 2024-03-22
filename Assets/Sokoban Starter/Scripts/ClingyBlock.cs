using UnityEngine;

namespace Sokoban_Starter.Scripts {
    public class ClingyBlock : Block {
        public override bool move(Vector2Int direction, string type) {
            if (isChecked) return false;

            if (type != "pull") return false;
            
            successMove(direction);
            return true;
        }
    }
}