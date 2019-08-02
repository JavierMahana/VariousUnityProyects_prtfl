using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	public GameObject selectedUnit;
    public GameObject destination;

    GameObject movementGO;
    Movement movementComp;

    void Start()
    {
        movementGO = GameObject.FindObjectOfType<Movement>().gameObject;
        movementComp = movementGO.GetComponent<Movement>();
    }

    void Update ()
    {
        SelectUnit();
        
	}

	void SelectObject (GameObject selectedObject)
	{
		if (selectedUnit != null) {
			if (selectedObject == selectedUnit) {
				return;
			}
			ClearSelection ();
		}
		selectedUnit = selectedObject;

		Renderer[] rs = selectedUnit.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs) {
			Material m = r.material;
			m.color = Color.red;
			r.material = m;
		}
	}


	public void ClearSelection()
	{
		if (selectedUnit == null) {
			return;
		}
		Renderer[] rs = selectedUnit.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs) {
			Material m = r.material;
			m.color = Color.white;
			r.material = m;
		}

		selectedUnit = null;
	}

    void SelectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 8))
            {

                GameObject hitObject = hitInfo.transform.GetComponentInParent<Unit>().gameObject;
                SelectObject(hitObject);


            }
            else if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 10))
            {
                Debug.Log("se selecciona el objetivo");
                destination = hitInfo.transform.gameObject;
                movementComp.MoveToDestination(destination);

                ClearSelection();
            }
            else
            {
                ClearSelection();
            }
        }
    }


   

}
