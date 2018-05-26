using UnityEngine;

public class MainBlock : MonoBehaviour
{
    public Block.BlockType blockType;
    public Material cubeMaterial;
    public bool drawCombined;

    // Use this for initialization
    void Start() {
        Block block = new Block(blockType, Vector3.zero);
        BlockRenderer blockRenderer = gameObject.GetComponent<BlockRenderer>();

        if (drawCombined) {
            blockRenderer.DrawCombined(block, cubeMaterial);
        } else {
            blockRenderer.Draw(block, cubeMaterial);
        }
    }
}
