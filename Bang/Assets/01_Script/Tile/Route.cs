using System.Collections.Generic;

public class Route
{
    public Route beforeRoute;
    public Pos pos;
    public int cost;
    public List<Pos> checkedTiles;
    public List<Route> nextRoute;

    public Route(Pos _tilePos, Route _beforeRoute = null)
    {
        pos = _tilePos;
        nextRoute = new List<Route>();

        if (_beforeRoute == null) checkedTiles = new List<Pos>();
        else if (_beforeRoute != null)
        {
            beforeRoute = _beforeRoute;
            checkedTiles = beforeRoute.checkedTiles;
        }
    }

    public void Check()
    {
        if (beforeRoute != null)
            checkedTiles.Add(beforeRoute.pos);
    }
}
