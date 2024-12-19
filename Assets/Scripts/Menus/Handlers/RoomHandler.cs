using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomHandler : MonoBehaviour
{
    public TMP_Text roomID;
    public GameObject roomBtnPrefab;
    public Transform roomListParent;
    public List<GameObject> allRooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        updateRoomsList();
    }

    public void onClickJoin()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.onClickJoin(roomID.text);
    }

    public void onClickBack()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.MAINMENU);
    }

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    clearAllRooms();
    //    base.OnRoomListUpdate(roomList);
    //    foreach (RoomInfo room in roomList)
    //    {
    //        if (room.IsOpen)
    //        {
    //            GameObject temp = Instantiate(roomBtnPrefab, roomListParent);
    //            RoomBtnPrefabHandler tempHandler = temp.GetComponent<RoomBtnPrefabHandler>();
    //            tempHandler.roomName.text = room.Name.ToString();
    //            allRooms.Add(temp);
    //        }
    //    }
    //}

    public void updateRoomsList()
    {
        clearAllRooms();
        foreach (RoomInfo room in SceneManager.Instance.allRooms)
        {
            if (room.IsOpen)
            {
                GameObject temp = Instantiate(roomBtnPrefab, roomListParent);
                RoomBtnPrefabHandler tempHandler = temp.GetComponent<RoomBtnPrefabHandler>();
                tempHandler.roomName.text = room.Name.ToString();
                allRooms.Add(temp);
            }
        }
    }

    public void clearAllRooms()
    {
        if (allRooms.Count > 0)
        {
            for (int i = 0; i < allRooms.Count; i++)
            {
                GameObject temp;
                temp = allRooms[i];
                allRooms.Remove(temp);
                Destroy(temp);
                //allRooms.Remove(allRooms[i]);
                
            }
            //foreach (GameObject r in allRooms)
            //{
            //    allRooms.Remove(r);
            //    Destroy(r);
            //}
        }
    }
}
