using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdjustableSoftShadow))]
public class SoftShadowEditor : Editor
{

    private AdjustableSoftShadow adjustableSoftShadow;
    private SerializedProperty propertySoftShadow;
    private bool generation = true;

    private Sprite sprite1;
    private Sprite sprite2;
    private Sprite sprite3;
    private Sprite sprite4;
    private Sprite sprite5;
    private string text = "X1";
    void Awake()
    {
        adjustableSoftShadow = (AdjustableSoftShadow)target;

        sprite1 = Resources.Load("Shadow/shadow_1", typeof(Sprite)) as Sprite;
        sprite2 = Resources.Load("Shadow/shadow_2", typeof(Sprite)) as Sprite;
        sprite3 = Resources.Load("Shadow/shadow_3", typeof(Sprite)) as Sprite;
        sprite4 = Resources.Load("Shadow/shadow_4", typeof(Sprite)) as Sprite;
        sprite5 = Resources.Load("Shadow/shadow_5", typeof(Sprite)) as Sprite;

        adjustableSoftShadow.shadow1 = sprite1;
        adjustableSoftShadow.shadow2 = sprite2;
        adjustableSoftShadow.shadow3 = sprite3;
        adjustableSoftShadow.shadow4 = sprite4;
        adjustableSoftShadow.shadow5 = sprite5;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        

        

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("HelpBox");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("type"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("colorShadow"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("magnificationSizeShadow"), false);       
        

        EditorGUILayout.Space();



        if (adjustableSoftShadow.shadow != null)
        {

            EditorGUILayout.HelpBox(text,MessageType.Info);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("X1"))
            {
                text = "X1";
                adjustableSoftShadow.shadowSize = 1;
                adjustableSoftShadow.ReCreateShadow();
            }
            if (GUILayout.Button("X2"))
            {
                text = "X2";
                adjustableSoftShadow.shadowSize = 2;
                adjustableSoftShadow.ReCreateShadow();
            }
            if (GUILayout.Button("X3"))
            {
                text = "X3";
                adjustableSoftShadow.shadowSize = 3;
                adjustableSoftShadow.ReCreateShadow();
            }
            if (GUILayout.Button("X4"))
            {
                text = "X4";
                adjustableSoftShadow.shadowSize = 4;
                adjustableSoftShadow.ReCreateShadow();
            }
            if (GUILayout.Button("X5"))
            {
                text = "X5";
                adjustableSoftShadow.shadowSize = 5;
                adjustableSoftShadow.ReCreateShadow();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            adjustableSoftShadow.shadow.color = adjustableSoftShadow.colorShadow;

            if (GUILayout.Button("Re-create Shadow"))
            {
                adjustableSoftShadow.ReCreateShadow();
            }

            if (GUILayout.Button("Delete Shadow"))
            {
                adjustableSoftShadow.DeleteShadow();
            }
        }
        else
        {
            if (GUILayout.Button("Сreate Shadow"))
            {
                adjustableSoftShadow.CreateShadow();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
