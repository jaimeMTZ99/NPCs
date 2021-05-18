﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityMap : GridData
{
	public int Width { get{ return gridMap.Nodos.GetLength(0); } }
	public int Height { get{ return gridMap.Nodos.GetLength(1); } }
	public Nodo GetValue(int x, int y)
	{
		return gridMap.Nodos[x, y];
	}

	[SerializeField] 
	private Grid gridMap;

	
	public VisibilityMap(Grid grid)
	{
        Debug.Log(grid);
		gridMap = grid;
        Debug.Log(gridMap);
	}



}