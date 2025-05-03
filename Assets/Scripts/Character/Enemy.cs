using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*[Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Player.Instance.TakeDamage(direction);
        }
    }*/

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolDistance = 5f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToEnd = true;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.right * patrolDistance;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingToEnd = !movingToEnd;
            targetPosition = startPosition + (movingToEnd ? Vector3.right : Vector3.left) * patrolDistance;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
