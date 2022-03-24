using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        
        Debug.Log("player");
        
        var player = col.GetComponent<PeopleControl>();

        if (player)
        {
            player.Win();
        }
    }
}
