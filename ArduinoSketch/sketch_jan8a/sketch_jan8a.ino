int tickCounter = 0;

int rotaryS1 = 4;
int rotaryS2 = 5;
int rotS1New = 0;
int rotS1Prev = 0;
int rotaryCounter = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.setTimeout(50);

  pinMode(rotaryS1, INPUT);
  pinMode(rotaryS2, INPUT);
}

void loop() {
  // read and send elevator values
  rotS1New = digitalRead(rotaryS1);
  if(rotS1New != rotS1Prev)
  {
    rotS1Prev = rotS1New;
    if (digitalRead(rotaryS2) == rotS1New){rotaryCounter++;}
    else {rotaryCounter--;}
    rotaryCounter = max(rotaryCounter, 0);
    send_data(6, String(rotaryCounter));
  }

  if(tickCounter % 500 == 0)
  {
    //read_data();
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
  //Serial.println("PLS");
  Serial.flush();
  delay(50);
  String gsm = Serial.readString();
}
