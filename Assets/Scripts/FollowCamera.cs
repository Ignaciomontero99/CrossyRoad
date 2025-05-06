using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public float height = 7f;
    public float distanceBehind = 6f;
    public float smoothSpeed = 0.2f;
    public float safeZone = 3f;

    private float maxZ; // Cámara nunca se moverá hacia atrás

    void Start()
    {
        if (player != null)
        {
            maxZ = player.position.z;
        }
    }

    void LateUpdate()
    {
        if (!player) return;

        // Solo avanzar la cámara si el jugador avanza más allá de la zona segura
        if (player.position.z > maxZ + safeZone)
        {
            maxZ = player.position.z - safeZone;
        }

        // Cámara colocada detrás y encima del jugador
        Vector3 desiredPosition = new Vector3(
            player.position.x, 
            player.position.y + height, 
            maxZ - distanceBehind
        );

        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPos;

        // Mirar al jugador (centrado horizontal y en dirección de avance)
        transform.LookAt(new Vector3(player.position.x, player.position.y, maxZ));
    }
}
