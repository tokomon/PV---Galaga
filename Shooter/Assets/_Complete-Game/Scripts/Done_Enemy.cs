using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Enemy 
{
    public GameObject enemy;
    public bool moveI;
    public Enemy(GameObject en)
    {
        enemy = en;
        moveI= true;
    }
    public void moveR(GameObject target)
    {
        float distanceWanted = 3.0f;
        Vector3 diff = enemy.transform.position - target.transform.position;
        diff.y = 0.0f; // ignore Y (as requested in question)
        enemy.transform.position = target.transform.position + diff.normalized * distanceWanted;

    }
  
}

public class Done_Enemy : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Done_Boundary boundary;

    public GameObject shot;
    public GameObject enemy;
    public Transform shotSpawn;
    public float fireRate;
    public Mesh[] mesh = new Mesh[7];

    private float nextFire;

    public Text levelText;
    public Material[] Planet = new Material[8];
    Material[] PlanetT = new Material[1];

    GameObject player;
    int result = 1;
    private MeshFilter mFilter;
    private MeshRenderer mRenderer;
    private MeshCollider mCollider;

    void Start()
    {
        Vector3 spawnPosition;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j<5; j++ )
            {
                if (j%2!=0)
                {
                    if (i != 5)
                    {
                        spawnPosition = new Vector3(-5 + i * 2.0F + 1F, 0, 12 - j * 1.2F);
                        enemy.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(enemy, spawnPosition, spawnRotation);
                    }
                }
                else
                {
                    spawnPosition = new Vector3(-5 + i * 2.0F , 0, 12 - j * 1.2F);
                    enemy.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(enemy, spawnPosition, spawnRotation);
                }
               
            }
            
        }

        //mesh = Resources.LoadAll<Mesh>("Meshes/StarSparrowExamples1");

        /*
        GetComponent<MeshFilter>().mesh = mesh[result - 1];
        PlanetT[0] = Planet[0];
        GetComponent<MeshRenderer>().materials = PlanetT;
        GetComponent<MeshCollider>().sharedMesh = mesh[result - 1];
        transform.localScale = new Vector3(0.15F, 0.15F, 0.15F);
        */
    }
    void Update()
    {
        result = System.Int32.Parse(levelText.text.ToString());

        if (result > 1)
        {/*
            Debug.Log(result);
            mFilter.GetComponent<MeshFilter>().mesh = mesh[result - 1];
            PlanetT[0] = Planet[result - 1];
            mRenderer.materials = PlanetT;
            //  mCollider.GetComponent<MeshCollider>().sharedMesh = mesh[result - 1];
            /// transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
        */}


        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
        /*    nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            //Instantiate(shot, new Vector3(shotSpawn.position.x+2, shotSpawn.position.y, shotSpawn.position.z), shotSpawn.rotation);

            GetComponent<AudioSource>().Play();*/
        }
    }

    void FixedUpdate()
    {
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );


        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        */
    }
}
