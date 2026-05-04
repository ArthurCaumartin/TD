using UnityEngine;

public class TowerDescriptor : MonoBehaviour
{
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private ProjectileDescriptor _projectilePrefab;
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
            ProjectileDescriptor p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            p.transform.up = (_targetFinder.CurrentTarget.position - transform.position).normalized;
            p.Init(5, 5, _targetFinder.CurrentTarget, _targetFinder.DetectionLayer);
        }
    }
}
