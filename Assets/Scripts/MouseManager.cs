using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Mouse moves around and when it moves into a new box collider area, it sets the origin point
    of the grid. It can then know to how to snap to the grid even when there's a on-the-table grid
    and a on-the-floor grid.
*/

public class MouseManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer mouseMarkerSprite;
    [SerializeField] Color insideGridColor;
    [SerializeField] Color outsideColor;

    [Header("Grid Settings")]
    [SerializeField] GameObject linePrefab;
    [SerializeField] Vector2 horizontalAxis;
    [SerializeField] Vector2 verticalAxis;
    [SerializeField] Vector2 origin;
    [SerializeField] Vector2 gridSize;

    GridUtil grid;

    private void Start() {
        grid = new GridUtil(horizontalAxis, verticalAxis, origin, gridSize);
        grid.CreateLineGrid(linePrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (grid.IsOnGrid(mousePosition)) {
            mouseMarkerSprite.color = insideGridColor;
            mousePosition = grid.SnapToGrid(mousePosition);
        } else { 
            mousePosition.z = 0;
            mouseMarkerSprite.color = outsideColor;
        }
        mouseMarkerSprite.transform.position = mousePosition;
    }
}
