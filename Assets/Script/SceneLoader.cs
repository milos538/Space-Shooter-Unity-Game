using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        int sceneLoaderCount = FindObjectsOfType<SceneLoader>().Length;
        if(sceneLoaderCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public static void playGame()
    {
        SceneManager.LoadScene(1);
    }
    public static void playerDead()
    {
        SceneManager.LoadScene(2);
    }
    public static void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public static void quitGame()
    {
        Application.Quit();
    }

    // Kill player
    public void playerIsDead(GameObject player)
    {
        StartCoroutine(destroyPlayer(player));
    }
    private static IEnumerator destroyPlayer(GameObject player)
    {
        player.gameObject.transform.DetachChildren();
        Destroy(player);
        List<GameObject> parts = new List<GameObject>();
        parts.Add(GameObject.Find("body"));
        parts.Add(GameObject.Find("left wing"));
        parts.Add(GameObject.Find("right wing"));
        parts.Add(GameObject.Find("top"));
        parts.Add(GameObject.Find("left gun"));
        parts.Add(GameObject.Find("right gun"));
        parts.Add(GameObject.Find("left engine"));
        parts.Add(GameObject.Find("right engine"));
        for (var i = 0; i < parts.Capacity; i++)
        {
            Rigidbody2D rb = parts[i].AddComponent<Rigidbody2D>();
            rb.velocity = new Vector3(Random.value * 3, Random.value * 3, parts[i].transform.position.z);
            rb.AddTorque(10f);
        }
        yield return new WaitForSeconds(5f);
        playerDead();
    }
}
