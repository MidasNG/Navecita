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
        foreach (Aro aro in aros)
        {
            aro.GetComponent<Renderer>().material.color = Color.black;
        }
        currAroIndex = 0;
        aros[currAroIndex].GetComponent<Renderer>().material.color = Color.green;
    }

    public void CheckRing(Aro aro)
    {
        if (aro == aros[currAroIndex])
        {
            aro.GetComponent<Renderer>().material.color = Color.red;
            currAroIndex++;
            GetComponent<GameManager>().RingUp();

            if (currAroIndex < aros.Length) aros[currAroIndex].GetComponent<Renderer>().material.color = Color.green;
            else GetComponent<GameManager>().GameOver();
        }
    }
}
