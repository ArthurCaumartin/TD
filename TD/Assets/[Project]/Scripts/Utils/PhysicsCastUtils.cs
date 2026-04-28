using System;
using UnityEngine;

public static class PhysicsCastUtils2D
{
    public static T[] GetTypeInOverlapCircle<T>(Vector3 point, float raduis, LayerMask layerMask) where T : Component
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(point, raduis, layerMask);
        return GetFromColliderArray<T>(cols);
    }

    public static T[] GetTypeInCircleCast<T>(Vector2 origine, float radius, Vector2 direction) where T : Component
    {
        RaycastHit2D[] cols = Physics2D.CircleCastAll(origine, radius, direction);
        return GetFromRayCastHitArray<T>(cols);
    }

    public static T[] GetTypeInLine<T>(Vector3 startPoint, Vector3 endPoint, LayerMask layerMask) where T : Component
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(startPoint, endPoint, layerMask);
        return GetFromRayCastHitArray<T>(hits);
    }

    private static T[] GetFromColliderArray<T>(Collider2D[] colliders) where T : Component
    {
        T[] foundArray = new T[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            T t = colliders[i].GetComponent<T>();
            if (t) foundArray[i] = t;
        }
        foundArray = Array.FindAll(foundArray, x => x != null);
        return foundArray;
    }

    private static T[] GetFromRayCastHitArray<T>(RaycastHit2D[] colliders) where T : Component
    {
        T[] foundArray = new T[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].collider) continue;
            T t = colliders[i].collider.GetComponent<T>();
            if (t) foundArray[i] = t;
        }
        foundArray = Array.FindAll(foundArray, x => x != null);
        return foundArray;
    }
}