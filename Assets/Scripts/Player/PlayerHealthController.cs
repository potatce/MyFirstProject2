using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerController target;
    public Image[] hearts;
    
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < target.playerHealth)
            {
                hearts[i].color = Color.red;
            }

            else
            {
                hearts[i].color = new Color(1, 1, 1,0.5f);
            }
        }
    }
}
