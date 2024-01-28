using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // for loading scene
    void Start()
    {
        if(PhotonNetwork.IsConnected){
            PhotonNetwork.ConnectUsingSettings();
        }else{
            PhotonNetwork.OfflineMode = true;
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("b");
        if(PhotonNetwork.OfflineMode){
            SceneManager.LoadScene("Lobby");
        }else{
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("a");
        SceneManager.LoadScene("Lobby");
    }
    public void CreateLocalRoom(){

    }
}
