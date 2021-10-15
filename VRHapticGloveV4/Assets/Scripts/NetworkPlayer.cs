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

    //head and hand position settings
    private Transform myHeadRig;
    private Transform myLeftHandRig;
    private Transform myRightHandRig;

    //fingers setting
    private GameObject leftHandPrefab, rightHandPrefab; // to assign OVRskeleton in Network player, need access parent to GetComponent
    private OVRSkeleton leftHandSkeleton, rightHandSkeleton;
    private List<OVRBone> leftFingerBones, rightFingerBones;
    public List<Transform> networkPlayerLeftFingerBones, networkPlayerRightFingerBones;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        StartCoroutine(BoneIdentify()); // wait few second for the systems to recognize the bones properly at the beginning

        leftHandPrefab = GameObject.Find("HankOVRCameraRig/TrackingSpace/LeftHandAnchor/OVRHandPrefab");
        rightHandPrefab = GameObject.Find("HankOVRCameraRig/TrackingSpace/RightHandAnchor/OVRHandPrefab");
        Debug.Log(leftHandPrefab);
        Debug.Log(rightHandPrefab);
        try
        {
            OVRCameraRig OVRrig = FindObjectOfType<OVRCameraRig>(); //get OVRrig

            myHeadRig = OVRrig.transform.Find("TrackingSpace/CenterEyeAnchor");
            myLeftHandRig = OVRrig.transform.Find("TrackingSpace/LeftHandAnchor/OVRHandPrefab");
            myRightHandRig = OVRrig.transform.Find("TrackingSpace/RightHandAnchor/OVRHandPrefab");

            leftHandSkeleton = leftHandPrefab.GetComponent<OVRSkeleton>();
            rightHandSkeleton = rightHandPrefab.GetComponent<OVRSkeleton>();
            Debug.Log(leftHandSkeleton);
            Debug.Log(rightHandSkeleton);
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

    //delay at start for the system to identify the bones at the beginning
    private IEnumerator BoneIdentify()
    {
        yield return new WaitForSeconds(2f);
        leftFingerBones = new List<OVRBone>(leftHandSkeleton.Bones);
        rightFingerBones = new List<OVRBone>(rightHandSkeleton.Bones);
    }
}
