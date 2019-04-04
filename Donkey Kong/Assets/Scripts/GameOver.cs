/*****************
 * By Nico
 * Purpose: game over script
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Text gameOverText;
    private SpriteRenderer boxSprite;
    public float endTime = 2.0f;
    public float endTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        boxSprite = GetComponentInChildren<SpriteRenderer>();
        boxSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( boxSprite.enabled)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public void Lose()
    {
        boxSprite.enabled = true;
        endTimer = endTime;
        gameOverText.enabled = true;
    }
}
