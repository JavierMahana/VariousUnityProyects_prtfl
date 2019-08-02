using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Clase clase;

    [Space]
    public Casilla casillaActual;

    [Space]
    [Header("Stats")]

    public int maxHP;
     
    public int hP;
    public int strength;
    public int defence;
    public int speed;
    [Space]
    public int movement;
    public int remainingMovement;

    [Space]
    [Header("Estados")]

    public bool isActive;
    public bool isFriendly;
}
