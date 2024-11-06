using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;  // Nhân vật hoặc đối tượng cần theo dõi
    public float smoothSpeed = 0.125f;  // Tốc độ mượt
    public Vector3 offset;  // Độ lệch của camera so với đối tượng

    private void Update()
    {
        if (target == null) return;  // Đảm bảo có đối tượng để theo dõi

        // Vị trí mục tiêu + độ lệch
        Vector3 desiredPosition = target.position + offset;

        // Nội suy giữa vị trí hiện tại và vị trí mong muốn để tạo chuyển động mượt
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Đặt vị trí mới cho camera
        transform.position = smoothedPosition;
    }
}
