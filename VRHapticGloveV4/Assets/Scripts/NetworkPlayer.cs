using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NetworkPlayer : MonoBehaviour
{
    public Transform networkPlayerHead;
    public Transform networkPlayerLeftHand;
    public Transform networkPlayerRightHand;

    
    private PhotonView photonView;

    private Transform myHeadRig;
    private Transform myLeftHandRig;
    private Transform myRightHandRig;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        

        try
        {
            OVRCameraRig OVRrig = FindObjectOfType<OVRCameraRig>();

            myHeadRig = OVRrig.transform.Find("TrackingSpace/CenterEyeAnchor");
            myLeftHandRig = OVRrig.transform.Find("TrackingSpace/LeftHandAnchor/OVRHandPrefab");
            myRightHandRig = OVRrig.transform.Find("TrackingSpace/RightHandAnchor/OVRHandPrefab");

        }
        catch
        {
            Debug.LogError("OVR not found");
        }      
    }
   
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            networkPlayerHead.gameObject.SetActive(false);
            networkPlayerLeftHand.gameObject.SetActive(false);
            networkPlayerRightHand.gameObject.SetActive(false);

            MapTransform(networkPlayerHead, myHeadRig);
            MapTransform(networkPlayerLeftHand, myLeftHandRig);
            MapTransform(networkPlayerRightHand, myRightHandRig);
        }
        
    }

    void MapTransform(Transform networkPlayer, Transform myTransform)
    {
        networkPlayer.position = myTransform.position;
        networkPlayer.rotation = myTransform.rotation;
    }
}
