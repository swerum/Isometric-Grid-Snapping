using UnityEngine;

public class GridUtil
{
    Vector3 horizontalAxis;
    Vector3 verticalAxis;
    Vector3 origin;
    Vector2 gridSize;

    public GridUtil(Vector2 horizontal, Vector2 vertical, Vector2 origin, Vector2 gridSize) {
        horizontalAxis =    new Vector3(horizontal.x, horizontal.y, 0);
        verticalAxis =      new Vector3(vertical.x, vertical.y, 0);
        //origin is the 
        this.origin =       new Vector3(origin.x, origin.y, 0);
        this.gridSize = gridSize;
    }

    public void CreateLineGrid(GameObject linePrefab, Transform lineParent) {
        Vector3 outlineOrigin = origin - 0.5f * horizontalAxis - 0.5f * verticalAxis;
        for (int i = 0; i <= gridSize.x; i++)
        {
            Vector3 startPoint = outlineOrigin + i * horizontalAxis;
            Vector3 endPoint = startPoint + gridSize.y * verticalAxis;
            CreateLine(startPoint, endPoint);
        }
        for (int j = 0; j <= gridSize.y; j++)
        {
            Vector3 startPoint = outlineOrigin + j * verticalAxis;
            Vector3 endPoint = startPoint + gridSize.x * horizontalAxis;
            CreateLine(startPoint, endPoint);
        }


        void CreateLine(Vector3 startPoint, Vector3 endPoint) {
            GameObject line = GameObject.Instantiate(linePrefab, lineParent);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPositions(new Vector3[2] { startPoint, endPoint}); 
        }
    }

    public Vector3 SnapToGrid(Vector3 pos) {
        Vector2 gridCoords = FindGridCoordinates(pos);
        Vector3 newPos = gridCoords.x * horizontalAxis + gridCoords.y * verticalAxis;
        return newPos + origin;
    }

    public bool IsOnGrid(Vector3 pos) {
        Vector2 gridCoords = FindGridCoordinates(pos);
        if (gridCoords.x < 0  || gridCoords.x >= gridSize.x) return false;
        if (gridCoords.y < 0  || gridCoords.y >= gridSize.y) return false;
        return true;
    }

    private Vector2Int FindGridCoordinates(Vector3 pos) {
        Vector3 p = pos - origin;
        Vector3 v = verticalAxis; Vector3 h = horizontalAxis;
        float verticalProjection = (h.x * p.y - h.y * p.x) / (v.y * h.x - v.x * h.y);
        float horizontalProjection =  (p.x - verticalProjection * verticalAxis.x) / h.x;
        return new Vector2Int(Mathf.RoundToInt(horizontalProjection), Mathf.RoundToInt(verticalProjection));
    }

}
