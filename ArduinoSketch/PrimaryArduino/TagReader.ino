// tag reader
bool check_for_tag(){

  // look for new cards
  if ( ! mfrc522.PICC_IsNewCardPresent() || ! mfrc522.PICC_ReadCardSerial()) {
    return false;
  }
  else { return true; }

}

String get_tag_nuid(){
  String nuidString = "";

  // convert NUID to hex string
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    if (mfrc522.uid.uidByte[i] < 0x10) {
      nuidString += "0";
    }
    nuidString += String(mfrc522.uid.uidByte[i], HEX);
  }

  nuidString.toUpperCase();
  //Serial.print("Scanned NUID: ");
  //Serial.println(nuidString);

  // halt PICC (stop reading current tag)
  mfrc522.PICC_HaltA();
  mfrc522.PCD_StopCrypto1();

  return nuidString;
}