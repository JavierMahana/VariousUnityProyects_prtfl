using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementManger : MonoBehaviour {

    //FALTA
    //Que no puedan haber 2 tropas en la misma casilla
    //que si se cambia la seleccion, tambien cambien los "CanMoveTiles"

    #region Crear Ayuda Visual ---- Can move to

    public static void CrearAyudaVisual(GameManager gm)
    {


        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.isActive)
            {
                //si se selecciona Otra unidad que cumple los requisitos
                //pero antes ya estaba seleccionada una unidad
                if (gm.prevSelectedUnit != gm.selectedUnit)
                {
                    ClearContainer(gm.containerCMTT);
                }

                if (gm.containerCMTT.transform.childCount < 1)
                {
                    CreateMovementOptions(gm.selectedUnit, gm.posibilidadesDeMovimiento, gm.containerCMTT);
                }
                
                
            }
            gm.prevSelectedUnit = gm.selectedUnit; 
        }
        else
            ClearContainer(gm.containerCMTT);
    }
    #endregion


    #region CrearMovementPath method --- Crea la ayuda visual del camino que uno va a tomar con una unidad



    public static void CrearMovementPath(GameManager gm)
    {

        Casilla casillaMouse = gm.casillaMouse;
        Unit selectedUnit = gm.selectedUnit;
        GameObject contenedor = gm.contenedorMovimiento;
        List<Casilla> CPM = gm.posibilidadesDeMovimiento;

        //Primero vacia el contenedor si esque tiene algo
        if (contenedor.transform.childCount > 0)
        {
            for (int i = 0; i < contenedor.transform.childCount; i++)
            {
                Destroy(contenedor.transform.GetChild(i).gameObject);
            }
        }
        if (selectedUnit == null)
        {
            return;
        }
        if (CPM.Contains(casillaMouse) == false)
        {
            return;
        }

        Casilla[] casillasParaMoverse = PathFinding.Driskas(selectedUnit, casillaMouse).ToArray();

        if (casillasParaMoverse.Length == 1)
        {
            return;
        }

        for (int i = 0; i < casillasParaMoverse.Length; i++)
        {
            //asigno los valores para las casillas actual,(ya no) anterior y siguiente a revisar en el loop

            Casilla actual = casillasParaMoverse[i];



            //parto viendo el inicio y a que direccion se debe ir
            if (i == 0)
            {


                if (casillasParaMoverse[i + 1].position.x == (actual.position.x + 1))
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().inicio_derecha, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if (casillasParaMoverse[i + 1].position.x == (actual.position.x - 1))
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().inicio_izquierda, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if (casillasParaMoverse[i + 1].position.y == (actual.position.y + 1))
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().inicio_arriba, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if (casillasParaMoverse[i + 1].position.y == (actual.position.y - 1))
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().inicio_abajo, actual.transform.position, Quaternion.identity, contenedor.transform);
                }

            }
            //aca se ve el final
            else if (i == (casillasParaMoverse.Length - 1))
            {
                Casilla anterior = casillasParaMoverse[i - 1];

                if ((anterior.position.x + 1) == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().fin_derecha, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.x - 1) == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().fin_izquierda, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.y + 1) == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().fin_Arriba, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.y - 1) == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().fin_Abajo, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
            }
            //luego hago el recto del recorrido
            else
            {
                Casilla siguiente = casillasParaMoverse[i + 1];
                Casilla anterior = casillasParaMoverse[i - 1];
                //derechos(rectos)
                if (anterior.position.x == actual.position.x && siguiente.position.x == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().derecho_vertical, actual.transform.position, Quaternion.identity, contenedor.transform);
                }


                if (anterior.position.y == actual.position.y && siguiente.position.y == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().derecho_horizontal, actual.transform.position, Quaternion.identity, contenedor.transform);
                }


                //curbas
                if ((anterior.position.y + 1) == actual.position.y && (siguiente.position.x + 1) == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Abajo_izquierda, actual.transform.position, Quaternion.identity, contenedor.transform);
                }

                if ((anterior.position.y - 1) == actual.position.y && (siguiente.position.x - 1) == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Arriba_Derecha, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.y - 1) == actual.position.y && (siguiente.position.x + 1) == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Arriba_Izquierda, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.y + 1) == actual.position.y && (siguiente.position.x - 1) == actual.position.x)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Abajo_Derecha, actual.transform.position, Quaternion.identity, contenedor.transform);
                }

                //los contrarios
                if ((anterior.position.x + 1) == actual.position.x && (siguiente.position.y + 1) == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Abajo_izquierda, actual.transform.position, Quaternion.identity, contenedor.transform);
                }

                if ((anterior.position.x - 1) == actual.position.x && (siguiente.position.y - 1) == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Arriba_Derecha, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.x + 1) == actual.position.x && (siguiente.position.y - 1) == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Arriba_Izquierda, actual.transform.position, Quaternion.identity, contenedor.transform);
                }
                if ((anterior.position.x - 1) == actual.position.x && (siguiente.position.y + 1) == actual.position.y)
                {
                    Instantiate(contenedor.GetComponent<ContenedorMovimiento>().curba_Abajo_Derecha, actual.transform.position, Quaternion.identity, contenedor.transform);
                }

            }

        }



    }
    #endregion

    //debe tener limitaciones
    public static void Mover(GameManager gm)
    {
        Casilla casillaMouse = gm.casillaMouse;
        Casilla casillaMouseFAnterior = gm.casillaMouseFAnterior;


        if (Input.GetMouseButtonDown(1))
        {
            MovementManger.CrearMovementPath(gm);

        }

        if (Input.GetMouseButtonUp(1))
        {
            RealizarMovimiento(gm);
        }

        if (Input.GetMouseButton(1))
        {
            if (casillaMouse != casillaMouseFAnterior)
            {
                MovementManger.CrearMovementPath(gm);

            }
        }

        casillaMouseFAnterior = casillaMouse;


    }
    




    private static void CreateMovementOptions(Unit unit,List<Casilla> casillas, GameObject container)
    {

        foreach (Casilla c in casillas)
        {
            Instantiate(container.GetComponent<CMTTContainer>().canMoveTo, 
                c.transform.position, Quaternion.identity, container.transform);
        }
    }


  



    private static void RealizarMovimiento(GameManager gm)
    {
        //Primero hace chequea si se cumplen los parametros necesarios para generer el movimiento
        //Que haya una unidad seleccionada y que esta sea amistosa y este activa
        // y que la casilla donde esta el mouse este en el area donde se puede mover
        Unit selectedUnit = gm.selectedUnit;
        Casilla nuevaCasilla = gm.casillaMouse;

        

        if (selectedUnit == null)
            return;
        else if (gm.posibilidadesDeMovimiento.Contains(nuevaCasilla) == false)
            return;
        else if (selectedUnit.isActive == false)
            return;
        else if (selectedUnit.isFriendly == false)
            return;
        else if (selectedUnit.casillaActual == gm.casillaMouse)
        {
            gm.selectedUnit = null;
            gm.prevSelectedUnit = null;
            ClearContainer(gm.contenedorMovimiento);
            return;
        }

        //se puede realizar el movimiento
        else
        {
            NodeRecord nR;
            PathFinding.Driskas(selectedUnit, gm.casillaMouse, out nR);

            //se dezplaza y se cambia la casilla actual de la unidad
            selectedUnit.transform.position = nuevaCasilla.transform.position;
            selectedUnit.casillaActual = nuevaCasilla;

            //se le resta el movimiento y si se queda sin movimiento queda inactiva
            selectedUnit.remainingMovement -= nR.costSoFar;
            if (selectedUnit.remainingMovement == 0)
            {
                selectedUnit.isActive = false;
            }

            //se eliminan los dos contenedores
            ClearContainer(gm.containerCMTT);
            ClearContainer(gm.contenedorMovimiento);
        }
       
       
      


    }


    private static void ClearContainer(GameObject container)
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
    }

    //Moverse tendra 3 pasos:
    //primera fase
    //donde la unidad que esta seleccionada muestra una ayuda visual de donde puede moverse
    //segunda fase
    //cuando se apreta el click derecho para mover la unidad seleccionada
    //y esta crea un camino de lineas punteadas y el selector hacia donde se deberia mover
    //fase 3
    //se mueve y se descuenta de la cantidad de movimientos restantes la cantidad de movimiento que gasto
    //si la 

}
