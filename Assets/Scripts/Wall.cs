using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    
    #region Variables

    #endregion

    #region Unity methods

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other) {
        // if (other.gameObject.CompareTag("Wall"))
        // {
        //     Debug.Log("Sike");
        // }
        // else {
        //     Debug.Log("No soke");
        // }
    }

    #endregion
}
