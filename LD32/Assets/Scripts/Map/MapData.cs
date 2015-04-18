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

    public MapData(string mapFile, GameObject enemyContainer, Enemy enemyPrefab, Player player)
    {
        foreach(Transform child in enemyContainer.transform){
            if (Application.isPlaying)
            {
                GameObject.Destroy(child.gameObject);
            }
            else
            {
                Debug.Log("Destroying...");
                GameObject.DestroyImmediate(child.gameObject);
            }
            
        }
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
        enemies = new Enemy[enemyCount];
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
            enemies[i] = enemyObject;
        }

        int startEndCount = map.ObjectGroups[1].Objects.Count;
        enemies = new Enemy[enemyCount];
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
                player.Spawn(worldPos);
            }
            else if (startEnd.Name == "End")
            {
                LevelEndTrigger end = (LevelEndTrigger)GameObject.Instantiate(Resources.Load("LevelEndTrigger"), worldPos, Quaternion.identity);
            }
            //Vector3 worldPos = new Vector3(-(enemyX / tile_width), 0.01f, enemyY / tile_height);

            //GetRelativePosition(enemyX, enemyY);
            //Enemy enemyObject = (Enemy)GameObject.Instantiate(enemyPrefab, worldPos, enemyPrefab.transform.rotation);
            //enemyObject.transform.parent = enemyContainer.transform;
            //enemyObject.transform.localPosition = worldPos;
            //enemies[i] = enemyObject;
        }
    }

    public Vector3 GetRelativePosition(int x, int y){
        Vector3 position = new Vector3(1, 2, 3);
        int map_width = tile_width * horizontal_tiles;
        int map_height = tile_height * vertical_tiles;

        int x_tilepos = x / tile_width;
        int y_tilepos = y / tile_height;
        Debug.Log("[" + x + ", " + y + "] is [" + x_tilepos + ", " + y_tilepos + "]");

        return position;
    }
}
