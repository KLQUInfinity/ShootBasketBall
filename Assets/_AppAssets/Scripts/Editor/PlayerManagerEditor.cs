using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    #region Camera Setting
    private bool cameraSettingFoldout;

    SerializedProperty rotSpeed;

    SerializedProperty lockXRot;
    SerializedProperty xRotLimit;

    SerializedProperty lockYRot;
    SerializedProperty yRotLimit;
    #endregion

    #region Shoot setting
    private bool shootSettingFoldout;

    SerializedProperty ballPrefab;

    #endregion

    private void OnEnable()
    {
        #region Camera Setting        
        rotSpeed = serializedObject.FindProperty("RotSpeed");

        lockXRot = serializedObject.FindProperty("LockXRot");
        xRotLimit = serializedObject.FindProperty("XRotLimit");

        lockYRot = serializedObject.FindProperty("LockYRot");
        yRotLimit = serializedObject.FindProperty("YRotLimit");
        #endregion

        #region Shoot setting
        ballPrefab = serializedObject.FindProperty("BallPrefab");
        #endregion

        #region Editor Variables
        cameraSettingFoldout = EditorPrefs.GetBool("cameraSettingFoldout",false);
        shootSettingFoldout = EditorPrefs.GetBool("shootSettingFoldout", false);
        #endregion
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CameraSettingEditor();
        ShootSettingEditor();

        serializedObject.ApplyModifiedProperties();
    }

    private void CameraSettingEditor()
    {
        EditorGUILayout.Space();

        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        cameraSettingFoldout = EditorGUILayout.Foldout(cameraSettingFoldout, new GUIContent("Camera Setting"), true);
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (cameraSettingFoldout)
        {
            EditorGUILayout.PropertyField(rotSpeed, new GUIContent("Rotation Speed"));

            EditorGUILayout.PropertyField(lockXRot, new GUIContent("Lock X Rotation"));
            if (lockXRot.boolValue)
            {
                EditorGUILayout.PropertyField(xRotLimit, new GUIContent("Limit of Rotation on X"));
            }

            EditorGUILayout.PropertyField(lockYRot, new GUIContent("Lock Y Rotation"));
            if (lockYRot.boolValue)
            {
                EditorGUILayout.PropertyField(yRotLimit, new GUIContent("Limit of Rotation on Y"));
            }
        }

        EditorPrefs.GetBool("cameraSettingFoldout", cameraSettingFoldout);
    }

    private void ShootSettingEditor()
    {
        EditorGUILayout.Space();

        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        shootSettingFoldout = EditorGUILayout.Foldout(shootSettingFoldout, new GUIContent("Shoot Setting"), true);
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (shootSettingFoldout)
        {
            EditorGUILayout.PropertyField(ballPrefab, new GUIContent("Ball Prefab"));
        }

        EditorPrefs.GetBool("shootSettingFoldout", shootSettingFoldout);
    }
}
