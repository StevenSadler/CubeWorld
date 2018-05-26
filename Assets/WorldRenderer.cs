using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour {

    public void Draw(Dictionary<string, Chunk> chunks, int chunkSize, Material cubeMaterial) {
        DrawChunks(chunks, chunkSize, cubeMaterial, QuadUtils.RenderQuads);
    }

    public void DrawCombined(Dictionary<string, Chunk> chunks, int chunkSize, Material cubeMaterial) {
        DrawChunks(chunks, chunkSize, cubeMaterial, QuadUtils.CombineQuads);
    }

    void DrawChunks(Dictionary<string, Chunk> chunks, int chunkSize, Material cubeMaterial, QuadUtils.RenderDelegate del) {
        
        foreach (KeyValuePair<string, Chunk> chunk in chunks) {
            GameObject chunkObject = AddChunkObject(chunk.Value);
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
            chunkRenderer.DrawChunk(chunk.Value, chunkSize, chunkObject);

            del(chunkObject, cubeMaterial);

            chunkObject.transform.position = chunk.Value.position;
        }
    }

    GameObject AddChunkObject(Chunk chunk) {
        // create chunk gameobject to hold quads
        Vector3 chunkPosition = chunk.position;
        string chunkName = World.BuildChunkName(chunkPosition);
        GameObject chunkObject = new GameObject(chunkName);
        chunkObject.transform.parent = transform;
        chunkObject.AddComponent<ChunkRenderer>();

        return chunkObject;
    }
}
