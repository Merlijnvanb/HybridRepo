#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9          // Configurable, reset pin for RC522
#define SS_PIN          10         // Configurable, slave select pin for RC522

MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance

void setup() {
  Serial.begin(9600);
  SPI.begin();                  // Init SPI bus
  mfrc522.PCD_Init();           // Init MFRC522
  delay(100);
  Serial.println("Primary arduino initialized...");

}

void loop() {
  // put your main code here, to run repeatedly:

  if(check_for_tag()){
    send_data(1, get_tag_nuid()) ;

    delay(1500); // debounce delay
  }
}
