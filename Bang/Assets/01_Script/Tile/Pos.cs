using UnityEngine;

public class Pos
{
    public int x;
    public int z;

    public Pos(int _x, int _z)
    { x = _x; z = _z; }

    public string ShowPos()
    {
        return (x + " / " + z);
    }

    public bool EqualPos(Pos _pos)
    {
        return (_pos.x == x && _pos.z == z);
    }

    public void UpdatePos(Pos _addPos)
    {
        x += _addPos.x;
        z += _addPos.z;
    }

    public void Move(Pos _pos, Transform _transform)
    {
        UpdatePos(_pos);
        _transform.position += new Vector3(_pos.x * 5, 0, -_pos.z * 5);
    }
}
