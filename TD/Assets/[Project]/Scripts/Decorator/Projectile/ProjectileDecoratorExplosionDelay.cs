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

    public override void Kill()
    {
        Damagable[] dmg = PhysicsCastUtils2D.GetTypeInOverlapCircle<Damagable>(transform.position, 20, layerMask);
        foreach (var item in dmg)
            item.TakeDamage(20000);
        base.Kill();
    }

    public ProjectileDecoratorExplosionDelay(ProjectileDecoratorBase projectileBase, float delay)
    : base(projectileBase)
    {
        _timer = delay;
    }
}