using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerup : MonoBehaviour
{
    
    #region Variables
    public float lightIncrease = 5f;
    #endregion

    #region Unity methods

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            StartCoroutine(LightCoroutine(other.gameObject));
        }
    }
    IEnumerator LightCoroutine(GameObject other)
    {
        Light spotlight = other.GetComponentInChildren<Light>();
        spotlight.spotAngle = spotlight.spotAngle + lightIncrease;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(5f);
        spotlight.spotAngle = spotlight.spotAngle - lightIncrease;
        Destroy(gameObject);
    }
    #endregion
}
