using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameNetworkManager : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ConnectToServer();
    }
    public void ConnectToServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            isConnecting = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            JoinMatchmaking();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        if (isConnecting)
        {
            JoinMatchmaking();
            isConnecting = false;
        }
    }

    private void JoinMatchmaking()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(null, options);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room. Players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered. Total players: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}