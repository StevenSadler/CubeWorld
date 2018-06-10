using UnityEngine;

public class Chunk
{
    public Block[,,] blocks;
    public Vector3 position;

    private GameObject chunkObject;

    public Chunk(Vector3 position, int chunkSize) {
        this.position = position;
        BuildChunk(chunkSize);
    }

    public void SetViewRef(GameObject chunkObject) {
        this.chunkObject = chunkObject;
    }

    public GameObject GetViewRef() {
        return chunkObject;
    }

    public void BuildChunk(int chunkSize) {

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

    public void ResetChunk(Vector3 position, int chunkSize) {
        this.position = position;

        // reset blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    Vector3 blockPosition = new Vector3(x, y, z);
                    Vector3 pos = blockPosition + position;

                    int worldX = (int)pos.x;
                    int worldY = (int)pos.y;
                    int worldZ = (int)pos.z;

                    // need to get blocktype from world for each block by its position

                    blocks[x, y, z].SetBlockType(GetBlockType(worldX, worldY, worldZ));
                }
            }
        }
    }
    
    private Block.BlockType GetBlockType(int worldX, int worldY, int worldZ) {

        Block.BlockType blockType;
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

    private Block.BlockType GetBlockTypeOld(int worldX, int worldY, int worldZ) {

        Block.BlockType blockType;
        if (NoiseUtils.FractalBrownianMotion3D(worldX, worldY, worldZ, 0.1f, 3) < 0.44f) {
            blockType = Block.BlockType.AIR;
        } else 
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
        x += (int)direction.x;
        y += (int)direction.y;
        z += (int)direction.z;
        try {
            return blocks[x, y, z].IsSolid();
        }
        catch (System.IndexOutOfRangeException) {
            int worldX = x + (int)position.x;
            int worldY = y + (int)position.y;
            int worldZ = z + (int)position.z;
            return Block.IsBlockTypeSolid(GetBlockType(worldX, worldY, worldZ));
        }
    }

    public bool HasSolidNeighbor(Block block, Vector3 direction) {
        return HasSolidNeighbor(block.getX(), block.getY(), block.getZ(), direction);
    }
}
