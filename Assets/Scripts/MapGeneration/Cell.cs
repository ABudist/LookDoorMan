using UnityEngine;

namespace MapGeneration
{
  public class Cell
  {
    public int Row, Column;
    public Vector3 Position;
    public bool LeftWall, UpWall, BottomWall, RightWall, Exit;

    public Cell(Vector3 position, int row, int column)
    {
      Position = position;
      Row = row;
      Column = column;
      LeftWall = RightWall = UpWall = BottomWall = true;
    }
  }
}