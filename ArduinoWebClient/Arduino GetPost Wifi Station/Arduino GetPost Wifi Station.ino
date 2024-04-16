/* Wi-Fi STA Connect and Disconnect Example

   This example code is in the Public Domain (or CC0 licensed, at your option.)

   Unless required by applicable law or agreed to in writing, this
   software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
   CONDITIONS OF ANY KIND, either express or implied.
   
*/
#include <WiFi.h>
#include <ESPAsyncWebServer.h>
#include <ArduinoJson.h> 

const char* wifi_network_ssid = "GoogleFiber";
const char* wifi_network_password =  "goCougars";
 
AsyncWebServer server(80);
 

// void handlePost() {
//   StaticJsonDocument<200> doc;
//   DeserializationError error = deserializeJson(doc, server.arg("plain"));

//   if (error) {
//     server.send(400, "text/plain", "Failed to parse JSON");
//     return;
//   }

//   String response;
//   serializeJson(doc, response);
//   server.send(200, "application/json", response);
// }

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

server.on("/post", HTTP_POST, [](AsyncWebServerRequest * request){
    
    if (ON_STA_FILTER(request)) {
      request->send(200, "text/plain", "Post Registered");
      return;
    }
  });

  server.on("/light", HTTP_GET, [](AsyncWebServerRequest * request) {
 
    if (ON_STA_FILTER(request)) {
      request->send(200, "text/plain", "Turning on the lights");
      return;
    }
   
  });
 
  server.begin();
 
}
 
void loop() {}

