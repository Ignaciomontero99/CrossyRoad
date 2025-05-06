using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs; // 0 = Grass, 1 = Road, 2 = River
    public int numberOfRows = 50;
    public int rowWidth = 7;

    public GameObject[] carPrefabs;      // Prefabs de coches
    public GameObject[] trunkPrefabs;    // Prefabs de troncos

    public GameObject[] grassDecorations;
    public GameObject[] riverDecorations;

    public Transform player;
    public int tilesAhead = 15;
    private int lastZ = 0;

    void Start()
    {
        for (int z = 0; z < numberOfRows; z++)
        {
            GenerateRow(z);
            lastZ = z;
        }
    }

    void Update()
    {
        int playerZ = Mathf.FloorToInt(player.position.z);

        while (lastZ < playerZ + tilesAhead)
        {
            lastZ++;
            GenerateRow(lastZ);
        }
    }

    void GenerateRow(int z)
    {
        int tileIndex = (z == 0) ? 0 : Random.Range(0, tilePrefabs.Length); // Grass fijo en z = 0

        for (int x = -rowWidth / 2; x <= rowWidth / 2; x++)
        {
            Vector3 position = new Vector3(x, 0, z);
            GameObject tile = Instantiate(tilePrefabs[tileIndex], position, Quaternion.identity, transform);

            if (tileIndex == 0) // Grass
                GenerateTileContent(grassDecorations, position, 2, 0.6f);
            else if (tileIndex == 2) // River
                GenerateTileContent(riverDecorations, position, 1, 0.4f);
        }

        if (tileIndex == 1) AddVehicleSpawner(z);
        if (tileIndex == 2) AddTrunkSpawner(z);
    }

    void GenerateTileContent(GameObject[] prefabs, Vector3 center, int maxCount, float chance)
    {
        if (prefabs.Length == 0) return;

        int num = Random.Range(1, maxCount + 1);

        for (int i = 0; i < num; i++)
        {
            if (Random.value <= chance)
            {
                int index = Random.Range(0, prefabs.Length);
                Vector3 offset = new Vector3(Random.Range(-0.4f, 0.4f), 0f, Random.Range(-0.4f, 0.4f));
                Vector3 spawnPos = new Vector3(center.x + offset.x, 0f, center.z + offset.z);
                Instantiate(prefabs[index], spawnPos, Quaternion.identity, transform);
            }
        }
    }

    void AddVehicleSpawner(int z)
    {
        GameObject spawner = new GameObject($"VehicleSpawner_Z{z}");
        spawner.transform.position = new Vector3(4f, 0f, z); // origen a la derecha

        var vs = spawner.AddComponent<VehicleSpawner>();
        vs.carPrefabs = carPrefabs;
        vs.spawnX = 4f;
        vs.moveDirection = Vector3.left; // derecha a izquierda
        vs.spawnInterval = Random.Range(1.5f, 3f);
    }

    void AddTrunkSpawner(int z)
    {
        GameObject spawner = new GameObject($"TrunkSpawner_Z{z}");
        spawner.transform.position = new Vector3(4f, 0f, z);

        var ls = spawner.AddComponent<VehicleSpawner>();
        ls.carPrefabs = trunkPrefabs;
        ls.spawnX = 4f;
        ls.moveDirection = Vector3.left;
        ls.spawnInterval = Random.Range(2f, 4f);
    }
}
