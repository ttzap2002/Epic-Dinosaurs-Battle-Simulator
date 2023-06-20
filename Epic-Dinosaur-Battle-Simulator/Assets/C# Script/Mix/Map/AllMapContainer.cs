using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AllMapContainer
{
    Map[] mapList;

    public AllMapContainer() 
    {
        mapList = CreateMaps();
    }

    public Map[] MapList { get => mapList;}

    private Map[] CreateMaps()
    {

        Map[] maps = new Map[4] { new Map(0, new List<Obstacle>{new Obstacle(12,0,42,1),new Obstacle(92,0,12,0),new Obstacle(52,0,45,0),new Obstacle(58,0,31,0)}), 
            new Map(1, new List<Obstacle>{new Obstacle(12.43f,0,12,1),new Obstacle(22,0,51,1),new Obstacle(22,0,65,0),new Obstacle(99,0,91,1) }), 
            new Map(2,  new List<Obstacle>{new Obstacle(27.43f,0,5.1f,0),new Obstacle(49,0,56,1),new Obstacle(32,0,45,0),new Obstacle(98,0,41.11f,1),new Obstacle(40,0,71.11f,1) }), 
            new Map(3, new List<Obstacle>{new Obstacle(27.43f,0,5.1f,1),new Obstacle(49,0,56,1),new Obstacle(32,0,45,1),new Obstacle(98,0,41.11f,1),new Obstacle(40,0,71.11f,1),new Obstacle(40,0,91.11f,1),new Obstacle(20,0,31.11f,1), })};
        return maps;
    }
}

