using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeft : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Hancurkan object jika sudah jauh ke kiri (opsional)
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
