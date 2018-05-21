using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block
{
    private static Vector3[] directions = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    public enum BlockType
    {
        GRASS,
        DIRT,
        STONE,
        TNT,
        AIR
    };

    BlockType blockType;
    Vector3 position;
    GameObject parent;
    Chunk owner;

    public Block(BlockType blockType, Vector3 position, GameObject parent, Chunk owner) {
        this.blockType = blockType;
        this.position = position;
        this.parent = parent;
        this.owner = owner;
    }

    public bool IsSolid() {
        return blockType != BlockType.AIR;
    }

    public bool HasSolidNeighbor(Vector3 direction) {
        Block[,,] neighborBlocks = owner.blocks;  // parent.GetComponent<Chunk>().blocks;
        Vector3 neighbor = position + direction;
        int x = (int)neighbor.x;
        int y = (int)neighbor.y;
        int z = (int)neighbor.z;

        if (World.IsOutsideChunk(x,y,z)) {
            // get the neighbor chunk of this block's chunk
            Vector3 neighborChunkPosition = parent.transform.position + direction * World.chunkSize;
            string neighborName = World.BuildChunkName(neighborChunkPosition);

            

            // get the neighbor chunk's blocks
            Chunk neighborChunk;
            if (World.chunks.TryGetValue(neighborName, out neighborChunk)) {
                neighborBlocks = neighborChunk.blocks;

                // convert this xyz to neighbor chunk's neighbor block values
                x = ConvertBlockIndexToLocal(x);
                y = ConvertBlockIndexToLocal(y);
                z = ConvertBlockIndexToLocal(z);
            } else {
                return false;
            }

        } else {
            neighborBlocks = owner.blocks;
        }

        try {
            return neighborBlocks[x,y,z].IsSolid();
        }
        catch (System.IndexOutOfRangeException) { }

        return false;
    }

    public void Draw() {
        if (blockType == BlockType.AIR) return;

        foreach (Vector3 direction in directions) {
            if (!HasSolidNeighbor(direction)) {
                CreateQuad(direction);
            }
        }
    }

    private void CreateQuad(Vector3 direction) {
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";

        mesh.vertices = QuadUtils.GetVertices(direction);
        mesh.normals = QuadUtils.GetNormals(direction);
        mesh.triangles = QuadUtils.GetTriangles();
        mesh.uv = TextureUtils.GetUVs(blockType, direction);

        mesh.RecalculateBounds();

        GameObject quad = new GameObject("quad");
        quad.transform.position = position;
        quad.transform.parent = parent.transform;
        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        //MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
        //renderer.material = cubeMaterial;
    }

    private int ConvertBlockIndexToLocal(int i) {
        if (i == -1) {
            i = World.chunkSize - 1;
        } else if (i == World.chunkSize) {
            i = 0;
        }
        return i;
    }
}
