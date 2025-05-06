using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 5f;
    public int rowWidth = 7; // Debe coincidir con TerrainGenerator

    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving) return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // 🚫 No retroceder si está en Z = 0
            if (Mathf.RoundToInt(transform.position.z) <= 0) return;
            direction = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // 🚫 Límite izquierdo
            if (Mathf.RoundToInt(transform.position.x) <= -rowWidth / 2) return;
            direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // 🚫 Límite derecho
            if (Mathf.RoundToInt(transform.position.x) >= rowWidth / 2) return;
            direction = Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            targetPosition = transform.position + direction * moveDistance;
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;
        while ((target - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        isMoving = false;
    }
}
