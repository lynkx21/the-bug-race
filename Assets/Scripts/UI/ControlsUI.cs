using UnityEngine;
using TMPro;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text controls;
    [SerializeField] private TMP_Text bottom;

    private void Start()
    {
        string controlsString = "MOVE\n" +
            "WASD or ARROWS\n\n" +
            "PRIMARY SHOOT\t\t\n" +
            "SPACEBAR or LEFT CLICK\n\n" +
            "SECONDARY SHOOT\t\t\t\n" +
            "LEFT ALT or RIGHT CLICK";            ;
        title.text = UIHelper.WriteStringToFont("Controls".ToUpper());
        controls.text = UIHelper.WriteStringToFont(controlsString.ToUpper());
        bottom.text = UIHelper.WriteStringToFont("Press ESC to exit menu".ToUpper());
    }
}
