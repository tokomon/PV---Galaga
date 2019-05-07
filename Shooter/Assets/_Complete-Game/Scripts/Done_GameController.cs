using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Timers;
public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text timeText;
    public Text gameOverText;
    public Text levelText;
    public Text levelT;
    public GameObject enemy;
    public GameObject[] enemyTol;
    //public ArrayList<Enemy> enemyTotal=new Enemy[24];
    private bool gameOver;
    private bool restart;
    private int score;

    public GameObject target; //the enemy's target
    float moveSpeed = 5; //move speed
    float rotationSpeed = 5; //speed of turning
    private Rigidbody rb;
    public RawImage img;
    public AudioSource[] AudioClips = null;
    public GameObject mesh;



    int level =0;
    int top;
    Timer _timer;
    const int UPDATE_TIME_MS = 1000;//Updates every second
    System.DateTime _startTime;
    System.TimeSpan _elapsedTime;


    void  StartMonitoringTime()
    {
        top = 100;
        _timer = new System.Timers.Timer(UPDATE_TIME_MS);
        _startTime = System.DateTime.Now;
        _timer.Start();
    }
    void Start()
    {
        StartCoroutine(Intro());

    }
  
    IEnumerator Intro()
    {
        GetComponent<AudioSource>().Play();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";

        yield return new WaitForSeconds(5);
        img.enabled = false;

        levelT.text = "Level: ";
        level = 1;

        levelText.text = level.ToString(); ;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartMonitoringTime();
        Enemys();
    }

    IEnumerator nextL()
    {
      

        yield return new WaitForSeconds(5);
        //img.enabled = false;


       

        //enemyTol = GameObject.FindGameObjectsWithTag("EnemyN");

    }


    void Enemys()
    {
        int id = 0; 

        Vector3 spawnPosition;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (j % 2 != 0)
                {
                    if (i != 5)
                    {
                        spawnPosition = new Vector3(-5 + i * 2.0F + 1F, 0, 12 - j * 1.2F);
                        enemy.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(enemy, spawnPosition, spawnRotation);
                        /*enemy.GetComponent<MeshFilter>().mesh = mesh.GetComponent<MeshFilter>().sharedMesh;
                        enemy.GetComponent<MeshRenderer>().materials = mesh.GetComponent<MeshRenderer>().materials;
                        enemy.GetComponent<MeshCollider>().sharedMesh = mesh.GetComponent<MeshFilter>().sharedMesh;
                        */
                    id++;

                    }
                }
                else
                {
                    spawnPosition = new Vector3(-5 + i * 2.0F, 0, 12 - j * 1.2F);
                    enemy.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(enemy, spawnPosition, spawnRotation);
                    id++;
                }

            }

        }

    }


    void Update()
    {
        enemyTol = GameObject.FindGameObjectsWithTag("EnemyN");
       
        if (level >0)
        {
            _elapsedTime = System.DateTime.Now - _startTime;
            timeText.text = "Time: " + _elapsedTime.ToString(@"hh\:mm\:ss");

            if (enemyTol.Length == 0)
            {
                Win();
                //pase d nivel
                //velocidad mas rapida
                StartCoroutine(nextL());

                GetComponent<AudioSource>().Play();
                gameOver = false;
                restart = false;
                restartText.text = "";
                gameOverText.text = "";

                level += 1;
                levelText.text = level.ToString();
                if (!AudioClips[0].isPlaying)
                {
                    AudioClips[0].Play();
                }
                UpdateScore();
                StartCoroutine(SpawnWaves());
                //StartMonitoringTime();
                Enemys();

            }
            
             /*   if (score % top == 0 && score > 0)
                {
                    top += 100;
                    level += 1;
                    levelText.text = level.ToString();
                    if (!AudioClips[0].isPlaying)
                    {
                        AudioClips[0].Play();
                    }
                }
                */
            
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
       

        //Enemy enem = enemyTotal[Random.Range(0, enemyTotal.Length)];
        //enem.enemy.GetComponent<Rigidbody>().velocity = transform.forward * -2;
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {

            GameObject enem = enemyTol[Random.Range(0, enemyTol.Length)];
          
            if (enem != null)
            {
                enem.GetComponent<Done_Mover>().moveT = true;
                //enem.enemy.GetComponent<Done_WeaponController>().moveT = true;

            }
            /*
              for (int i = 0; i < hazardCount; i++)
              {
                  GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                  Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                  Quaternion spawnRotation = Quaternion.identity;
                  Instantiate(hazard, spawnPosition, spawnRotation);
                  yield return new WaitForSeconds(spawnWait);
              }*/

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
             
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
    public void Win()
    {
        gameOverText.text = "Winner !!!";
        gameOver = true;
    }
}

