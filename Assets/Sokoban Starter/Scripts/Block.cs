using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public abstract class Block : MonoBehaviour {
    private static Dictionary<Vector2Int, Block> blockMap = new();
    
    private GridObject gridObject;

    public bool isChecked = false;

    protected Block getBlockAt(Vector2Int pos) {
        if (!isInBound(pos)) return null;
        if (!blockMap.ContainsKey(pos)) return null;
        return blockMap[pos];
    }

    public Vector2Int pos {
        get => lengthToIndex(gridObject.gridPosition);
        set {
            if (getBlockAt(pos) == this) blockMap.Remove(pos);
            gridObject.gridPosition = indexToLength(value);
            blockMap[pos] = this;
        }
    }

    private Vector2Int indexToLength(Vector2Int index) {
        return index + new Vector2Int(1, 1);
    }

    private Vector2Int lengthToIndex(Vector2Int length) {
        return length - new Vector2Int(1, 1);
    }
    
    // Start is called before the first frame update
    void Awake() {
        gridObject = GetComponent<GridObject>();
        
        int randomX = UnityEngine.Random.Range(0, (int)GridMaker.reference.dimensions.x);
        int randomY = UnityEngine.Random.Range(0, (int)GridMaker.reference.dimensions.y);
        Vector2Int tempPos = new Vector2Int(randomX, randomY);
        while (getBlockAt(tempPos) != null) {
            randomX = UnityEngine.Random.Range(0, (int)GridMaker.reference.dimensions.x);
            randomY = UnityEngine.Random.Range(0, (int)GridMaker.reference.dimensions.y);
            tempPos = new Vector2Int(randomX, randomY);
        }

        pos = tempPos;
    }
    
    public void Update() {
        isChecked = false;
    }

    protected void forAdjacentBlocks(Action<Vector2Int> action) {
        action(new Vector2Int(1, 0));
        action(new Vector2Int(-1, 0));
        action(new Vector2Int(0, 1));
        action(new Vector2Int(0, -1));
    }

    protected bool isInBound(Vector2Int pos) {
        if (pos.x < 0 || pos.y < 0) return false;
        Vector2Int length = new Vector2Int((int)GridMaker.reference.dimensions.x, (int)GridMaker.reference.dimensions.y);
        if (pos.x >= length.x || pos.y >= length.y) return false;
        return true;
    }

    public abstract bool move(Vector2Int direction, string type);

    protected void successMove(Vector2Int direction) {
        isChecked = true;
        Vector2Int oldPos = pos;
        pos += direction;
        forAdjacentBlocks(cur => {
            Block b = getBlockAt(cur + oldPos);
            if (b is null) return;
                
            if (cur == direction) return;
            if (-cur == direction) 
                b.move(direction, "pull");

            b.move(direction, "stick");
        });
    }
}
