using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComandManager {

    public static List<IComand> recordedComands;
    public static IComand current;
    public static IComand previousTurnPointer;


    public static void ReturnToLastTurn()
    {

        /*
         * se marca el "undoingComand" como el actual
         * luego entra a un loop que dura hasta que el comando sea el que marca el inicio del turno anterior
         * En este loop se llama a la función undo de el comando undoing y luego se avanza al siguiente elemento de la lista
         */
        IComand undoingComand = current;

        while (undoingComand != previousTurnPointer)
        {
            undoingComand.Undo();

            /*
             * la forma de avanzar mediante los undoing comand, sería tomando el index anterior a este
             */

            int index = recordedComands.IndexOf(undoingComand);
            undoingComand = recordedComands[index - 1];
        }

        current = undoingComand;
    }

    public static void DeleteComandsAfterCurrent ()
    {
        /*
         El index +1 es el idex inicial, ya que no se quiere remover ese valor
         Al restar el index con el numero total de comandos + 1
         se consigue el numero de elementos a eliminar(Count)
         ej 5-2(+1) 0-1-(2-3-4)

         */
        int ammountOfComands = recordedComands.Count;
        int currentIndex = recordedComands.IndexOf(current);

        recordedComands.RemoveRange(currentIndex, ammountOfComands - currentIndex);

    }


}
