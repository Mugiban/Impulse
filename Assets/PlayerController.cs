
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float impulseForce = 300f;

    [SerializeField]
    private bool dragging;
    [SerializeField] private bool impulse;

    private Vector3 startDragPosition;
    private Vector3 endDragPosition;

    private Vector2 direction;
    private bool endDrag;

    private Vector2 worldPosition;

    [SerializeField]
    private float velocityMultiplier = 1;

    Rigidbody2D rb;

    private void OnEnable()
    {
        //Lean.Touch.LeanTouch.OnFingerDown += OnMouseDown;
    }

    private void OnDisable()
    {
        //Lean.Touch.LeanTouch.OnFingerDown -= OnMouseDown;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0)) {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0)) {
            endDrag = true;
        }
    }

    private void EndDrag()
    {
        dragging = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;
        rb.AddForce(-direction, ForceMode2D.Impulse);
        endDrag = false;
    }

    private void ContinueDrag(Vector2 worldPosition)
    {
        endDragPosition = worldPosition;
        
        direction = endDragPosition - startDragPosition;
        Debug.DrawLine(endDragPosition, startDragPosition);
    }

    private void StartDrag(Vector2 worldPosition)
    {
        dragging = true;
        rb.gravityScale = 0.2f;
        
        startDragPosition = worldPosition;
    }

    void FixedUpdate()
    {
        if(endDrag)
        {
            EndDrag();
        }

        if(dragging)
        {
            rb.velocity *=0.5f;
        }
    }

    public void Explode()
    {
        rb.AddExplosionForce(300f, transform.position, 30f);
    }

}
