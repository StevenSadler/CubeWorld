using UnityEngine;

public class MainChunk : MonoBehaviour {

    public int chunkSize;
    public Material cubeMaterial;
    public bool drawCombined;

    // Use this for initialization
    void Start () {
        Chunk chunk = new Chunk(Vector3.up * chunkSize, chunkSize);
        ChunkRenderer chunkRenderer = gameObject.GetComponent<ChunkRenderer>();

        if (drawCombined) {
            chunkRenderer.DrawCombined(chunk, chunkSize, gameObject, cubeMaterial);
        } else {
            chunkRenderer.Draw(chunk, chunkSize, gameObject, cubeMaterial);
        }
    }
}
