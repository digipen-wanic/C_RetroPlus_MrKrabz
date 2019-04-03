using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisplay : MonoBehaviour
{
    public GameObject scoreTextPrefab;
    public float time;
    private float timer;
    private bool timed = false;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timed)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                text.enabled = false;
                timed = false;
            }
        }
    }
    public void DisplayScore(int points, Vector2 pos)
    {
        this.transform.position = pos;
        text.text = points.ToString();
        text.enabled = true;
        timed = true;
        timer = time;
    }
}
