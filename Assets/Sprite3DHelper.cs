using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite3DHelper : MonoBehaviour
{
    public bool onUpdate;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)-transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        if (onUpdate) spriteRenderer.sortingOrder = (int)-transform.position.z;
    }
}