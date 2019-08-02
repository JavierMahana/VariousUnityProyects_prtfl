using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    public GameObject[] prefabs;

    public enum Class
    {
        warrior,
        archer,
        knight,
        bomber
    }

    public void InstantiateUnit(Class unitClass, bool friendly)
    {
        switch (unitClass)
        {
            case Class.warrior:
                Instantiate(prefabs[(int)unitClass]);
                break;
            case Class.archer:
                break;
            case Class.knight:
                break;
            case Class.bomber:
                break;
            default:
                break;
        }
    }


}
