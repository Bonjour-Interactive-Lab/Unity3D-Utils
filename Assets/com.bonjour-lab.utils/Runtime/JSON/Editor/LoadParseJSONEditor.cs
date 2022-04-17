using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoadParseJSON))]
[CanEditMultipleObjects]
public class LoadParseJSONEditor : Editor{
    SerializedProperty configfile;
    SerializedProperty pathToFile;
    SerializedProperty sourcetype;

    void OnEnable()
    {
        configfile            = serializedObject.FindProperty ("configfile");
        pathToFile            = serializedObject.FindProperty ("pathToFile");
        sourcetype            = serializedObject.FindProperty ("sourcetype");
    }

     public override void OnInspectorGUI () {
            // DrawDefaultInspector();
            serializedObject.Update ();

            EditorGUILayout.PropertyField(configfile, new GUIContent("JSON file", "Set the name of the json file without the extension"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(sourcetype, new GUIContent("Source path type", "Define if you want to taregt the Streaming Asset, a cutom path or a path inside the Assets folder"));
            
            LoadParseJSON.SOURCETYPE _type = (LoadParseJSON.SOURCETYPE) SerializedPropertyExtensions.GetValue(sourcetype);
            if( _type == LoadParseJSON.SOURCETYPE.CUSTOM ||
                _type == LoadParseJSON.SOURCETYPE.FROM_ASSETS_ROOT){
                    EditorGUILayout.PropertyField(pathToFile, new GUIContent("Path to File"));
            }
            serializedObject.ApplyModifiedProperties();
     }
}