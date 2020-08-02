using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviour
{
    //# symbol is for pre-processing directives
    //# symbol always starts with #___ and ends with #end___
    string gameVersion = "1";

    //Monobehaviour called before Start()
    void Awake() 
    {
        //This makes sure we can use PhotonNetwork.LoadLevel() 
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    /// Start the connection process
    /// - If already connected, join random room
    /// - If not, connect player to the instance of the Photon Cloud Network created for this game
    void Connect() 
    {
        // Check if connected
        if (PhotonNetwork.IsConnected)
        {
            // Join random room
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //Connect to instance of Photon Cloud Network
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
}