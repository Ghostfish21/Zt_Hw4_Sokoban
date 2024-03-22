using UnityEngine;

namespace Sokoban_Starter.Scripts {
    public class SmoothBlock : Block {
        public override bool move(Vector2Int direction, string type) {
            if (isChecked) return false;
            
            if (type != "push") return false;
            
            Vector2Int posFront = pos + direction;
            if (!isInBound(posFront)) return false;
            
            Block frontBlock = getBlockAt(posFront);
            
            if (frontBlock is null) {
                successMove(direction);
                return true;
            }
            
            if (frontBlock is WallBlock) return false;
            
            bool pushResult = frontBlock.move(direction, "push");
            if (isChecked) return false;
            if (pushResult) successMove(direction);
            return pushResult;
        }
    }
}