using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText, ringText, timerText, endCoinText, endRingText, endTimeText, scoreText, victoryText, defeatText;
    public GameObject coinPrefab, endCanvas, player;
    public List<GameObject> coinSpawnPos;
    private List<int> possibleSpawns;

    private int coinsCollected, ringsPassed, minutes = 1, seconds = 30;
    private float time = 1;

    private void Start()
    {
        //Datos iniciales para el botón de volver a jugar
        minutes = 1;
        seconds = 30;
        coinsCollected = 0;
        ringsPassed = 0;

        ringText.text = "Rings: " + ringsPassed;
        coinText.text = "Coins: " + coinsCollected;
        endTimeText.text = "Time: " + minutes + ":" + seconds;

        //Para crear las monedas he utilizado dos fases para los dos grupos de aros. Usando List<int> puedo eliminar los números de posiciones posibles sin problemas de posiciones nulas.
        //En ambas fases se eliminan 10 posiciones aleatorias (15 monedas de 25 posiciones y 5 monedas de 15 posiciones).
        //La lista de posiciones posibles se borra antes de la segunda fase para llenarla con posiciones nuevas.

        //Primeros 5 aros
        possibleSpawns = new List<int>();
        for (int i = 1; i <= 25; i++)
        {
            possibleSpawns.Add(i);
        }
        for (int i = 0; i < 10; i++)
        {
            possibleSpawns.RemoveAt(Random.Range(0, possibleSpawns.Count));
        }

        //Resultado
        for (int i = 1; i <= 25; i++)
        {
            if (possibleSpawns.Contains(i)) Instantiate(coinPrefab, coinSpawnPos[i].transform.position, Quaternion.identity, coinSpawnPos[i].transform);
        }

        //Aros 6-8
        possibleSpawns = new List<int>();
        for (int i = 26; i <= 40; i++)
        {
            possibleSpawns.Add(i);
        }
        for (int i = 0; i < 10; i++)
        {
            possibleSpawns.RemoveAt(Random.Range(0, possibleSpawns.Count));
        }

        //Resultado
        for (int i = 26; i <= 40; i++)
        {
            if (possibleSpawns.Contains(i)) Instantiate(coinPrefab, coinSpawnPos[i].transform.position, Quaternion.identity, coinSpawnPos[i].transform);
        }
    }

    private void Update()
    {
        //Temporizador
        time -= Time.deltaTime;
        if (time <= 0 && (minutes > 0 || seconds > 0))
        {
            time += 1;
            seconds--;
        }

        if (seconds < 0 && minutes > 0)
        {
            seconds += 60;
            minutes--;
        }
        else if (seconds == 0 && minutes == 0)
        {
            GameOver();
        }

        if (seconds >= 10) timerText.text = "Time: " + minutes + ":" + seconds;
        else timerText.text = "Time: " + minutes + ":0" + seconds;
    }

    //Contador de monedas
    public void CoinUp()
    {
        coinsCollected++;
        coinText.text = "Coins: " + coinsCollected;
    }

    //Contador de aros
    public void RingUp()
    {
        ringsPassed++;
        ringText.text = "Rings: " + ringsPassed;
    }

    //Final del juego
    public void GameOver()
    {
        //El tiempo se para y se activa el Canvas con datos de victoria/derrota
        Time.timeScale = 0;
        endCanvas.SetActive(true);
        if (seconds == 0 && minutes == 0) defeatText.gameObject.SetActive(true);
        else victoryText.gameObject.SetActive(true);

        //Se rellenan los datos en la pantalla
        endCoinText.text = coinsCollected.ToString();
        endRingText.text = ringsPassed.ToString();
        if (seconds >= 10) endTimeText.text = "Time: " + minutes + ":" + seconds;
        else endTimeText.text = "Time: " + minutes + ":0" + seconds;

        scoreText.text = "Total Score: " + (minutes * 60 + seconds + coinsCollected*2);
    }

    //Botón de volver a jugar
    public void Restart()
    {
        //El tiempo vuelve a moverse y se eliminan las monedas
        Time.timeScale = 1;
        foreach (Coin coin in GameObject.FindObjectsOfType<Coin>()) Destroy(coin.gameObject);

        //Se desactiva la interfaz de victoria/derrota
        endCanvas.SetActive(false);
        defeatText.gameObject.SetActive(false);
        victoryText.gameObject.SetActive(false);

        //Se ejecutan los Start de los managers y la nave para reiniciar el juego
        Start();
        player.GetComponent<Nave>().Start();
        GetComponent<AroManager>().Start();
    }

    //Botón de salir
    public void Quit()
    {
        Application.Quit();
    }
}
