int tickCounter = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.setTimeout(50);
}

void loop() {
  
  float testVal = sin(millis()*0.1);
  send_data(6, String(testVal));

  if(tickCounter % 500 == 0)
  {
    read_data();
  }
  tickCounter++;
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
  String msg = String("MSG:") + eventType + String(":") + value;
  Serial.println(msg);
}

void read_data()
{
  // PLS = "i am now awaiting data"
  Serial.println("PLS");
  Serial.flush();
  delay(50);
  String gsm = Serial.readString();
}
