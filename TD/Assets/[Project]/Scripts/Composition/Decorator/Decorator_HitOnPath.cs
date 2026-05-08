using UnityEngine;

public class Decorator_HitOnPath : Decorator
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _layerMask;
    private Vector3 _lastFramePosition;

    public Decorator_HitOnPath(Composable composable, Transform transform, float damage, LayerMask layerMask)
    : base(composable)
    {
        this._transform = transform;
        this._damage = damage;
        this._layerMask = layerMask;

        _lastFramePosition = _transform.position;
    }

    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        Damagable[] t = PhysicsCastUtils2D.GetTypeInLine<Damagable>(_transform.position, _lastFramePosition, _layerMask);
        // Debug.DrawLine(_transform.position, _lastFramePosition, Color.red, 1f);
        // Debug.Log("HitOnPath count : " + t.Length);
        if (t.Length != 0)
        {
            Damagable d = t.GetNearset(_transform.position);
            if (d)
            {
                d.TakeDamage(_damage);
                killCallEvent.Invoke();
                return;
            }
        }
        _lastFramePosition = _transform.position;
    }
}