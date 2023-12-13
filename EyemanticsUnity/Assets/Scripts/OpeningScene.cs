using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;
using System.Runtime.InteropServices;

public class OpeningScene : MonoBehaviour
{

    [DllImport("UnityMagicLeap", CallingConvention = CallingConvention.Cdecl, EntryPoint = "UnityMagicLeap_SegmentedDimmer_KeepAlpha")]

    private static extern void SetSegmentedDimmerKeepAlpha(bool status);


    public OpeningScene ()
    {
        SetSegmentedDimmerKeepAlpha(true);

    }


    void Awake()
    {
        SetSegmentedDimmerKeepAlpha(true);
    }

    public TMP_Text IpAddressText;
    // Start is called before the first frame update
    void Start()
    {
        TCPServer.connectionIP = TCPServer.GetLocalIPAddress();
        IpAddressText.text = TCPServer.connectionIP;

        ThreadStart ts = new ThreadStart(TCPServer.Connect);
        TCPServer.connThread = new Thread(ts);
        TCPServer.connThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(TCPServer.connected)
        {
            OpenMainScene();
        }
        
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene("GazeAndMeshing", LoadSceneMode.Single);
    }
}
