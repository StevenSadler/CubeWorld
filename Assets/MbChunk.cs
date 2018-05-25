using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbChunk : MonoBehaviour {

    private Chunk chunk;

    public int chunkSize;
    public Block.BlockType blockType;
    public Material cubeMaterial;

	// Use this for initialization
	void Start () {
        chunk = new Chunk(Vector3.zero, chunkSize);
        DrawChunk(chunk);
        QuadUtils.CombineQuads(gameObject, cubeMaterial);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawChunk(Chunk chunk) {
        // draw blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    DrawQuads(x, y, z);
                }
            }
        }
    }

    void DrawQuads(int x, int y, int z) {
        if (chunk.blocks[x, y, z].IsSolid() == false) return;

        foreach (Vector3 direction in Block.directions) {
            if (!chunk.HasSolidNeighbor(x, y, z, direction)) {
                GameObject quad = QuadUtils.CreateQuad(chunk.blocks[x, y, z], direction);
                quad.transform.parent = transform;

                MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
                renderer.material = cubeMaterial;
            }
        }
    }
}
