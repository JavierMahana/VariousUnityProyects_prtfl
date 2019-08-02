using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Terreno")]
public class Terreno : ScriptableObject {

    public Sprite visuals;

    public MovementCost mc;

    public bool isPassable = true;

}
