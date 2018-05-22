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
        chunk = new Chunk(Vector3.zero, chunkSize, blockType, cubeMaterial);
        DrawChunk(chunk);
        CombineQuads();
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
        foreach (Vector3 direction in Block.directions) {
            if (!chunk.HasSolidNeighbor(x, y, z, direction)) {
                GameObject quad = chunk.blocks[x, y, z].CreateQuad(direction);
                quad.transform.parent = transform;

                MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
                renderer.material = cubeMaterial;
            }
        }
    }

    void CombineQuads() {
        // combine all child meshes
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        int limit = meshFilters.Length;
        CombineInstance[] combine = new CombineInstance[limit];
        for (int i = 0; i < limit; i++) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        // create a new mesh on the parent object
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        // add combined child meshes to the parent mesh
        meshFilter.mesh.CombineMeshes(combine);

        // create a renderer for the parent
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = cubeMaterial;

        // delete all uncombined children
        foreach (Transform quad in transform) {
            Destroy(quad.gameObject);
        }
    }
}
