using System;
using UnityEngine;

[Serializable]
public class ProjectileDecoratorBase : DecoratorEntity
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Transform target;
    protected Vector3 targetPos = Vector3.one;
    protected Vector3 lastFramePos;
    protected float distanceWithTarget;
    [SerializeField] protected float spriteSize;

    public Transform Transform => transform;
    public LayerMask LayerMask => layerMask;
    public float Damage => damage;
    public float Speed => speed;
    public Transform Target => target;

    public override void ComposableUpdate()
    {
        base.ComposableUpdate();

        if (target)
            targetPos = target.position;

        Move();

        if (DetectTargetOnPath())
        {
            if (target) GameObject.Destroy(target.gameObject);
            Kill();
            return;
        }

        if (distanceWithTarget < 0.01)
        {
            Kill();
            return;
        }

        lastFramePos = transform.position;
    }

    public void Move()
    {
        distanceWithTarget = Vector2.Distance(transform.position, targetPos);
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        transform.up = dir;
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    protected bool DetectTargetOnPath()
    {
        Transform[] t = PhysicsCastUtils2D.GetTypeInLine<Transform>(transform.position, lastFramePos, layerMask);
        return t.Length > 0;
    }

    public override Action DrawGizmoDebug()
    {
        if (!transform) return null;
        return () =>
        {
            Debug.DrawLine(transform.position, transform.position - (transform.up * Time.deltaTime * speed), Color.red, 1f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spriteSize / 2);
        };
    }


    public ProjectileDecoratorBase(Composable composable,
                               Transform transform,
                               float damage,
                               float speed,
                               Transform target,
                               LayerMask layerMask) : base(composable, transform)
    {
        this.transform = transform;
        this.layerMask = layerMask;
        this.damage = damage;
        this.speed = speed;
        this.target = target;

        lastFramePos = transform.position;
    }

    //TODO le link au constructeur au dessut (plus clean)
    public ProjectileDecoratorBase(ProjectileDecoratorBase projectileBase) : base(projectileBase, projectileBase?.Transform)
    {
        transform = projectileBase.Transform;
        layerMask = projectileBase.LayerMask;
        damage = projectileBase.Damage;
        speed = projectileBase.Speed;
        target = projectileBase.Target;

        lastFramePos = transform.position;
    }
}
