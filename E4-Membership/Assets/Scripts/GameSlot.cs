using TMPro;
using UnityEngine;

public class GameSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTitleLabel;

    public void SetGame(string gameTitle)
    {
        gameTitleLabel.text = gameTitle;
    }
}
