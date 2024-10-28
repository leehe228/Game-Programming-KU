using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Sight sightSensor;
    public Transform[] pillarTransforms;
    public Transform nearestPillarTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;

    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;

    public GameObject boatMesh;
    public Transform firePoint;

    public enum EnemyState 
    {
        GoToBase, AttackBase, ChasePlayer, AttackPlayer
    }
    public EnemyState currentState;

    private void Awake()
    {
        if (boatMesh == null)
        {
            boatMesh = transform.parent.Find("Boat").gameObject;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.GoToBase:
                GoToBase();
                break;
            case EnemyState.AttackBase:
                AttackBase();
                break;
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            case EnemyState.AttackPlayer:
                AttackPlayer();
                break;
        }
    }

    void FindNearestPillar() 
    {
        float minDistance = float.MaxValue;
        Transform nearestPillar = null;

        for (int i = 0; i < pillarTransforms.Length; i++)
        {
            if (pillarTransforms[i] == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, pillarTransforms[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPillar = pillarTransforms[i];
            }
        }

        nearestPillarTransform = nearestPillar;
    }

    void GoToBase() 
    {
        Debug.Log("GoToBase");
        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }

        // Find nearest pillar
        FindNearestPillar();

        // Move to nearest pillar
        if (nearestPillarTransform == null)
        {
            return;
        }
        LookTo(nearestPillarTransform.position);
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, nearestPillarTransform.position, 0.1f);

        float distanceToBase = Vector3.Distance(transform.position, nearestPillarTransform.position);

        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
    }

    void AttackBase() 
    {
        Debug.Log("AttackBase");
        FindNearestPillar();

        // Pillar 이 파괴되면 GoToBase 상태로 변경
        if (nearestPillarTransform == null)
        {
            currentState = EnemyState.AttackPlayer;
            return;
        }

        // Pillar보다 가까운 player 가 있으면 ChasePlayer 상태로 변경
        // Pillar와 player 거리 비교
        if (sightSensor.detectedObject != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
            float distanceToPillar = Vector3.Distance(transform.position, nearestPillarTransform.position);

            if (distanceToPlayer < distanceToPillar)
            {
                currentState = EnemyState.ChasePlayer;
                return;
            }
        }

        // 가장 가까운 pillar 와 거리가 baseAttackDistance 보다 멀어지면 GoToBase 상태로 변경
        float distanceToBase = Vector3.Distance(transform.position, nearestPillarTransform.position);
        if (distanceToBase > baseAttackDistance)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(nearestPillarTransform.position);
        Shoot();
    }

    void ChasePlayer() 
    {
        Debug.Log("ChasePlayer");
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, sightSensor.detectedObject.transform.position, 0.1f);
    }

    void AttackPlayer() 
    {
        Debug.Log("AttackPlayer");
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            
            Debug.Log("Shoot");

            // 총알을 앞으로 가도록 add force
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
            Destroy(bullet, 5f);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }

    void Disembark()
    {
        // 배를 안보이게
        if (boatMesh != null)
        {
            boatMesh.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyGoal"))
        {
            Disembark();
        }
    }
}
