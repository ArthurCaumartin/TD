using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[Flags]
public enum DecoratorTag
{
    Tag1,
    Tag2,
    Tag3,
    Tag4
}


[CustomEditor(typeof(ScriptableUpgarde))]
public class ScriptableUpgardeEditor : Editor
{
    private ScriptableUpgarde _upgarde;
    private string _searchBarInput = "";
    private DecoratorTag _tag;
    private float _viewWidth;
    private float _lineHeight;
    private float _lineSpacing;

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        _upgarde = target as ScriptableUpgarde;
        if (_upgarde.selectorList == null || _upgarde.selectorList.Count == 0) return;

        _viewWidth = EditorGUIUtility.currentViewWidth;
        _lineHeight = EditorGUIUtility.singleLineHeight;
        _lineSpacing = EditorGUIUtility.standardVerticalSpacing;

        GUILayoutUtility.GetRect(_viewWidth, 1000);

        Rect searchBarRect = new Rect(10, 25, _viewWidth / 2, _lineHeight);
        DisplaySearchBar(searchBarRect, out List<DecoratorSelector> filterSelector);

        Rect rect = new Rect(10, 25, _viewWidth - 30, _lineHeight);
        rect.y += 25;

        if (filterSelector == null || filterSelector.Count == 0) return;
        for (int i = 0; i < filterSelector.Count; i++)
        {
            DisplaySelector(filterSelector[i], rect, out float displayHeight);
            rect.y += 5 + displayHeight;
        }
    }

    private void DisplaySearchBar(Rect rect, out List<DecoratorSelector> filterSelector)
    {
        // GUILayoutUtility.GetRect(rect.width, rect.height);
        filterSelector = new List<DecoratorSelector>();
        _searchBarInput = EditorGUI.TextField(rect, _searchBarInput);
        foreach (DecoratorSelector selector in _upgarde.selectorList)
        {
            if (selector.name.ToLower().Contains(_searchBarInput.ToLower()))
                filterSelector.Add(selector);
        }
    }

    private void DisplayEnumTag(Rect rect, out List<DecoratorSelector> filterSelector)
    {
        filterSelector = new List<DecoratorSelector>();
        // EditorGUI.EnumPopup(rect, _tag);
        //TODO
    }

    private void DisplaySelector(DecoratorSelector selector, Rect rect, out float displayHeight)
    {
        BindingFlags flags = BindingFlags.Instance
                             | BindingFlags.Public
                             | BindingFlags.NonPublic;
        FieldInfo[] infosArray = selector.compositionFactory.GetType().GetFields(flags);
        List<FieldInfo> filterInfos = new List<FieldInfo>();
        foreach (var item in infosArray)
        {
            if (item.IsPublic)
                filterInfos.Add(item);

            if (!item.IsPublic && Attribute.IsDefined(item, typeof(SerializeField)))
                filterInfos.Add(item);
        }
        displayHeight = rect.height +
                        _lineHeight * filterInfos.Count +
                        _lineSpacing * (filterInfos.Count - 1) + 2.5f
                        + _lineSpacing;

        Texture2D backTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/[Project]/Editor/10x10_gray.png");
        GUI.DrawTexture(new Rect(rect.x,
                                 rect.y,
                                 rect.width,
                                 displayHeight),
                                 backTex);

        // Rect toogleRect = new Rect(rect.x + 2.5f, rect.y + 5, rect.width, 10);
        rect.x += 2.5f;
        selector.isSelected = EditorGUI.ToggleLeft(rect, selector.name, selector.isSelected);

        Rect fieldRect = new Rect(
            rect.x,
            rect.y + _lineHeight + _lineSpacing,
            rect.width - 5,
            _lineHeight
        );
        
        for (int i = 0; i < filterInfos.Count; i++)
        {
            EditorDrawUtils.DrawField(fieldRect, filterInfos[i], selector.compositionFactory);
            fieldRect.y += _lineHeight + _lineSpacing;
        }
    }
}
