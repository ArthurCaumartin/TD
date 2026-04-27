using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _attackPerSecond = 1;
    private float _shootTimer = 0;


    private void Update()
    {
        if (!_targetFinder.CurrentTarget)
        {
            _shootTimer = 0;
            return;
        }
        _shootTimer += Time.deltaTime;
        if (_shootTimer > 1 / _attackPerSecond)
        {
            _shootTimer = 0;
            Projectile p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            p.Init(5, 5, _targetFinder.CurrentTarget);
        }
    }




}
