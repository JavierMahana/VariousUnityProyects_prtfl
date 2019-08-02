using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitManager))]
public class InspectorUnitManager : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        

        if (GUILayout.Button("Update Units!"))
        {
            UnitManager.ActualizarTodasLasUnidades(GameObject.FindObjectOfType<GameManager>());
        }
    }
}
