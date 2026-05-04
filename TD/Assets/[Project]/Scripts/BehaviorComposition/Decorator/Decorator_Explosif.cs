using UnityEngine;

public class Decorator_Explosif : Decorator
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    public Decorator_Explosif(Composable composable, Transform transform, float explosifDamage, float radius, LayerMask layerMask)
    : base(composable)
    {
        _transform = transform;
        _damage = explosifDamage;
        _radius = radius;
        _layerMask = layerMask;
    }

    private void Explode()
    {
        Debug.Log("explosifDamage");
        Damagable[] dmg = PhysicsCastUtils2D.GetTypeInOverlapCircle<Damagable>(_transform.position, _radius, _layerMask);
        Debug.Log(dmg.Length);
        foreach (var item in dmg)
        {
            item.TakeDamage(_damage);
        }
    }

    public override void Kill()
    {
        base.Kill();
        Explode();
    }

    public override void DrawGizmoDebug()
    {
        base.DrawGizmoDebug();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _radius);
    }
}