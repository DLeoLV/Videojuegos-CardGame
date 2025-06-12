using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class CardGameNetworkManager : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;
    public Button connectButton;
    public TMP_Text buttonText;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
        connectButton.interactable = false;
        buttonText.text = PhotonNetwork.NetworkClientState.ToString();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(null, options);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room. Players in room: " +
        PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered. Total players: " +
        PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 &&
        PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}