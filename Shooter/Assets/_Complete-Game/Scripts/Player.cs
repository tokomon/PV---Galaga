using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Mesh mesh;
    

    // Start is called before the first frame update
    void Start()
    {
        mesh = Resources.Load("Meshes/StarSparrowExamples1", typeof(Mesh)) as Mesh;
        GameObject levels = GameObject.Find("Player");
        meshFilter = levels.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        Material[] Planet = new Material[2];
        Planet[0] = Resources.Load("Materials/StarSparrow Black", typeof(Material)) as Material;
        Planet[1] = Resources.Load("Materials/StarSparrow Black", typeof(Material)) as Material;

        levels.GetComponent<MeshRenderer>().materials = Planet;
        levels.GetComponent<MeshCollider>().sharedMesh= mesh;
        levels.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
