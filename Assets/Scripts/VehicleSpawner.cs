using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float spawnInterval = 2f;
    public float spawnX = -3f;
    public Vector3 moveDirection = Vector3.left;

    private float timer = 0f;

    void Start()
    {
        int dificultad = PlayerPrefs.GetInt("Difficulty", 1);

        float speedMultiplier = 1f;

        switch (dificultad)
        {
            case 0: speedMultiplier = 0.8f; break; // fácil
            case 1: speedMultiplier = 1.0f; break; // normal
            case 2: speedMultiplier = 1.4f; break; // difícil
        }

        spawnInterval /= speedMultiplier; // aparecen más rápido en difícil
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCar();
            timer = 0f;
        }
    }

    void SpawnCar()
    {
        if (carPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, carPrefabs.Length);
        GameObject selectedPrefab = carPrefabs[randomIndex];

        // Posición alineada al suelo
        Vector3 spawnPos = new Vector3(spawnX, 0f, transform.position.z);

        // 👉 Rotación: solo en el eje Y, sin inclinar el objeto
        Quaternion rotationY = Quaternion.Euler(0, moveDirection == Vector3.right ? 90f : -90f, 0);

        GameObject car = Instantiate(selectedPrefab, spawnPos, rotationY);

        // Asignar dirección de movimiento
        MovingObject moving = car.GetComponent<MovingObject>();
        if (moving != null)
        {
            moving.direction = moveDirection;
        }
    }


}
