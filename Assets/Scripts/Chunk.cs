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

        int testGenerateHeight = NoiseUtils.GenerateHeight(0, 0);
        //Debug.Log("testGenerateHeight= " + testGenerateHeight);

        // need to get chunkSize from world

        blocks = new Block[chunkSize, chunkSize, chunkSize];

        // create blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    Vector3 blockPosition = new Vector3(x, y, z);
                    Vector3 pos = blockPosition + position;

                    int worldX = (int)pos.x;
                    int worldY = (int)pos.y;
                    int worldZ = (int)pos.z;

                    // need to get blocktype from world for each block by its position

                    blocks[x,y,z] = new Block(GetBlockType(worldX, worldY, worldZ), blockPosition);
                }
            }
        }
    }

    private Block.BlockType GetBlockType(int worldX, int worldY, int worldZ) {

        // check and set block type from the bottom of each column upward
        Block.BlockType blockType;
        if (worldY <= NoiseUtils.GenerateStoneHeight(worldX, worldZ)) {
            blockType = Block.BlockType.STONE;
        } else if (worldY < NoiseUtils.GenerateHeight(worldX, worldZ)) {
            blockType = Block.BlockType.DIRT;
        } else if (worldY == NoiseUtils.GenerateHeight(worldX, worldZ)) {
            blockType = Block.BlockType.GRASS;
        } else {
            blockType = Block.BlockType.AIR;
        }

        // for now stub this
        //blockType = (Random.Range(0, 100) < 50 ? Block.BlockType.GRASS : Block.BlockType.AIR);
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
