using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbBlock : MonoBehaviour {

    private Block block;

    public Block.BlockType blockType;
    public Material cubeMaterial;

	// Use this for initialization
	void Start () {
        block = new Block(blockType, Vector3.zero);
        Draw(block);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Draw(Block block) {
        if (block.blockType == Block.BlockType.AIR) return;

        foreach (Vector3 direction in Block.directions) {
            GameObject quad = block.CreateQuad(direction);
            quad.transform.parent = transform;

            MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
            renderer.material = cubeMaterial;

            //if (!HasSolidNeighbor(direction)) {
            //    CreateQuad(direction);
            //}
        }
    }

    //private void CreateQuad(Vector3 direction) {
    //    Mesh mesh = new Mesh();
    //    mesh.name = "ScriptedMesh";

    //    mesh.vertices = QuadUtils.GetVertices(direction);
    //    mesh.normals = QuadUtils.GetNormals(direction);
    //    mesh.triangles = QuadUtils.GetTriangles();
    //    mesh.uv = TextureUtils.GetUVs(block.blockType, direction);

    //    mesh.RecalculateBounds();

    //    GameObject quad = new GameObject("quad");
    //    quad.transform.position = block.position;
    //    quad.transform.parent = transform;
    //    MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
    //    meshFilter.mesh = mesh;

    //    MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
    //    renderer.material = cubeMaterial;
    //}
}
