using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour , ISelectable {

    protected enum UnitState
    {
        Fighting,
        Persuing,
        Idle,
        Moving
    }
    public Tile_ standingTile;
    protected UnitState state = UnitState.Idle;
    Tile_ prevStandingTile;
    Map map;
    public GameObject radious;
    Vector3[] destinations;
    Animator animator;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        map = FindObjectOfType<Map>();


        UpdateStandingTile(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
    }

    void Update()
    {
        switch (state)
        {
            case UnitState.Fighting:
                break;
            case UnitState.Persuing:
                break;
            case UnitState.Idle:
                break;
            case UnitState.Moving:
                
                break;
            default:
                break;
        }
    }


    public void Move(Vector2Int destination)
    {
        state = UnitState.Moving;

        List<Tile_> path = Pathfinding.ShortestPath(map,standingTile,map.GetTile(destination.x,destination.y));
        Vector3[] pathPositions = new Vector3[path.Count];
        if (path != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Tile_ current = path[i];
                Vector3 currentPos = new Vector3( current.position.x, current.position.y);
                pathPositions[i] = currentPos;
            }
        }
        StartCoroutine(MoveTo(pathPositions, new Vector3(destination.x, destination.y), 5));
    }

    IEnumerator MoveTo(Vector3[] path, Vector3 destination, float speed)
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (transform.position.y < path[i].y )
            {
                animator.SetBool("WalkUp", true);
                //animator.GetCurrentAnimatorStateInfo().
            }
            while (transform.position != path[i])
            {

                this.transform.position = Vector3.MoveTowards(transform.position, path[i], speed * Time.deltaTime);
                yield return null;
            }
        }
        
    }

     
    public void OnSelect()
    {
        if (radious.gameObject.activeInHierarchy == false)
        {
            radious.gameObject.SetActive(true);
        }
    }
    public void OnDeselect()
    {
        if (radious.gameObject.activeInHierarchy)
        {
            radious.gameObject.SetActive(false);
        }
    }

    
    void UpdateStandingTile(int x, int y)
    {
        if (prevStandingTile != null)
        {
            prevStandingTile.ocupied = false;
        }
        
        standingTile = map.GetTile(x, y);
        standingTile.ocupied = true;
        prevStandingTile = standingTile;
    }

}
