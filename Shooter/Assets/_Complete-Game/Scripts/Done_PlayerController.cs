using UnityEngine;
using System.Collections;

using UnityEngine.UI;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}


public class Done_PlayerController : MonoBehaviour
{
    public AudioSource[] AudioClips = null;

    public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
    public Mesh[] mesh = new Mesh[7];

    private float nextFire;

    public Text levelText;
    public Material[] Planet = new Material[8];
    Material[] PlanetT = new Material[1];

    GameObject player;
    int result= 1;
    private MeshFilter mFilter;
    private MeshRenderer mRenderer;
    private MeshCollider mCollider;
    public GameObject playerN;

    void Start()
    {
        /*
        transform.localScale = new Vector3(0.15F, 0.15F, 0.15F);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject newE = Instantiate(playerN, new Vector3(0.0F,0.0F,0.0F), spawnRotation);
        */
       
        GetComponent<MeshFilter>().mesh = mesh[result-1];
        PlanetT[0] = Planet[0];
        GetComponent<MeshRenderer>().materials = PlanetT;
        GetComponent<MeshCollider>().sharedMesh = mesh[result-1];
        transform.localScale = new Vector3(0.15F, 0.15F, 0.15F);

    }
    void Awake()
    {
        mFilter = GetComponent<MeshFilter>();
        mRenderer = GetComponent<MeshRenderer>();
        mCollider = GetComponent<MeshCollider>();
    }


    void Update ()
	{
        result = System.Int32.Parse(levelText.text.ToString());

        if (result > 1)
        {
            Debug.Log(result);
            mFilter.GetComponent<MeshFilter>().mesh = mesh[result - 1];
            PlanetT[0] = Planet[result - 1];
            mRenderer.materials = PlanetT;
          //  mCollider.GetComponent<MeshCollider>().sharedMesh = mesh[result - 1];
          /// transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
        }


        if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
            nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            //Instantiate(shot, new Vector3(shotSpawn.position.x+2, shotSpawn.position.y, shotSpawn.position.z), shotSpawn.rotation);

            GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);


        GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
