using System;
using Alchemy.Inspector;
using UnityEngine;

[Serializable]
public struct PathNode
{
    public Vector2 position;
    public float distanceWithNext;

    public PathNode(Vector2 position, float distanceWithNext)
    {
        this.position = position;
        this.distanceWithNext = distanceWithNext;
    }
}


[ExecuteInEditMode]
public class LevelPath : MonoBehaviour
{
    [SerializeField] private DebugCondition _debug;
    [SerializeField, ReadOnly] Transform[] _pathDebug;
    private PathNode[] _path;
    public PathNode[] Path => _path;

    private void Update()
    {
        if (Application.isPlaying) return;
        Transform[] pathTransformArray = new Transform[transform.childCount];
        if (pathTransformArray == null || pathTransformArray.Length == 0) return;
        for (int i = 0; i < transform.childCount; i++)
        {
            pathTransformArray[i] = transform.GetChild(i);
            pathTransformArray[i].name = "Point_" + i;
        }
        if (Application.isEditor)
            _pathDebug = pathTransformArray;
        _path = ConvertPath(pathTransformArray);
    }

    public PathNode[] ConvertPath(Transform[] transforms)
    {
        PathNode[] newPath = new PathNode[transforms.Length];
        for (int i = 0; i < transforms.Length; i++)
        {
            Vector2 nextPosition = i == transforms.Length - 1 ? transforms[i].position : transforms[i + 1].position;
            float distance = Vector2.Distance(transforms[i].position, nextPosition);

            PathNode node = new PathNode(transforms[i].position, distance);
            newPath[i] = node;
        }
        return newPath;
    }

    private void OnDrawGizmos()
    {
        if (!_debug.enable) return;
        if (_path == null || _path.Length == 0) return;
        for (int i = 0; i < _path.Length - 1; i++)
        {
            Gizmos.color = _debug.colorA;
            Gizmos.DrawLine(_path[i].position, _path[i + 1].position);
        }
    }
}
