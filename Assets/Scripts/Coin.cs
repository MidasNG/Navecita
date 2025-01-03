using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed, maxRotateSpeed;
    private float t = 0, rotateSpeed;
    public AnimationCurve easing;

    private static GameObject player;
    private static GameManager game;

    private void Start()
    {
        if (player == null) player = FindObjectOfType<Nave>().gameObject;
        if (game == null) game = FindObjectOfType<GameManager>();
    }

   private void Update()
    {
        t = Mathf.Clamp01(t + Time.deltaTime * moveSpeed);
        transform.localPosition = new Vector3(0, Mathf.Lerp(-0.15f, 0.15f, easing.Evaluate(t)), 0);

        if (t == 0 || t == 1) moveSpeed = -moveSpeed;

        rotateSpeed = maxRotateSpeed / Vector3.Distance(transform.position, player.transform.position) * 15;
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            game.CoinUp();
            Destroy(gameObject);
        }
    }

}
