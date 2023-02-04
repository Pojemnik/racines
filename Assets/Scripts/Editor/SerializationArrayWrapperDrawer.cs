using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(SerializationArrayWrapper<GameObject>))]
public class SerializationArrayWrapperDrawer : PropertyDrawer
{
    private Dictionary<string, ReorderableList> arrayLists = new Dictionary<string, ReorderableList>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        arrayLists[property.propertyPath].DoList(position);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty arrayProperty = property.FindPropertyRelative("Array");
        if (!arrayLists.ContainsKey(property.propertyPath) || arrayLists[property.propertyPath].index > arrayLists[property.propertyPath].count - 1)
        {
            arrayLists[property.propertyPath] = new ReorderableList(arrayProperty.serializedObject, arrayProperty, true, false, true, true);

            arrayLists[property.propertyPath].drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2;
                EditorGUI.PropertyField(rect, arrayProperty.GetArrayElementAtIndex(index), true);
            };
        }
        return arrayLists[property.propertyPath].GetHeight();
    }
}
