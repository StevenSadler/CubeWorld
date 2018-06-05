using UnityEngine;

public class MainWorld : MonoBehaviour
{
    public int columnHeight = 3;
    public int chunkSize = 4;
    public int worldSize = 2;
    public int radius = 1;
    public Material cubeMaterial;
    public GameObject player;
    public GameObject centerMark;
    public bool drawCombined;
    public BreadthFirstSearch.WorldType worldType;

    CenterManager centerManager;

    // Use this for initialization
    void Start() {
        Vector3 surfacePosition = new Vector3(12, 0, 8);
        int surfaceY = GetSurfaceY(surfacePosition);
        surfacePosition.y = surfaceY;
        int chunkY = surfaceY - surfaceY % chunkSize;
        Vector3 chunkPosition = new Vector3(0, chunkY, 0);

        player.transform.position = surfacePosition + Vector3.up * 2;

        centerManager = new CenterManager(player, chunkSize);
        centerMark.transform.position = centerManager.GetLastCenter();


        World world = new World(chunkPosition, chunkSize, radius, worldType);
        WorldRenderer worldRenderer = gameObject.GetComponent<WorldRenderer>();
        worldRenderer.SetModel(world);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        if (drawCombined) {
            //worldRenderer.DrawCombined(cubeMaterial);
            worldRenderer.DrawCollide(cubeMaterial);
        } else {
            worldRenderer.Draw(cubeMaterial);
        }
    }

    int GetSurfaceY(Vector3 worldPosition) {
        int stoneHeight = NoiseUtils.GenerateStoneHeight(worldPosition.x, worldPosition.z);
        int grassHeight = NoiseUtils.GenerateHeight(worldPosition.x, worldPosition.z);
        if (grassHeight > stoneHeight) {
            return grassHeight;
        }
        return stoneHeight;
    }

    void Update() {
        if (centerManager.UpdateLastCenter()) {

            centerMark.transform.position = centerManager.GetLastCenter();
        }
    }
}
