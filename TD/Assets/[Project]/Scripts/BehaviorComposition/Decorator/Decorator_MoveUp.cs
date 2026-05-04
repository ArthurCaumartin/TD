using UnityEngine;

public class Decorator_MoveUp : Decorator
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;

    public Decorator_MoveUp(Composable composable, Transform transform, float speed)
    : base(composable)
    {
        this._transform = transform;
        this._speed = speed;
    }

    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        _transform.Translate(Vector2.up * Time.deltaTime * _speed, Space.Self);
    }
}
