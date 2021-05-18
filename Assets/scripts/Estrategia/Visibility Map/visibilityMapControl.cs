using UnityEngine;
using System.Collections.Generic;

public class visibilityMapControl : MonoBehaviour
{
	[SerializeField]
	private Transform bottomLeft;
	
	[SerializeField]
	private Transform upperRight;
	
	[SerializeField]
	private float gridSize = 1;
	
	VisibilityMap visibilityMap;

	[SerializeField]
	private GridDisplayV display;

	[SerializeField] 
	private Grid gridMap;


	public void CreateMap() {
		visibilityMap = new VisibilityMap(gridMap);
		display.SetGridData(visibilityMap);
		display.CreateMesh(bottomLeft.position, gridSize);
	}

	public void Initialize() {
		CreateMap();
		
	}

}
