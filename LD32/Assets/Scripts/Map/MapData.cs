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
    private GameObject enemyPrefab;

    public MapData(string mapFile, GameObject enemyContainer)
    {
        enemyPrefab = (GameObject)Resources.Load("Enemy");
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
            Debug.Log("object at [" + enemy.X + ", " + enemy.Y + "]");
            Vector3 worldPos = new Vector3((float)enemy.X, 0, (float)enemy.Y);

            //Enemy enemyObject = (Enemy)GameObject.Instantiate(enemyPrefab, worldPos, Quaternion.identity);
            //enemyObject.transform.parent = enemyContainer.transform;
            //enemies[i] = enemyObject
        }
        
    }

    public Vector3 GetRelativePosition(int x, int y){
        Vector3 position = new Vector3(1, 2, 3);
        int map_width = tile_width * horizontal_tiles;
        int map_height = tile_height * vertical_tiles;



        return position;
    }
}
