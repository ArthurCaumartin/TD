using Alchemy.Inspector;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField, ReadOnly] private float _damage;
    [SerializeField, ReadOnly] private float _speed;
    [SerializeField, ReadOnly] private Transform _target;
    private Vector3 _targetPos = Vector3.one;
    private Vector3 _startPos;
    private float _distanceWithTarget;
    private float _travelTime;
    private bool _isInit = false;

    public virtual Projectile Init(float damage, float speed, Transform target)
    {
        _isInit = true;
        _damage = damage;
        _speed = speed;
        _target = target;

        _startPos = transform.position;
        return this;
    }

    protected virtual void Update()
    {
        if (!_isInit)
        {
            Destroy(gameObject);
            return;
        }

        if (_target)
            _targetPos = _target.position;

        _distanceWithTarget = Vector2.Distance(transform.position, _targetPos);
        Vector3 dir = _targetPos - transform.position;
        dir = dir.normalized;
        transform.up = dir;
        transform.position = Vector2.Lerp(_startPos, _targetPos, _travelTime);
        _travelTime += Time.deltaTime * _speed / _distanceWithTarget;

        if (_travelTime >= 1)
        {
            if (_target) Destroy(_target.gameObject);
            Destroy(gameObject);
            return;
        }
    }
}
