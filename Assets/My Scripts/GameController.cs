using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject restartButton;

    [Header("Gameplay")]
    [SerializeField] private float killFloor;
    [SerializeField] private Transform playerObject;

    private int score;

    // Start is called before the first frame update
    private void Start()
    {
        scoreTMP.gameObject.SetActive(false);
        PauseGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerObject.position.y < killFloor)
        {
            scoreTMP.gameObject.SetActive(true);
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
        scoreTMP.gameObject.SetActive(true);
        startButton.SetActive(false);
        pauseButton.SetActive(true);
        restartButton.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        startButton.SetActive(true);
        pauseButton.SetActive(false);
        restartButton.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void UpdateScore()
    {
        score++;
        scoreTMP.text = "Platforms: " + score;
    }
}
