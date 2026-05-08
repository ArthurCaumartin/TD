using System;
using UnityEngine;

public class Decorator_Shoot : Decorator
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private int _projectileCount;

    public Decorator_Shoot(Composable composable, Transform shootPoint, int projectileCount)
    : base(composable)
    {
        this._projectileCount = projectileCount;
        this._shootPoint = shootPoint;
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

    private void InstantiateProjectile(ProjectileInstaller projectile, Transform target, StatContainer stat, Vector2 position)
    {
        ProjectileInstaller p = GameObject.Instantiate(projectile, position, Quaternion.identity);
        p.transform.up = (target.position - _shootPoint.position).normalized;
        p.Init(stat.damage, stat.speed, target, stat.layerMask);
    }

}