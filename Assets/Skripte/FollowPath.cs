using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

	public Transform Point1;
    public Transform Point2;
    public bool moveToPoint1;
    public float Speed=1;
	public float MaxDistanceToGoal = 0.1f;
    private SpriteRenderer renderer;

	public void Start()
	{
        moveToPoint1 = true;
        renderer = GetComponent<SpriteRenderer>();
	}

    public void Update(){

        if (moveToPoint1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point1.transform.position, Time.deltaTime * Speed);
            var distanceSquared = (transform.position - Point1.transform.position).sqrMagnitude;
            if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            {
                moveToPoint1 = !moveToPoint1;
                renderer.flipX = !renderer.flipX;
            }
        }
        else if (!moveToPoint1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point2.transform.position, Time.deltaTime * Speed);
            var distanceSquared = (transform.position - Point2.transform.position).sqrMagnitude;
            if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            {
                moveToPoint1 = !moveToPoint1;
                renderer.flipX = !renderer.flipX;
            }
        }
    }
}

