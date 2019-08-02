using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour {

    public GameObject selectedObject;
    GameObject prevSelected;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Select();
        }

        if (SelectionChange())
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<ISelectable>().OnSelect();
            }
            if (prevSelected != null)
            {
                prevSelected.GetComponent<ISelectable>().OnDeselect();
            }
        }
        prevSelected = selectedObject;
    }
    public void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Units", "Buildings");
        if (Physics.Raycast(ray, out hit, 20f, layerMask))
        {
            selectedObject = hit.transform.gameObject;
        }
        else
            selectedObject = null;
    }
    bool SelectionChange()
    {
        if (prevSelected != selectedObject)
        {
            return true;
        }
        return false;
    }
}
