using System.Collections.Generic;
using UnityEngine;

public class World
{
    public Dictionary<string, Chunk> chunks;
    //public Dictionary<string, GameObject> chunkObjects;

    public World(int columnHeight, int chunkSize, int worldSize) {
        chunks = new Dictionary<string, Chunk>();
        //chunkObjects = new Dictionary<string, GameObject>();

        //BuildChunkColumn(columnHeight, chunkSize);
        BuildWorld(columnHeight, chunkSize, worldSize);
    }

    public static string BuildChunkName(Vector3 v) {
        return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
    }

    void BuildChunkColumn(int columnHeight, int chunkSize) {
        for (int i = 0; i < columnHeight; i++) {
            Vector3 chunkPosition = new Vector3(0, i * chunkSize, 0);
            Chunk chunk = new Chunk(chunkPosition, chunkSize);
            string chunkName = BuildChunkName(chunkPosition);
            chunks.Add(chunkName, chunk);
        }
    }

    void BuildWorld(int columnHeight, int chunkSize, int worldSize) {
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
}
