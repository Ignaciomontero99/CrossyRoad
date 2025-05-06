using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("🚗 Te atropelló un coche");
            FindObjectOfType<GameOverManager>().ShowGameOver();
        }
        else if (other.CompareTag("Trunk"))
        {
            Debug.Log("🪵 Te llevó un buen tronco");
            FindObjectOfType<GameOverManager>().ShowGameOver();
        }
        else if (other.CompareTag("Decoration"))
        {
            Debug.Log("Es decoración, sigue jugando");
        }
    }
}
