using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] 
[RequireComponent(typeof(Rigidbody))] 
public class TuberiaController : MonoBehaviour
{
    private Rigidbody RB;
    private float forceMoviment=3;
    private LineRenderer lineRenderer;
    private GameObject tubera;
    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        if (tubera != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, tubera.transform.position);
        }
    }
    public void NewTuberia(GameObject newGameObject)
    {
        tubera = newGameObject;
        lineRenderer.enabled = true;
    }

    private void FixedUpdate()
    {
        RB.AddForce(Vector3.left * forceMoviment);
    }
}
