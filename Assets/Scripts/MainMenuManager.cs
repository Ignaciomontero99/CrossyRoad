using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text recordText;

    private int selectedDifficulty = 1; // 0 = fácil, 1 = normal, 2 = difícil

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    void Start()
    {
        // Cargar récord
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        recordText.text = "Récord: " + highScore;

        // Cargar dificultad previa
        selectedDifficulty = PlayerPrefs.GetInt("Difficulty", 1);
        UpdateDifficultyButtons();
    }

    public void PlayGame()
    {
        Debug.Log("Botón JUGAR pulsado. Cargando escena de juego...");
        PlayerPrefs.SetInt("Difficulty", selectedDifficulty);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game"); 
    }

    public void SelectDifficulty(int difficulty)
    {
        selectedDifficulty = difficulty;
        Debug.Log("Dificultad seleccionada: " + DifficultyName(difficulty));
        UpdateDifficultyButtons();
    }

    void UpdateDifficultyButtons()
    {
        easyButton.interactable = selectedDifficulty != 0;
        normalButton.interactable = selectedDifficulty != 1;
        hardButton.interactable = selectedDifficulty != 2;
    }

    string DifficultyName(int value)
    {
        switch (value)
        {
            case 0: return "Fácil";
            case 1: return "Normal";
            case 2: return "Difícil";
            default: return "Desconocida";
        }
    }
}
