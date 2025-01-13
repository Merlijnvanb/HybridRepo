using UnityEngine;
using System;
using System.IO.Ports;

public class ArduinoBridge : MonoBehaviour
{
    public static event Action<ArduinoMessageData> onReceiveMessage;
    
    // we use two different Arduino's, one for reading and one for writing
    [SerializeField] private PortData[] ports;
    [SerializeField] private bool logIncomingMessages;

    private SerialPort[] streams;
    private bool[] streamIsOpen;

    private void Awake()
    {
        streams = new SerialPort[ports.Length];
        streamIsOpen = new bool[ports.Length];
        
        for (int i = 0; i < ports.Length; i++)
        {
            OpenStream(i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < ports.Length; i++)
        {
            if (!streamIsOpen[i] || ports[i].designation == PortDesignation.WRITE_ONLY) return;
            string line = streams[i].ReadLine();

            if (!string.IsNullOrEmpty(line))
            {
                if(logIncomingMessages) {Debug.Log(line);}
            
                ParseArduinoMessage(line);
            }
        }
    }

    public void OpenStream(int portID)
    {
        PortData port = ports[portID];
        
        try
        {
            streams[portID] = new SerialPort(port.name, 115200);
            streams[portID].ReadTimeout = port.readTimeout;
            streams[portID].WriteTimeout = port.writeTimeout;
            streams[portID].Open();
            streamIsOpen[portID] = true;
        }
        catch (System.Exception e)
        {
            streamIsOpen[portID] = false;
            Debug.LogError(e.Message);
            Debug.LogError("Could not open SerialPort at " + port.name);
        }
        
        Send("Connected with unity!", portID);
    }

    public void CloseStreams()
    {
        for (int i = 0; i < ports.Length; i++)
        {
            streams[i].Close();
            streamIsOpen[i] = false;
        }
    }

    public void Send(string message)
    {
        for (int i = 0; i < ports.Length; i++)
        {
            Send(message, i);
        }
    }
    
    public void Send(string message, int portID)
    {
        if (ports[portID].designation != PortDesignation.READ_ONLY)
        {
            streams[portID].WriteLine(message);
            streams[portID].BaseStream.Flush();
        }
    }

    private void ParseArduinoMessage(string msg)
    {
        // MSG:[MessageType]:[Data]
        if (msg.StartsWith("MSG:"))
        {
            // remove MSG:
            msg = msg.Remove(0, 4);
            
            // read and then remove message type from the string
            MessageType type = (MessageType)int.Parse(msg.Substring(0,1));
            msg = msg.Remove(0, 2);

            // compose and send data
            ArduinoMessageData data = new ArduinoMessageData(type, msg);
            onReceiveMessage?.Invoke(data);
        }
        // arduino is requesting data
        else if (msg == "PLS")
        {
            // add stuff here like lights status
        }
    }
}

public struct ArduinoMessageData
{
    public MessageType type;
    public string value;

    public ArduinoMessageData(MessageType _type, string _value)
    {
        type = _type;
        value = _value;
    }
}

[System.Serializable]
public struct PortData
{
    public string name;
    public PortDesignation designation;
    
    // set to -1 if infinite
    public int readTimeout;
    public int writeTimeout;
}

public enum PortDesignation{READ_ONLY, WRITE_ONLY, BOTH}

public enum MessageType
{
    // ignore
    NONE = 0,
    // read as string
    NFC = 1,
    // read as float
    AIR_PRESSURE = 2,
    CRANK_MUSIC = 3,
    CRANK_POWER = 4,
    BUTTON_END = 5,
    ROPE_ELEVATOR = 6
}
