using UnityEngine;
public class GeneratorTuberia : MonoBehaviour
{
    [Header("Pipe Settings")]
    public GameObject pipePrefab;
    public float gapSize;
    public float spawnInterval;
    public float minY;
    public float maxY;
    public float spawnX;

    [SerializeField] private NewMaterial newMaterial;

    Quaternion superior;
    Quaternion inferior;
    private void Start()
    {
        
        superior=Quaternion.Euler(90f, 0f, 0f);
        inferior=Quaternion.Euler(-90f, 0f, 0f);
        Invoke("SpawnPipePair", 0);
    }

    private void SpawnPipePair()
    {
        float randomY = Random.Range(minY, maxY);
        Material newMaterial= this.newMaterial.GetRandonMaterial();
        GameObject lowerPipe = Instantiate(pipePrefab, new Vector3(spawnX, randomY - gapSize / 2, 0), inferior);
        lowerPipe.tag = "Abajo";
        lowerPipe.GetComponent<Renderer>().material = newMaterial;

        GameObject upperPipe = Instantiate(pipePrefab, new Vector3(spawnX, randomY + gapSize / 2, 0), superior);
        upperPipe.tag = "Arriba";
        upperPipe.GetComponent<Renderer>().material = newMaterial;

        lowerPipe.transform.parent = transform;
        upperPipe.transform.parent = transform;
        Invoke("SpawnPipePair", spawnInterval);
    }
}
