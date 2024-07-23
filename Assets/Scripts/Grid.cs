using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int length;

    [SerializeField] private int height;

    [SerializeField] private float tileLength;

    [SerializeField] private float tileHeight;
    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private Color laneHighlightColor;

    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
    
    public static Grid Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
    void Start()
    {
        for (float y = 0; y < height; y += tileHeight)
        {
            for (float x = 0; x < length; x += tileLength)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                tile.transform.localScale = new Vector3(tileLength, tileHeight, 1);
                tiles.Add(new Vector2(x, y), tile.GetComponent<Tile>());
            }
        }
    }

    public Vector3 GetTileSize()
    {
        return new Vector3(tileLength, tileHeight, 1);
    }

    Vector2 RoundVector(Vector2 pos)
    {
        float x = Mathf.Round(pos.x / tileLength) * tileLength;
        float y = Mathf.Round(pos.y / tileHeight) * tileHeight;
        return new Vector2(x, y);
    }
    
    public Tile GetTileAtPos(Vector2 pos)
    {
        pos = RoundVector(pos);
        return tiles.GetValueOrDefault(pos);
    }
    
    public void HighlightLane(int lane)
    {
        lane = height - 1 - lane;
        for (int w = 0; w < length; w++)
        {
            Vector2 pos = new Vector2(w, lane);
            SpriteRenderer tileRender = tiles.GetValueOrDefault(pos).GetComponent<SpriteRenderer>();
            tileRender.color = laneHighlightColor;
        }
    }

    public void DisableHightLight(int lane)
    {
        for (int w = 0; w < length; w++)
        {
            Vector2 pos = new Vector2(w, lane);
            SpriteRenderer tileRender = tiles.GetValueOrDefault(pos).GetComponent<SpriteRenderer>();
            tileRender.color = Color.white;
        }
    }
}
