using UnityEngine;
using System.Collections;

public class LooseObject {
    private string name;
    private int id;
    private int type;

    public LooseObject(string name, int id, int type){
        this.name = name;
        this.id = id;
        this.type = type;
    }

    public string Name{
        get{
            return name;
        }
    }

    public int Id{
        get{
            return id;
        }
    }

    public int Type{
        get{
            return type;
        }
    }
}
