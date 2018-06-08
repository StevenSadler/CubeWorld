using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour
{
    private World world;
    public Dictionary<Vector3, GameObject> viewChunks;

    QuadUtils.RenderDelegate del;
    Material cubeMaterial;

    public void SetModel(World world) {
        this.world = world;
        viewChunks = new Dictionary<Vector3, GameObject>();
    }

    public void Draw(Material cubeMaterial) {
        del = QuadUtils.RenderQuads;
        this.cubeMaterial = cubeMaterial;
        DrawWorld();
    }

    public void DrawCombined(Material cubeMaterial) {
        del = QuadUtils.CombineQuads;
        this.cubeMaterial = cubeMaterial;
        DrawWorld();
    }

    public void DrawCollide(Material cubeMaterial) {
        del = QuadUtils.CollideQuads;
        this.cubeMaterial = cubeMaterial;
        DrawWorld();
    }

    public void UpdateView() {
        Vector3 centerChunkPosition = world.centerChunkPosition;

        List<GameObject> clearObjects = new List<GameObject>();
        foreach (KeyValuePair<Vector3, GameObject> chunkObjectKVPair in viewChunks) {
            GameObject chunkObject = chunkObjectKVPair.Value;
            if (!world.IsInWorldView(chunkObject.transform.position)) {
                clearObjects.Add(chunkObject);
                viewChunks.Remove(chunkObjectKVPair.Key);
            }
        }

        foreach (GameObject clearObject in clearObjects) {
            // add new chunk opposite the center from the cleared chunk
            Vector3 chunkPosition = 2 * centerChunkPosition - clearObject.transform.position;
            Chunk chunk = world.GetChunkAt(chunkPosition);

            GameObject chunkObject = CreateChunkObject(chunk);
            chunkObject.transform.parent = transform;
            DrawWorldChunk(chunk, chunkObject);

            del(chunkObject, cubeMaterial);

            // must set chunkObject's position AFTER DrawChunkWorld
            // so the quads will be in the correct position
            chunkObject.transform.position = chunkPosition;


            viewChunks.Add(chunkPosition, chunkObject);

            Destroy(clearObject);
        }
        clearObjects.Clear();
    }

    void DrawWorld() {

        foreach (KeyValuePair<Vector3, Chunk> chunk in world.modelChunks) {
            // if chunk position is inside render radius
            if (world.IsInWorldView(chunk.Value.position)) {
                GameObject chunkObject = CreateChunkObject(chunk.Value);
                chunkObject.transform.parent = transform;
                DrawWorldChunk(chunk.Value, chunkObject);

                del(chunkObject, cubeMaterial);

                // must set chunkObject's position AFTER DrawChunkWorld
                // so the quads will be in the correct position
                chunkObject.transform.position = chunk.Value.position;
            }
        }
    }

    GameObject CreateChunkObject(Chunk chunk) {
        // create chunk gameobject to hold quads
        Vector3 chunkPosition = chunk.position;
        string chunkName = world.BuildChunkName(chunkPosition);
        GameObject chunkObject = new GameObject(chunkName);
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

            //if (!world.HasSolidNeighbor(nx, ny, nz, chunk, direction)) {
            //    GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
            //    quad.transform.parent = gameObject.transform;
            //}

            if (!chunk.HasSolidNeighbor(nx, ny, nz, direction)) {
                GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
                quad.transform.parent = gameObject.transform;
            }
        }
    }
}
