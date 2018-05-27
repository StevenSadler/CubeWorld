using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour
{
    private World world;

    public void SetModel(World world) {
        this.world = world;
    }

    public void Draw(Material cubeMaterial) {
        DrawWorld(cubeMaterial, QuadUtils.RenderQuads);
    }

    public void DrawCombined(Material cubeMaterial) {
        DrawWorld(cubeMaterial, QuadUtils.CombineQuads);
    }

    public void DrawCollide(Material cubeMaterial) {
        DrawWorld(cubeMaterial, QuadUtils.CollideQuads);
    }

    void DrawWorld(Material cubeMaterial, QuadUtils.RenderDelegate del) {

        foreach (KeyValuePair<string, Chunk> chunk in world.chunks) {
            GameObject chunkObject = AddChunkObject(chunk.Value);
            DrawWorldChunk(chunk.Value, chunkObject);

            del(chunkObject, cubeMaterial);

            chunkObject.transform.position = chunk.Value.position;
        }
    }

    GameObject AddChunkObject(Chunk chunk) {
        // create chunk gameobject to hold quads
        Vector3 chunkPosition = chunk.position;
        string chunkName = world.BuildChunkName(chunkPosition);
        GameObject chunkObject = new GameObject(chunkName);
        chunkObject.transform.parent = transform;
        chunkObject.AddComponent<ChunkRenderer>();

        return chunkObject;
    }

    void DrawWorldChunk(Chunk chunk, GameObject gameObject) {
        // draw blocks
        for (int z = 0; z < world.chunkSize; z++) {
            for (int y = 0; y < world.chunkSize; y++) {
                for (int x = 0; x < world.chunkSize; x++) {
                    DrawWorldQuads(x, y, z, chunk, gameObject);
                }
            }
        }
    }

    void DrawWorldQuads(int x, int y, int z, Chunk chunk, GameObject gameObject) {
        if (chunk.blocks[x, y, z].IsSolid() == false) return;

        foreach (Vector3 direction in Block.directions) {

            // get potential neighbor block x,y,z by incrementing/decrementing my position by direction
            Vector3 neighborBlockPosition = chunk.blocks[x, y, z].position + direction;
            int nx = (int)neighborBlockPosition.x;
            int ny = (int)neighborBlockPosition.y;
            int nz = (int)neighborBlockPosition.z;

            if (!world.HasSolidNeighbor(nx, ny, nz, chunk, direction)) {
                GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
                quad.transform.parent = gameObject.transform;
            }
        }
    }
}
