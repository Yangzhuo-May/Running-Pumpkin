using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lifeIcons; 
    public Sprite heartFull;

    private void Start()
    {
        // 初始化时显示最大生命
        UpdateLifeUI(3);  // 假设玩家初始时有3条生命
    }

    // 更新生命值 UI
    public void UpdateLifeUI(int currentLife)
    {
        // 更新生命图标显示
        for (int i = 0; i < lifeIcons.Length; i++)
                {
                    if (i < currentLife)
                    {
                        lifeIcons[i].sprite = heartFull;  // 设置为满的心形图标
                        lifeIcons[i].gameObject.SetActive(true);  // 显示心形图标
                    }
                    else
                    {
                        lifeIcons[i].gameObject.SetActive(false);  // 隐藏心形图标
                    }
                }
    }
}

