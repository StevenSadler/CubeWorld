using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour {

    public void DrawColumn(Dictionary<string, Chunk> chunks, int chunkSize, Material cubeMaterial) {
        foreach (KeyValuePair<string, Chunk> chunk in chunks) {
            GameObject chunkObject = DrawChunk(chunk.Value, chunkSize);
            QuadUtils.CombineQuads(chunkObject, cubeMaterial);
        }
    }

    GameObject DrawChunk(Chunk chunk, int chunkSize) {
        // create chunk gameobject to hold quads
        Vector3 chunkPosition = chunk.position;
        string chunkName = World.BuildChunkName(chunkPosition);
        GameObject chunkObject = new GameObject(chunkName);
        chunkObject.transform.position = chunkPosition;
        chunkObject.transform.parent = transform;

        Debug.Log("draw chunk= " + chunk + " position= " + chunkPosition);
        Debug.Log("chunkObject= " + chunkObject);
        Debug.Log("chunk.blocks= " + chunk.blocks);
        Block block = chunk.blocks[chunkSize - 1, chunkSize - 1, chunkSize - 1];
        Debug.Log("block.position= " + block.position);

        // draw blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    DrawQuads(x, y, z, chunk, chunkObject);
                }
            }
        }


        return chunkObject;
    }

    void DrawQuads(int x, int y, int z, Chunk chunk, GameObject chunkObject) {
        if (chunk.blocks[x, y, z].IsSolid() == false) return;

        foreach (Vector3 direction in Block.directions) {
            if (!chunk.HasSolidNeighbor(x, y, z, direction)) {
                GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
                quad.transform.parent = chunkObject.transform;
            }
        }
    }
}
