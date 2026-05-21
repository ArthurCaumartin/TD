using UnityEngine;
using BehaviorComposition;

public class TowerInstaller : DecoratorInstaller
{
    [SerializeField] private ScriptableUpgarde _upgradeTower;
    [Space]
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private ProjectileInstaller _projectilePrefab;
    [SerializeField] private StatContainer _statContainer;
    private float _shootTimer = 0;

    public void Start()
    {
        CompositionContext context = new CompositionContext(
            transform,
            transform,
            _targetFinder,
            _projectilePrefab,
            _statContainer
        );

        behavior = null;
        behavior = _upgradeTower.GetComposition(behavior, context);
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
            behavior.Shoot(_projectilePrefab, _statContainer);
        }
    }
}