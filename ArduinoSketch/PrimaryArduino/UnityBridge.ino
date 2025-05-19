// 0 = none
// 1 = nfc
// 2 = crank
// 3 = button
void send_data(int eventType, String value)
{
  String msg = String("MSG:") + eventType + String(":") + value;
  Serial.println(msg);
}

void read_data()
{
  // PLS = "i am now awaiting data"
  //Serial.println("PLS");
  Serial.flush();
  delay(50);
  String gsm = Serial.readString();
}