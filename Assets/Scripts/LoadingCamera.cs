using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCamera : MonoBehaviour
{
    
    #region Variables
    public float speed = 5f;
    #endregion

    #region Unity methods

    void Start()
    {
        
    }

    
    void Update()
    {
        // transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speed * Time.deltaTime, transform.eulerAngles.z);
    }
  
    #endregion
}
