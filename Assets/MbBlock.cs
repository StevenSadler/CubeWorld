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
        QuadUtils.CombineQuads(gameObject, cubeMaterial);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Draw(Block block) {
        if (block.blockType == Block.BlockType.AIR) return;

        foreach (Vector3 direction in Block.directions) {
            GameObject quad = QuadUtils.CreateQuad(block, direction);
            quad.transform.parent = transform;

            MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
            renderer.material = cubeMaterial;

            //if (!HasSolidNeighbor(direction)) {
            //    CreateQuad(direction);
            //}
        }
    }
}
