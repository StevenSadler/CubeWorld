using System.Collections.Generic;
using UnityEngine;

public class World
{
    public Dictionary<string, Chunk> chunks;

    public int columnHeight;
    public int chunkSize;
    public int worldSize;

    public World(int columnHeight, int chunkSize, int worldSize) {

        this.columnHeight = columnHeight;
        this.chunkSize = chunkSize;
        this.worldSize = worldSize;

        chunks = new Dictionary<string, Chunk>();
        BuildWorld();
    }

    void BuildWorld() {
        for (int z = 0; z < worldSize; z++) {
            for (int x = 0; x < worldSize; x++) {
                for (int y = 0; y < columnHeight; y++) {
                    Vector3 chunkPosition = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize);
                    Chunk chunk = new Chunk(chunkPosition, chunkSize);
                    string chunkName = BuildChunkName(chunkPosition);
                    chunks.Add(chunkName, chunk);
                }
            }
        }
    }

    public string BuildChunkName(Vector3 v) {
        return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
    }

    public bool HasSolidNeighbor(int x, int y, int z, Chunk chunk, Vector3 direction) {
        Block[,,] blocks;

        // if neighbor block is outside of this chunk
        if (IsOutsideChunk(x, y, z)) {
            Vector3 neighborChunkPosition = chunk.position + direction * chunkSize;
            string neighborName = BuildChunkName(neighborChunkPosition);

            x = ConvertBlockIndexToLocal(x);
            y = ConvertBlockIndexToLocal(y);
            z = ConvertBlockIndexToLocal(z);

            Chunk neighborChunk;
            if (chunks.TryGetValue(neighborName, out neighborChunk)) {
                blocks = neighborChunk.blocks;
            } else {
                return false;
            }

        }
        // else neighbor block is in this chunk
        else {
            blocks = chunk.blocks;
        }


        try {
            return blocks[x, y, z].IsSolid();
        }
        catch (System.IndexOutOfRangeException) { }

        return false;
    }

    bool IsOutsideChunk(int x, int y, int z) {
        return (
            x < 0 || x > chunkSize - 1 ||
            y < 0 || y > chunkSize - 1 ||
            z < 0 || z > chunkSize - 1);
    }

    int ConvertBlockIndexToLocal(int i) {
        if (i == -1) {
            return chunkSize - 1;
        } else if (i == chunkSize) {
            return 0;
        }
        return i;
    }
}
