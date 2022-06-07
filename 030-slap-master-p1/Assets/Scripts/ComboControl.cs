using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboControl : MonoBehaviour
{
    public int countOfCombo=0;
    


    private void OnTriggerEnter(Collider other)
    {
        countOfCombo++;
        
    }

    
}
