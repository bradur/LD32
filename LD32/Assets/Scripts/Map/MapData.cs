using TiledSharp;

public class MapData {

    public MapSquare[] tiles;
    public int horizontal_tiles;
    public int vertical_tiles;
    public string tileSetName;
    public int tileCount;

    public MapData(string mapFile)
    {
        TmxMap map = new TmxMap(mapFile);
        horizontal_tiles = map.Width;
        vertical_tiles = map.Height;

        tileSetName = map.Tilesets[0].Name;

        tileCount = map.Layers[0].Tiles.Count;
        tiles = new MapSquare[tileCount]; ;
        for (int i = 0; i < tileCount; i++)
        {
            tiles[i] = new MapSquare(map.Layers[0].Tiles[i]);
        }

    }

}
