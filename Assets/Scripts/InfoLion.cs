using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;

public class InfoLion : MonoBehaviour
{
    public int nLion_Hp;
    public int nLion_AttackDamage;
    public bool isHittable = true;
    public bool isFastAttackable = true;
    public bool isWaliking = false;
    public bool isEscape = false;
    public bool isDodge = false;
    public bool isStartLionAi = false;
    public bool isDead = false;
    public bool isHit = false;
    private bool once = true;

    public GameObject objSword;
    public GameObject objShield;
    public GameObject objPlayer;
    public GameObject objVictoryCanvas;

    public TimelineController timelineController;

    public GameObject objEscapePoint;
    public Vector3 objDodgePoint;

    public Canvas canvas_PlayerHp;
    public Slider slider_Hp;

    public AnimationControl_Lion animationControl_Lion;
    public NavMeshAgent agent;

    public Transform transform_Player;

    public LayerMask whatIsGround, whatIsPlayer;

    // 정찰하기
    public Vector3 vector_WalkPoint;
    bool walkPointSet;
    public float fWalkPointRange;

    // 공격
    public float fTimeBetweenAttacks;
    bool alreadyAttacked;

    // 상태 확인
    public float fSightRange, fAttackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Vector3 v3InitPos;

    private void Start()
    {
        transform_Player = objPlayer.transform;
        agent = GetComponent<NavMeshAgent>();

        v3InitPos = gameObject.transform.position;
    }

    void Update()
    {
        slider_Hp.transform.LookAt(objPlayer.transform);

        if (isStartLionAi)
        {
            if (!isDead)
            {
                // 시야범위와 공격범위를 체크
                playerInSightRange = Physics.CheckSphere(transform.position, fSightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, fAttackRange, whatIsPlayer);

                if (!isHit)
                {
                    if (!playerInSightRange && !playerInAttackRange)
                    {
                        Patroling();
                    }

                    if (playerInSightRange && !playerInAttackRange)
                    {
                        ChasePlayer();
                    }

                    if (playerInSightRange && playerInAttackRange)
                    {
                        AttackPlayer();
                    }
                }
            }
            else
            {
                objVictoryCanvas.SetActive(true);
                timelineController.objPlayer.GetComponent<CharacterController>().enabled = false;
                Invoke(nameof(GoToLobby), 3f);
                isStartLionAi = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objSword)
        {
            Debug.Log("플레이어 공격 확인");
            Hit();
        }

        if (other.gameObject == objEscapePoint)
        {
            isEscape = false;
            isHit = false;
            objEscapePoint.SetActive(false);
            animationControl_Lion.StopRunning();
            animationControl_Lion.Walking();
            isWaliking = true;
            agent.speed = 3.5f;
        }
    }

    public void GoToLobby()
    {
        isHittable = true;
        isFastAttackable = true;
        isWaliking = false;
        isEscape = false;
        isDodge = false;
        isStartLionAi = false;
        isDead = false;
        isHit = false;
        once = true;
        nLion_Hp = 100;
        slider_Hp.value = nLion_Hp;

        gameObject.transform.position = v3InitPos;

        timelineController.EndLionFight();
        objVictoryCanvas.SetActive(false);
        gameObject.SetActive(false);
        objSword.SetActive(false);
        objShield.SetActive(false);
        canvas_PlayerHp.gameObject.SetActive(false);
    }

    /// <summary>
    /// 플레이어가 사자를 공격했을 때 실행
    /// </summary>
    private void Hit()
    {
        if (isHittable)
        {
            isHittable = false;
            StartCoroutine(HitDelay());
            isHit = true;

            ScSoundManager.gSoundManager.audiosEffects[4].Play();

            int nRandom = Random.Range(0, 100);
            if (nRandom >= 50)
            {
                SetDodgePoint();
            }
            else
            {
                slider_Hp.value -= 18;

                if (slider_Hp.value <= 0)
                {
                    isDead = true;
                    animationControl_Lion.Dead();
                }
                else
                {
                    objEscapePoint.SetActive(true);
                    EscapePlayer();
                }
            }
        }
    }

    /// <summary>
    /// 사자가 회피 할 때의 좌표값을 설정
    /// </summary>
    private void SetDodgePoint()
    {
        objDodgePoint = transform.position;

        Vector3 dir = (transform.position - transform_Player.position).normalized;
        objDodgePoint += dir * 5f;
        objDodgePoint = new Vector3(objDodgePoint.x, 0, objDodgePoint.z);

        if (Physics.Raycast(objDodgePoint, Vector3.down, 5f, whatIsGround))
        {
            animationControl_Lion.Dodge();
            transform.DOMove(objDodgePoint, 0.5f).SetEase(Ease.InSine);
            Invoke(nameof(ResetHitable), 0.5f);
        }
    }

    private void ResetHitable()
    {
        isHit = false;
    }

    /// <summary>
    /// 사자가 범위안에 랜덤 좌표로 이동
    /// </summary>
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            if (!isWaliking)
            {
                animationControl_Lion.StopRunning();
                animationControl_Lion.Walking();
                isWaliking = true;
            }

            agent.SetDestination(vector_WalkPoint);
        }

