using UnityEngine;
using System.Collections;
using System;

public class Tile {

	public enum TileType { Water, Land };
	private float tileValue;

	LooseObject looseObject;
	InstallableObject installedObject;

	public int X { get; protected set; }
	public int Y { get; protected set; }
	public TileType Type { get; set; }

	public Tile( int x, int y ) {
		this.X = x;
		this.Y = y;
		tileValue = 0;
	}

	public float getValue(){
		return tileValue;
	}

	public void setValue(float value){
		this.tileValue = value;
	}

}
