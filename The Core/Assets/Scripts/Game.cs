using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public enum GameState
    {
        Setup,
        Battle,
        Menu
    }
    public GameState state;



}
