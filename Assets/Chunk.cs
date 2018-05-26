using UnityEngine;

public class Chunk
{
    public Block[,,] blocks;
    public Vector3 position;

    public Chunk(Vector3 position, int chunkSize) {
        this.position = position;
        BuildChunk(chunkSize);
    }

    void BuildChunk(int chunkSize) {
        // need to get chunkSize from world

        blocks = new Block[chunkSize, chunkSize, chunkSize];

        // create blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    Vector3 position = new Vector3(x, y, z);

                    // need to get blocktype from world for each block by its position

                    blocks[x, y, z] = new Block(GetBlockType(position), position);
                }
            }
        }
    }

    private Block.BlockType GetBlockType(Vector3 blockPosition) {
        // calculate blocktype by applying a function such as perlin noise
        // after adding chunk position and block position vectors

        // for now stub this
        Block.BlockType blockType = (Random.Range(0, 100) < 50 ? Block.BlockType.GRASS : Block.BlockType.AIR);
        return blockType;
    }

    public bool HasSolidNeighbor(int x, int y, int z, Vector3 direction) {
        try {
            return blocks[x + (int)direction.x, y + (int)direction.y, z + (int)direction.z].IsSolid();
        }
        catch (System.IndexOutOfRangeException) { }

        return false;
    }
}
