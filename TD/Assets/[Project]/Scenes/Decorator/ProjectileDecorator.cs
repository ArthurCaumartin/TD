using System.Linq;
using UnityEngine;

public class ProjectileDecoratorBase : Decorator
{
    public ProjectileDecoratorBase(Composable composable) : base(composable) { }

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    private Vector3 _targetPos = Vector3.one;
    private Vector3 _lastFramePos;
    private float _distanceWithTarget;
    private Transform transform;


    public virtual void Init(Transform transform, float damage, float speed, Transform target, LayerMask layerMask)
    {
        _layerMask = layerMask;
        _damage = damage;
        _speed = speed;
        _target = target;
        _lastFramePos = transform.position;
    }


    public override void ComposableUpdate()
    {
        base.ComposableUpdate();

        if (_target)
            _targetPos = _target.position;

        _distanceWithTarget = Vector2.Distance(transform.position, _targetPos);
        Vector3 dir = _targetPos - transform.position;
        dir = dir.normalized;
        transform.up = dir;
        transform.Translate(Vector3.up * Time.deltaTime * _speed);

        if (DetectTargetOnPath())
        {
            if (_target) GameObject.Destroy(_target.gameObject);
            GameObject.Destroy(transform.gameObject);
            return;
        }

        _lastFramePos = transform.position;
    }

    protected bool DetectTargetOnPath()
    {
        Transform[] t = PhysicsCastUtils2D.GetTypeInLine<Transform>(transform.position, _lastFramePos, _layerMask);
        return t.Contains(_target);
    }
}