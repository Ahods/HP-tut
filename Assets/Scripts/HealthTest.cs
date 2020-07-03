using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    PlayerHpHud hpHud;
    // Start is called before the first frame update
    void Start()
    {
        hpHud = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHpHud>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            hpHud.DeltaCurrentHealth(1);

        if (Input.GetKeyDown(KeyCode.M))
            hpHud.DeltaCurrentHealth(-1);
    }
}
