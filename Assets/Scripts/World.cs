using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class World
{
    //public List<Chunk> modelChunks;
    public Dictionary<Vector3, Chunk> modelChunks;
    //public Dictionary<string, Chunk> viewChunks;

    public int columnHeight;
    public int chunkSize;
    public int radius;

    public int worldSize;
    public Vector3 centerChunkPosition;

    private BreadthFirstSearch bfsModel;
    //private BreadthFirstSearch bfsView;

    public World(Vector3 chunkPosition, int chunkSize, int radius, BreadthFirstSearch.WorldType worldType) {
        
        this.chunkSize = chunkSize;
        this.radius = radius;
        centerChunkPosition = chunkPosition;
        //modelChunks = new List<Chunk>();
        modelChunks = new Dictionary<Vector3, Chunk>();
        //viewChunks = new Dictionary<string, Chunk>();

        bfsModel = new BreadthFirstSearch(radius, worldType);
        //bfsView = new BreadthFirstSearch(radius, worldType);
        BuildStartWorld(chunkPosition);

        //BuildFirstChunk(chunkPosition);
    }

    public Chunk GetChunkAt(Vector3 chunkPosition) {
        Chunk chunk = null;
        if (!modelChunks.TryGetValue(chunkPosition, out chunk)) {
            Debug.Log("World.GetChunkAt() new view chunkPosition not found in modelChunks: " + chunkPosition);
        }
        return chunk;
    }

    public bool IsInWorldView(Vector3 chunkPosition) {
        return bfsModel.isInWorld(centerChunkPosition, chunkPosition, radius * chunkSize);
    }

    public bool IsInWorldModel(Vector3 chunkPosition) {
        return bfsModel.isInWorld(centerChunkPosition, chunkPosition, radius * chunkSize);
    }

    void BuildStartWorld(Vector3 firstChunkPosition) {
        foreach (Vector3 bfsPos in bfsModel.allNodes) {
            Vector3 chunkPosition = firstChunkPosition + bfsPos * chunkSize;
            Chunk chunk = new Chunk(chunkPosition, chunkSize);
            modelChunks.Add(chunkPosition, chunk);
        }
        //foreach (Vector3 bfsPos in bfsView.allNodes) {
        //    Vector3 chunkPosition = firstChunkPosition + bfsPos * chunkSize;
        //    Chunk chunk = new Chunk(chunkPosition, chunkSize);
        //    string chunkName = BuildChunkName(chunkPosition);
        //    viewChunks.Add(chunkName, chunk);
        //}
    }

    //void BuildFirstChunk(Vector3 chunkPosition) {
    //    Chunk chunk = new Chunk(chunkPosition, chunkSize);
    //    string chunkName = BuildChunkName(chunkPosition);
    //    modelChunks.Add(chunkName, chunk);
    //}

    //public World(int columnHeight, int chunkSize, int worldSize) {

    //    this.columnHeight = columnHeight;
    //    this.chunkSize = chunkSize;
    //    this.worldSize = worldSize;

    //    modelChunks = new Dictionary<string, Chunk>();
    //    BuildWorld();
    //}

    //void BuildWorld() {
    //    for (int z = 0; z < worldSize; z++) {
    //        for (int x = 0; x < worldSize; x++) {
    //            for (int y = 0; y < columnHeight; y++) {
    //                Vector3 chunkPosition = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize);
    //                Chunk chunk = new Chunk(chunkPosition, chunkSize);
    //                string chunkName = BuildChunkName(chunkPosition);
    //                modelChunks.Add(chunkName, chunk);
    //            }
    //        }
    //    }
    //}

    public string BuildChunkName(Vector3 v) {
        return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
    }
    
    public void UpdateWorldModel(Vector3 lastCenter, Vector3 secondLastCenter) {

        List<Chunk> clearChunks = new List<Chunk>();

        DebugOut("World UpdateWorld", "radius= " + radius + "  modelChunks.Count= " + modelChunks.Count);

        foreach (KeyValuePair<Vector3, Chunk> chunkKVPair in modelChunks) {
            Chunk chunk = chunkKVPair.Value;

            if (!IsInWorldModel(chunk.position)) {
                clearChunks.Add(chunk);
            }
        }

        DebugOut("World UpdateWorld", "clearChunks.Count=" + clearChunks.Count);
        foreach (Chunk clearChunk in clearChunks) {
            // add new chunk opposite the center from the cleared chunk
            Vector3 flipCenter = (lastCenter + secondLastCenter) * 0.5f;
            Vector3 chunkPosition = 2 * flipCenter - clearChunk.position;
            modelChunks.Remove(clearChunk.position);

            clearChunk.ResetChunk(chunkPosition, chunkSize);

            modelChunks.Add(chunkPosition, clearChunk);

            //yield return null;
        }
        DebugOut("World UpdateWorld", "model update end");
        clearChunks.Clear();
        DebugOut("World UpdateWorld", "end");
        
    }

    public void DebugOut(string message1, string message2) {
        StringBuilder sb = new StringBuilder();
        sb.Append(message1);
        sb.Append("  ");
        sb.Append(message2);
        sb.Append("  ");
        sb.Append(Time.realtimeSinceStartup);
        Debug.Log(sb.ToString());
    }
}
