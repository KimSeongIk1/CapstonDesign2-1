using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourTal : MonoBehaviour
{
    [SerializeField] private bool onPortal = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPortal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPortal = false;
        }
    }
    public void portal()
    {
        if (onPortal == true)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
