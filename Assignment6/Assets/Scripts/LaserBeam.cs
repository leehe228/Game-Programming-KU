using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LaserBeam : MonoBehaviour
{
    public Transform laserOrigin; // 레이저 시작점인 GameObject (Transform 컴포넌트)
    public GameObject targetObject; // 레이저 끝점으로 설정할 특정 GameObject
    public float maxLaserDistance = 100f; // 최대 레이저 길이
    public float damagePerSecond = 5f; // 초당 데미지
    public float laserDuration = 6f; // 레이저 유지 시간
    public float laserCooldown = 5f; // 레이저 쿨타임
    public Slider laserProgressBar; // 레이저 상태를 표시할 슬라이더

    private LineRenderer lineRenderer;
    private Coroutine damageCoroutine;
    public bool isLaserActive = false;
    private bool isCooldownActive = false;
    private Image progressBarFill;
    public GameManager gameManager;
    public AudioSource laserBeamAudioSource;

    void Start()
    {
        // Line Renderer 초기화
        lineRenderer = GetComponent<LineRenderer>();

        if (laserOrigin == null)
        {
            Debug.LogError("Laser origin not set. Please assign the laserOrigin Transform.");
        }

        if (targetObject != null)
        {
            targetObject.SetActive(false); // 처음에는 targetObject를 비활성화 상태로 설정
        }

        lineRenderer.enabled = false; // 처음에는 레이저 비활성화

        if (laserProgressBar != null)
        {
            laserProgressBar.gameObject.SetActive(false); // 처음에는 Progress Bar 비활성화
            progressBarFill = laserProgressBar.fillRect.GetComponent<Image>();
        }

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isMainGameStarted)
        {
            return;
        }
        
        if (Keyboard.current.eKey.wasPressedThisFrame && !isLaserActive && !isCooldownActive)
        {
            StartCoroutine(ActivateLaser());
        }
    }

    IEnumerator ActivateLaser()
    {
        laserBeamAudioSource.Play(); // 레이저 빔 효과음 재생
        isLaserActive = true;
        lineRenderer.enabled = true;

        if (laserProgressBar != null)
        {
            laserProgressBar.gameObject.SetActive(true);
            laserProgressBar.value = 1f;
            progressBarFill.color = Color.red; // 레이저 활성화 중 빨간색으로 설정
        }

        float elapsedTime = 0f;
        while (elapsedTime < laserDuration)
        {
            RenderLaserBeam();
            elapsedTime += Time.deltaTime;

            if (laserProgressBar != null)
            {
                laserProgressBar.value = 1f - (elapsedTime / laserDuration);
            }

            yield return null;
        }

        lineRenderer.enabled = false;
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        isLaserActive = false;
        targetObject.SetActive(false);
        isCooldownActive = true;
        laserBeamAudioSource.Stop(); // 레이저 빔 효과음 정지

        if (laserProgressBar != null)
        {
            laserProgressBar.value = 0f;
            progressBarFill.color = Color.green; // 쿨다운 중 초록색으로 설정
        }

        float cooldownTime = 0f;
        while (cooldownTime < laserCooldown)
        {
            cooldownTime += Time.deltaTime;

            if (laserProgressBar != null)
            {
                laserProgressBar.value = cooldownTime / laserCooldown;
            }

            yield return null;
        }

        if (laserProgressBar != null)
        {
            laserProgressBar.gameObject.SetActive(false);
        }

        isCooldownActive = false;
    }

    void RenderLaserBeam()
    {
        // Line Renderer 시작점 설정
        lineRenderer.SetPosition(0, laserOrigin.position);

        Vector3 endPoint;
        bool hitDetected = false;

        // Raycast로 충돌 체크
        Ray ray = new Ray(laserOrigin.position, laserOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxLaserDistance))
        {
            // 충돌한 지점이 있으면 끝점을 충돌 지점으로 설정
            endPoint = hit.point;
            hitDetected = true;

            // 충돌한 오브젝트가 "Enemy" 태그를 가진 경우 데미지 주기
            if (hit.collider.CompareTag("Enemy"))
            {
                if (damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(DealDamageOverTime(hit.collider.gameObject));
                }
            }
        }
        else
        {
            // 충돌한 지점이 없으면 최대 길이까지 레이저 확장
            endPoint = laserOrigin.position + laserOrigin.forward * maxLaserDistance;
            hitDetected = false;

            // 레이저가 충돌하지 않으면 데미지 코루틴 중지
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }

        // Line Renderer의 끝점 설정
        lineRenderer.SetPosition(1, endPoint);

        // targetObject의 활성화/비활성화 설정
        if (targetObject != null)
        {
            if (hitDetected)
            {
                targetObject.SetActive(true);
                targetObject.transform.position = endPoint; // targetObject를 충돌 지점으로 이동
            }
            else
            {
                targetObject.SetActive(false);
            }
        }
    }

    IEnumerator DealDamageOverTime(GameObject enemy)
    {
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            while (true)
            {
                enemyComponent.TakeDamage(damagePerSecond);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
