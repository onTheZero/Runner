using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI muteTMP;
    [SerializeField] private TextMeshProUGUI pauseTMP;
    [SerializeField] private TextMeshProUGUI highScoreTMP;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private AudioListener audioListener;

    [Header("Gameplay")]
    [SerializeField] private float killFloor;
    [SerializeField] private Transform playerObject;

    private int score;
    private int highScore;
    private int muteSetting;

    // Start is called before the first frame update
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        muteSetting = PlayerPrefs.GetInt("muteSetting", 0);

        if (muteSetting == 1) MuteUnmute();
        highScoreTMP.text = "High Score: " + highScore;

        highScoreTMP.transform.parent.gameObject.SetActive(false);
        scoreTMP.transform.parent.gameObject.SetActive(false);
        startButton.SetActive(true);
        pauseButton.SetActive(false);
        restartButton.SetActive(false);

        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerObject.position.y < killFloor)
        {
            highScoreTMP.transform.parent.gameObject.SetActive(true);
            scoreTMP.transform.parent.gameObject.SetActive(true);
            startButton.SetActive(false);
            pauseButton.SetActive(false);
            restartButton.SetActive(true);
        }
    }

    public void Reload()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void StartGame()
    {
        highScoreTMP.transform.parent.gameObject.SetActive(true);
        scoreTMP.transform.parent.gameObject.SetActive(true);
        startButton.SetActive(false);
        pauseButton.SetActive(true);
        restartButton.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0.0f) 
        {
            pauseTMP.text = "RESUME";

            highScoreTMP.transform.parent.gameObject.SetActive(true);
            scoreTMP.transform.parent.gameObject.SetActive(true);
            startButton.SetActive(false);
            pauseButton.SetActive(true);
            restartButton.SetActive(false);

            Time.timeScale = 0.0f;
        }
        else
        {
            pauseTMP.text = "PAUSE";

            highScoreTMP.transform.parent.gameObject.SetActive(true);
            scoreTMP.transform.parent.gameObject.SetActive(true);
            startButton.SetActive(false);
            pauseButton.SetActive(true);
            restartButton.SetActive(false);

            Time.timeScale = 1.0f;

        }
    }

    public void UpdateScore()
    {
        score++;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            highScoreTMP.text = "High Score: " + highScore;
        }

        scoreTMP.text = "Platforms: " + score;
    }

    public void MuteUnmute()
    {
        if (audioListener.isActiveAndEnabled)
        {
            PlayerPrefs.SetInt("muteSetting", 1);

            muteTMP.text = "UNMUTE";
            audioListener.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("muteSetting", 0);

            muteTMP.text = "MUTE";
            audioListener.enabled = true;
        }
    }
}
