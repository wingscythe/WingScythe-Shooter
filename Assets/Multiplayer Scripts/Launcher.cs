using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    //# symbol is for pre-processing directives
    //# symbol always starts with #___ and ends with #end___
    string gameVersion = "1";

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject startMenu;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject loadingLabel;




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
    public void Connect() 
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

    //Checks if player was connected / disconnected
    // - If player was connected make player join random room
    // - If not, send a message/cause
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster Called");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected Called", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinRandomFailed");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom Called");
    }

    //Serialized to expose non public field
    //Non public so it can only be modified in inspector and not in other scripts
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

   /* public override void CreateRoom()
    {

    }
   */
}