using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Camera Limits")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float followSmoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (_playerTransform == null) return;

        // Target posisi kamera mengikuti pemain + offset
        Vector3 targetPosition = _playerTransform.position + offSet;

        // Clamp posisi X agar kamera tidak melewati batas
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        // Jaga posisi Y dan Z tetap (atau ikuti jika mau)
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSmoothSpeed);
        transform.position = smoothPosition;
    }
}
