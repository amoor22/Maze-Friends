using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    
    #region Variables
    public GameObject cube;
    private float speed = 10f;
    private int direction;
    private Rigidbody rb;
    Vector3 movement;
    #endregion

    #region Unity methods

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.CompareTag("Player1")) {
            direction = 1;
        } else if (gameObject.CompareTag("Player2")) {
            direction = -1;
        }
    }

    
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = movement * speed * direction;
        // cube.transform.Translate(new Vector3(0, speed * direction * Time.deltaTime, 0));
        
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player1"))
        {   
            Destroy(gameObject);
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);
            try
            {
                if (SceneManager.GetActiveScene().buildIndex < 12)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                } 
                else 
                {
                    PlayerPrefs.SetInt("level", 0);
                    SceneManager.LoadScene(0);
                }
            }
            catch (Exception e)
            {
                PlayerPrefs.SetInt("level", 0);
                SceneManager.LoadScene(0);
            }
            // Destroy(gameObject);
        }
    }
    #endregion
}