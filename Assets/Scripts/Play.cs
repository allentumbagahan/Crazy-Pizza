using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public AudioSource _audioSource;
    void Start()
    {

    }

 
    void Update()
    {

    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void OnMouseDown()
    {
        Debug.Log("Object Clicked!");
        _audioSource.Play();
        ChangeScene("Loading Scene");
        //ChangeScene("In-Game");
    }
}
