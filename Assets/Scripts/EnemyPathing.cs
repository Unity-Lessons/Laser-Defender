using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;                          // EnemyPathing script takes in a WaveConfig scriptable object - contains all info about a wave
    List<Transform> waypoints;                      // waypoints of a wave given from WaveConfig using GetWaypoints()
    int waypointIndex = 0;

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        this.transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
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
