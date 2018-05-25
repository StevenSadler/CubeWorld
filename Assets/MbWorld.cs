using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbWorld : MonoBehaviour {

    public static Dictionary<string, Chunk> chunks;
    public static Dictionary<string, GameObject> chunkObjects;

    public int columnHeight = 2;
    public int chunkSize = 8;
    //public int worldSize = 2;

    public Material textureAtlas;
    public Block.BlockType blockType;  // only used for testing with all the same block type

    public static string BuildChunkName(Vector3 v) {
        return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
    }

    //public static bool IsOutsideChunk(int x, int y, int z) {
    //    return !(x > -1 && x < chunkSize &&
    //        y > -1 && y < chunkSize &&
    //        z > -1 && z < chunkSize);
    //}

    IEnumerator BuildChunkColumn() {
        for (int i = 0; i < columnHeight; i++) {
            Vector3 chunkPosition = new Vector3(transform.position.x, i * chunkSize, transform.position.z);
            Chunk chunk = new Chunk(chunkPosition, chunkSize);
            string chunkName = BuildChunkName(chunkPosition);
            chunks.Add(chunkName, chunk);
        }
        Debug.Log("chunks Count= " + chunks.Count);

        foreach (KeyValuePair<string, Chunk> chunk in chunks) {
            GameObject chunkObject = DrawChunk(chunk.Value);
            QuadUtils.CombineQuads(chunkObject, textureAtlas);

            //yield return null;
        }

        
        yield return null;
    }

    GameObject DrawChunk(Chunk chunk) {
        // create chunk gameobject to hold quads
        Vector3 chunkPosition = chunk.position;
        string chunkName = BuildChunkName(chunkPosition);
        GameObject chunkObject = new GameObject(chunkName);
        chunkObject.transform.position = chunkPosition;
        chunkObject.transform.parent = transform;

        Debug.Log("draw chunk= " + chunk + " position= " + chunkPosition);
        Debug.Log("chunkObject= " + chunkObject);
        Debug.Log("chunk.blocks= " + chunk.blocks);
        Block block = chunk.blocks[chunkSize -1, chunkSize - 1, chunkSize - 1];
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

                MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
                renderer.material = textureAtlas;
            }
        }
    }

    //IEnumerator BuildWorld() {
    //    for (int z = 0; z < worldSize; z++) {
    //        for (int x = 0; x < worldSize; x++) {
    //            for (int y = 0; y < columnHeight; y++) {
    //                Vector3 chunkPosition = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize);
    //                Chunk chunk = new Chunk(chunkPosition, textureAtlas);
    //                chunk.gameObject.transform.parent = transform;
    //                chunks.Add(chunk.gameObject.name, chunk);
    //            }
    //        }
    //    }

    //    foreach (KeyValuePair<string, Chunk> chunk in chunks) {
    //        chunk.Value.DrawChunk();
    //        yield return null;
    //    }
    //}

    // Use this for initialization
    void Start () {
        chunks = new Dictionary<string, Chunk>();
        chunkObjects = new Dictionary<string, GameObject>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        StartCoroutine(BuildChunkColumn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
