using UnityEngine;
using System;
using System.IO.Ports;

public class ArduinoBridge : MonoBehaviour
{
    public static event Action<ArduinoMessageData> onReceiveMessage;
    
    [SerializeField] private string port;

    private SerialPort stream;
    private bool isOpen;
    private string lastMessage;

    private void Start()
    {
        OpenStream();
    }

    private void Update()
    {
        if (!isOpen) return;
        string line = stream.ReadLine();

        // this might drop inputs, test test!
        if (line != lastMessage)
        {
            ParseAndSendMessage(line);
            lastMessage = line;
        }
    }

    public void OpenStream()
    {
        // if port is null, port is COM1
        port ??= "COM1";
        try
        {
            stream = new SerialPort(port, 115200);
            stream.ReadTimeout = 50;
            stream.Open();
            isOpen = true;
        }
        catch (System.Exception e)
        {
            isOpen = false;
            Debug.LogError(e.Message);
            Debug.LogError("Could not open SerialPort stream");
        }
        
        Send("Connected with unity!");
    }

    public void CloseStream()
    {
        stream.Close();
        isOpen = false;
    }

    public void Send(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    private void ParseAndSendMessage(string msg)
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
