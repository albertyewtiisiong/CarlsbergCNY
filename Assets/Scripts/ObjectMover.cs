using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        // Move left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Destroy if off screen (left side)
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}