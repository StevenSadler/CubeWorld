using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterManager
{
    // read the player position from FPSController
    // do not write to playerPosition
    GameObject player;
    int chunkSize;

    //Vector3 lastPlayerPosition;
    Vector3 lastCenter;
    Vector3 secondLastCenter;
    bool centerMoved;

    public CenterManager(GameObject player, int chunkSize) {
        this.chunkSize = chunkSize;
        this.player = player;
        //lastPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

        //lastCenter = new Vector3(
        //    Mathf.Floor(playerPosition.x / chunkSize),
        //    Mathf.Floor(playerPosition.y / chunkSize),
        //    Mathf.Floor(playerPosition.z / chunkSize)
        //);

        lastCenter = ConvertWorldToChunk(player.transform.position);

        secondLastCenter = new Vector3(lastCenter.x, lastCenter.y, lastCenter.z);
    }

    Vector3 ConvertWorldToChunk(Vector3 worldPos) {
        Vector3 scaled = worldPos / chunkSize;
        scaled.x = Mathf.Floor(scaled.x);
        scaled.y = Mathf.Floor(scaled.y);
        scaled.z = Mathf.Floor(scaled.z);
        return scaled * chunkSize;
    }

    public Vector3 GetLastCenter() {
        return lastCenter;
    }

    public Vector3 GetSecondLastCenter() {
        return secondLastCenter;
    }

    public bool UpdateLastCenter() {
        Vector3 playerPosition = player.transform.position;
        //Debug.Log("playerPosition = " + playerPosition);
        centerMoved = false;

        // negative axis directions

        // moving left
        if (lastCenter.x > playerPosition.x + chunkSize) {
            Debug.Log("moved left");
            PrepCenterMoveX();
            lastCenter.x = Mathf.Ceil(playerPosition.x);
        }

        // moving down
        if (lastCenter.y > playerPosition.y + chunkSize) {
            Debug.Log("moved down");
            PrepCenterMoveY();
            lastCenter.y = Mathf.Ceil(playerPosition.y);
        }

        // moving back
        if (lastCenter.z > playerPosition.z + chunkSize) {
            Debug.Log("moved back");
            PrepCenterMoveZ();
            lastCenter.z = Mathf.Ceil(playerPosition.z);
        }


        // positive axis directions

        // moving right
        if (lastCenter.x < playerPosition.x - chunkSize) {
            Debug.Log("moved right");
            PrepCenterMoveX();
            lastCenter.x = Mathf.Floor(playerPosition.x);
        }

        // moving up
        if (lastCenter.y < playerPosition.y - chunkSize) {
            Debug.Log("moved up");
            PrepCenterMoveY();
            lastCenter.y = Mathf.Floor(playerPosition.y);
        }

        // moving forward
        if (lastCenter.z < playerPosition.z - chunkSize) {
            Debug.Log("moved forward");
            PrepCenterMoveZ();
            lastCenter.z = Mathf.Floor(playerPosition.z);
        }


        return centerMoved;
    }

    void PrepCenterMoveX() {
        secondLastCenter.x = lastCenter.x;
        centerMoved = true;
    }

    void PrepCenterMoveY() {
        secondLastCenter.y = lastCenter.y;
        centerMoved = true;
    }

    void PrepCenterMoveZ() {
        secondLastCenter.z = lastCenter.z;
        centerMoved = true;
    }

}
