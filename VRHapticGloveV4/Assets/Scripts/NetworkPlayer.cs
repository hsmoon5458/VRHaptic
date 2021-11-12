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
            
    public Transform[] networkPlayerLeftHandFingers, networkPlayerRightHandFingers;
    private Transform L1J1, L1J2, L1J3, L2J1, L2J2, L2J3, L3J1, L3J2, L3J3, L4J1, L4J2, L4J3, L5J1, L5J2, L5J3, LPalm;
    private Transform R1J1, R1J2, R1J3, R2J1, R2J2, R2J3, R3J1, R3J2, R3J3, R4J1, R4J2, R4J3, R5J1, R5J2, R5J3, RPalm;
    void Start()
    {
        
        photonView = GetComponent<PhotonView>();

        this.gameObject.name = photonView.Owner.NickName; //change the object name to NickName

        //catch the Transform of each joints 
        leftHandPrefab = GameObject.Find("HankOVRCameraRig/TrackingSpace/LeftHandAnchor/OVRCustomHandPrefab_L");
        rightHandPrefab = GameObject.Find("HankOVRCameraRig/TrackingSpace/RightHandAnchor/OVRCustomHandPrefab_R");

        L1J1 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_thumb0/b_l_thumb1");
        L1J2 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_thumb0/b_l_thumb1/b_l_thumb2");
        L1J3 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_thumb0/b_l_thumb1/b_l_thumb2/b_l_thumb3");
        
        L2J1 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_index1");
        L2J2 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_index1/b_l_index2");
        L2J3 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_index1/b_l_index2/b_l_index3");

        L3J1 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_middle1");
        L3J2 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_middle1/b_l_middle2");
        L3J3 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_middle1/b_l_middle2/b_l_middle3");

        L4J1 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_ring1");
        L4J2 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_ring1/b_l_ring2");
        L4J3 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_ring1/b_l_ring2/b_l_ring3");

        L5J1 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_pinky0/b_l_pinky1");
        L5J2 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_pinky0/b_l_pinky1/b_l_pinky2");
        L5J3 = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_pinky0/b_l_pinky1/b_l_pinky2/b_l_pinky3");

        LPalm = leftHandPrefab.transform.Find("OculusHand_L/b_l_wrist/b_l_forearm_stub");

        R1J1 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_thumb0/b_r_thumb1");
        R1J2 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_thumb0/b_r_thumb1/b_r_thumb2");
        R1J3 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_thumb0/b_r_thumb1/b_r_thumb2/b_r_thumb3");

        R2J1 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_index1");
        R2J2 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_index1/b_r_index2");
        R2J3 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_index1/b_r_index2/b_r_index3");

        R3J1 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_middle1");
        R3J2 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_middle1/b_r_middle2");
        R3J3 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_middle1/b_r_middle2/b_r_middle3");

        R4J1 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_ring1");
        R4J2 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_ring1/b_r_ring2");
        R4J3 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_ring1/b_r_ring2/b_r_ring3");

        R5J1 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_pinky0/b_r_pinky1");
        R5J2 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_pinky0/b_r_pinky1/b_r_pinky2");
        R5J3 = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_pinky0/b_r_pinky1/b_r_pinky2/b_r_pinky3");

        RPalm = rightHandPrefab.transform.Find("OculusHand_R/b_r_wrist/b_r_forearm_stub");

        try
        {
            OVRCameraRig OVRrig = FindObjectOfType<OVRCameraRig>(); //get OVRrig

            myHeadRig = OVRrig.transform.Find("TrackingSpace/CenterEyeAnchor");
            myLeftHandRig = OVRrig.transform.Find("TrackingSpace/LeftHandAnchor/OVRCustomHandPrefab_L");
            myRightHandRig = OVRrig.transform.Find("TrackingSpace/RightHandAnchor/OVRCustomHandPrefab_R");
            
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
            //Set network player layer as 'Network' and change camera culling mask to disable 'Network'
            //By doing so, network palyer is not visable, but it can be referenced for grabbing object

            //networkPlayerHead.gameObject.SetActive(false);
            //networkPlayerLeftHand.gameObject.SetActive(false);
            //networkPlayerRightHand.gameObject.SetActive(false);

            MapTransform(networkPlayerHead, myHeadRig);
            MapTransform(networkPlayerLeftHand, myLeftHandRig);
            MapTransform(networkPlayerRightHand, myRightHandRig);

            MapTransform(networkPlayerLeftHandFingers[0], L1J1);
            MapTransform(networkPlayerLeftHandFingers[1], L1J2);
            MapTransform(networkPlayerLeftHandFingers[2], L1J3);
            MapTransform(networkPlayerLeftHandFingers[3], L2J1);
            MapTransform(networkPlayerLeftHandFingers[4], L2J2);
            MapTransform(networkPlayerLeftHandFingers[5], L2J3);
            MapTransform(networkPlayerLeftHandFingers[6], L3J1);
            MapTransform(networkPlayerLeftHandFingers[7], L3J2);
            MapTransform(networkPlayerLeftHandFingers[8], L3J3);
            MapTransform(networkPlayerLeftHandFingers[9], L4J1);
            MapTransform(networkPlayerLeftHandFingers[10], L4J2);
            MapTransform(networkPlayerLeftHandFingers[11], L4J3);
            MapTransform(networkPlayerLeftHandFingers[12], L5J1);
            MapTransform(networkPlayerLeftHandFingers[13], L5J2);
            MapTransform(networkPlayerLeftHandFingers[14], L5J3);
            MapTransform(networkPlayerLeftHandFingers[15], LPalm);

            MapTransform(networkPlayerRightHandFingers[0], R1J1);
            MapTransform(networkPlayerRightHandFingers[1], R1J2);
            MapTransform(networkPlayerRightHandFingers[2], R1J3);
            MapTransform(networkPlayerRightHandFingers[3], R2J1);
            MapTransform(networkPlayerRightHandFingers[4], R2J2);
            MapTransform(networkPlayerRightHandFingers[5], R2J3);
            MapTransform(networkPlayerRightHandFingers[6], R3J1);
            MapTransform(networkPlayerRightHandFingers[7], R3J2);
            MapTransform(networkPlayerRightHandFingers[8], R3J3);
            MapTransform(networkPlayerRightHandFingers[9], R4J1);
            MapTransform(networkPlayerRightHandFingers[10], R4J2);
            MapTransform(networkPlayerRightHandFingers[11], R4J3);
            MapTransform(networkPlayerRightHandFingers[12], R5J1);
            MapTransform(networkPlayerRightHandFingers[13], R5J2);
            MapTransform(networkPlayerRightHandFingers[14], R5J3);
            MapTransform(networkPlayerRightHandFingers[15], RPalm);

        }

    }

    void MapTransform(Transform networkPlayer, Transform myTransform)
    {
        networkPlayer.position = myTransform.position;
        networkPlayer.rotation = myTransform.rotation;
    }

    void MapFingerTransform(Transform networkPlayer, Transform myTransform)
    {
        networkPlayer.position = myTransform.position;
        networkPlayer.rotation = myTransform.rotation;
    }


    /*
    //delay at start for the system to identify the bones at the beginning

    private OVRSkeleton leftHandSkeleton, rightHandSkeleton;


    // StartCoroutine(BoneIdentify()); // wait few second for the systems to recognize the bones properly at the beginning

    //leftHandSkeleton = leftHandPrefab.GetComponent<OVRSkeleton>();
    //rightHandSkeleton = rightHandPrefab.GetComponent<OVRSkeleton>();

    private IEnumerator BoneIdentify()
    {
        yield return new WaitForSeconds(2f);
        leftFingerBones = new List<OVRBone>(leftHandSkeleton.Bones);
        rightFingerBones = new List<OVRBone>(rightHandSkeleton.Bones);
    }
    */
}
