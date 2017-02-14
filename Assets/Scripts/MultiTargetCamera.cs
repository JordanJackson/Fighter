using UnityEngine;
using System.Collections;

public class MultiTargetCamera : MonoBehaviour
{

    public Transform player1;
    public Transform player2;

    public float minDistance;
    public float maxDistance;

    // z distance multiplier
    public float distanceModifier;

    void Start()
    {
        // set fighter references
        Fighter[] fighters = FindObjectsOfType<Fighter>();
        foreach (Fighter f in fighters)
        {
            if (f.GetPlayerNum() == 1)
            {
                player1 = f.transform;
            }
            if (f.GetPlayerNum() == 2)
            {
                player2 = f.transform;
            }
        }

        if (maxDistance <= minDistance)
        {
            maxDistance = 2 * minDistance;
        }
    }

    void Update()
    {
        // distance between players with multiplier applied
        float distance = Mathf.Abs(player1.position.x - player2.position.x);
        distance *= distanceModifier;
        // central point for camera to focus on
        float midpointX = (player1.position.x + player2.position.x) / 2;
        // set camera position
        this.transform.position = new Vector3(midpointX, this.transform.position.y, -ClampDistance(distance));
    }

    // maintain minimum and maximum distance
    float ClampDistance(float distance)
    {
        if (distance < minDistance)
        {
            return minDistance;
        }
        else if (distance > maxDistance)
        {
            return maxDistance;
        }
        else
        {
            return distance;
        }
    }
}
