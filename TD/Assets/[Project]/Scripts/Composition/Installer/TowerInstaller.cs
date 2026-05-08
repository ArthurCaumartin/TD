using UnityEngine;

public class TowerInstaller : DecoratorInstaller
{
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private ProjectileInstaller _projectilePrefab;
    [Space]
    [SerializeField] private StatContainer _statContainer;
    private float _shootTimer = 0;

    public void Start()
    {
        behavior = new Decorator_Shoot(null, transform, 3);
        behavior = new Decorator_MoveSwing(behavior, transform, 1, 1);
        SubToDecoratorEvent();

        behavior.Spawn();
    }

    protected override void Update()
    {
        base.Update();
        if (!_targetFinder.CurrentTarget)
        {
            _shootTimer = 0;
            return;
        }
        _shootTimer += Time.deltaTime;
        if (_shootTimer > 1 / _statContainer.attackSpeed)
        {
            _shootTimer = 0;
            behavior.Shoot(_projectilePrefab, _targetFinder.CurrentTarget, _statContainer);
        }
    }
}
