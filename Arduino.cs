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
	bool hasHeardFromArduino = false;
	float timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<TextEdit>("ArduinoContents");
		ItemList items = GetNode<ItemList>("DeviceList");
		serialPort = new SerialPort();
		serialPort.PortName = "COM8";
		serialPort.BaudRate = 115200; //make sure this is the same in Arduino as it is in Godot.
		serialPort.Open();
		if(serialPort.IsOpen){
			items.AddItem(serialPort.PortName);
		}
		
	}

	public override void _Process(double delta)
{
		if(!serialPort.IsOpen) return; //checks if serial port is open, if it's not do nothing.
		if (text.TextChanged -= "*")
		{
			items.AddItem(text.Text);
		}
		//text.Text += '\n' + serialPort.PortName;
		//string serialMessage = serialPort.ReadLine();		
		//Debug.WriteLine(serialMessage);
		//try{
			//double messageValue = double.Parse(serialMessage);
			//try{
				//if(messageValue > 100.0){
					//text.Text = serialMessage;
				//}
				//else{
					//text.Text = "I'm Cold";
				//}
			//}
			//catch{
				//text.Text = "Can't validate";
			//}
		//}
		//catch{
			//text.Text += serialPort.PortName;
		//}
		//if(serialMessage.Contains("Hello Godot") && !hasHeardFromArduino){
			//text.Text = "Hello Arduino, I hear you :)";
			//hasHeardFromArduino = true;
			//timer = Time.GetTicksMsec();
			//
		//}
//
		//if(hasHeardFromArduino && Time.GetTicksMsec() - timer > 3000){
			//text.Text += "\n Turning on the Light for you :D";
			//serialPort.Write("1");
			//hasHeardFromArduino = false;
		//}		
	}
}
