
using System;
using System.Collections;
using System.Collections.Generic;
using ID;
using ID.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float impulseForce { get; private set; }

    [SerializeField] public int currentMeters;
    public static event Action<float> OnDistanceSurpassed;
    public static event Action OnStartDrag;
    public static event Action OnEndDrag;

    [SerializeField] private float normalGravity = 1f;
    [SerializeField] private float noGravity = 0f;
    
    public static PlayerController Instance { get; private set; }

    public Audio bounceSound;
    public Audio endSound;
    public Audio impulseSound;
    [SerializeField]
    private bool dragging;

    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private Vector2 direction;
    private bool endDrag;

    private Vector2 worldPosition;

    private Camera mainCamera;

    private LineRenderer lineRenderer;
    [SerializeField]
    private float velocityMultiplier = 1;

    private SpriteRenderer sprite;
    private CircleCollider2D circleCollider;
    Rigidbody2D rb;

    public bool Dead;
    public static event Action<float> OnForceChanged;

    private void OnEnable()
    {
        
        Obstacle.OnPlayerPassedObstacle += IncrementMeters;
    }

    private void OnDisable()
    {
        Obstacle.OnPlayerPassedObstacle -= IncrementMeters;
    }

    void IncrementMeters()
    {
        currentMeters++;
        OnDistanceSurpassed?.Invoke(currentMeters);
    }
    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        sprite = GetComponentInChildren<SpriteRenderer>();
        circleCollider = GetComponentInChildren<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Reset();
    }


    // Update is called once per frame
    void Update()
    {
        worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0)) {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0)) {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }

    }

    private void StartDrag(Vector2 worldPosition)
    {
        dragging = true;
        transform.GetChild(0).GetComponent<TrailRenderer>().enabled = true;
        SetImpulseForce(Vector3.zero);
        rb.gravityScale = noGravity;
        startDragPosition = worldPosition;   
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startDragPosition);
        lineRenderer.SetPosition(1,startDragPosition);
        lineRenderer.useWorldSpace = true;
        
        lineRenderer.enabled = true;
        OnStartDrag?.Invoke();
    }
    
    private void ContinueDrag(Vector2 worldPosition)
    {
        endDragPosition = worldPosition;
        
        direction = endDragPosition - startDragPosition;
        SetImpulseForce(direction);
        lineRenderer.SetPosition(1,endDragPosition);
        Debug.DrawLine(endDragPosition, startDragPosition);
    }
    
    private void EndDrag()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 0;
        dragging = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = normalGravity;
        rb.AddForce(-direction, ForceMode2D.Impulse);
        AudioManager.Play(impulseSound);
        endDrag = false;
        OnEndDrag?.Invoke();
    }

    private void SetImpulseForce(Vector3 direction)
    {
        impulseForce = direction.magnitude *5f;
        OnForceChanged?.Invoke(impulseForce);
    }

    void FixedUpdate()
    {
        if(dragging)
        {
            rb.velocity *=velocityMultiplier;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            AudioManager.Play(bounceSound);
        }

        if (other.collider.CompareTag("Ground") || other.collider.CompareTag("Obstacle"))
        {
            if (!Dead)
            {
                Dead = true;
                AudioManager.Play(endSound);
                rb.gravityScale = noGravity;
            }

            GameController.Instance.EndGame();
        }
    }

    public void Explode()
    {
        //rb.AddExplosionForce(300f, transform.position, 30f);
    }

    public void Reset()
    {
        transform.GetChild(0).GetComponent<TrailRenderer>().enabled = false;
        transform.position = new Vector3(0, -4.3f, 0);
        startDragPosition = Vector3.zero;
        endDragPosition = Vector3.zero;
        rb.gravityScale = noGravity;
        ResetDistancetraveled();
        dragging = false;
        SetImpulseForce(Vector3.zero);
        rb.velocity = Vector3.zero;
        Dead = false;
    }

    void ResetDistancetraveled()
    {
        currentMeters = 0;
        OnDistanceSurpassed?.Invoke(currentMeters);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void HideLineRenderer()
    {
        lineRenderer.enabled = false;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    public void ActivateRigidbody()
    {
        rb.gravityScale = normalGravity;
    }
}
