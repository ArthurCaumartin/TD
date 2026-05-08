using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Mathf = UnityEngine.Mathf;

/// Container grouping a Type with its serializable fields, used to iterate the Composable chain
public struct TypeFieldInfoContainer
{
    public Decorator decoratorInstance;
    public Type type;
    public List<FieldInfo> fieldInfosInType;
    public TypeFieldInfoContainer(Decorator decoratorInstance, Type type, List<FieldInfo> fieldInfoArrayInType)
    {
        this.decoratorInstance = decoratorInstance;
        this.type = type;
        this.fieldInfosInType = fieldInfoArrayInType;
    }
}

[CustomPropertyDrawer(typeof(Composable), useForChildren: true)]
public class ComposablePropertyDrawer : PropertyDrawer
{
    private bool _showPrivate = false;
    float lineHeight = 17;
    float lineSpacing = 3;
    float compositionSpacing = 25;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        object targetObj = property.serializedObject.targetObject; /// Get MonoBehaviour instance that contain the composable
        Decorator decoratorInstance = fieldInfo.GetValue(targetObj) as Decorator; /// Get the composable instance to draw
        if (!decoratorInstance) return;
        List<TypeFieldInfoContainer> containerList = ExtractDecoratorChainInfos(decoratorInstance);


        { /// Draw each field for all type find in the composable chain
            Rect rect = new Rect(position.position.x,
                                 position.position.y,
                                 position.width,
                                 lineHeight);
            _showPrivate = EditorGUI.ToggleLeft(rect, "Show Private", _showPrivate);
            rect.y += lineHeight + lineSpacing;

            for (int i = 0; i < containerList.Count; i++)
            {
                EditorGUI.LabelField(rect, i + " / " + containerList[i].type.ToString());
                for (int j = 0; j < containerList[i].fieldInfosInType.Count; j++)
                {
                    rect.y += lineHeight + lineSpacing;
                    DrawField(rect, containerList[i].fieldInfosInType[j], containerList[i].decoratorInstance);
                }
                rect.y += compositionSpacing;
            }
        }
        EditorGUI.EndProperty();
    }

    /// Go through composition chain and return a list with TypeFieldInfoContainer for each Decorator instance
    private List<TypeFieldInfoContainer> ExtractDecoratorChainInfos(Decorator decoratorInstance)
    {
        List<TypeFieldInfoContainer> decoratorInfos = new List<TypeFieldInfoContainer>();
        Decorator warrpedComposable = decoratorInstance;
        while (warrpedComposable != null)
        {
            decoratorInfos.Add(GetMemberContainers(warrpedComposable));
            warrpedComposable = warrpedComposable.WarrpedComposable as Decorator;
        }
        decoratorInfos.Reverse();
        return decoratorInfos;
    }

    /// Return a list with a TypeFieldInfoContainer for filtered field find in the Composable chain
    public TypeFieldInfoContainer GetMemberContainers(Decorator targetObject)
    {
        BindingFlags flag = BindingFlags.Instance
                            | BindingFlags.Public
                            | BindingFlags.NonPublic;
        FieldInfo[] fieldInfoArray = targetObject.GetType().GetFields(flag);
        List<FieldInfo> infos = new List<FieldInfo>();
        foreach (var field in fieldInfoArray)
        {
            if (field.IsPublic)
                infos.Add(field);

            if (!field.IsPublic && (Attribute.IsDefined(field, typeof(SerializeField)) || _showPrivate))
                infos.Add(field);
        }
        return new TypeFieldInfoContainer(targetObject, targetObject.GetType(), infos);
    }

    private void DrawField(Rect rect, FieldInfo fieldInfo, object decoratorInstance)
    {
        if (decoratorInstance == null)
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
            fieldInfo.SetValue(decoratorInstance, EditorGUI.IntField(rectLabel, name, (int)fieldInfo.GetValue(decoratorInstance)));
        }

        if (type == typeof(float))
        {
            fieldInfo.SetValue(decoratorInstance, EditorGUI.FloatField(rectLabel, name, (float)fieldInfo.GetValue(decoratorInstance)));
        }

        if (type == typeof(bool))
        {
            fieldInfo.SetValue(decoratorInstance, EditorGUI.Toggle(rectLabel, name, (bool)fieldInfo.GetValue(decoratorInstance)));
        }

        if (type == typeof(Vector2))
        {
            fieldInfo.SetValue(decoratorInstance, EditorGUI.Vector2Field(rectLabel, name, (Vector2)fieldInfo.GetValue(decoratorInstance)));
        }

        if (type == typeof(Vector3))
        {
            fieldInfo.SetValue(decoratorInstance, EditorGUI.Vector3Field(rectLabel, name, (Vector3)fieldInfo.GetValue(decoratorInstance)));
        }

        if (type == typeof(LayerMask))
        {
            string layerDisplay = "";
            LayerMask layerMask = (LayerMask)fieldInfo.GetValue(decoratorInstance);
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
            EditorGUI.ObjectField(rectLabel, name, (UnityEngine.Object)fieldInfo.GetValue(decoratorInstance), type, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        object targetObj = property.serializedObject.targetObject;
        List<TypeFieldInfoContainer> containers = ExtractDecoratorChainInfos(fieldInfo.GetValue(targetObj) as Decorator);
        float height = lineHeight + lineSpacing;
        foreach (var container in containers)
        {
            foreach (var field in container.fieldInfosInType)
                height += lineHeight + lineSpacing;
            height += compositionSpacing;
        }
        return Mathf.Max(10, height + 20);
    }
}