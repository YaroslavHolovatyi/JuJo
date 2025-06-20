
using TMPro;
using UnityEngine;

public class UserScoreEntry : MonoBehaviour
{
    public UnityEngine.UI.Image userImage;
    public TMP_Text userName;
    public TMP_Text userScore;
    
    public void SetData(string name, int score, Sprite avatar = null)
    {
        if (avatar != null) userImage.sprite = avatar;

        userName.text = name;
        userScore.text = score.ToString();
    }
    
}
