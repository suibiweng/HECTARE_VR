using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
	private int height = 10;
	private int width = 10;
	private float GridSpaceSize = 2f;
	
	[SerializeField] private GameObject gridCellPrefab;
	private GameObject[,] gameGrid;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
    	gameGrid = new GameObject[height, width];
    	if(gridCellPrefab == null){
    		Debug.LogError("Error: Grid cell prefab not found");
    		return;  
    	}
    	for(int y=0;y<height;y++){
    	    for(int x=0;x<width;x++){
    	    	gameGrid[x,y] = Instantiate(gridCellPrefab, new Vector3(x*GridSpaceSize, y*GridSpaceSize), Quaternion.identity);
    	    	gameGrid[x,y].transform.parent=transform; 
    	    	gameGrid[x,y].gameObject.name = "Grid space";
    	    }
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

