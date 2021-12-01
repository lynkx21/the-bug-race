using UnityEngine;
using TMPro;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text startText;
    [SerializeField] private TMP_Text menuText;
    [SerializeField] private TMP_Text creditsText;
    private float blinkTime = 0.7f;
    private float timer;

    private void Start()
    {
        titleText.text = UIHelper.WriteStringToFont("The Bug Race".ToUpper());
        startText.text = UIHelper.WriteStringToFont("Press Spacebar to start".ToUpper());
        menuText.text = UIHelper.WriteStringToFont("Press Escape for options".ToUpper());
        creditsText.text = UIHelper.WriteStringToFont("BalottaGames 2021".ToUpper());
        timer = blinkTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Blink();
            timer = blinkTime;
        }
    }

    private void Blink()
    {
        Color currentColor = startText.color;
        float alpha = Mathf.Abs(currentColor.a - 1);
        startText.color = new Color(1, 1, 1, alpha);
    }
}
