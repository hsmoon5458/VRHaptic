using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialDataSend : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM4", 115200);
    void Start()
    {
        data_stream.Open(); //Initiate the Serial stream
    }

    void Update()
    {

    }

    //touch with other user
    private void Pattern1()
    {
        data_stream.WriteLine("L1");
        FingerRead.L1J3 = false;
    }

    //creating object
    private void Pattern2()
    {
        data_stream.WriteLine("L2");
        FingerRead.L1J3 = false;
    }

    //locating object
    private void Pattern3()
    {
        data_stream.WriteLine("L3");
        FingerRead.L1J3 = false;
    }

    //
    private void Pattern4()
    {
        data_stream.WriteLine("L4");
        FingerRead.L1J3 = false;
    }

    private void Pattern5()
    {
        data_stream.WriteLine("L5");
        FingerRead.L1J3 = false;
    }

}


