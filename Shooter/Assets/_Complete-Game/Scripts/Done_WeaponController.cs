using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
    public bool moveT=false;
	void Start ()
	{

        InvokeRepeating("Fire", delay, fireRate);
        
    }
   

    void Fire ()
	{
        if(moveT==true)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
		
	}
}
