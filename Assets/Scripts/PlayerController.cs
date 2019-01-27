using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movementLocation;
    private bool usingStairs = false;
    private Vector3 nextStairPoint;
    private Vector3 offset;
    private int collisionBoundsState = 0;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (Input.GetMouseButtonDown(0) && !LeanTween.isTweening(gameObject))
        {
            if (hit.collider && clearOfBounds(hit.point))
            {
                if (hit.collider.tag == "Stairs")
                {
                    Debug.Log("Stairs");
                    usingStairs = true;
                    GameObject bottom = hit.collider.transform.Find("Bottom").gameObject;
                    GameObject top = hit.collider.transform.Find("Top").gameObject;
                    if (Mathf.Abs(transform.position.y - bottom.transform.position.y) < Mathf.Abs(transform.position.y - top.transform.position.y))
                    {
                        Vector3 offset = transform.position - bottom.transform.position;
                        offset.z = 0f;
                        offset.x = 0f;
                        movementLocation = bottom.transform.position + offset;
                        nextStairPoint = top.transform.position + offset;
                    }
                    else
                    {
                        Vector3 offset = transform.position - top.transform.position;
                        offset.z = 0f;
                        offset.x = 0f;
                        movementLocation = top.transform.position + offset;
                        nextStairPoint = bottom.transform.position + offset;
                    }
                    LeanTween.move(gameObject, movementLocation, .5f);
                }
                else
                {
                    Vector3 hitPoint = hit.point;
                    hitPoint.z = transform.position.z;
                    hitPoint.y = transform.position.y;
                    LeanTween.move(gameObject, hitPoint, 1f);
                }
            }
        }

        //Stairs code stuffs
        if (usingStairs && !LeanTween.isTweening(gameObject))
        {
            LeanTween.move(gameObject, nextStairPoint, .8f);
            nextStairPoint = Vector3.zero;
            usingStairs = false;
        }
    }

    private bool clearOfBounds(Vector3 point)
    {
        switch (collisionBoundsState)
        {
            default:
                return true;
            //Bounds left
            case -1:
                if (point.x < transform.position.x)
                    return false;
                else return true;
            //Bounds right
            case 1:
                if (point.x > transform.position.x)
                    return false;
                else return true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Bounds")
        {
            LeanTween.cancel(gameObject);
            if (col.transform.position.x < transform.position.x) collisionBoundsState = -1;
            else collisionBoundsState = 1;
        }
        Debug.Log("Collide");
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Bounds")
        {
            collisionBoundsState = 0;
        }
    }
}