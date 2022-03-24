using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    public Enums.Biomes biome;
    public int brushsize;

    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<TileData> tileDatas;

    public TileBase[] tiles;
   

    //public TileBase grassTile;
    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map.CompressBounds();
    }

    public void paint(Vector3Int pos, TileBase tile)
    {
        for (int x = -brushsize; x <= brushsize; x++)
        {
            for (int y = -brushsize; y <= brushsize; y++)
            {
                if (map.GetTile(new Vector3Int(pos.x + x, pos.y + y, 0)) != null && map.GetTile(new Vector3Int(pos.x + x, pos.y + y, 0)) != tiles[4])
                {

                map.SetTile(new Vector3Int(pos.x + x, pos.y + y, 0), tile);
                }
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3Int gridpos = map.WorldToCell(mousePos);

            switch (biome)
            {
                case Enums.Biomes.Plains:
                    paint(gridpos, tiles[0]);
                    break;
                case Enums.Biomes.Snow:
                    paint(gridpos, tiles[1]);
                    break;
                case Enums.Biomes.Sand:
                    paint(gridpos, tiles[2]);
                    break;
                case Enums.Biomes.Forest:
                    paint(gridpos, tiles[3]);
                    break;
            }
            

        }
    }

    public TileData getTileData(Vector3 pos)
    {  
        Vector3Int gridpos = map.WorldToCell(pos);
        TileBase tile = map.GetTile(gridpos);
        if (tile == null)
        {
            return null;
        }
        else
        {
            return dataFromTiles[tile];
        }
    }


}
