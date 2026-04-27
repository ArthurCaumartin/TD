using Alchemy.Inspector;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private DebugCondition _debug;
    [SerializeField] private float _radius = 5;
    [SerializeField] private LayerMask _searchLayer;
    [SerializeField, ReadOnly] private Transform _currentTarget;

    public LayerMask DetectionLayer => _searchLayer;
    public Transform CurrentTarget => _currentTarget;


    private void Update()
    {
        if (!_currentTarget) _currentTarget = GetTarget();
        if (!_currentTarget) return;

        float distance = Vector3.Distance(transform.position, _currentTarget.position);
        if (distance > _radius) _currentTarget = null;
    }

    private Transform GetTarget()
    {
        Target[] t = PhysicsCastUtils2D.GetTypeInOverlapCircle<Target>(transform.position, _radius, _searchLayer);
        print(t.Length);
        if (t.Length == 0) return null;
        return t.GetRandom().transform;
    }

    private void OnDrawGizmos()
    {
        if (!_debug.enable) return;
        Gizmos.color = _debug.colorA;
        Gizmos.DrawWireSphere(transform.position, _radius);
        if (_currentTarget)
            Gizmos.DrawLine(transform.position, _currentTarget.position);
    }
}