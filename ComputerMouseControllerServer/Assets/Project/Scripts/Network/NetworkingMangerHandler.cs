using kcp2k;
using Mirror;
using System;
using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class NetworkingMangerHandler : NetworkManager
{
    [Space]
    [Header("Start Server Settings")]
    [SerializeField] private TMP_InputField _port = null;

    [Space]
    [Header("Client Connect Settings")]
    [SerializeField] private TMP_InputField _connectPort = null;
    [SerializeField] private TMP_InputField _connectIP = null;

    public event Action OnServerStarted = null;

    private KcpTransport _kcpTransport = null;

    private new void Awake() => _kcpTransport = GetComponent<KcpTransport>();

    public void StartMirrorServer()
    {
        ushort port = ushort.Parse(_port.text);

        string localIp = GetLocalIP();

        _kcpTransport.Port = port;
        networkAddress = localIp;

        StartHost();
    }

    public void StartMirrorClient()
    {
        _kcpTransport.Port = ushort.Parse(_connectPort.text);

        networkAddress = _connectIP.text;

        StartClient();
    }

    public override void OnStartServer() => OnServerStarted?.Invoke();

    private string GetLocalIP()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        }

        return string.Empty;
    }
}