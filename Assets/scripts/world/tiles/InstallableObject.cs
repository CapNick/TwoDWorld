using UnityEngine;
using System.Collections;

public class InstallableObject {
    private string name;
    private int id;
    private int type;

    public InstallableObject(string name, int id, int type){
        this.name = name;
        this.id = id;
        this.type = type;
    }
    public string getObjectName(){
        return name;
    }

    public int getObjectId(){
        return id;
    }

    public int getObjectType(){
        return type;
    }
}
