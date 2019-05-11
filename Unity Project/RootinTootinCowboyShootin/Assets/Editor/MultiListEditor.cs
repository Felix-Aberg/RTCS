using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MultiList)), CanEditMultipleObjects]
public class MultiListEditor : Editor
{
    /*
    public SerializedProperty state_property;
    public SerializedProperty enemy_variables_property;
    public SerializedProperty enemy_variables_twin_property;
    public SerializedProperty special_event_property;
    public SerializedProperty test_integer_property;
    public SerializedProperty controllable_property;

    void OnEnable()
    {
        //Get properties
        state_property =                serializedObject.FindProperty("type_of_event");
        enemy_variables_property =      serializedObject.FindProperty("enemy_variables");
        enemy_variables_twin_property = serializedObject.FindProperty("enemy_variables_twin");
        special_event_property =        serializedObject.FindProperty("special_event");

        test_integer_property = serializedObject.FindProperty("test_integer");

        controllable_property =         serializedObject.FindProperty("controllable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(state_property);

        MultiList.Events state = (MultiList.Status)state_property.enumValueIndex;

        switch (state)
        {
            case MultiList.Status.ENEMY:
                EditorGUILayout.PropertyField(controllable_property, new GUIContent("controllable"));
                EditorGUILayout.PropertyField(enemy_variables_property, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(test_integer_property, 0, 10, new GUIContent("test_integer"));
                break;

            case MultiList.Status.TWINENEMIES:
                EditorGUILayout.PropertyField(controllable_property, new GUIContent("controllable"));
                break;

            case MultiList.Status.EVENT:
                EditorGUILayout.PropertyField(controllable_property, new GUIContent("controllable"));
                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
    //*/
}