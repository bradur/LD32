using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MeshTileMap))]
public class MeshTileMapEditor : Editor {
    
    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        DrawDefaultInspector();
        
        if(GUILayout.Button("Reload")) {
            MeshTileMap meshTileMap = (MeshTileMap)target;
            meshTileMap.GenerateMesh();
        }
    }
}
