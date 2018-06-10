using System.Text;
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

    World world;
    WorldRenderer worldRenderer;

    float frameStartTime;

    // Use this for initialization
    void Start() {
        Vector3 surfacePosition = new Vector3(0, 0, 0);
        int surfaceY = GetSurfaceY(surfacePosition);
        surfacePosition.y = surfaceY;
        int chunkY = surfaceY - surfaceY % chunkSize;
        //Vector3 chunkPosition = new Vector3(0, chunkY, 0);

        player.transform.position = surfacePosition + Vector3.up * 2;

        centerManager = new CenterManager(player, chunkSize);
        centerMark.transform.position = centerManager.GetLastCenter();

        Vector3 chunkPosition = centerMark.transform.position;
        world = new World(chunkPosition, chunkSize, radius, worldType);
        worldRenderer = gameObject.GetComponent<WorldRenderer>();
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
        frameStartTime = Time.realtimeSinceStartup;

        if (centerManager.UpdateLastCenter()) {
            Vector3 secondLastCenter = centerManager.GetSecondLastCenter();
            Vector3 lastCenter = centerManager.GetLastCenter();
            
            centerMark.transform.position = lastCenter;
            //world.UpdateModel(lastCenter);

            FrameDebugOut("MainWorld Update", "start  lastCenter=" + lastCenter + "  secondLastCenter=" + secondLastCenter);

            world.UpdateWorldModel(lastCenter, secondLastCenter);

            FrameDebugOut("MainWorld Update", "after model update");

            //StartCoroutine(world.UpdateWorldModel());
            worldRenderer.UpdateView();

            FrameDebugOut("MainWorld Update", "after view update");

            world.moveChunks.Clear();

           /*
            * algorithm
            * 
            * 
            * move the last center
            * 
            * check all model chunks 
            *   if isinworld radius + 1
            *     mark as keep
            *   else
            *     mark as clear
            *     add to clear model list
            *     
            * iterate clear model list
            *   save position of chunk
            *   flip position across last center
            *   create new model chunk
            *   add model chunk
            */
            
        



                
           /*     
            * check all view chunks
            *   if isinworld radius
            *     mark as keep
            *   else
            *     mark as clear
            *     add to clear view list
            * 
            * iterate clear view list
            *   save position of chunk
            *   flip position across last center
            *   create new view chunk
            *   add view chunk
            */
        }
    }

    public void FrameDebugOut(string message1, string message2) {
        StringBuilder sb = new StringBuilder();
        sb.Append(message1);
        sb.Append("  ");
        sb.Append(message2);
        sb.Append("  ");
        sb.Append(Time.realtimeSinceStartup - frameStartTime);
        Debug.Log(sb.ToString());
    }
}
