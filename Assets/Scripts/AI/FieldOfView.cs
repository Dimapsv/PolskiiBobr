using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask ObstacleMask;
    public bool canSeePlayer;

    public bool bobrIsHidden;

    public bool isForbiddenBoy;

    public GameObject viweIndicatorUI;

    public AI_PATROL ai;

    private void OnEnable()
    {
        ThirdPersonController.OnBobrHiddenChanged.AddListener(CheckBobr);
    }

    private void OnDisable()
    {
        ThirdPersonController.OnBobrHiddenChanged.RemoveListener(CheckBobr);
    }

    private void Start()
    {
        viweIndicatorUI.SetActive(false);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        ai = gameObject.GetComponent<AI_PATROL>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FixedUpdate()
    {
        if (canSeePlayer == true)
        {
            ai.AI_Enemy = AI_PATROL.AI_State.Chase;
            
        }
        else if (canSeePlayer == false && isForbiddenBoy)
        {
            ai.AI_Enemy = AI_PATROL.AI_State.Stay;
        }
        else if (canSeePlayer == false)
        {
            ai.AI_Enemy = AI_PATROL.AI_State.Patrol;
        } 

        
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask) && !bobrIsHidden)
                {
                    viweIndicatorUI.SetActive(true);
                    canSeePlayer = true;
                }

                else
                {
                    canSeePlayer = false;
                    viweIndicatorUI.SetActive(false);
                }

            }
            else
            {
                canSeePlayer = false;
                viweIndicatorUI.SetActive(false);
            }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            viweIndicatorUI.SetActive(false);
        }
            
    }

    public void CheckBobr(bool isHidden)
    {
        bobrIsHidden = isHidden;
    }
}
