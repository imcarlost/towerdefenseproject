using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public ArrayList routePointList = new ArrayList();
    private int currentRoutePoint = 0;
    public int lives = 1;
    public Vector3 currentMovePoint;
    public Animator spriteAnimator;

    // Start is called before the first frame update
    public void init(){
        // Debug.Log("EnemyController::Start");
        movePoint = transform.Find("movePoint");
        movePoint.parent = null;
        // set animator child Sprinte Animator
        spriteAnimator = transform.Find("Sprite").GetComponent<Animator>();


    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            spriteAnimator.SetBool("isMoving", false);
        }else{
            spriteAnimator.SetBool("isMoving", true);
        }
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
        currentMovePoint = (Vector3) routePointList[currentRoutePoint];

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
            // Debug.Log("currentMovePoint: " + currentMovePoint);
            // set new move point
            setNewMovePoint(currentMovePoint);
            return true;
        }
    }
}
