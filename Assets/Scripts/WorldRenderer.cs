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
        Debug.Log("WorldRenderer UpdateView started  moveChunks.Count=" + world.moveChunks.Count);
        Vector3 centerChunkPosition = world.centerChunkPosition;

        foreach (Chunk chunk in world.moveChunks) {
            Vector3 chunkPosition = chunk.position;
            //GameObject chunkObject = chunk.GetViewRef();
            //Destroy(chunkObject);

            GameObject chunkObject = CreateChunkObject(chunk);
            chunkObject.transform.parent = transform;
            DrawWorldChunk(chunk, chunkObject);

            //ResetChunkObject(chunk, chunkObject);


            //DrawWorldChunk(chunk, chunkObject);

            del(chunkObject, cubeMaterial);

            // must set chunkObject's position AFTER DrawChunkWorld
            // so the quads will be in the correct position
            chunkObject.transform.position = chunkPosition;

            chunk.SetViewRef(chunkObject);
        }
    }

    void DrawWorld() {

        //foreach (KeyValuePair<Vector3, Chunk> chunkKVPair in world.modelChunks) {
            //Chunk chunk = chunkKVPair.Value;
        foreach (Chunk chunk in world.modelChunks) {
                // if chunk position is inside render radius
                if (world.IsInWorldView(chunk.position)) {
                GameObject chunkObject = CreateChunkObject(chunk);
                chunkObject.transform.parent = transform;
                DrawWorldChunk(chunk, chunkObject);

                del(chunkObject, cubeMaterial);

                // must set chunkObject's position AFTER DrawChunkWorld
                // so the quads will be in the correct position
                chunkObject.transform.position = chunk.position;

                chunk.SetViewRef(chunkObject);
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
        Block block = chunk.blocks[x, y, z];
        if (block.IsSolid() == false) return;

        foreach (Vector3 direction in Block.directions) {
            if (!chunk.HasSolidNeighbor(block, direction)) {
                GameObject quad = QuadUtils.CreateQuad(block, direction);
                quad.transform.parent = gameObject.transform;
            }
        }
    }

    void ResetChunkObject(Chunk chunk, GameObject chunkObject) {
        chunkObject.transform.position = Vector3.zero;

        Destroy(chunkObject.GetComponent<MeshFilter>().mesh);
        Destroy(chunkObject.GetComponent<MeshCollider>());
        Destroy(chunkObject.GetComponent<MeshFilter>());
        Destroy(chunkObject.GetComponent<MeshRenderer>());

        chunkObject.name = world.BuildChunkName(chunk.position);
    }
}
