void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
}

void loop() {
  
  float testVal = sin(millis()*0.1);
  send_data(6, String(testVal));

}

// 0 = none
// 1 = nfc
// 2 = air pressure
// 3 = music crank
// 4 = power crank
// 5 = end shift button
// 6 = rope elevator
void send_data(int eventType, String value)
{
  String testString = String("MSG:") + eventType + String(":") + value;
  Serial.println(testString);
}
