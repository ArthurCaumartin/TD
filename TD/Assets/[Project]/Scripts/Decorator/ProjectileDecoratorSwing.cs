using System;
using UnityEngine;

[Serializable]
public class ProjectileDecoratorSwing : ProjectileDecoratorBase
{
    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        transform.Translate(Vector2.right * Time.deltaTime * Mathf.Cos(Time.time * 10) * 5);
    }

    public ProjectileDecoratorSwing(Composable composable, ProjectileDecoratorBase projectileDecoratorBase)
    : base(composable, projectileDecoratorBase)
    {
    }
}

public class ProjectileDecoratorExplosionDelay : ProjectileDecoratorBase
{
    [SerializeField] private float _timer;

    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            GameObject.Destroy(transform.gameObject);
            return;
        }
    }

    public ProjectileDecoratorExplosionDelay(Composable composable, ProjectileDecoratorBase projectileDecoratorBase, float delay)
    : base(composable, projectileDecoratorBase)
    {
        _timer = delay;
    }
}