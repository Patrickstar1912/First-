using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckRoleDie : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject splashGO;

    public Text scoreText;
    private int Score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Game over ,other.gameObject.transform.position : {other.gameObject.transform.position}");
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            GameObject.Destroy(other.gameObject);
            gameOverText.SetActive(true);
            //Reload the current scene in seconds
            Invoke("ReloadScene", 3f);
            Instantiate(splashGO, other.gameObject.transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Role"))
        {
            GameObject.Destroy(other.gameObject);
            Instantiate(splashGO, other.gameObject.transform.position, Quaternion.identity);
            Score++;
            scoreText.text = "points：" + Score;
            //Reload the current scene in seconds
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}