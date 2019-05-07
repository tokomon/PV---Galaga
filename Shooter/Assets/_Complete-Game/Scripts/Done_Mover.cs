using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
    public float speed;
    int mov;
    public float movS = -60;
    bool flag = false;
    float min = -60.0F;
    float max = 60.0F;
    public GameObject target;
    public GameObject enemyClone;
    public bool moveT;
    bool inicial;
    bool goHome;
    public AudioSource[] AudioClips = null;

    public Vector3 targetPos;

    public float speedT ;

    public float arcHeight = 1;


    public Vector3 startPos;


    void Start()
    {

        target = GameObject.Find("Done_Player");
        enemyClone = new GameObject();
        enemyClone.tag = "Enemy";
        enemyClone.transform.position = gameObject.transform.position;
        enemyClone.transform.rotation = gameObject.transform.rotation;
        inicial = false;
        goHome = false;
        startPos = transform.position;
        targetPos = target.transform.position;
        speedT = 0.1F;

        if (this.gameObject.tag != "EnemyN")
        {

            GetComponent<Rigidbody>().velocity = transform.forward * speed;
           

        }
       

    }

    void Update()
    {

        if (tag == "EnemyN")
        {
            moveInt();
        }
        if (moveT == true)
        {
            
            moveRR();
            //moveParb();
        }
        else
        {
            //audio[1].Stop();
        }

    }
    void moveInt()
    {
        if (movS < max && flag == false)
        {
            movS++;
            transform.position = new Vector3(transform.position.x + 0.01F, transform.position.y, transform.position.z);
            if (movS == max)
            {
                flag = true;
            }
        }
        else
        {
            if (movS > min && flag == true)
            {
                movS--;
                transform.position = new Vector3(transform.position.x - 0.01F, transform.position.y, transform.position.z);
                if (movS == min)
                {
                    flag = false;
                }
            }
        }
    }
    void moveParb()
    {
        Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speedT * Time.deltaTime);

        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speedT * Time.deltaTime);
        
        float baseY = Mathf.Lerp(startPos.z, targetPos.z, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        nextPos = new Vector3(nextX, transform.position.y,baseY + arc);

        // Rotate to face the next position, and then move there
        //transform.rotation = nextPos - transform.position;
        transform.position = nextPos;

        // Do something when we reach the target
        if (nextPos == targetPos) Arrived();

    }
    void Arrived()
    {
        Destroy(gameObject);
    }

    void moveRR()
    {
        float MoveSpeed = 1.5F;
        float MinDist = 2.0F;
        float MaxDist = 2.5F;

        if ( goHome ==false)
        {

            //if(inicial)
            if (inicial == false)
            {
                if (!AudioClips[0].isPlaying)
                {
                    AudioClips[0].Play();
                }
                if (target != null)
                {
                    transform.LookAt(target.transform);


                    if (Vector3.Distance(transform.position, target.transform.position) >= MinDist)
                    {
                        GetComponent<Done_WeaponController>().moveT = true;
                        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                        if (Vector3.Distance(transform.position, target.transform.position) <= MaxDist)
                        {
                            inicial = true;
                            GetComponent<Done_WeaponController>().moveT = false;


                            //regrese
                        }
                        //Here Call any function U want Like Shoot at here or something
                    }
                }
                else
                {
                    inicial = true;
                    GetComponent<Done_WeaponController>().moveT = false;
                }
            }
            else
            {
                transform.LookAt(enemyClone.transform);
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;


                if (Vector3.Distance(transform.position,enemyClone.transform.position)<1.0F)
                 {
                    transform.position = enemyClone.transform.position;
                    transform.rotation = enemyClone.transform.rotation;
                    goHome = true;
                    moveT = false;

                }

            }
        }
    }
}


            //transform.Translate(Vector3.forward * 2 * Time.deltaTime);

