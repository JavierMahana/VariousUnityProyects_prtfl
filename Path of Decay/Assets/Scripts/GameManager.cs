using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Unit selectedUnit;
    [HideInInspector]

    public List<Unit> allUnits;
    public List<Unit> friendlyUnits;
    public List<Unit> activeFriendlyUnits;
    public List<Unit> enemyUnits;

    public List<Casilla> mapa;

    #region variables para selccion y movimiento
    public Unit prevSelectedUnit;
    public List<Casilla> posibilidadesDeMovimiento;
    public Casilla casillaMouse;
    public Casilla casillaMouseFAnterior;
    
    //containers
    public GameObject containerCMTT;
    public GameObject contenedorMovimiento;

    #endregion

    private void Awake()
    {
        containerCMTT = GameObject.FindObjectOfType<CMTTContainer>().gameObject;
        contenedorMovimiento = GameObject.FindObjectOfType<ContenedorMovimiento>().gameObject;

        allUnits = new List<Unit>(GameObject.FindObjectsOfType<Unit>());
        mapa = new List<Casilla>(GameObject.FindObjectsOfType<Casilla>());
    }

    //Acá se controlarán todos los sucesos que pasen.
    private void Update()
    {

        Seleccion();

        #region movimiento
        MovementManger.Mover(this);
        //por alguna razon debe estar almedio de estos dos metodos para funcionar bien
        ActualizarPosibilidadesDeMovimiento();

        MovementManger.CrearAyudaVisual(this);
        #endregion

        GetCasillaMouse();

    }

    #region metodos para finalizar el turno

    void EndTundIfNoActiveUnits()
    {
        if (activeFriendlyUnits.Count == 0)
        {
            EndTurn.EndTheTurn();
        }
    }

    #endregion

    #region Metodos para seleccionar una unidad, actualzar la casilla mouse, posibilidades de movimiento
    private void Seleccion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectManager.ControlarSeleccion(out selectedUnit);
        }
    }
    private void GetCasillaMouse()
    {
        CasillaMouseManager.ActualizarCasillaMouse(out casillaMouse);
    }
    private void ActualizarPosibilidadesDeMovimiento()
    {
        if (selectedUnit != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                posibilidadesDeMovimiento = PathFinding.GetListOfWalkableTiles(selectedUnit);
            }
            if (Input.GetMouseButtonUp(1))
            {
                posibilidadesDeMovimiento = PathFinding.GetListOfWalkableTiles(selectedUnit);
            }
        }
        
    }
    #endregion
}


