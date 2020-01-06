using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour 
{
    const string playerNamePrefKey = "PlayerName";

    private void Start()
    {
        string defaultName = string.Empty;
        InputField inputField = GetComponent<InputField>();

        if(inputField != null)
        {
            defaultName = PlayerPrefs.GetString(playerNamePrefKey);
            inputField.text = defaultName;
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;


        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}