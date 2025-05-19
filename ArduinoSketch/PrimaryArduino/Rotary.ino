int pinA = 3; // Verbonden met CLK
int pinB = 4; // Verbonden met DT
int pinBtn = 5; // Verbonden met SW
int encoderPosCount = 0;
int pinALast;
int aVal;
boolean bCW;
boolean btnPressed = false;

void rotary_setup() {
  pinMode(pinA, INPUT);
  pinMode(pinB, INPUT);
  pinMode(pinBtn, INPUT_PULLUP);
  /* Lees Pin A uit
  De huidige staat van de pin reflecteert de laatste positie
  */
  pinALast = digitalRead(pinA);
  //Serial.begin(9600);
}

void rotary_loop() {
  aVal = digitalRead(pinA);
  if (aVal != pinALast){ // Dit betekent dat de knop gedraaid wordt
    // We moeten de draairichting bepalen door Pin B uit te lezen
    if (digitalRead(pinB) != aVal) { // Dit betekent dat Pin A als eerste is veranderd - we draaien met de klok mee
      encoderPosCount ++;
      bCW = true;
    } else { // Anders is Pin B als eerste veranderd en draaien we tegen de klok in
      bCW = false;
      encoderPosCount--;
    }
    //Serial.print("Gedraaid: ");
    //if (bCW){
    //  Serial.println("met de klok mee");
    //}else{
    //  Serial.println("tegen de klok in");
    //}
    //Serial.print("Encoder Positie: ");
    //Serial.println(encoderPosCount);
    send_data(2, to_string(encoderPosCount));
  }
  pinALast = aVal;
  
  if(digitalRead(pinBtn) == LOW && !btnPressed){ // Dit betekent dat de knop is ingedrukt
    btnPressed = true;
    //Serial.println("Knop ingedrukt");
  } else if (digitalRead(pinBtn) == HIGH && btnPressed){ // Dit betekent dat de knop is losgelaten
    btnPressed = false;
    //Serial.println("Knop losgelaten");
  }
}