using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameover;
    [SerializeField] private Button buttonLoadScene;
    private void Start()
    {
        TimeGame(1);
        buttonLoadScene.onClick.AddListener(LoadScene);
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }
    private void GameOver()
    {
        gameover.SetActive(true);
        TimeGame(0);
    }

    private void TimeGame(float time)
    {
        Time.timeScale = time;
    }

    private void OnEnable()
    {
        PlayerController.eventGameOver += GameOver;
    }
    private void OnDisable()
    {
        PlayerController.eventGameOver -= GameOver;
    }
}
