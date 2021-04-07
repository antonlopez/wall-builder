
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


[RequireComponent(typeof(ARRaycastManager))]
public class PlaceBrickOnPlane : MonoBehaviour
{

    public GameObject[] bricks;

    public Transform bricksParent;


    private List<GameObject> bricksPlaced;


    ARRaycastManager m_RaycastManager;
    public ARSession ar_session;

    private GameObject brickToPlace;
    private Vector3 brickSize;


    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public bool startScanning;
    private int brickId;
    private int brickIdSize;



    void Awake()
    {
        brickId = 0;
        m_RaycastManager = GetComponent<ARRaycastManager>();

        brickSize = bricks[0].transform.localScale;

        brickToPlace = bricks[brickId];
    }

    void Update()
    {
        PlaceBrick();

    }





    private void PlaceBrick()
    {

        if (!IsPointerOverUIObject())
        {


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {

                    if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                    {

                        Pose hitPose = s_Hits[0].pose;

                        var spawnedBrick = Instantiate(brickToPlace, hitPose.position, hitPose.rotation); // original one
                        spawnedBrick.transform.SetParent(bricksParent);

                        spawnedBrick.transform.position = hitPose.position;

                        spawnedBrick.transform.localScale = brickSize;

                        RotateBrick(spawnedBrick, brickIdSize);

                        SaveLoadManager.GetInstance().SaveOnPlace(brickId, spawnedBrick.transform.localPosition,
                            spawnedBrick.transform.localScale, spawnedBrick.transform.localRotation);

                    }
                }
            }

        }
    }


    private void RotateBrick(GameObject brick, int size) {

        var rotX = brick.transform.localRotation.x;
        var rotZ = brick.transform.localRotation.z;

        switch (size)
        {
            case 1:
                //small size
                brick.transform.localRotation = Quaternion.Euler(rotX, 90f, rotZ);
                break;
            case 2:
                //Regular size
                brick.transform.localRotation = Quaternion.Euler(rotX, 25f, rotZ);
                break;
            case 3:
                //large size
                brick.transform.localRotation = Quaternion.Euler(rotX, 45f, rotZ);
                break;
            default:
                brick.transform.localRotation = Quaternion.Euler(rotX, 180f, rotZ);
                break;
        }

    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //blue- 0 , green -1, pink - 2, red -3, yellow -4
    public void SetBrickColor(int id)
    {
        brickToPlace = bricks[id];
     
    }

    public void SetBrickSize(int size)
    {

        switch (size)
        {
            case 1:
                //small size
                brickSize = new Vector3(.025f, .025f, .05f);
                break;
            case 2:
                //Regular size
                brickSize = new Vector3(.05f, .05f, .1f);
                break;
            case 3:
                //large size
                brickSize = new Vector3(.1f, .1f, .2f);
                break;
            default:
                brickSize = new Vector3(.05f, .05f, .1f);
                break;
        }

        brickIdSize = size;

    }









}
