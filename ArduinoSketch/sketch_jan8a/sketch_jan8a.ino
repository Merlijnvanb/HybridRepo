int tickCounter = 0;

int rotaryS1 = 4;
int rotaryS2 = 5;
int rotS1New = 0;
int rotS1Prev = 0;
int rotaryCounter = 0;

 /* MFRC522 pin layout
 * -----------------------------------
 *             MFRC522      Arduino    
 *             Reader/PCD   Uno/101    
 * Signal      Pin          Pin        
 * ------------------------------------
 * RST/Reset   RST          9          
 * SPI SS      SDA(SS)      10         
 * SPI MOSI    MOSI         11
 * SPI MISO    MISO         12
 * SPI SCK     SCK          13
 */

#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9           // Configurable, see typical pin layout above
#define SS_PIN          10          // Configurable, see typical pin layout above

MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.setTimeout(50);

  SPI.begin();                                                  // Init SPI bus
  mfrc522.PCD_Init();                                           // Init MFRC522 card

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

  // MFRC522 things (keep at end)
  // Reset the loop if no new card present on the sensor/reader. This saves the entire process when idle.
  if ( ! mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  // Select one of the cards
  if ( ! mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  //mfrc522.PICC_DumpDetailsToSerial(&(mfrc522.uid)); //dump some details about the card
}

// 0 = none
// 1 = nfc
// 2 = air pressure button
// 3 = music/power crank
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
