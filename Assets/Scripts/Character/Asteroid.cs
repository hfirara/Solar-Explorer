using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2 moveDirection = new Vector2(1f, -1f); // Arah miring kanan-bawah
    [SerializeField] private Transform destroyPoint;

    private void Update()
    {
        // Gerak ke arah yang ditentukan (miring)
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        // Jika sudah melewati batas bawah layar (Y), hancurkan
        if (transform.position.y < destroyPoint.position.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Player.Instance.TakeDamage(direction);
        }
    }
}
