using Godot;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.IO;
using System.Net;

public partial class Arduino : Node2D
{
	SerialPort serialPort;
	TextEdit text;
	ItemList items;
	int timeElapsedCycles = 0;
	bool hasHeardFromArduino = false;
	float timer;

	public override void _Ready()
	{
		text = GetNode<TextEdit>("ArduinoContents");
		ItemList items = GetNode<ItemList>("DeviceList");
		serialPort = new SerialPort();
		serialPort.PortName = "COM8";
		serialPort.BaudRate = 115200; 
		serialPort.Open();
	}

	public override void _Process(double delta)
	{
		if(timeElapsedCycles < 1)
		{
			string url = "http://192.168.1.230/hello";

			// Deprecated since 2012, examine updated operations later - 
			// Filestreams, Httpclients, etc...
			var jsonData = new WebClient().DownloadString(url); 

			Debug.WriteLine(jsonData);
			timeElapsedCycles += 1;
			text.Text = jsonData;
		}
		else
		{
			return;
		}
	}
}
