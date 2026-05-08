using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private LevelPath _levelPath;
    [SerializeField] private EnnemyInstaller ennemyPrefab;
    [SerializeField] private float _spawnPerSecond = 2;
    private float _timer;



    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1 / _spawnPerSecond)
        {
            _timer = 0;
            EnnemyInstaller ennemy = Instantiate(ennemyPrefab, _levelPath.Path[0].position, Quaternion.identity);
            ennemy.Init(_levelPath.Path, 100, 2);
        }
    }
}