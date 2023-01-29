using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public ArrayList routePointList = new ArrayList();
    public int currentRoutePoint = 0;

    public Vector3 currentMovePoint;

    // Start is called before the first frame update
    void Start()
    {
        // search for the movePoint child object
        movePoint = transform.Find("movePoint");
        movePoint.parent = null;

        // debug route points
        Debug.Log("routePointList.Count: " + routePointList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    // setNewMovePoint() is called when the player clicks on the ground
    public void setNewMovePoint(Vector3 newMovePoint)
    {
        movePoint.position = newMovePoint;
    }

    // move
    public bool move()
    {
        // current move point + 1
        currentMovePoint = (Vector3)routePointList[currentRoutePoint];

        currentRoutePoint++;
        // if the current move point is the last one
        if(currentRoutePoint == routePointList.Count)
        {
            // destroy the enemy
            Destroy(gameObject);
            return false;
        }
        else
        {
            Debug.Log("currentMovePoint: " + currentMovePoint);
            // set new move point
            setNewMovePoint(currentMovePoint);
            return true;
        }
    }
}
