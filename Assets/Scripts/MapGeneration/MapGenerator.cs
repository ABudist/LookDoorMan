using UnityEngine;

namespace MapGeneration
{
  public class MapGenerator : MonoBehaviour
  {
    [SerializeField] private Wall _wallPrefab;
    [SerializeField] private Floor _floorPrefab;
    [SerializeField] private Exit _exitPrefab;

    private const float CELL_SIZE = 3;
    private const float CORNER_FACTOR = 0.7f;
    private const int MAX_WALL_LENGHT = 3;

    private Cell[,] _cells;
    private int _gridRows;
    private int _gridColumns;

    private Vector3 _playerSpawnPos;
    private Cell _exitCell;
    
    public LevelData GenerateMap(int rows, int columns)
    {
      _gridRows = rows;
      _gridColumns = columns;
      
      GenerateCells(rows, columns);
      CreateLabyrinth();
      FindGamePlaces();
      SpawnWalls();
      CreateFloor();
      
      return new LevelData(_playerSpawnPos);
    }

    private void CreateFloor()
    {
      Floor floor = Instantiate(_floorPrefab);
      floor.transform.position = new Vector3(0, -1,0);
      floor.transform.localScale = new Vector3(_gridColumns * CELL_SIZE, 1, _gridRows * CELL_SIZE);
      
      floor.Bake();
    }

    private void GenerateCells(int rows, int columns)
    {
      _gridColumns = columns;
      _gridRows = rows;
      _cells = new Cell[rows, columns];

      Vector3 startPosition = new Vector3(-CELL_SIZE * columns / 2 + CELL_SIZE / 2, 0, CELL_SIZE * rows / 2 - CELL_SIZE / 2);

      Vector3 position = startPosition;

      for (int row = 0; row < rows; row++)
      {
        for (int column = 0; column < columns; column++)
        {
          _cells[row, column] = new Cell(position, row, column);

          position.x += CELL_SIZE;
        }

        position.x = startPosition.x;
        position.z -= CELL_SIZE;
      }
    }

    private void SpawnWalls()
    {
      for (int y = 0; y < _cells.GetLength(1); y++)
      {
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
          Cell current = _cells[x, y];

          if (current.Exit)
          {
            if (current.Row == 0)
            {
              SpawnExit(current.Position + new Vector3(0, 0, CELL_SIZE / 2), Vector3.zero);
            }
            else if(current.Row == _gridRows - 1)
            {
              SpawnExit(current.Position - new Vector3(0, 0, CELL_SIZE / 2), Vector3.zero);
            }
            else if(current.Column == 0)
            {
              SpawnExit(current.Position - new Vector3(CELL_SIZE / 2, 0, 0), new Vector3(0, 90, 0));
            }
            else
            {
              SpawnExit(current.Position + new Vector3(CELL_SIZE / 2, 0, 0), new Vector3(0, 90, 0));
            }
            
            continue;
          }
          
          if (current.LeftWall)
          {
            SpawnWall(current.Position - new Vector3(CELL_SIZE / 2, 0, 0), new Vector3(0, 90, 0));
          }

          if (current.RightWall)
          {
            SpawnWall(current.Position + new Vector3(CELL_SIZE / 2, 0, 0), new Vector3(0, 90, 0));
          }

          if (current.UpWall)
          {
            SpawnWall(current.Position + new Vector3(0, 0, CELL_SIZE / 2), Vector3.zero);
          }

          if (current.BottomWall)
          {
            SpawnWall(current.Position - new Vector3(0, 0, CELL_SIZE / 2), Vector3.zero);
          }
        }
      }
    }

    private void SpawnWall(Vector3 position, Vector3 rotation)
    {
      Wall wall = Instantiate(_wallPrefab);
      wall.transform.position = position;
      wall.transform.rotation = Quaternion.Euler(rotation);
    }

    private void SpawnExit(Vector3 position, Vector3 rotation)
    {
      Exit exit = Instantiate(_exitPrefab);
      exit.transform.position = position;
      exit.transform.rotation = Quaternion.Euler(rotation);
    }

    private void FindGamePlaces()
    {
      switch (Random.Range(0, 4))
      {
        case 0:
          _playerSpawnPos = GetCellInLeftUpperCorner().Position;
          _exitCell = GetCellInRightBottomCorner();
          break;
        case 1:
          _playerSpawnPos = GetCellInRightUpperCorner().Position;
          _exitCell = GetCellInLeftBottomCorner();
          break;
        case 2:
          _playerSpawnPos = GetCellInRightBottomCorner().Position;
          _exitCell = GetCellInLeftUpperCorner();
          break;
        case 3:
          _playerSpawnPos = GetCellInLeftBottomCorner().Position;
          _exitCell = GetCellInRightUpperCorner();
          break;
      }

      _exitCell.Exit = true;
    }

