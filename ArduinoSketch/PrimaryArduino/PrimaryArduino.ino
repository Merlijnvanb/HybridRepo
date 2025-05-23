#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9          // Configurable, reset pin for RC522
// SDA = SS
#define SS_PIN          10         // Configurable, slave select pin for RC522
// MOSI : Pin 11
// MISO : Pin 12
// SCK : Pin 13

MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance

void setup() {
  button_setup();
  rotary_setup();

  // delay to allow Unity to handshake
  delay(2000);
  Serial.begin(9600);
  SPI.begin();                  // Init SPI bus
  mfrc522.PCD_Init();           // Init MFRC522  

  // delay before first message
  delay(100);
  Serial.println("Primary arduino initialized...");
  Serial.flush();
}

void loop() {
  // tag checking code
  if(check_for_tag()){
    send_data(1, get_tag_nuid()) ;

    delay(1500); // debounce delay
  }

  // button loop
  button_loop();
  rotary_loop();

  //delay(30);
}