using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWorld : MonoBehaviour {

    
    

    public int columnHeight = 2;
    public int chunkSize = 8;
    public Material cubeMaterial;

    // Use this for initialization
    void Start() {
        World world = new World(columnHeight, chunkSize);
        WorldRenderer worldRenderer = gameObject.GetComponent<WorldRenderer>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        worldRenderer.DrawColumn(World.chunks, chunkSize, cubeMaterial);
    }

    //void Start() {
    //    chunks = new Dictionary<string, Chunk>();
    //    chunkObjects = new Dictionary<string, GameObject>();
    //    transform.position = Vector3.zero;
    //    transform.rotation = Quaternion.identity;
    //    StartCoroutine(BuildChunkColumn());
    //}
}