    private void CreateLabyrinth()
    {
      int rows = _cells.GetLength(0);
      int columns = _cells.GetLength(1);

      for (int row = 0; row < rows; row++)
      {
        for (int column = 0; column < columns; column++)
        {
          Cell current = _cells[row, column];
          
          if (row == 0)
          {
            if (column == columns - 1)
            {
              continue;
            }
            
            DeleteRightWall(current);
            
            if (Random.Range(0, 2) == 0)
            {
              DeleteBottomWall(current);
            }
          }
          else
          {
            if (column == columns - 1)
            {
              DeleteUpperWall(current);
              
              continue;
            }
              
            if (Random.Range(0, 2) == 0)
            {
              DeleteUpperWall(current);
            }
            
            if (Random.Range(0, 2) == 0)
            {
              DeleteBottomWall(current);
            }
            
            if (Random.Range(0, 2) == 0)
            {
              DeleteLeftWall(current);
            }
            
            if (Random.Range(0, 2) == 0)
            {
              DeleteRightWall(current);
            }
          }
        }
      }
      
      DeleteLongWalls();
    }

    private void DeleteLongWalls()
    {
      int counter = 0;

      for (int row = 1; row < _cells.GetLength(0); row++)
      {
        for (int column = 0; column < _cells.GetLength(1); column++)
        {
          if (_cells[row, column].UpWall)
          {
            counter++;

            if (counter == MAX_WALL_LENGHT)
            {
              _cells[row, column].UpWall = false;
              GetUpperCell(_cells[row, column]).BottomWall = false;
              
              counter = 0;
            }
          }
        }
      }

      counter = 0;
      
      for (int column = 1; column < _cells.GetLength(1); column++)
      {
        for (int row = 0; row < _cells.GetLength(0); row++)
        {
          if (_cells[row, column].LeftWall)
          {
            counter++;

            if (counter == MAX_WALL_LENGHT)
            {
              _cells[row, column].LeftWall = false;
              GetLeftCell(_cells[row, column]).RightWall = false;
              
              counter = 0;
            }
          }
        }
      }
    }
    
    private void DeleteRightWall(Cell cell)
    {
      Cell right = GetRightCell(cell);

      if (right != null)
      {
        cell.RightWall = false;
        right.LeftWall = false;
      }
    }
    
    private void DeleteLeftWall(Cell cell)
    {
      Cell left = GetLeftCell(cell);

      if (left != null)
      {
        left.RightWall = false;
        cell.LeftWall = false;
      }
    }
    
    private void DeleteUpperWall(Cell cell)
    {
      Cell upper = GetUpperCell(cell);

      if (upper != null)
      {
        upper.BottomWall = false;
        cell.UpWall = false;
      }
    }
    
    private void DeleteBottomWall(Cell cell)
    {
      Cell bottom = GetBottomCell(cell);

      if (bottom != null)
      {
        bottom.UpWall = false;
        cell.BottomWall = false;
      }
    }

    private Cell GetLeftCell(Cell target)
    {
      if (target.Column == 0)
      {
        return null;
      }

      return _cells[target.Row, target.Column - 1];
    }

    private Cell GetRightCell(Cell target)
    {
      if (target.Column == _gridColumns - 1)
      {
        return null;
      }

      return _cells[target.Row, target.Column + 1];
    }

    private Cell GetUpperCell(Cell target)
    {
      if (target.Row == 0)
      {
        return null;
      }

      return _cells[target.Row - 1, target.Column];
    }

    private Cell GetBottomCell(Cell target)
    {
      if (target.Row == _gridRows - 1)
      {
        return null;
      }

      return _cells[target.Row + 1, target.Column];
    }

    private Cell GetCellInRightUpperCorner()
    {
      if (Random.Range(0, 2) == 0)
        return _cells[0, Random.Range((int)(_gridColumns * CORNER_FACTOR), _gridColumns - 1)];
      else
        return _cells[Random.Range(1, (int)(_gridRows * (1 - CORNER_FACTOR))), _gridColumns - 1];
    }
    
    private Cell GetCellInLeftUpperCorner()
    {
      if (Random.Range(0, 2) == 0)
        return _cells[0, Random.Range(1, (int)(_gridColumns * (1 - CORNER_FACTOR)))];
      else
        return _cells[Random.Range(1, (int)(_gridRows * (1 - CORNER_FACTOR))), 0];
    }
    
    private Cell GetCellInLeftBottomCorner()
    {
      if (Random.Range(0, 2) == 0)
        return _cells[Random.Range((int)(_gridRows * CORNER_FACTOR), _gridRows - 1), 0];
      else
        return _cells[_gridRows - 1, Random.Range(1, (int)(_gridRows * (1 - CORNER_FACTOR)))];
    }
    
    private Cell GetCellInRightBottomCorner()
    {
      if (Random.Range(0, 2) == 0)
        return _cells[Random.Range((int)(_gridRows * CORNER_FACTOR), _gridRows - 1), _gridColumns - 1];
      else
        return _cells[_gridRows - 1, Random.Range((int)(_gridColumns * CORNER_FACTOR), _gridColumns - 1)];
    }
    
    private void OnDrawGizmos()
    {
      if (_cells != null)
      {
        for (int row = 0; row < _cells.GetLength(0); row++)
        {
          for (int column = 0; column < _cells.GetLength(1); column++)
          {
            Gizmos.DrawSphere(_cells[row, column].Position, 0.1f);
          }
        }
      }
    }
  }
}