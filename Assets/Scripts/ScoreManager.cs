using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int highScore = 0;
    public Text highScoreText;

    public Transform player;
    public Text scoreText;
    public Text percentageText;

    public int goalDistance = 100; // Meta final para el 100%

    private int maxZReached = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
    }


    void Update()
    {
        if (player == null) return;

        int currentZ = Mathf.FloorToInt(player.position.z);

        // Si avanza, actualizamos la puntuación
        if (currentZ > maxZReached)
        {
            maxZReached = currentZ;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Distancia: " + maxZReached;

        float progress = Mathf.Clamp01((float)maxZReached / goalDistance);
        percentageText.text = "Progreso: " + Mathf.RoundToInt(progress * 100f) + "%";

        if (maxZReached > highScore)
        {
            highScore = maxZReached;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); // guarda en disco
            UpdateHighScoreUI();
        }
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "Récord: " + highScore;
        }
    }


}
