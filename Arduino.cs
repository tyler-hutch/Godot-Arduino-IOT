using Godot;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Collections.Generic;

public partial class Arduino : Node2D
{
	SerialPort serialPort;
	TextEdit text;
	ItemList items;
	LineEdit temperatureValue;
	LineEdit temperatureSetPoint;
	int timeElapsedCycles = 0;
	string[] networkAddresses;
	bool hasHeardFromArduino = false;
	float timer;

	public override void _Ready()
	{
		networkAddresses = new string[2] {"http://192.168.1.230/hello", "http://192.168.1.130/hello"};
		text = GetNode<TextEdit>("ArduinoContents");
		items = GetNode<ItemList>("DeviceList");
		temperatureValue = GetNode<LineEdit>("TemperatureOutInfo");
		temperatureSetPoint = GetNode<LineEdit>("TemperatureInput");
		//serialPort = new SerialPort();
		//serialPort.PortName = "COM4";
		//serialPort.BaudRate = 115200; 
		//serialPort.Open();
		if(networkAddresses.Length > 0)
		{
			foreach(string address in networkAddresses)
			{
			items.AddItem(address);
			}
			items.Visible = true;
		}
	}

	public override void _Process(double delta)
	{
		if(timeElapsedCycles < 1)
		{
			//string url = "http://192.168.1.230/hello";

			// Deprecated since 2012, examine updated operations later - 
			// Filestreams, Httpclients, etc...
			//try
			//{
				//var jsonData = new WebClient().DownloadString(url); 
//
				//Debug.WriteLine(jsonData);
				//timeElapsedCycles += 1;
				//if(jsonData.Length == 2)
				//{
					//temperatureValue.Visible = true;
					//temperatureSetPoint.Visible = true;
					//
					//temperatureValue.Text = jsonData;
				//}
				//else
				//{
					//return;
				//}
			//}
			//catch
			//{
				//text.Visible = false;
			//}

		}
		var selectedIndex = items.GetSelectedItems();
		var selectedItem = items.GetItemText(selectedIndex[0]);
		if(selectedItem.Length > 0 && ! text.Text.Contains(selectedItem))
		{
			text.Text = selectedItem;
		}
		
	}
}


//private void _on_device_list_item_selected(long index)
//{
	//return items.// Replace with function body.
//}
