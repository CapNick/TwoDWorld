using System.Collections;
using System;
using UnityEngine;
public class Filters {

	public static float[,] meanFilter(float[,] image, int width, int height){
		float[,] filteredGraph = image;
		for(int x = 1; x < width; x++){// to avoid null pointer exceptions we are accessing the array 1 in on both x and y.
			for(int y = 1; y < height; y++){
				//Debug.Log ("Accessing array ["+x+"]["+y+"]");
				try{
					float total = (image [x,y] * 4) + (image [x+1,y] * 2) + (image [x-1,y] * 2) 
						+ (image [x,y-1] * 2) + image [x+1,y-1] + image [x-1,y-1] 
						+ (image [x,y+1] * 2) + image [x+1,y+1] + image [x-1,y+1];
					filteredGraph [x,y] = total / 16 ;
				}
				catch(IndexOutOfRangeException){
					filteredGraph [x,y] = image [x,y];
				}



				//float total = 0;
				//total += graph [x] [y]; //1
				//total += graph [x+1] [y];
				//total += graph [x-1] [y];
				//total += graph [x] [y-1];
				//total += graph [x+1] [y-1];
				//total += graph [x-1] [y-1];
				//total += graph [x] [y+1];
				//total += graph [x+1] [y+1];
				//total += graph [x-1] [y+1] ; //9
				//total = total / 9 ;
			}
		}
		return filteredGraph;
	}
		


}