        Vector3 vector_DistanceToWalkPoint = transform.position - vector_WalkPoint;
        // 목표지점에 도달
        if (vector_DistanceToWalkPoint.magnitude < 1.0f)
        {
            walkPointSet = false;
        }
    }

    /// <summary>
    /// 이동할 랜덤좌표를 구하는 메서드
    /// </summary>
    private void SearchWalkPoint()
    {
        // 범위내 무작위 좌표값을 계산
        float fRandomZ = Random.Range(-fWalkPointRange, fWalkPointRange);
        float fRandomX = Random.Range(-fWalkPointRange, fWalkPointRange);

        vector_WalkPoint = new Vector3(transform.position.x + fRandomX, transform.position.y, transform.position.z + fRandomZ);

        if (Physics.Raycast(vector_WalkPoint, -transform.up, 5f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    /// <summary>
    /// 사자의 추적 범위 안에있는 플레이어를 추적
    /// </summary>
    private void ChasePlayer()
    {
        if (!isWaliking)
        {
            animationControl_Lion.StopRunning();
            animationControl_Lion.Walking();
            isWaliking = true;
        }

        if (isFastAttackable)
        {
            isFastAttackable = false;
            Invoke(nameof(FastAttackDelay), 5.0f);

            animationControl_Lion.Running();
            isWaliking = true;

            agent.speed = 7.5f;
        }

        agent.SetDestination(transform_Player.position);
    }

    private void FastAttackDelay()
    {
        isFastAttackable = true;
    }

    /// <summary>
    /// 공격범위 안에 있는 플레이어를 공격
    /// </summary>
    private void AttackPlayer()
    {
        // 공격시에는 잠시 멈춘다
        agent.speed = 3.5f;
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            int nRandNum = Random.Range(0, 100);
            if (nRandNum >= 30)
            {
                NormalAttack();
            }
            else
            {
                BackStep();
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), fTimeBetweenAttacks);
        }
    }

    private void NormalAttack()
    {
        animationControl_Lion.StopRunning();
        animationControl_Lion.StopWalking();
        animationControl_Lion.ClawsAttack();
        isWaliking = false;

        ScSoundManager.gSoundManager.audiosEffects[1].Play();
    }

    private void BackStep()
    {
        animationControl_Lion.StopRunning();
        animationControl_Lion.StopWalking();
        animationControl_Lion.Dodge();
        isWaliking = false;

        objDodgePoint = transform.position;

        Vector3 dir = (transform.position - transform_Player.position).normalized;
        objDodgePoint += dir * 5f;
        objDodgePoint = new Vector3(objDodgePoint.x, 0, objDodgePoint.z);

        transform.DOMove(objDodgePoint, 0.5f).SetEase(Ease.InSine);

        ScSoundManager.gSoundManager.audiosEffects[10].Play();
    }

    /// <summary>
    /// 사자의 공격 딜레이
    /// </summary>
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    /// <summary>
    /// 플레이어에게 피격당했을 때 도망
    /// </summary>
    private void EscapePlayer()
    {
        objEscapePoint.transform.position = transform.position;

        Vector3 dir = (transform.position - transform_Player.position).normalized;
        objEscapePoint.transform.position += dir * 17.5f;
        objEscapePoint.transform.position = new Vector3(objEscapePoint.transform.position.x, 0, objEscapePoint.transform.position.z);
        if (Physics.Raycast(objEscapePoint.transform.position, -transform.up, 2f, whatIsGround))
        {
            isEscape = true;
            agent.speed = 7.5f;
            animationControl_Lion.OnHit();


            if (isWaliking)
            {
                animationControl_Lion.StopWalking();
                animationControl_Lion.Running();
                isWaliking = false;
            }
            else
            {
                animationControl_Lion.Running();
            }

            agent.SetDestination(objEscapePoint.transform.position);
        }
    }

    /// <summary>
    /// 사자의 피격 딜레이 시간을 재는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(1.0f);
        isHittable = true;
    }

    public void StartLionAi()
    {
        isStartLionAi = true;
        canvas_PlayerHp.gameObject.SetActive(true);
        canvas_PlayerHp.transform.GetChild(0).GetComponent<Slider>().value = 100;
        slider_Hp.value = 100;
    }
}
