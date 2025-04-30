using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation State")]
    [SerializeField] private float _flipRotationTime = 0.5f;

    [Header("Camera Limits")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    
    [SerializeField] private Vector3 offSet; 

    private Coroutine _turnCoroutine;
    private Player _player;
    private bool _isFacingRight;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<Player>();
        _isFacingRight = _player.IsFacingRight;
    }
    
    private void Update()
    {
        //transform.position = _playerTransform.position + offSet;

        Vector3 targetPosition = _playerTransform.position + offSet;

        // Clamp X agar kamera tidak lewat batas kiri dan kanan
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        transform.position = targetPosition;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if(_isFacingRight)
        {
            return 180f;
        }

        else
        {
            return 0f;
        }
    }
}
