using UnityEngine;

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