using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 1.5f;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                    transform.position,
                    targetPosition,
                    movementThisFrame);
            if (transform.position == targetPosition)
                waypointIndex++;
        }
        else
            Destroy(gameObject);

    }
}
