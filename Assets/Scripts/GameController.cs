using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private int hazardStart = 0;

    public AudioClip win;
    public AudioClip lose;
    public AudioClip spaceClip;
    public AudioSource audioSource;

    public Text scoreText;
    public Text gameOverText;
    public Text RestartText;
    
    private int score;
    private bool restart;
    private bool gameOver;
    public bool winGame;

    public ParticleSystem psIn;
    public ParticleSystem psOut;

    void Start()
    {
        audioSource.clip = spaceClip;
        audioSource.Play();
        winGame = false;
        gameOver = false;
        restart = false;
        RestartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        //StartWaves();

    }

    public void StartWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    public void AlienMode()
    {
        hazardStart = hazards.Length - 1;
        hazardCount = 5;
        waveWait = 2;
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                SceneManager.LoadScene("Main");
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(hazardStart, hazards.Length)]; 
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'L' for Restart";
                restart = true;
                break;
            }
        }
        
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        if (score >= 100)
        {
            gameOverText.text = "You win! Game created by Aaron Hobgood!";
            gameOver = true;
            restart = true;
            WinGame();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over! By Aaron Hobgood";
        gameOver = true;
        audioSource.clip = lose;
        audioSource.Play();

    }

    void WinGame()
    {
        winGame = true;
        var main1 = psIn.main;
        main1.simulationSpeed = 25.0f;
        var main2 = psOut.main;
        main2.simulationSpeed = 15.0f;
        audioSource.clip = win;
        audioSource.Play();
    }

}
