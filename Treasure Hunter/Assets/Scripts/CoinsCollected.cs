using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CoinsCollected : MonoBehaviour
{

    [SerializeField] private Text coinCounter;
    [SerializeField] private Text winText;

    private int pcsOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pcsOfCoins = 0;
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        coinCounter.text = "Coins: " + pcsOfCoins.ToString();

        if (pcsOfCoins >= 8)
        {
            winText.text = "You Win!";
            Time.timeScale = 0f;

            StartCoroutine("ReloadScene");
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>())
        {
            pcsOfCoins += 1;
            Destroy(collision.gameObject);
        }
    }
    IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("_Main");
    }
}
