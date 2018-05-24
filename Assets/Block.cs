using System.Collections;
using System.Collections.Generic;
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
        AIR
    };


    public BlockType blockType;
    public Vector3 position;

    //GameObject parent;
    //Chunk owner;

    public Block(BlockType blockType, Vector3 position) {
        this.blockType = blockType;
        this.position = position;
    }

    public bool IsSolid() {
        return blockType != BlockType.AIR;
    }

    public Block(BlockType blockType, Vector3 position, GameObject parent, Chunk owner) {
        this.blockType = blockType;
        this.position = position;
        //this.parent = parent;
        //this.owner = owner;
    }
    
    //public bool HasSolidNeighbor(Vector3 direction) {
    //    if (owner == null) { return false; }

    //    Block[,,] neighborBlocks = owner.blocks;  // parent.GetComponent<Chunk>().blocks;
    //    Vector3 neighbor = position + direction;
    //    int x = (int)neighbor.x;
    //    int y = (int)neighbor.y;
    //    int z = (int)neighbor.z;

    //    if (World.IsOutsideChunk(x,y,z)) {
    //        // get the neighbor chunk of this block's chunk
    //        Vector3 neighborChunkPosition = parent.transform.position + direction * World.chunkSize;
    //        string neighborName = World.BuildChunkName(neighborChunkPosition);



    //        // get the neighbor chunk's blocks
    //        Chunk neighborChunk;
    //        if (World.chunks.TryGetValue(neighborName, out neighborChunk)) {
    //            neighborBlocks = neighborChunk.blocks;

    //            // convert this xyz to neighbor chunk's neighbor block values
    //            x = ConvertBlockIndexToLocal(x);
    //            y = ConvertBlockIndexToLocal(y);
    //            z = ConvertBlockIndexToLocal(z);
    //        } else {
    //            return false;
    //        }

    //    } else {
    //        neighborBlocks = owner.blocks;
    //    }

    //    try {
    //        return neighborBlocks[x,y,z].IsSolid();
    //    }
    //    catch (System.IndexOutOfRangeException) { }

    //    return false;
    //}
    

    //private int ConvertBlockIndexToLocal(int i) {
    //    if (i == -1) {
    //        i = World.chunkSize - 1;
    //    } else if (i == World.chunkSize) {
    //        i = 0;
    //    }
    //    return i;
    //}
}
