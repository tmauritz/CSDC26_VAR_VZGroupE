using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    
    [SerializeField]
    private GameObject _cubePrefab;
    
    public void spawnCube()
    {
        Instantiate(_cubePrefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
