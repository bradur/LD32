using TiledSharp;
using UnityEngine;

public class MapData {

    public MapSquare[] tiles;
    public int horizontal_tiles;
    public int vertical_tiles;
    public int tile_width;
    public int tile_height;
    public string tileSetName;
    public Enemy[] enemies;
    public int tileCount;
    private Enemy enemyPrefab;
    private GameObject wallPref;

    public MapData(string mapFile, GameObject enemyContainer, GameObject wallContainer, Enemy enemyPrefab, Player player, Material tileSheet)
    {
        //enemyPrefab = Resources.Load("Enemy") as GameObject;
        TmxMap map = new TmxMap(mapFile);
        horizontal_tiles = map.Width;
        vertical_tiles = map.Height;

        tile_width = map.TileWidth;
        tile_height = map.TileHeight;

        tileSetName = map.Tilesets[0].Name;

        tileCount = map.Layers[0].Tiles.Count;
        tiles = new MapSquare[tileCount]; ;
        for (int i = 0; i < tileCount; i++)
        {
            tiles[i] = new MapSquare(map.Layers[0].Tiles[i]);
        }

        
        int enemyCount = map.ObjectGroups[0].Objects.Count;
        //enemies = new Enemy[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            TmxObjectGroup.TmxObject enemy = map.ObjectGroups[0].Objects[i];
            //Debug.Log("object at [" + enemy.X + ", " + enemy.Y + "]");
            int enemyX = (int)enemy.X / tile_width;
            int enemyY = (int)enemy.Y / tile_height;
            Vector3 worldPos = new Vector3(-enemyX, 0.01f, enemyY);

            //GetRelativePosition(enemyX, enemyY);
            Enemy enemyObject = (Enemy)GameObject.Instantiate(enemyPrefab, worldPos, enemyPrefab.transform.rotation);
            enemyObject.transform.parent = enemyContainer.transform;
            enemyObject.transform.localPosition = worldPos;
            //enemies[i] = enemyObject;
        }

        int startEndCount = map.ObjectGroups[1].Objects.Count;

        for (int i = 0; i < startEndCount; i++)
        {
            TmxObjectGroup.TmxObject startEnd = map.ObjectGroups[1].Objects[i];
            //Debug.Log("object at [" + enemy.X + ", " + enemy.Y + "]");
            int startEndX = (int)startEnd.X / tile_width;
            int startEndY = (int)startEnd.Y / tile_height;
            Vector3 worldPos = new Vector3(-startEndX + 0.5f, 1f, startEndY - 1.5f);
            Debug.Log(startEnd.Name);
            if (startEnd.Name == "Start")
            {
                player.Spawn(worldPos, tile_width, tile_height);
            }
            else if (startEnd.Name == "End")
            {
                GameObject endObject = (GameObject)GameObject.Instantiate(Resources.Load("LevelEndTrigger"), worldPos, Quaternion.identity);
                //LevelEndTrigger end = endObject.GetComponent<LevelEndTrigger>();
            }
            //Vector3 worldPos = new Vector3(-(enemyX / tile_width), 0.01f, enemyY / tile_height);

            //GetRelativePosition(enemyX, enemyY);
            //Enemy enemyObject = (Enemy)GameObject.Instantiate(enemyPrefab, worldPos, enemyPrefab.transform.rotation);
            //enemyObject.transform.parent = enemyContainer.transform;
            //enemyObject.transform.localPosition = worldPos;
            //enemies[i] = enemyObject;
        }


        int wallCount = map.ObjectGroups[2].Objects.Count;
        CreateWall(tileSheet);

        for (int i = 0; i < wallCount; i++)
        {
            TmxObjectGroup.TmxObject wall = map.ObjectGroups[2].Objects[i];
            //Debug.Log("object at [" + enemy.X + ", " + enemy.Y + "]");
            int startEndX = (int)wall.X / tile_width;
            int startEndY = (int)wall.Y / tile_height;
            Vector3 worldPos = new Vector3(-startEndX, 0.5f, startEndY);

            GameObject wallObject = (GameObject)GameObject.Instantiate(wallPref, worldPos, Quaternion.identity);
            wallObject.transform.parent = wallContainer.transform;
            wallObject.transform.localPosition = worldPos;
            //LevelEndTrigger end = endObject.GetComponent<LevelEndTrigger>();

            //Vector3 worldPos = new Vector3(-(enemyX / tile_width), 0.01f, enemyY / tile_height);

            //GetRelativePosition(enemyX, enemyY);
            //Enemy enemyObject = (Enemy)GameObject.Instantiate(enemyPrefab, worldPos, enemyPrefab.transform.rotation);
            //enemyObject.transform.parent = enemyContainer.transform;
            //enemyObject.transform.localPosition = worldPos;
            //enemies[i] = enemyObject;
        }
        if (Application.isPlaying)
        {
            GameObject.Destroy(wallPref);
        }
        
    }

    
    void CreateWall(Material wallMaterial)
    {
        wallPref = (GameObject)GameObject.Instantiate(Resources.Load("Wall"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        wallPref.GetComponent<Renderer>().material = wallMaterial;
        //Mesh mesh = wallPref.GetComponent<MeshFilter>().mesh;
        Mesh mesh = wallPref.GetComponent<MeshFilter>().sharedMesh;
        Vector2 texture = new Vector2(2f, 3f);
        float tileUnit = 0.25f;

        float left = tileUnit * texture.x;
        float right = left + tileUnit;
        float bottom = tileUnit * texture.y;
        float top = bottom + tileUnit;

        Rect wallText = new Rect(
            left,
            top,
            tileUnit,
            tileUnit
        );

        Vector2 text1 = new Vector2(wallText.x, wallText.y);
        Vector2 text2 = new Vector2(wallText.x + tileUnit, wallText.y);
        Vector2 text3 = new Vector2(wallText.x, wallText.y - tileUnit); ;
        Vector2 text4 = new Vector2(wallText.x + wallText.width, wallText.y - tileUnit);
        //print(text1 + ", " + text2 + "," + text3 + "," + text4);

        Vector2[] uv = new Vector2[mesh.uv.Length];
        uv = mesh.uv;
        // FRONT    2    3    0    1
        uv[2] = text1;
        uv[3] = text2;
        uv[0] = text3;
        uv[1] = text4;
        // BACK    6    7   10   11
        uv[6] = text1;
        uv[7] = text2;
        uv[10] = text3;
        uv[11] = text4;
        // LEFT   19   17   16   18
        uv[19] = text1;
        uv[17] = text2;
        uv[16] = text3;
        uv[18] = text4;
        // RIGHT   23   21   20   22
        uv[23] = text1;
        uv[21] = text2;
        uv[20] = text3;
        uv[22] = text4;

        // TOP    4    5    8    9
        uv[4] = text1;
        uv[5] = text2;
        uv[8] = text3;
        uv[9] = text4;

        // BOTTOM   15   13   12   14
        uv[15] = text1;
        uv[13] = text2;
        uv[12] = text3;
        uv[14] = text4;
        mesh.uv = uv;

    }

        /*
    public Vector3 GetRelativePosition(int x, int y){
        Vector3 position = new Vector3(1, 2, 3);
        int map_width = tile_width * horizontal_tiles;
        int map_height = tile_height * vertical_tiles;

        int x_tilepos = x / tile_width;
        int y_tilepos = y / tile_height;
        Debug.Log("[" + x + ", " + y + "] is [" + x_tilepos + ", " + y_tilepos + "]");

        return position;
    }*/
}
