using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;
    private const int NUM_OF_TILES = 8;  //a chessboard is 8x8

    private int selectionX = -1;
    private int selectionY = -1;


	// Use this for initialization
	private void Start () {
        DrawChessboard();
	}
	
	// Update is called once per frame
	private void Update () {
        UpdateUnitSelection();
	}

    private void UpdateUnitSelection()
    {
        if (!Camera.main)
        {
            return;
        }

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
            {
                //Debug.Log(hit.point);
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;

                Debug.Log(hit.point);
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        }
        
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * NUM_OF_TILES;
        Vector3 heightLine = Vector3.forward * NUM_OF_TILES;

        Material outlinedMaterial = (Material)Resources.Load("OutlinedMaterial", typeof(Material));
        Renderer rend = GetComponent<Renderer>();
        rend.enabled = true;

        //Draw the board cells
        for (int i = 0; i < NUM_OF_TILES; i++)
        {
            Vector3 lineStart = Vector3.forward * i;

            
            Debug.DrawLine(lineStart, lineStart + widthLine);
            for (int j = 0; j < NUM_OF_TILES; ++j)
            {
                lineStart = Vector3.right * j;
                Debug.DrawLine(lineStart, lineStart + heightLine);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(1, 0.1f, 1);
                cube.transform.Translate(j + TILE_OFFSET, 0, i + TILE_OFFSET);
                cube.tag = "Cell";
                
                if ((i % 2 == 1 && j % 2 == 0) || (i % 2 == 0 && j % 2 == 1))
                {
                    cube.GetComponent<Renderer>().material.color = new Color(0.2f, 0, 0, 1);
                }

            }
        }


    }

    private void DrawBoardSelection()
    {
        //Draw the selected cell

        if (selectionX >= 0 && selectionY >= 0)
        {
            
            //Debug.Log(selectionX);
            //Debug.Log(selectionY);
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));


        }
    }
}
