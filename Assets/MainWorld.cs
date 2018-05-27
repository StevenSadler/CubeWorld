using UnityEngine;

public class MainWorld : MonoBehaviour
{
    public int columnHeight = 3;
    public int chunkSize = 4;
    public int worldSize = 2;
    public Material cubeMaterial;
    public bool drawCombined;

    // Use this for initialization
    void Start() {
        World world = new World(columnHeight, chunkSize, worldSize);
        WorldRenderer worldRenderer = gameObject.GetComponent<WorldRenderer>();
        worldRenderer.SetModel(world);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        if (drawCombined) {
            worldRenderer.DrawCombined(cubeMaterial);
        } else {
            worldRenderer.Draw(cubeMaterial);
        }
    }
}
