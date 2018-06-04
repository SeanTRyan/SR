using Managers;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerPortrait : MonoBehaviour
{
    [SerializeField] private GameObject m_parentPanel = null;
    [SerializeField] private int m_playerPortraitIndex = 0;

    private Image m_characterImage = null;

    private void Awake()
    {
        m_characterImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        PlayerSettings.SetCharacter(null, m_playerPortraitIndex);
        PlayerSettings.SetCharacterPortrait(null, m_playerPortraitIndex);
    }

    // Update is called once per frame
    void Update()
    {
        Sprite characterPortrait = PlayerSettings.GetCharacterPortrait(m_playerPortraitIndex);

        m_characterImage.sprite = characterPortrait;
    }
}
