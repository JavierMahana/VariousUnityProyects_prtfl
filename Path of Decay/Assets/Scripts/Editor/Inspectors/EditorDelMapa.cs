using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class EditorDelMapa : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelGenerator lg = (LevelGenerator)target;
        GameObject lgGO = lg.gameObject;

        if (GUILayout.Button("Create Map!"))
        {
            if (lg.mapContainerGO != null)
            {
                return;
            }
            lg.GenerateMap();
        }

        if (GUILayout.Button("Destroy Map!"))
        {

            if (lg.mapContainerGO == null)
            {
                return;
            }
            DestroyImmediate(lg.mapContainerGO);
        }
    }

}