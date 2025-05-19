int pushButton = 2;  // Pin voor de knop
int ledPin = 9;      // Pin voor de LED in de knop

int brightness = 0;  // Start helderheid van de LED
int fadeAmount = 5;  // Hoeveel de helderheid verandert per stap

bool ledState = true;       // Houdt bij of de LED aan (true) of uit (false) is
bool buttonPressed = false; // Houdt bij of de knop is ingedrukt
bool lastLedState = true;   // Houdt bij wat de vorige status van de LED was

void button_setup(){
  // set up pins
  pinMode(pushButton, INPUT_PULLUP); // Knop als input met interne pull-up
  pinMode(ledPin, OUTPUT);           // LED als output
}

void button_loop(){
  // Lees de status van de knop
  int buttonState = digitalRead(pushButton);

  // Detecteer een knopdruk (alleen bij verandering van status)
  if (buttonState == LOW && !buttonPressed) { // Knop ingedrukt
    buttonPressed = true;
    ledState = !ledState; // Wissel LED-status
    send_data(3, "1");
  }

  if (buttonState == HIGH) { // Knop losgelaten
    buttonPressed = false;
    send_data(3, "0");
  }

  // Alleen printen als de LED-status is veranderd
  if (ledState != lastLedState) {
    //Serial.println(ledState ? 0 : 1);
    lastLedState = ledState; // Update de laatste status
  }

  // Update de LED
  if (ledState) {
    // Laat de LED pulseren
    analogWrite(ledPin, brightness);

    // Verander de helderheid voor de volgende loop
    brightness += fadeAmount;

    // Keer om als helderheid de grenzen bereikt
    if (brightness <= 0 || brightness >= 255) {
      fadeAmount = -fadeAmount;
    }
  } else {
    digitalWrite(ledPin, LOW); // Zet LED uit
  }
}