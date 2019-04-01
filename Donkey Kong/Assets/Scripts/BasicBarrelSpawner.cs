using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBarrelSpawner : MonoBehaviour
{
    public float interval = 2;
    public Barrel barrelPrefab;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = interval;
            Barrel barrel = Instantiate<Barrel>(barrelPrefab);
            barrel.transform.position = this.transform.position;
            barrel.going = 1;
        }
    }
}
