using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // 앞으로 나아가는 속도
    public float sideSpeed = 2f;        // 좌우로 움직이는 속도
    public float sideMovementFrequency = 0.5f; // 좌우 움직임 빈도

    private Rigidbody rb;
    private float nextSideMoveTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 앞으로 계속 움직임
        Vector3 forwardMovement = transform.forward * moveSpeed;

        // 일정한 시간 간격으로 좌우로 랜덤하게 움직임
        if (Time.time >= nextSideMoveTime)
        {
            float randomDirection = Random.Range(-1f, 1f);  // -1은 왼쪽, 1은 오른쪽
            Vector3 sideMovement = transform.right * randomDirection * sideSpeed;
            rb.velocity = forwardMovement + sideMovement;

            // 다음 좌우 움직임까지의 시간 간격 설정
            nextSideMoveTime = Time.time + sideMovementFrequency;
        }
        else
        {
            rb.velocity = forwardMovement;  // 좌우 움직임이 없는 동안에도 앞으로 계속 이동
        }

        if (transform.position.z > 316) {
            Destroy(gameObject);
        }
    }
}
