using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
	public enum TypeTile { water, ile, forest, deepWater};
	public enum Position { gauche_haut, haut, droite_haut, gauche, centre, droite, gauche_bas, bas, droite_bas, none};

	[Header("Parametre")]
	[Space]
	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;

	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;
	
	[Header("Filtre")]
	[Space]
	[SerializeField] private FiltreStruct[] filtres;
	[SerializeField] private bool noFiltre;

	[Header("Tile")]
	[Space]
	[SerializeField] private TileMap[] tiles;
	
	[SerializeField] private Tilemap water;
	[SerializeField] private Tilemap iles;
	[SerializeField] private Tilemap forest;
	
	[Header("affichage")]
	[Space]
	public TerrainType[] regions;

	public void GenerateMap() {
		//clear
		water.ClearAllTiles();
		iles.ClearAllTiles();
		forest.ClearAllTiles();

		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        if (!noFiltre)
        {
			noiseMap = Filtre.Apply(noiseMap, Filtre.MakeFiltre(filtres[0], mapWidth, mapHeight), mapWidth, mapHeight);
			noiseMap = Filtre.Apply(noiseMap, Filtre.MakeFiltre(filtres[1], mapWidth, mapHeight), mapWidth, mapHeight);
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						Position p;
						switch (regions[i].type)
                        {
							case TypeTile.water :
								water.SetTile(new Vector3Int(x - (mapWidth / 2), y - (mapHeight / 2), 0), tiles[0].tile);
								break;
							case TypeTile.deepWater :
								water.SetTile(new Vector3Int(x - (mapWidth / 2), y - (mapHeight / 2), 0), tiles[1].tile);
								break;
							case TypeTile.ile :
								iles.SetTile(new Vector3Int(x - (mapWidth / 2), y - (mapHeight / 2), 0), tiles[2].tile);
								break;
							case TypeTile.forest :
								forest.SetTile(new Vector3Int(x - (mapWidth / 2), y - (mapHeight / 2), 0), tiles[3].tile);
								break;
						}
						break;
					}
				}
			}
		}
	}

	void OnValidate() {
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public MapGenerator.TypeTile type;
}

[System.Serializable]
public struct TileMap
{
	public MapGenerator.TypeTile type;
	public MapGenerator.Position position;
	public Tile tile;
}
[System.Serializable]
public struct FiltreStruct
{
	public enum TypeFiltre { centre, bord };
	public AnimationCurve moditication;
	public TypeFiltre type;
}