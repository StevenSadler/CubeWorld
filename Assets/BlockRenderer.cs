using UnityEngine;

public class BlockRenderer : MonoBehaviour {

    public void Draw(Block block, Material cubeMaterial) {
        DrawQuads(block);
        QuadUtils.RenderQuads(gameObject, cubeMaterial);
    }

    public void DrawCombined(Block block, Material cubeMaterial) {
        DrawQuads(block);
        QuadUtils.CombineQuads(gameObject, cubeMaterial);
    }

    private void DrawQuads(Block block) {
        if (block.IsSolid() == false) return;
        
        foreach (Vector3 direction in Block.directions) {
            GameObject quad = QuadUtils.CreateQuad(block, direction);
            quad.transform.parent = transform;
        }
    }
}
