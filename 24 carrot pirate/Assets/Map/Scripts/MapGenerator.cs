using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
	public enum TypeTile { water, ile, forest, deepWater};
	public enum Position { gauche_haut, haut, droite_haut, gauche, centre, droite, gauche_bas, bas, droite_bas, none};

	[Header("Parametre	")]
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

	[SerializeField] private Filtre[] settingsFiltre;


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

		float[,] noiseMap = filtre(Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset));

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
								water.SetTile(new Vector3Int(x, y, 0), tiles[19].tile);
								break;
							case TypeTile.deepWater :
								water.SetTile(new Vector3Int(x, y, 0), tiles[18].tile);
								break;
							case TypeTile.ile:
								iles.SetTile(new Vector3Int(x, y, 0), tiles[7].tile);
								break;
							case TypeTile.forest:
								forest.SetTile(new Vector3Int(x, y, 0), tiles[10].tile);
								break;
						}
						break;
					}
				}
			}
		}
	}

	public float[,] filtre(float[,]noiseMap)
    {
		float[,] finalNoise = new float[mapWidth, mapHeight];
		float[,] filtreBorder = new float[mapWidth, mapHeight];
		float[,] filtreCentre = new float[mapWidth, mapHeight];

		//init filtre border
		/*for(int i = 0; i < this.settingsFiltre.Length; i++)
        {
			for (int y = 0; y < mapHeight; y++)
			{
				for (int x = 0; x < mapWidth; x++)
				{
					
				}
			}
		}

		//application des filtres
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				finalNoise[x, y] = noiseMap[x, y] * filtreBorder[x, y];
			}
		}*/

		return noiseMap;
    }

	public Position getPostion(float[,] noiseMap, int x, int y, float h)
	{
		bool gauche = true, haut = true, droite = true, bas = true;
		Position p;
		int config = 0;
		if (x != 0 && noiseMap[x - 1, y] >= h)
			config += (int)Mathf.Pow(2, 0);
		if (y != 0 && noiseMap[x, y - 1] >= h)
			config += (int)Mathf.Pow(2, 1);
		if (x != mapWidth && noiseMap[x + 1, y] >= h)
			config += (int)Mathf.Pow(2, 2);
		if (y != mapHeight && noiseMap[x, y + 1] >= h)
			config += (int)Mathf.Pow(2, 3);

		//1100
		if (config == 12)
			p = Position.gauche_haut;
		//1000
		else if (config == 8)
			p = Position.haut;
		//1001
		else if (config == 9)
			p = Position.droite_haut;
		//0001
		else if (config == 1)
			p = Position.droite;
		//0100
		else if (config == 4)
			p = Position.gauche;
		//0110
		else if (config == 6)
			p = Position.gauche_bas;
		//0010
		else if (config == 2)
			p = Position.bas;
		//0011
		else if (config == 3)
			p = Position.droite_bas;
		else
			p = Position.centre;

		return p;
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
