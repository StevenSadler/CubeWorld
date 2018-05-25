using UnityEngine;

public class Chunk {

    public Block[,,] blocks;
    public Vector3 position;

    public Chunk(Vector3 position, int chunkSize) {
        this.position = position;
        BuildChunk(chunkSize);
    }

    void BuildChunk(int chunkSize) {
        // need to get chunkSize from world

        blocks = new Block[chunkSize, chunkSize, chunkSize];

        // create blocks
        for (int z = 0; z < chunkSize; z++) {
            for (int y = 0; y < chunkSize; y++) {
                for (int x = 0; x < chunkSize; x++) {
                    Vector3 position = new Vector3(x, y, z);

                    // need to get blocktype from world for each block by its position

                    blocks[x, y, z] = new Block(GetBlockType(position), position);
                }
            }
        }
    }

    private Block.BlockType GetBlockType(Vector3 blockPosition) {
        // calculate blocktype by applying a function such as perlin noise
        // after adding chunk position and block position vectors

        // for now stub this
        Block.BlockType blockType = (Random.Range(0, 100) < 50 ? Block.BlockType.GRASS : Block.BlockType.AIR);
        return blockType;
    }

    public bool HasSolidNeighbor(int x, int y, int z, Vector3 direction) {
        try {
            return blocks[x + (int)direction.x, y + (int)direction.y, z + (int)direction.z].IsSolid();
        } catch (System.IndexOutOfRangeException) { }

        return false;
    }

    //// making highlevel classes compilable
    //public void DrawChunk() {
    //    // draw blocks
    //    for (int z = 0; z < chunkSize; z++) {
    //        for (int y = 0; y < chunkSize; y++) {
    //            for (int x = 0; x < chunkSize; x++) {
    //                //blocks[x, y, z].Draw();
    //            }
    //        }
    //    }
    //}

    //   void BuildChunk()
    //{
    //       int chunkSize = World.chunkSize;
    //       blocks = new Block[chunkSize, chunkSize, chunkSize];

    //        // create blocks
    //        for (int z = 0; z<chunkSize; z++) {
    //            for (int y = 0; y<chunkSize; y++) {
    //                for (int x = 0; x<chunkSize; x++) {
    //                    Vector3 position = new Vector3(x, y, z);

    //                    if (Random.Range(0,100) < 50) {
    //                        blocks[x, y, z] = new Block(Block.BlockType.DIRT, position, gameObject, this);
    //} else {
    //                        blocks[x, y, z] = new Block(Block.BlockType.AIR, position, gameObject, this);
    //                    }
    //                }
    //            }
    //        }
    //   }

    //   public void DrawChunk() {
    //       int chunkSize = World.chunkSize;

    //           // draw blocks
    //           for (int z = 0; z<chunkSize; z++) {
    //               for (int y = 0; y<chunkSize; y++) {
    //                   for (int x = 0; x<chunkSize; x++) {
    //                       blocks[x, y, z].Draw();
    //}
    //               }
    //           }
    //       CombineQuads();
    //   }

    //void CombineQuads() {
    //    // combine all child meshes
    //    MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
    //    int limit = meshFilters.Length;
    //    CombineInstance[] combine = new CombineInstance[limit];
    //    for (int i = 0; i < limit; i++) {
    //        combine[i].mesh = meshFilters[i].sharedMesh;
    //        combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
    //    }

    //    // create a new mesh on the parent object
    //    MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
    //    meshFilter.mesh = new Mesh();

    //    // add combined child meshes to the parent mesh
    //    meshFilter.mesh.CombineMeshes(combine);

    //    // create a renderer for the parent
    //    MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
    //    renderer.material = cubeMaterial;

    //    // delete all uncombined children
    //    foreach (Transform quad in gameObject.transform) {
    //        GameObject.Destroy(quad.gameObject);
    //    }
    //}

}
