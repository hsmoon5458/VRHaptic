using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RPCDataSync : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    void Update()
    {
        PV.RPC("GameStepVariableSetup", RpcTarget.AllBuffered, RoomGameManager.gameStep);
    }

    [PunRPC]
    void GameStepVariableSetup(int step)
    {
        RoomGameManager.gameStep = step;
    }
}
