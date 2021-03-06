﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshTileMap : MonoBehaviour {

    //public int tileCountX = 10;                     // number of tiles horizontally
    //public int tileCountZ = 10;                     // number of tiles vertically
    //public float tileSize = 1f;                     // size of a tile in unity world space

    //public Texture2D[] spriteSheet;                 // contains 2d textures to be drawn ?
    //public int tileResolution;                      // resolution of a single texture in pixels

    public float tileUnit = 0.125f;
    private TextAsset mapFile;
    public Player player;
    public GameObject enemyContainer;
    public GameObject wallContainer;
    public Enemy enemyPrefab;
    public int emptyTileId = 22;
    public AnimatedText mapTitle;

    [HideInInspector]
    public Vector2[] tiles;
    
    /*Vector2 tileWood = new Vector2(0, 0);
    Vector2 tileMud = new Vector2(1, 0);
    Vector2 tileWater = new Vector2(3, 3);
    Vector2 tileFloor = new Vector2(2, 0);*/

    // This first list contains every vertex of the mesh that we are going to render
    private List<Vector3> vertices = new List<Vector3>();

    // Normals
    private List<Vector3> normals = new List<Vector3>();

    // The triangles tell Unity how to build each section of the mesh joining the vertices
    private List<int> triangles = new List<int>();

    // The UV list is unimportant right now but it tells Unity how the texture is
    // aligned on each polygon
    private List<Vector2> uv = new List<Vector2>();
    private int squareCount;
    private Mesh mesh;
    MapData mapData;

    void Start () {
        //mapFileName = "Assets/Maps/level1.tmx";
        //GenerateMesh();
    }

    void GetTiles()
    {
        // Unity & Tiled texture mapping:
        /*
         Unity:
         x -> 0  1  2  3 
           .-------------.
         0 | 12 13 14 15 |
         1 | 09 10 11 11 |
         2 | 04 05 06 07 |
         3 | 00 01 02 03 |
           '-------------'
         y
         
         so:
         x = 3, y = 3 is 03
         x = 3, y = 0 is 15
         
         Tiled:

         x -> 0  1  2  3 
           .-------------.
         0 | 00 01 01 03 |
         1 | 04 05 06 07 |
         2 | 08 09 10 11 |
         3 | 12 13 14 15 |
           '-------------'
         y
        */
        int size = (int)(1f / tileUnit);
        tiles = new Vector2[size * size];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                //print("(" + (x) + ", " + (size - z - 1) + ") "+(x + size * z));
                tiles[x + size * z] = new Vector2(x, size - z - 1);
            }
        }
    }

    public void SetMap(TextAsset map)
    {
        /*if (Debug.isDebugBuild)
        {
            this.mapFileName = "Assets/maps/" + mapName;
        }*/

        this.mapFile = map;
        
    }

    public void GenerateMesh()
    {
        //mesh = GetComponent<MeshFilter>().mesh;
        //PurgeData();
        GetTiles();
        
        LoadMap();
        //BuildMesh();
        UpdateMesh();
        mapTitle.AnimateTitle(mapData.mapTitle);
    }

    /*private void PurgeData()
    {
        Destroy(GameObject.Find("LevelEndTrigger"));
        foreach (Transform child in enemyContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in wallContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }*/

    void LoadMap()
    {

        mapData = new MapData(mapFile, enemyContainer, wallContainer, enemyPrefab, player, GetComponent<MeshRenderer>().sharedMaterial);


        /*print(mapData.tileCount);
        print(mapData.tileSetName);
        print(mapData.horizontal_tiles);
        print(mapData.vertical_tiles);*/
        for (int i = 0; i < mapData.tiles.Length; i++)
        {
            // WARGNING: may hang unity (MANY prints!)
            //print(mapData.tiles[i].tileSetId + " " + mapData.tiles[i].x + " " + mapData.tiles[i].y + "-----------------");
            //print("");
            //print(mapData.tiles[i].tileSetId);
            GenerateSquare(mapData.tiles[i].x, mapData.tiles[i].y, mapData.tiles[i].tileSetId);
        }
        /*foreach(MapSquare mapSquare in mapData.tiles ){
            print("\n---");
            print(mapSquare.tileSetId);
            print(mapSquare.x);
            print(mapSquare.y);
            print("---");
        }*/
        
    }


    /*void BuildMesh()
    {
        for (int z = 0; z < tileCountZ; z++)
        {
            for (int x = 0; x < tileCountX; x++)
            {
                int randomItem = Random.Range(0, tiles.Length);
                //Debug.Log(randomItem);
                GenerateSquare(x, z, tiles[randomItem]);
            }
        }
    }*/

    void GenerateSquare(int x, int z, int textureId)
    {

        Debug.Log(textureId);


        Vector3 normal = Vector3.up;
        if(textureId == this.emptyTileId){
            Debug.Log("empty tile!");
            //normal = Vector3.down;
        }
        else
        {
            Vector2 texture = tiles[textureId - 1];
            vertices.Add(new Vector3(-x, 0, z));
            vertices.Add(new Vector3(-x + 1, 0, z));
            vertices.Add(new Vector3(-x + 1, 0, z - 1));
            vertices.Add(new Vector3(-x, 0, z - 1));
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);

            triangles.Add(squareCount * 4);
            triangles.Add((squareCount * 4) + 1);
            triangles.Add((squareCount * 4) + 3);
            triangles.Add((squareCount * 4) + 1);
            triangles.Add((squareCount * 4) + 2);
            triangles.Add((squareCount * 4) + 3);

            /*uv.Add(new Vector2(tileUnit * texture.x, tileUnit * texture.y + tileUnit));
            uv.Add(new Vector2(tileUnit * texture.x + tileUnit, tileUnit * texture.y + tileUnit));
            uv.Add(new Vector2(tileUnit * texture.x + tileUnit, tileUnit * texture.y));
            uv.Add(new Vector2(tileUnit * texture.x, tileUnit * texture.y));*/


            uv.Add(new Vector2(tileUnit * texture.x + tileUnit, tileUnit * texture.y));
            uv.Add(new Vector2(tileUnit * texture.x, tileUnit * texture.y));
            uv.Add(new Vector2(tileUnit * texture.x, tileUnit * texture.y + tileUnit));
            uv.Add(new Vector2(tileUnit * texture.x + tileUnit, tileUnit * texture.y + tileUnit));
            squareCount++;
        }

        
    }


    void UpdateMesh() {
        mesh = new Mesh();
        //mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();
        //mesh.Optimize();
        //mesh.RecalculateNormals();


        squareCount = 0;
        // Assign our mesh to our filter/renderer/collider
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        vertices.Clear();
        triangles.Clear();
        normals.Clear();
        uv.Clear();
        //tiles.Clear();

        Debug.Log ("Done Mesh!");
    }
    
    
}
