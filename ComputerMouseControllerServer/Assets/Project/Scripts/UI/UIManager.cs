using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject _menuPanel = null;

    [SerializeField] private GameObject _startServerMenu = null;
    [SerializeField] private GameObject _connectToServerMenu = null;
    [SerializeField] private GameObject _inputPanel = null;

    private void Awake()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            _connectToServerMenu.SetActive(true);
            _inputPanel.SetActive(true);
            _menuPanel = _connectToServerMenu;
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            _startServerMenu.SetActive(true);
            _inputPanel.SetActive(false);
            _menuPanel = _startServerMenu;
        }

        var networkManager = FindObjectOfType<NetworkingMangerHandler>();

        networkManager.OnServerStarted += () => _menuPanel.SetActive(false);
    }
}
