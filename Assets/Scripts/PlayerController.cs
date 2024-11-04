using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody RB;
    private bool jump;
    [SerializeField] private float forceJump;
    [SerializeField] private float timeRotation;
    [SerializeField] private float forceMoviment;
    private float Xdirection;

    private Quaternion raise;
    private Quaternion descent;
    private Quaternion targetRotation;

    [Header("Limit")]
    [SerializeField] private float xmin;
    [SerializeField] private float xmax;
    [SerializeField] private float ymin;
    [SerializeField] private float ymax;

    static public event Action eventGameOver;

    private TrailRenderer trailRenderer;
    private ParticleSystem particleSystem;
    [SerializeField] private NewMaterial newMaterial;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        raise = Quaternion.Euler(0, 180, -45);
        descent = Quaternion.Euler(0, 180, 45);
        targetRotation = Quaternion.identity;
        NewMaterialTrailRenderer();
        particleSystem.startSize = 0;
    }

    private void Update()
    {
        Rotation();
        Limit();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jump = true;
        }
    }

    public void ActiveEventGameOver()
    {
        eventGameOver?.Invoke();
    }

    private void NewMaterialTrailRenderer()
    {
        trailRenderer.material= newMaterial.GetRandonMaterial();
    }

    public void XDirection(InputAction.CallbackContext context)
    {
        Xdirection = context.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        RB.AddForce(Vector3.right * Xdirection*forceMoviment, ForceMode.Force);
        if (jump)
        {
            RB.AddForce(new Vector3(0, forceJump, 0), ForceMode.Impulse);
            jump = false;
        }
    }

    private void Limit()
    {
        if (transform.position.x < xmin || transform.position.x > xmax||
            transform.position.y < ymin || transform.position.y > ymax)
        {
            ActiveEventGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arriba"))
        {
            print("Arirba");
            RB.AddForce(new Vector3(-20, -10, 0), ForceMode.Impulse);
            NewMaterialTrailRenderer();

            ParticleSystem.MainModule main = particleSystem.main;

            float currentSize = main.startSize.constant; 

            main.startSize = currentSize + 0.18f;

        }
        else if (other.gameObject.CompareTag("Abajo"))
        {
            print("Abajo");
            RB.AddForce(new Vector3(-20, 10, 0), ForceMode.Impulse);
            NewMaterialTrailRenderer();
            ParticleSystem.MainModule main = particleSystem.main;

            float currentSize = main.startSize.constant;

            main.startSize = currentSize + 0.18f;
        }
    }
    private void Rotation()
    {
        if (RB.velocity.y > 0)
        {
            targetRotation = raise;
        }
        else
        {
            targetRotation = descent;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, timeRotation * Time.deltaTime);
    }
}
