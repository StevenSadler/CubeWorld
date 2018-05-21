using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    
    public static int columnHeight = 2;
    public static int chunkSize = 8;
    public static int worldSize = 2;
    public static Dictionary<string, Chunk> chunks;

    public Material textureAtlas;

    public static string BuildChunkName(Vector3 v) {
        return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
    }

    public static bool IsOutsideChunk(int x, int y, int z) {
        return !(x > -1 && x < chunkSize &&
            y > -1 && y < chunkSize &&
            z > -1 && z < chunkSize);
    }

    IEnumerator BuildChunkColumn() {
        for (int i = 0; i < columnHeight; i++) {
            Vector3 chunkPosition = new Vector3(transform.position.x, i * chunkSize, transform.position.z);
            Chunk chunk = new Chunk(chunkPosition, textureAtlas);
            chunk.gameObject.transform.parent = transform;
            chunks.Add(chunk.gameObject.name, chunk);
        }

        foreach(KeyValuePair<string, Chunk> chunk in chunks) {
            chunk.Value.DrawChunk();
            yield return null;
        }
    }

    IEnumerator BuildWorld() {
        for (int z = 0; z < worldSize; z++) {
            for (int x = 0; x < worldSize; x++) {
                for (int y = 0; y < columnHeight; y++) {
                    Vector3 chunkPosition = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize);
                    Chunk chunk = new Chunk(chunkPosition, textureAtlas);
                    chunk.gameObject.transform.parent = transform;
                    chunks.Add(chunk.gameObject.name, chunk);
                }
            }
        }

        foreach (KeyValuePair<string, Chunk> chunk in chunks) {
            chunk.Value.DrawChunk();
            yield return null;
        }
    }

    // Use this for initialization
    void Start () {
        chunks = new Dictionary<string, Chunk>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        StartCoroutine(BuildWorld());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
