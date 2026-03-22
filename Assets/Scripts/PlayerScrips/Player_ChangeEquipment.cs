using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeEquipment : MonoBehaviour
{
    public Player_Combat combat;
    public Player_Bow bow;
    
    void Update()
    {
        if (Input.GetButtonDown("ChangeEquipment"))
        {
            bow.enabled = !bow.enabled;
        }
    }
}
