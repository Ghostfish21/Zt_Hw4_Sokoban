using UnityEngine;

namespace Sokoban_Starter.Scripts {
    public class PlayerBlock : Block {
        
        // Update is called once per frame
        public new void Update() {
            base.Update();
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
                move(new Vector2Int(0, -1), "");
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
                move(new Vector2Int(0, 1), "");
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
                move(new Vector2Int(-1, 0), "");
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
                move(new Vector2Int(1, 0), "");
        }
        
        public override bool move(Vector2Int direction, string type) {
            if (isChecked) return false;
            
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