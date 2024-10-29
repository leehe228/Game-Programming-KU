using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    GameObject gameManager;
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

    public Vector3 runPoint;

    public int hp = 10;

    public bool isEmbarked = false;

    public enum EnemyState 
    {
        GoToNearestPillar, AttackNearestPillar, ChasePlayer, ShootToPlayer, Run
    }
    public EnemyState currentState;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        runPoint = Vector3.zero;

        if (boatMesh == null)
        {
            boatMesh = transform.parent.Find("Boat").gameObject;
        }
    }

    private void Start()
    {
        pillarTransforms = GameObject.FindGameObjectsWithTag("Pillar").Select(p => p.transform).ToArray();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.GoToNearestPillar:
                GoToNearestPillar();
                break;
            case EnemyState.AttackNearestPillar:
                AttackNearestPillar();
                break;
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            case EnemyState.ShootToPlayer:
                ShootToPlayer();
                break;
            case EnemyState.Run:
                Run();
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

    void GoToNearestPillar() 
    {
        // Debug.Log("GoToNearestPillar");
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
            currentState = EnemyState.AttackNearestPillar;
        }
    }

    void AttackNearestPillar() 
    {
        // Debug.Log("AttackNearestPillar");
        FindNearestPillar();

        // Pillar 이 파괴되면 GoToNearestPillar 상태로 변경
        if (nearestPillarTransform == null)
        {
            currentState = EnemyState.GoToNearestPillar;
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

        // 가장 가까운 pillar 와 거리가 baseAttackDistance 보다 멀어지면 GoToNearestPillar 상태로 변경
        float distanceToBase = Vector3.Distance(transform.position, nearestPillarTransform.position);
        if (distanceToBase > baseAttackDistance)
        {
            currentState = EnemyState.GoToNearestPillar;
            return;
        }

        LookTo(nearestPillarTransform.position);
        ShootBullet();
    }

    void ChasePlayer() 
    {
        Debug.Log("ChasePlayer");
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToNearestPillar;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.ShootToPlayer;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, sightSensor.detectedObject.transform.position, 0.1f);
    }

    void ShootToPlayer() 
    {
        // Debug.Log("ShootToPlayer");
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToNearestPillar;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        ShootBullet();

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

    void ShootBullet()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            
            // Debug.Log("Shoot");

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

    void DisembarkShip()
    {
        isEmbarked = true;
        // 배를 안보이게
        if (boatMesh != null)
        {
            boatMesh.SetActive(false);
        }
    }

    void Embark()
    {
        isEmbarked = false;
        // 배를 보이게
        if (boatMesh != null)
        {
            boatMesh.SetActive(true);
        }
    }

    void Run()
    {
        // X, Z 축 현재 위치 기준 +- 20 ~ 30 범위 내 랜덤한 지점으로 이동
        if (runPoint == Vector3.zero)
        {
            float randomX = Random.Range(10f, 20f) * (Random.Range(0, 2) == 0 ? 1 : -1);
            float randomZ = Random.Range(10f, 20f) * (Random.Range(0, 2) == 0 ? 1 : -1);

            float runX = gameObject.transform.position.x + randomX;
            float runZ = gameObject.transform.position.z + randomZ;

            runX = Mathf.Clamp(runX, 209.5f, 282.7f);
            runZ = Mathf.Clamp(runZ, 326.5f, 353.6f);

            runPoint = new Vector3(runX, transform.parent.position.y, runZ);

            LookTo(runPoint);
        }

        Vector3 targetPosition = new Vector3(runPoint.x, transform.parent.position.y, runPoint.z);
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, 0.5f);

        if (Vector3.Distance(transform.parent.position, runPoint) < 3f)
        {
            currentState = EnemyState.GoToNearestPillar;
            runPoint = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyGoal") && !isEmbarked)
        {
            DisembarkShip();
        }

        /* else if (other.CompareTag("EnemyGoal") && isEmbarked)
        {
            Embark();
        }*/

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("hit");
            Destroy(other.gameObject);

            if (hp > 0)
            {
                hp--;
            } else {
                Destroy(gameObject.transform.parent.gameObject);
                gameManager.GetComponent<GameManager>().AddKillCount();
            }

            if (Random.Range(0, 10) < (10 - hp) && currentState != EnemyState.Run && isEmbarked)
            {
                Debug.Log("Run");
                currentState = EnemyState.Run;
            }
        }
    }
}
