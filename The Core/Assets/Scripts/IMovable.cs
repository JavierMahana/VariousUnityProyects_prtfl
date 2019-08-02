using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable {
    IEnumerator Move(Vector2Int destination);
}
