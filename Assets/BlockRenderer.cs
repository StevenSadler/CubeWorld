using UnityEngine;

public class BlockRenderer : MonoBehaviour {
    
    public void Draw(Block block, Material cubeMaterial) {
        DrawQuads(block, cubeMaterial);
    }

    public void DrawCombined(Block block, Material cubeMaterial) {
        DrawQuads(block);
        QuadUtils.CombineQuads(gameObject, cubeMaterial);
    }

    private void DrawQuads(Block block, Material cubeMaterial = null) {
        if (block.IsSolid() == false) return;
        
        foreach (Vector3 direction in Block.directions) {
            GameObject quad = QuadUtils.CreateQuad(block, direction);
            quad.transform.parent = transform;

            if (cubeMaterial) {
                MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
                renderer.material = cubeMaterial;
            }
        }
    }
}
