using UnityEngine;

public class Block
{
    public static Vector3[] directions = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    public enum BlockType
    {
        GRASS,
        DIRT,
        STONE,
        TNT,
        DIAMOND,
        AIR
    };

    public BlockType blockType;
    public Vector3 position;

    public Block(BlockType blockType, Vector3 position) {
        this.blockType = blockType;
        this.position = position;
    }

    public bool IsSolid() {
        return blockType != BlockType.AIR;
    }
}
