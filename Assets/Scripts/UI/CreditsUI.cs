using UnityEngine;
using TMPro;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text credits;
    [SerializeField] private TMP_Text bottom;

    private void Start()
    {
        string creditsString = "The Bug Race\n" +
            "\nMade for the Github Game Off Jam 2021\n" +
            "by BalottaGames\n" +
            "\nlynkx21\t\tJacoGasp\n" +
            "\nThanks for playing";
        title.text = UIHelper.WriteStringToFont("Credits".ToUpper());
        credits.text = UIHelper.WriteStringToFont(creditsString.ToUpper());
        bottom.text = UIHelper.WriteStringToFont("Press ESC to exit menu".ToUpper());
    }
}
