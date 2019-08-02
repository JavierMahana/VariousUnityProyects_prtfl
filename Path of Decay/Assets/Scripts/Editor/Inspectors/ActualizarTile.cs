using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Casilla))]
public class ActualizarTile : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Casilla casilla = (Casilla)target;
        Terreno TCasilla = casilla.terreno;
        SpriteRenderer SRCasilla = casilla.GetComponent<SpriteRenderer>();

        if (GUILayout.Button("Actualizar Sprite"))
        {
            SRCasilla.sprite = TCasilla.visuals;
        }
    }
}
