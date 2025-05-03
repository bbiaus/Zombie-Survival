using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainRockConverter))]
public class TerrainRockConverterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainRockConverter script = (TerrainRockConverter)target;
        if (GUILayout.Button("Convertir Piedras a Objetos con Collider"))
        {
            script.ConvertRocks();
        }
    }
}

