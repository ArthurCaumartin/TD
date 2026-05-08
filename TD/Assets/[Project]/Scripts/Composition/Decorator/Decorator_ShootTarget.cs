using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// Set la reference au TargetFinder pour get la target, plutot que de la psser en parametre a Shoot

public class Decorator_ShootTarget : Decorator
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private int _projectileCount;
    [SerializeField] private StatContainer _stats;

    public Decorator_ShootTarget(Composable composable, Transform shootPoint, StatContainer stats, int projectileCount)
    : base(composable)
    {
        this._projectileCount = projectileCount;
        this._shootPoint = shootPoint;
        this._stats = stats;
    }

    public override void Shoot(ProjectileInstaller projectile, Transform target, StatContainer stats)
    {
        base.Shoot(projectile, target, stats);
        if (_projectileCount == 1)
        {
            InstantiateProjectile(projectile, target, stats, _shootPoint.position);
            return;
        }
        Debug.Log("----I----");
        for (int i = 0; i < _projectileCount; i++)
        {
            float time = Mathf.InverseLerp(0, _projectileCount - 1, i);

            Debug.Log("i = " + time);
            Vector2 pos = Vector2.Lerp(
                _shootPoint.position - _shootPoint.right,
                _shootPoint.position + _shootPoint.right,
                time);

            InstantiateProjectile(projectile, target, stats, pos);
        }
    }

    private void InstantiateProjectile(ProjectileInstaller projectile, Transform target, StatContainer stats, Vector2 position)
    {
        ProjectileInstaller p = GameObject.Instantiate(projectile, position, Quaternion.identity);
        p.transform.up = (target.position - _shootPoint.position).normalized;

        Decorator behavior = new Decorator_MoveToTarget(null, p.transform, target, stats);
        behavior = new Decorator_HitOnPath(behavior, p.transform, stats);
        behavior = new Decorator_ShootBurstOnDeath(behavior, p.transform, projectile, stats, 50);

        p.Init(stats, behavior);
    }

}