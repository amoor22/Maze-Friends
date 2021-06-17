using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightFollow : MonoBehaviour
{
    
    #region Variables
    private Transform toFollow;
    #endregion

    #region Unity methods

    void Start()
    {

    }

    
    void FixedUpdate()
    {
        if (toFollow)
        {
            gameObject.transform.position = new Vector3(toFollow.position.x, toFollow.position.y, gameObject.transform.position.z);
        }
        else
        {
            toFollow = gameObject.GetComponentInParent<Transform>();
            // if (gameObject.name == "SpotLight Follow 1")
            // {
            //     if (GameObject.FindGameObjectWithTag("Player1")) {
            //         toFollow = GameObject.FindGameObjectWithTag("Player1").transform;
            //     }
            // }
            // else if (gameObject.name == "SpotLight Follow 2")
            // {
            //     if (GameObject.FindGameObjectWithTag("Player2")) {
            //         toFollow = GameObject.FindGameObjectWithTag("Player2").transform;
            //     }
            // }
        }
    }
  
    #endregion
}
