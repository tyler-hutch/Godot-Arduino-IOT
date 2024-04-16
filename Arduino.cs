using Godot;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using NetHttp = System.Net.Http;


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
		networkAddresses = new string[2] {"http://192.168.1.230/hello", "http://192.168.1.130/badAddress"};
		text = GetNode<TextEdit>("ArduinoContents");
		items = GetNode<ItemList>("DeviceList");
		temperatureValue = GetNode<LineEdit>("TemperatureOutInfo");
		temperatureSetPoint = GetNode<LineEdit>("TemperatureInput");
		//serialPort = new SerialPort();
		//serialPort.PortName = "COM4";
		//serialPort.BaudRate = 115200; 
		//serialPort.Open();
		_send_temperature_values();
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
	{}

	private void _on_device_list_item_selected(long index)
	{
		var selectedItem = items.GetItemText((int)index);
		if(!text.Text.Contains(selectedItem))
		{
			// Deprecated since 2012, examine updated operations later - 
			// Filestreams, Httpclients, etc...
			try{text.Text = new WebClient().DownloadString(selectedItem);}
			catch{text.Text = "Bad Link";}
		}
	}
	
	private async void _send_temperature_values()
	{
		string jsonData = "{\"name\": \"John\", \"age\": 30}";

		// Create an instance of HttpClient
		using (NetHttp.HttpClient client = new NetHttp.HttpClient())
		{
			try
			{
				string url = "http://192.168.1.230/post";
				NetHttp.HttpResponseMessage response = 
				await client.PostAsync(url, new NetHttp.StringContent(jsonData,
										Encoding.UTF8, "application/json"));

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();
					Debug.WriteLine("Response received:");
					Debug.WriteLine(responseBody);
				}
				else // Client but failed
				{
					Debug.WriteLine($"Failed with status code {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}
