using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	
	public int width = 8;
	public int height = 8;
	[Range(0, 6)]
	public int octave = 1;
	[Range(0, 1)]
	public float frequency = 1f;
	[Range(0, 1)]
	public float persistance = 0.7f;
	public Sprite[] terrain;
	public static WorldController Instance { get; protected set; }
	public World World { get; protected set; }

	float[][] filterNoise;

    // Use this for initialization
    void Start () {
		if(Instance != null) {
			Debug.LogError("There should never be two world controllers.");
		}
		Instance = this;
		World = new World(width,height);
		float[][] perlinNoise = PerlinNoise.GeneratePerlinNoise (width, height, octave, frequency, persistance);
		//filterNoise = Filters.meanFilter (perlinNoise, width, height);
		//filterNoise = Filters.meanFilter (filterNoise, width, height);
		filterNoise = perlinNoise;
		placeTiles ();
    }

	private void placeTiles(){
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				//Get The tile data
				Tile tile_data = World.GetTileAt(x, y);
				//Creates the game object which is going to contain the the tile and adds it to the scene
				GameObject tile_gameobject = new GameObject();
				tile_gameobject.name = "Tile_" + x + "_" + y;
				tile_gameobject.transform.position = new Vector3( tile_data.X, tile_data.Y, 0);
				tile_gameobject.transform.SetParent(this.transform, true);

				tile_gameobject.AddComponent<SpriteRenderer>();
				makeTerrian (x, y, tile_gameobject);

			}
		}
	}

	private void makeTerrian(int x, int y, GameObject tile_gameobject){
		float noiseNumber = filterNoise [x] [y];
		World.GetTileAt (x,y).setValue(noiseNumber);
		//Debug.Log ("noise["+x+"]["+y+"] = "+noiseNumber);
		if(noiseNumber < 0.5){
			tile_gameobject.GetComponent<SpriteRenderer> ().sprite = terrain [0];
			tile_gameobject.GetComponent<SpriteRenderer> ().color = new Color(noiseNumber,noiseNumber,noiseNumber);
		}
		else if (noiseNumber < 0.53){
			//GetTileAtWorldCoord (x, y).Type = Tile.TileType.Land;
			tile_gameobject.GetComponent<SpriteRenderer> ().sprite = terrain [1];
			tile_gameobject.GetComponent<SpriteRenderer> ().color = new Color(noiseNumber,noiseNumber,noiseNumber);
		}
		else{
			//GetTileAtWorldCoord (x, y).Type = Tile.TileType.Land;
			tile_gameobject.GetComponent<SpriteRenderer> ().sprite = terrain [2];
			tile_gameobject.GetComponent<SpriteRenderer> ().color = new Color(noiseNumber,noiseNumber,noiseNumber);
		}
	}
		
		
	/// <summary>
	/// Gets the tile at the unity-space coordinates
	/// </summary>
	/// <returns>The tile at world coordinate.</returns>
	/// <param name="coord">Unity World-Space coordinates.</param>
	public Tile GetTileAtWorldCoord(Vector3 coord) {
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);
		return World.GetTileAt(x, y);
	}
}
