/* Wi-Fi STA Connect and Disconnect Example

   This example code is in the Public Domain (or CC0 licensed, at your option.)

   Unless required by applicable law or agreed to in writing, this
   software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
   CONDITIONS OF ANY KIND, either express or implied.
   
*/
#include <WiFi.h>
#include <string.h>
#include <ESPAsyncWebServer.h>
#include <ArduinoJson.h> 

const char* wifi_network_ssid = "GoogleFiber";
const char* wifi_network_password =  "goCougars";
 
AsyncWebServer server(80);

String globalAction;

const char index_html[] PROGMEM = "<form action='action' method='post' id='myForm'>\
<input type='submit' name='B1' value='B1' onclick='updateTextBox(this)'>\
<input type='submit' name='B2' value='B2' onclick='updateTextBox(this)'>\
<input type='text' id='textBox' readonly>\
</form>\
<script>\
function updateTextBox(btn) {\
  document.getElementById('textBox').value = btn.name;\
}\
</script>";

void action(AsyncWebServerRequest *request) {
  Serial.println("ACTION!");

  int params = request->params();
  for (int i = 0; i < params; i++) {
    AsyncWebParameter* p = request->getParam(i);
    Serial.printf("POST[%s]: %s\n", p->name().c_str(), p->value().c_str());
    if (p->name() == "action") {
      globalAction = p->value(); // Store the post form action
    }
  }
  request->send_P(200, "text/html", index_html);
}

void setup() {
 
  Serial.begin(115200);
  WiFi.mode(WIFI_MODE_APSTA);
 
  WiFi.begin(wifi_network_ssid, wifi_network_password);
 
 
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.println("Connecting to WiFi..");
  }
 
  Serial.print("ESP32 IP on the WiFi network: ");
  Serial.println(WiFi.localIP());
 
 
  server.on("/hello", HTTP_GET, [](AsyncWebServerRequest * request) {
 
    if (ON_STA_FILTER(request)) {
      request->send(200, "text/plain", "Hello from STA");
      return;
    }

  });

  server.on("/", HTTP_GET, [](AsyncWebServerRequest * request) {
    request->send_P(200, "text/html", index_html);
  });
  
  server.on("/action", HTTP_POST, action);
  server.on("/post", HTTP_POST, action);

  server.on("/light", HTTP_GET, [](AsyncWebServerRequest * request) {
 
    if (ON_STA_FILTER(request)) {
      request->send(200, "text/plain", "Turning on the lights");
      return;
    }
   
  });
 
  server.begin();
 
}
 
void loop() {}

