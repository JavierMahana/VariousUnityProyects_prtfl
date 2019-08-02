using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeCaller : MonoBehaviour {

    private void Awake()
    {
        ComandManager.recordedComands = new List<IComand>();
    }

}
