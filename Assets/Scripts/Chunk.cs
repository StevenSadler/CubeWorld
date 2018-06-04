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

        Block.BlockType blockType;
        //if (NoiseUtils.FractalBrownianMotion3D(worldX, worldY, worldZ, 0.1f, 3) < 0.44f) {
        //    blockType = Block.BlockType.AIR;
        //} else 
        if (worldY <= NoiseUtils.GenerateStoneHeight(worldX, worldZ)) {
            if (NoiseUtils.FractalBrownianMotion3D(worldX, worldY, worldZ, 0.1f, 2) < 0.42f) {
                blockType = Block.BlockType.DIAMOND;
            } else {
                blockType = Block.BlockType.STONE;
            }
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
