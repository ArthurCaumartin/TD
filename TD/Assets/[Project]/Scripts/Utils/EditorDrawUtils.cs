using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class EditorDrawUtils
{
    
    public static void DrawField(Rect rect, FieldInfo fieldInfo, object objectInstance)
    {
        if (objectInstance == null)
        {
            Debug.Log("composable == null");
            return;
        }
        string name = fieldInfo.Name;
        Type type = fieldInfo.FieldType;

        Rect rectLabel = new Rect(rect.x + 20, rect.y, rect.width - 20, rect.height);
        EditorGUI.LabelField(rectLabel, name + " : ");

        if (type == typeof(int))
        {
            fieldInfo.SetValue(objectInstance, EditorGUI.IntField(rectLabel, name, (int)fieldInfo.GetValue(objectInstance)));
        }

        if (type == typeof(float))
        {
            fieldInfo.SetValue(objectInstance, EditorGUI.FloatField(rectLabel, name, (float)fieldInfo.GetValue(objectInstance)));
        }

        if (type == typeof(bool))
        {
            fieldInfo.SetValue(objectInstance, EditorGUI.Toggle(rectLabel, name, (bool)fieldInfo.GetValue(objectInstance)));
        }

        if (type == typeof(Vector2))
        {
            fieldInfo.SetValue(objectInstance, EditorGUI.Vector2Field(rectLabel, name, (Vector2)fieldInfo.GetValue(objectInstance)));
        }

        if (type == typeof(Vector3))
        {
            fieldInfo.SetValue(objectInstance, EditorGUI.Vector3Field(rectLabel, name, (Vector3)fieldInfo.GetValue(objectInstance)));
        }

        if (type == typeof(LayerMask))
        {
            string layerDisplay = "";
            LayerMask layerMask = (LayerMask)fieldInfo.GetValue(objectInstance);
            for (int i = 0; i < 32; i++)
            {
                if ((layerMask.value & (1 << i)) != 0) /// <= thanks IA
                {
                    layerDisplay += LayerMask.LayerToName(i) + " / ";
                }
            }
            EditorGUI.TextField(rectLabel, name, layerDisplay);
        }

        if (typeof(UnityEngine.Object).IsAssignableFrom(type))
        {
            fieldInfo.SetValue(objectInstance, 
                EditorGUI.ObjectField(
                    rectLabel,
                    name,
                    (UnityEngine.Object)fieldInfo.GetValue(objectInstance),
                    type,
                    false
                )
            );
        }
    }



}