using UnityEngine;
using System.Collections;

public class World {

	// A two-dimensional array to hold our tile data.
	Tile[,] tiles;

	// The tile width of the world.
	public int Width { get; protected set; }

	// The tile height of the world
	public int Height { get; protected set; }

	public World(int width = 100, int height = 100) {
		this.Width = width;
		this.Height = height;

		tiles = new Tile[width,height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles[x,y] = new Tile(x, y);
				tiles [x, y].Type = Tile.TileType.Water;
			}
		}

		Debug.Log ("World created with " + (width*height) + " tiles.");
	}
		
	public Tile GetTileAt(int x, int y) {
		if( x > Width || x < 0 || y > Height || y < 0) {
			Debug.LogError("Tile ("+x+","+y+") is out of range.");
			return null;
		}
		return tiles[x, y];
	}

}
