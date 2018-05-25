using UnityEngine;

public class ChunkRenderer : MonoBehaviour {

    public void Draw(Chunk chunk, int chunkSize, Material cubeMaterial) {
        DrawChunk(chunk, chunkSize, cubeMaterial);
    }

    public void DrawCombined(Chunk chunk, int chunkSize, Material cubeMaterial) {
        DrawChunk(chunk, chunkSize);
        QuadUtils.CombineQuads(gameObject, cubeMaterial);
    }

    void DrawChunk(Chunk chunk, int chunkSize, Material cubeMaterial = null) {
        // draw blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    DrawQuads(x, y, z, chunk, cubeMaterial);
                }
            }
        }
    }

    void DrawQuads(int x, int y, int z, Chunk chunk, Material cubeMaterial) {
        if (chunk.blocks[x, y, z].IsSolid() == false) return;

        foreach (Vector3 direction in Block.directions) {
            if (!chunk.HasSolidNeighbor(x, y, z, direction)) {
                GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
                quad.transform.parent = transform;

                if (cubeMaterial != null) {
                    MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
                    renderer.material = cubeMaterial;
                }
            }
        }
    }
}
