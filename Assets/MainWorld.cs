using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWorld : MonoBehaviour {

    public int columnHeight = 2;
    public int chunkSize = 8;
    public Material cubeMaterial;
    public bool drawCombined;

    // Use this for initialization
    void Start() {
        World world = new World(columnHeight, chunkSize);
        WorldRenderer worldRenderer = gameObject.GetComponent<WorldRenderer>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        if (drawCombined) {
            worldRenderer.DrawCombined(World.chunks, chunkSize, cubeMaterial);
        } else {
            worldRenderer.Draw(World.chunks, chunkSize, cubeMaterial);
        }
    }
}
