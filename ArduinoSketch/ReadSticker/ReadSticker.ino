#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9          // Configurable, reset pin for RC522
#define SS_PIN          10         // Configurable, slave select pin for RC522

MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance

String nuidString = "";

void setup() {
  Serial.begin(9600);
  SPI.begin();                  // Init SPI bus
  mfrc522.PCD_Init();           // Init MFRC522
  delay(100);
  Serial.println("Scan an Ntag213 tag...");
}

void loop() {
  // look for new cards
  if ( ! mfrc522.PICC_IsNewCardPresent() || ! mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  // clear string
  nuidString = "";

  // convert NUID to hex string
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    if (mfrc522.uid.uidByte[i] < 0x10) {
      nuidString += "0";
    }
    nuidString += String(mfrc522.uid.uidByte[i], HEX);
  }

  nuidString.toUpperCase();
  Serial.print("Scanned NUID: ");
  Serial.println(nuidString);

  // halt PICC (stop reading current tag)
  mfrc522.PICC_HaltA();
  mfrc522.PCD_StopCrypto1();

  delay(1500); // debounce delay
}