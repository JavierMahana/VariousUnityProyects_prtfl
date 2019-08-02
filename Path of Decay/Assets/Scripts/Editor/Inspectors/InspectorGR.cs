using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GrowthRates))]
public class InspectorGR : Editor {
    
    public override void OnInspectorGUI()
    {

        GrowthRates gr = (GrowthRates)target;

        GUILayout.Label("HP Growth Rate");
        gr.HPGrowthRate = EditorGUILayout.Slider(gr.HPGrowthRate, 0f, 1f);

        GUILayout.Label("Strength Growth Rate");
        gr.StengthGrowthRate = EditorGUILayout.Slider(gr.StengthGrowthRate, 0f, 1f);

        GUILayout.Label("Defense Growth Rate");
        gr.DefenseGrowthRate = EditorGUILayout.Slider(gr.DefenseGrowthRate, 0f, 1f);

        GUILayout.Label("Speed Growth Rate");
        gr.SpeedGrowthRate = EditorGUILayout.Slider(gr.SpeedGrowthRate, 0f, 1f);
    }
}
