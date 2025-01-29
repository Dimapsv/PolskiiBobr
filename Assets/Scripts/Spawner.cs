using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3[] objectSpawnPoints;
    public GameObject objectToSpawn;
    

    private void Start()
    {
        Vector3 obejctSpawnPoint = objectSpawnPoints[Random.Range(0, objectSpawnPoints.Length)];
        Instantiate(objectToSpawn, obejctSpawnPoint, Quaternion.identity);
    }

}
