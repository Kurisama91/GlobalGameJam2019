using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusingObject;
    public float followSpeed = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 focusingObjectPos = focusingObject.transform.position;
        focusingObjectPos.z = transform.position.z;
        transform.position = focusingObjectPos;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 focusingObjectPos = focusingObject.transform.position;
        focusingObjectPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, focusingObjectPos, followSpeed * Time.deltaTime);
    }
}