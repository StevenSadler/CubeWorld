using UnityEngine;

public class ChunkRenderer : MonoBehaviour
{
    public void Draw(Chunk chunk, int chunkSize, GameObject gameObject, Material cubeMaterial) {
        DrawChunk(chunk, chunkSize, gameObject);
        QuadUtils.RenderQuads(gameObject, cubeMaterial);
    }

    public void DrawCombined(Chunk chunk, int chunkSize, GameObject gameObject, Material cubeMaterial) {
        DrawChunk(chunk, chunkSize, gameObject);
        QuadUtils.CombineQuads(gameObject, cubeMaterial);
    }

    public void DrawChunk(Chunk chunk, int chunkSize, GameObject gameObject) {
        // draw blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    DrawQuads(x, y, z, chunk, gameObject);
                }
            }
        }
    }

    void DrawQuads(int x, int y, int z, Chunk chunk, GameObject gameObject) {
        if (chunk.blocks[x, y, z].IsSolid() == false) return;

        foreach (Vector3 direction in Block.directions) {
            if (!chunk.HasSolidNeighbor(x, y, z, direction)) {
                GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
                quad.transform.parent = gameObject.transform;
            }
        }
    }
}
