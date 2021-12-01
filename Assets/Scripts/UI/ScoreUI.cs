using UnityEngine;
using TMPro;
using Jam.Architecture;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private string room = "ROOM 00";
   
    private void Start()
    {
        GameManager.Instance.roomChangeEvent += OnRoomUpdate;
        text.text = UIHelper.WriteStringToFont(room.ToUpper());
    }

    private void OnRoomUpdate(int roomNumber)
    {
        string roomNumberString = roomNumber.ToString().PadLeft(2, '0');
        string roomScore = $"ROOM {roomNumberString}";
        text.text = UIHelper.WriteStringToFont(roomScore);
    }
}
