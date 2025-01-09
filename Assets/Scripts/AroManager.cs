using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AroManager : MonoBehaviour
{
    public Aro[] aros;
    private int currAroIndex, nextAroIndex;

    public void Start()
    {
        //Pintar todos los aros de negro
        foreach (Aro aro in aros)
        {
            aro.GetComponent<Renderer>().material.color = Color.black;
        }

        //Pintar el primer aro de verde
        currAroIndex = 0;
        aros[currAroIndex].GetComponent<Renderer>().material.color = Color.green;
    }

    //Comprueba si el aro atravesado es el mismo que el siguiente aro en la lista
    public void CheckRing(Aro aro)
    {
        if (aro == aros[currAroIndex])
        {
            //Color del aro atravesado
            aro.GetComponent<Renderer>().material.color = Color.red;

            //Cambio de aro a atravesar
            currAroIndex++;
            GetComponent<GameManager>().RingUp();

            //Color del siguiente aro o fin del juego (La condición evita que se intente cambiar el color de un aro fuera del rango)s
            if (currAroIndex < aros.Length) aros[currAroIndex].GetComponent<Renderer>().material.color = Color.green;
            else GetComponent<GameManager>().GameOver();
        }
    }
}
