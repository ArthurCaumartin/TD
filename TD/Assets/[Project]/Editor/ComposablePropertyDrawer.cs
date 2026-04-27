using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Mathf = UnityEngine.Mathf;

//? Container grouping a Type with its serializable fields, used to iterate the Composable chain
public struct TypeFieldInfoContainer
{
    public Type type;
    public List<FieldInfo> fieldInfosInType;
    public TypeFieldInfoContainer(Type type, List<FieldInfo> fieldInfoArrayInType)
    {
        this.type = type;
        this.fieldInfosInType = fieldInfoArrayInType;
    }
}

[CustomPropertyDrawer(typeof(Composable), useForChildren: true)]
public class ComposablePropertyDrawer : PropertyDrawer
{
    float lineHeight = 17;
    float lineSpacing = 3;
    float compositionSpacing = 25;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        object targetObj = property.serializedObject.targetObject; /// Get MonoBehaviour instance that contain the composable
        object decoratorInstance = fieldInfo.GetValue(targetObj); /// Get the composable instance to draw
        List<TypeFieldInfoContainer> containerList = GetMemberContainers(targetObj);

        { /// Draw each field for all type find in the composable chain
            Rect rect = new Rect(position.position.x,
                                 position.position.y,
                                 position.width,
                                 lineHeight);

            for (int i = 0; i < containerList.Count; i++)
            {
                EditorGUI.LabelField(rect, i + " / " + containerList[i].type.ToString());
                for (int j = 0; j < containerList[i].fieldInfosInType.Count; j++)
                {
                    rect.y += lineHeight + lineSpacing;
                    DrawField(rect, containerList[i].fieldInfosInType[j], decoratorInstance);
                }
                rect.y += compositionSpacing;
            }
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        object targetObj = property.serializedObject.targetObject;
        List<TypeFieldInfoContainer> containers = GetMemberContainers(targetObj);
        float height = 0;
        foreach (var container in containers)
        {
            foreach (var field in container.fieldInfosInType)
                height += lineHeight + lineSpacing;
            height += compositionSpacing;
        }
        return Mathf.Max(10, height + 20);
    }

    private void DrawField(Rect rect, FieldInfo fieldInfo, object composableInstance)
    {
        string name = fieldInfo.Name;
        Type type = fieldInfo.FieldType;

        Rect rectLabel = new Rect(rect.x + 20, rect.y, rect.width - 20, rect.height);
        EditorGUI.LabelField(rectLabel, name + " : ");

        if (type == typeof(int))
        {
            EditorGUI.IntField(rectLabel, name, (int)fieldInfo.GetValue(composableInstance));
        }

        if (type == typeof(float))
        {
            EditorGUI.FloatField(rectLabel, name, (float)fieldInfo.GetValue(composableInstance));
        }

        if (type == typeof(bool))
        {
            EditorGUI.Toggle(rectLabel, name, (bool)fieldInfo.GetValue(composableInstance));
        }

        if (type == typeof(LayerMask))
        {
            string layerDisplay = "";
            LayerMask layerMask = (LayerMask)fieldInfo.GetValue(composableInstance);
            for (int i = 0; i < 32; i++)
            {
                if((layerMask.value & (1 << i)) != 0) /// <= thanks IA
                {
                    layerDisplay += LayerMask.LayerToName(i) + " / ";
                }
            }
            EditorGUI.TextField(rectLabel, name, layerDisplay);
        }

        if (typeof(UnityEngine.Object).IsAssignableFrom(type))
        {
            EditorGUI.ObjectField(rectLabel, name, (UnityEngine.Object)fieldInfo.GetValue(composableInstance), type, true);
        }
    }

    /// Return a list with a TypeFieldInfoContainer for filtered field find in the Composable chain
    public List<TypeFieldInfoContainer> GetMemberContainers(object targetObject)
    {
        object value = fieldInfo.GetValue(targetObject);
        List<Type> types = value.GetInheranceCompo();
        types.RemoveAll(obj => obj == typeof(System.Object));

        List<TypeFieldInfoContainer> containerList = new List<TypeFieldInfoContainer>();
        for (int i = 0; i < types.Count; i++)
        {
            BindingFlags flag = BindingFlags.Instance
                                | BindingFlags.Public
                                | BindingFlags.NonPublic;
            FieldInfo[] fieldInfoArray = types[i].GetFields(flag);
            List<FieldInfo> infos = new List<FieldInfo>();
            foreach (var field in fieldInfoArray)
            {
                if (field.IsPublic)
                    infos.Add(field);

                if (!field.IsPublic && Attribute.IsDefined(field, typeof(SerializeField)))
                    infos.Add(field);
            }
            containerList.Add(new TypeFieldInfoContainer(types[i], infos));
        }

        return containerList;
    }
}