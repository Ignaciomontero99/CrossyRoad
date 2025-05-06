using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Vector3 direction = Vector3.left;
    public float speed = 4f;
    public float destroyDistance = 10f;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (player != null)
        {
            float distanceZ = transform.position.z - player.position.z;

            // Si está demasiado atrás en Z, destruir
            if (distanceZ < -destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
