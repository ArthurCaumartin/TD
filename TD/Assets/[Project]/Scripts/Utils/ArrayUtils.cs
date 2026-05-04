using UnityEngine;

public static class ArrayUtils
{
    public static T GetRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static T GetNearset<T>(this T[] array, Vector3 position) where T : MonoBehaviour
    {
        T nearest = null;
        float distance = Mathf.NegativeInfinity;
        for (int i = 0; i < array.Length; i++)
        {
            float currentDistance = (array[i].transform.position - position).sqrMagnitude;
            if(distance < currentDistance)
            {
                distance = currentDistance;
                nearest = array[i];
            }
        }
        return nearest;
    }
}
