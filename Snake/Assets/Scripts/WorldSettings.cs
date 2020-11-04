using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public int cellNumber = 10;
    public float cellSize = 1;
    private Transform _grid;

    public float PlaneSize => cellNumber / cellSize / 10; // Plane is 10x10

    public static WorldSettings Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _grid = GameObject.FindGameObjectWithTag("Grid").transform;
        ScaleGrid();
    }

    public void ScaleGrid() { _grid.localScale = new Vector3(PlaneSize, PlaneSize, PlaneSize); }
}