using UnityEngine;
using System.Collections.Generic;

public class InfluenceMapControl : MonoBehaviour
{
	[SerializeField]
	Transform bottomLeft;
	
	[SerializeField]
	Transform upperRight;
	
	[SerializeField]
	float gridSize = 1;
	
	[SerializeField]
	float decay = 0.1f;
	
	[SerializeField]
	float momentum = 1f;
	
	[SerializeField]
	float updateFrequency = 10;
	
	InfluenceMap influenceMap;

	[SerializeField]
	GridDisplay display;

	[SerializeField] 
	private Grid gridMap;

	[SerializeField]
	private List<SimplePropagator> propagators;

	void CreateMap(int x, int z) {
		// how many of gridsize is in Mathf.Abs(upperRight.positon.x - bottomLeft.position.x)
		int width = x;
		int height = z;
		
		//Debug.Log(width + " x " + height);
		
		influenceMap = new InfluenceMap(width, height, decay, momentum, gridMap);
		
		display.SetGridData(influenceMap);
		display.CreateMesh(bottomLeft.position, gridSize);
	}

	public Vector2I GetGridPosition(Vector3 pos)
	{
		//int x = (int)((pos.x - bottomLeft.position.x)/gridSize);
		//int y = (int)((pos.z - bottomLeft.position.z)/gridSize);
		var cellPosition =gridMap.GetNodoPosicionGlobal(pos);
		

		return new Vector2I((int)cellPosition.Posicion.x, (int)cellPosition.Posicion.z);
	}

	
	public void Initialize(int x, int z) {
		CreateMap(x, z);
		
		foreach (var propagator in propagators) {
			influenceMap.RegisterPropagator(propagator);
		}
		
		InvokeRepeating(nameof(PropagationUpdate), 0.001f, 1.0f/updateFrequency);
	}

	void PropagationUpdate()
	{
		influenceMap.Propagate();
	}

	void SetInfluence(int x, int y, float value)
	{
		influenceMap.SetInfluence(x, y, value);
	}

	void SetInfluence(Vector2I pos, float value)
	{
		influenceMap.SetInfluence(pos, value);
	}

	void Update()
	{
		influenceMap.Decay = decay;
		influenceMap.Momentum = momentum;
		
	}
}
