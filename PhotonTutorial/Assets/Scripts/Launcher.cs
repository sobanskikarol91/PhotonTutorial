using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    [SerializeField] byte maxPlayersPErRoom = 4;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField] private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField] private GameObject progressLabel;


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        HideProgressBar();
    }

    private void HideProgressBar()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
            PhotonNetwork.JoinRandomRoom();
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        HideProgressBar();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPErRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }
}