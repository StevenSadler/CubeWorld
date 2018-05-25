using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    public static Dictionary<string, Chunk> chunks;
    public static Dictionary<string, GameObject> chunkObjects;

    public World(int columnHeight, int chunkSize) {
        chunks = new Dictionary<string, Chunk>();
        chunkObjects = new Dictionary<string, GameObject>();

        BuildChunkColumn(columnHeight, chunkSize);
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
        Debug.Log("chunks Count= " + chunks.Count);

        //foreach (KeyValuePair<string, Chunk> chunk in chunks) {
        //    GameObject chunkObject = DrawChunk(chunk.Value);
        //    QuadUtils.CombineQuads(chunkObject, textureAtlas);
        //}
    }


}
