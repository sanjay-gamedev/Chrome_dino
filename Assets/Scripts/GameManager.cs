using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float IntialGameSpeed = 5f;
    public float GameSpeedIncrease = 0.1f;
    public float GameSpeed { get; private set; }

    public TextMeshProUGUI gameover;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public float score;
    public Button retry;

    private Player player;
    private Spawner spawner;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        GameSpeed = IntialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameover.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        UpdateHiscore();

    }

    public void GameOver()
    {
        GameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameover.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        UpdateHiscore();
    }

    private void Update()
    {
        GameSpeed += GameSpeedIncrease * Time.deltaTime;
        score += GameSpeed * Time.deltaTime;
        ScoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }
    
        private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        HighScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
