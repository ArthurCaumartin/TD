using UnityEngine;
using BehaviorComposition.Decorator;

public class EnnemyInstaller : MonoBehaviour
{
    [SerializeField] private Decorator _behavior;
    private PathNode[] _path;
    private float _speed;
    private int _pathIndex = 0;
    private float _moveTime;
    private Vector2 _pathOffSet;

    public EnnemyInstaller Init(PathNode[] path, float health, float speed)
    {
        this._path = path;
        this._speed = speed;
        _pathOffSet = Random.insideUnitCircle * .5f;
        return this;
    }

    private void Update()
    {
        if (_pathIndex >= _path.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        _moveTime += Time.deltaTime * _speed / _path[_pathIndex].distanceWithNext;
        transform.position = Vector2.Lerp(
            _path[_pathIndex].position,
            _path[_pathIndex + 1].position,
            _moveTime
        ) + _pathOffSet;

        if (_moveTime >= 1)
        {
            _moveTime = 0;
            _pathIndex++;
        }
    }
}