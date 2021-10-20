using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Photon.Pun;

public class SerialDataSend : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM4", 115200);
    private PhotonView PV;
    void Start()
    {
        data_stream.Open(); //Initiate the Serial stream
        PV = GetComponent<PhotonView>();
    }

    private void Pattern1()
    {
        data_stream.WriteLine("L1");
        //PV.RPC("RPC_DataSync", RpcTarget.AllBuffered, "L1");
    }

    private void Pattern2()
    {
        data_stream.WriteLine("L2");
        //PV.RPC("RPC_DataSync", RpcTarget.AllBuffered, "L2");
    }

    private void Pattern3()
    {
        data_stream.WriteLine("L3");
        //PV.RPC("RPC_DataSync", RpcTarget.AllBuffered, "L3");
    }

    private void Pattern4()
    {
        data_stream.WriteLine("L4");
        //PV.RPC("RPC_DataSync", RpcTarget.AllBuffered, "L4");
    }

    private void Pattern5()
    {
        data_stream.WriteLine("L5");
        //PV.RPC("RPC_DataSync", RpcTarget.AllBuffered, "L5");
    }

    [PunRPC]
    void RPCDataSync(string input)
    {
        data_stream.WriteLine(input);
        Debug.Log(input);
        Debug.Log("data Sent");
    }
}


