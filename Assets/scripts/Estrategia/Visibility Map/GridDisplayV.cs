using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// takes care of creating the mesh that will display the influence map


public class GridDisplayV : MonoBehaviour
{
	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	Mesh mesh;

	GridData data;

	[SerializeField]
	Material material;
	
	[SerializeField]
	Color neutralColor = Color.black;
	
	[SerializeField]
	Color positiveColor = Color.red;
	
	[SerializeField]
	Color positive2Color = Color.red;
	
	[SerializeField]
	Color negativeColor = Color.blue;
	
	[SerializeField]
	Color negative2Color = Color.blue;

	[SerializeField]
	Color negative3Color = Color.blue;
	
	[SerializeField]
	Color negative4Color = Color.blue;
	
	Color[] colorsArray;

	public void SetGridData(GridData m)
	{
		data = m;
	}

	public void CreateMesh(Vector3 bottomLeftPos, float gridSize)
	{
		mesh = new Mesh();
		mesh.name = name;
		meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		
		meshFilter.mesh = mesh;
		meshRenderer.material = material;
		
		float objectHeight = transform.position.y;
		float staX = 0;
		float staZ = 0;
		
		// create squares starting at bottomLeftPos
		List<Vector3> verts = new List<Vector3>();
		for (int yIdx = 0; yIdx < data.Height; ++yIdx)
		{
			for (int xIdx = 0; xIdx < data.Width; ++xIdx)
			{
				
				Vector3 bl = new Vector3(staX + (xIdx * gridSize), objectHeight, staZ + (yIdx * gridSize));
				Vector3 br = new Vector3(staX + ((xIdx+1) * gridSize), objectHeight, staZ + (yIdx * gridSize));
				Vector3 tl = new Vector3(staX + (xIdx * gridSize), objectHeight, staZ + ((yIdx+1) * gridSize));
				Vector3 tr = new Vector3(staX + ((xIdx+1) * gridSize), objectHeight, staZ + ((yIdx+1) * gridSize));
				
				
				verts.Add(bl);
				verts.Add(br);
				verts.Add(tl);
				verts.Add(tr);
			}
		}
		
		List<Color> colors = new List<Color>();
		for (int yIdx = 0; yIdx < data.Height; ++yIdx)
		{
			for (int xIdx = 0; xIdx < data.Width; ++xIdx)
			{
				colors.Add(Color.white);
				colors.Add(Color.white);
				colors.Add(Color.white);
				colors.Add(Color.white);
			}
		}
		colorsArray = colors.ToArray();
		
		List<Vector3> norms = new List<Vector3>();
		for (int yIdx = 0; yIdx < data.Height; ++yIdx)
		{
			for (int xIdx = 0; xIdx < data.Width; ++xIdx)
			{
				norms.Add(Vector3.up);
				norms.Add(Vector3.up);
				norms.Add(Vector3.up);
				norms.Add(Vector3.up);
			}
		}
		
		List<Vector2> uvs = new List<Vector2>();
		for (int yIdx = 0; yIdx < data.Height; ++yIdx)
		{
			for (int xIdx = 0; xIdx < data.Width; ++xIdx)
			{
				uvs.Add(new Vector2(0, 0));
				uvs.Add(new Vector2(1, 0));
				uvs.Add(new Vector2(0, 1));
				uvs.Add(new Vector2(1, 1));
			}
		}
		
		List<int> tris = new List<int>();
		for (int idx = 0; idx < verts.Count; idx+=4) {

			int bl = idx;
			int br = idx+1;
			int tl = idx+2;
			int tr = idx+3;

			
			tris.Add(bl);
			tris.Add(tl);
			tris.Add(br);
			
			tris.Add(tl);
			tris.Add(tr);
			tris.Add(br);
		}

		mesh.vertices = verts.ToArray();
		mesh.normals = norms.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.colors = colorsArray;
		mesh.triangles = tris.ToArray();
	}
	
	void SetColor(int x, int y, Color c)
	{
		int idx = ((y * data.Width) + x) * 4;
		colorsArray[idx] = c;
		colorsArray[idx+1] = c;
		colorsArray[idx+2] = c;
		colorsArray[idx+3] = c;
	}

	void Update()
	{
	if(data == null){

	}
	else{
		for (int yIdx = 0; yIdx < data.Height; ++yIdx)
		{
			for (int xIdx = 0; xIdx < data.Width; ++xIdx)
			{
				Nodo nodo = data.GetValue(xIdx, yIdx);
				Color c = neutralColor;
                switch(nodo.costeNodoVisibilidad()){
                    case 8:
                        c = Color.Lerp(positiveColor, positive2Color, 0.5f);
                        break;
                    case 24:
                        c = Color.Lerp(negativeColor, negative2Color,0.5f);
                        break;
                    case 48:
                        c = Color.Lerp(negative3Color, negative4Color,0.5f);
                        break;
                    case 0:
                        c = Color.Lerp(neutralColor, neutralColor,0.5f);
                        break;
                        }
				
				SetColor(xIdx, yIdx, c);
			}
		}
		
		mesh.colors = colorsArray;
		}
	}
}
