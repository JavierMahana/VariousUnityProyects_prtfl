using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour {

    public Button buy;
    public Button unit;
    public Button structure;
    public Button trash;
    public Button backgroundButton;
    public Button[] unitsButtons;
    public Button[] structureButtons;

    public GameObject wp;
    public GameObject ap;
    public GameObject kp;
    public GameObject bp;

    private GameObject buyedUnit;

    enum CurrentBuy
    {
        none,
        warrior,
        archer,
        knight,
        bomber
    }
    CurrentBuy current = CurrentBuy.none;

    public void BuyButton()
    {
        buy.gameObject.SetActive(false);
        backgroundButton.gameObject.SetActive(true);
        unit.gameObject.SetActive(true);
        structure.gameObject.SetActive(true);
    }

    public void UnitButton()
    {
        //if hay activos bottones de estructuras desactivarlos
        foreach (Button but in unitsButtons)
        {
            but.gameObject.SetActive(true);
        }
    }
    public void Trash()
    {
        unit.gameObject.SetActive(true);
        structure.gameObject.SetActive(true);
        trash.gameObject.SetActive(false);
        switch (current)
        {
            case CurrentBuy.none:
                break;
            case CurrentBuy.warrior:
                Destroy(buyedUnit);
                LoveManager.love += 2;
                current = CurrentBuy.none;
                break;
            case CurrentBuy.archer:
                Destroy(buyedUnit);
                LoveManager.love += 3;
                current = CurrentBuy.none;
                break;
            case CurrentBuy.knight:
                Destroy(buyedUnit);
                LoveManager.love += 5;
                current = CurrentBuy.none;
                break;
            case CurrentBuy.bomber:
                Destroy(buyedUnit);
                LoveManager.love +=5;
                current = CurrentBuy.none;
                break;
        }
    }

    public void WarriorButton()
    {
        SpawnUnit(2, wp);
        current = CurrentBuy.warrior;
    }
    public void ArcherButton()
    {
        SpawnUnit(3,ap);
        current = CurrentBuy.archer;
    }

    private void SpawnUnit(int cost, GameObject prefab)
    {
        if (LoveManager.love >= cost)
        {
            GameObject go = Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            go.GetComponent<UnitPlacer>().enabled = true;
            buyedUnit = go;
            LoveManager.love -= cost;
            unit.gameObject.SetActive(false);
            structure.gameObject.SetActive(false);
            trash.gameObject.SetActive(true);
        }
    }

    public void KnightButton()
    {
        SpawnUnit(5, kp);
        current = CurrentBuy.knight;
    }
    public void BomberButton()
    {
        SpawnUnit(5, bp);
        current = CurrentBuy.bomber;
    }

    public void StructuresButton()
    {

    }



    public void RestartButton()
    {
        buy.gameObject.SetActive(true);
        unit.gameObject.SetActive(false);
        structure.gameObject.SetActive(false);
        backgroundButton.gameObject.SetActive(false);
        foreach (Button but in unitsButtons)
        {
            but.gameObject.SetActive(false);
        }
        foreach (Button but in structureButtons)
        {
            but.gameObject.SetActive(false);
        }
    }

}
