using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;
    
    public Transform player;
    public float visionDistance = 8f;
    public float visionAngle = 45f;

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
    }

    void Update()
    {
        PatrolBehavior();
        CheckForPlayer();
    }

    void PatrolBehavior()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, 5f * Time.deltaTime);
        transform.LookAt(targetPoint.position);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void CheckForPlayer()
    {
        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= visionDistance)
        {
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < visionAngle / 2f)
            {
                Debug.Log("Player spotted by guard!");
                // Add your alert/game over logic here later
            }
        }
    }
}