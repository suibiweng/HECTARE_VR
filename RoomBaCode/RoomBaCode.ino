#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <SoftwareSerial.h>

#ifndef STASSID
#define STASSID "SuibiWifi"
#define STAPSK "12345678"
#endif

// buffers for receiving and sending data
char packetBuffer[UDP_TX_PACKET_MAX_SIZE + 1];  //buffer to hold incoming packet,
char ReplyBuffer[] = "acknowledged\r\n";
WiFiUDP Udp;
unsigned int localPort = 8585;



// Roomba Create2 connection
//int rxPin = 10;
//int txPin = 11;

int rxPin = D0;
int txPin = D5;
SoftwareSerial Roomba(rxPin, txPin);

// BT Module (HC-06)
//SoftwareSerial BT1(8, 9);
SoftwareSerial BT1(D6, D7);  // El pin 8 es Rx y el pin 9 es Tx

int lD = 68;
int lA = 65;
int lV = 86;
int lE = 69;
int lN = 78;
int lS = 83;
int lF = 70;
int lB = 66;
int lL = 76;
int lR = 82;
int lBlank = 32;

int letters[] = { lN, lA, lS, lA };

#define letterSize 4

int t = 0;

int reps = 0;
int counts;
void WiFISetup() {
  WiFi.mode(WIFI_STA);
  WiFi.begin(STASSID, STAPSK);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print('.');
    delay(500);
  }
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
  Serial.printf("UDP server on port %d\n", localPort);
  Udp.begin(localPort);
}


void setup() {


  // put your setup code here, to run once:

  Roomba.begin(115200);
  BT1.begin(115200);
  Serial.begin(115200);
  WiFISetup();
  // wait
  delay(5000);

  Serial.print("Roomba Remote Control");
  BT1.print("Roomba Remote Control");
  BT1.println('\n');

  // start
  Roomba.write(128);  // start

  //myPort.write(131); // safe mode!
  Roomba.write(132);  // full access

  //write name
  Roomba.write(164);  // start LED command
  Roomba.write(lN);
  Roomba.write(lA);
  Roomba.write(lS);
  Roomba.write(lA);

  // move it!
  /*moveStraight(200, 0);
  delay(500);
  rotateBy(180, 0);
  delay(500);
  moveStraight(200, 0);
  delay(500);*/

  //WiFISetup();


  TestCode();
}

void TestCode() {
  /*if(t < 4 ) {
    moveStraight(200, 0);
    delay(500);
    rotateBy(180, 0);
    delay(500);
    t++;
  }*/

 // sendDatatoWIFI();

  moveForward();
  moveBackward();
  moveLeft();
  moveRight();
}

void sendDatatoWIFI() {
  Udp.beginPacket("192.168.0.101", 8585);
  Udp.write("finished");
  Udp.endPacket();
  Serial.println("finished");
  counts=0;
}


void reciveDatafromWIFI() {

  int packetSize = Udp.parsePacket();
  if (packetSize) {


    int n = Udp.read(packetBuffer, UDP_TX_PACKET_MAX_SIZE);
    packetBuffer[n] = 0;
    Serial.println("Contents:");
    Serial.println(packetBuffer);

    char pb = packetBuffer[0];
    reps = pb - '0';
    Serial.println(reps);



 
  if (packetBuffer[1] == 'F'){
    for(int i=0 ; i<reps ;i++){
      moveForward();
    }
  }
    
  if (packetBuffer[1] == 'B') {
     
     for(int i=0 ; i<reps ;i++){
       moveBackward();
    }
    
    }
    
    


 


    

/*

    if (packetBuffer[1] == 'F') {

      moveForward();
      sendDatatoWIFI();
    }



    if (packetBuffer[1] == 'R') {

      moveRight();
      sendDatatoWIFI();
    }


    if (packetBuffer[1] == 'L') {

      moveLeft();
      sendDatatoWIFI();
    }


    if (packetBuffer[1] == 'B') {

      moveBackward();
      sendDatatoWIFI();
    }

    if (packetBuffer[0] == 'N') {

      nudgeLeft();
      sendDatatoWIFI();
    }

    if (packetBuffer[0] == 'M') {

      nudgeRight();
      sendDatatoWIFI();
    }




*/

    // send a reply, to the IP address and port that sent us the packet we received
    //    Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
    //    Udp.write(ReplyBuffer);
    //    Udp.endPacket();
  }
}



void loop() {
  reciveDatafromWIFI();
  // put your main code here, to run repeatedly:
  // speller();
  // delay(200);
  //

  /*
  if(t < 4 ) {
    moveStraight(200, 0);
    delay(500);
    rotateBy(180, 0);
    delay(500);
    t++;
  }*/
}



void moveForward() {

 
    Serial.println("forward rep");
    Roomba.write(164);  // start LED command
    Roomba.write(lF);
    Roomba.write(lBlank);
    Roomba.write(lBlank);
    Roomba.write(lBlank);

    int amount = 330;
    int radius = 0;

    Roomba.write(137);
    Roomba.write(amount >> 8);
    Roomba.write(amount);
    Roomba.write(radius >> 8);
    Roomba.write(radius);

    delay(1000);
  

  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);

  Roomba.write(164);  // start LED command
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);


counts++;
if(counts>=reps)

     sendDatatoWIFI();
  
}

void moveBackward() {


    Serial.println("backward rep");
    Roomba.write(164);  // start LED command
    Roomba.write(lB);
    Roomba.write(lBlank);
    Roomba.write(lBlank);
    Roomba.write(lBlank);

    int amount = -330;
    int radius = 0;

    Roomba.write(137);
    Roomba.write(amount >> 8);
    Roomba.write(amount);
    Roomba.write(radius >> 8);
    Roomba.write(radius);

    delay(1000);
  

  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);

  Roomba.write(164);  // start LED command
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);


  counts++;
if(counts>=reps)
     sendDatatoWIFI();
}

void moveLeft() {

  for (int i = 0; i < reps; i++) {
    Serial.println("left rep");
    Roomba.write(164);  // start LED command
    Roomba.write(lL);
    Roomba.write(lBlank);
    Roomba.write(lBlank);
    Roomba.write(lBlank);

    int radius = 1;
    int angle = 90;
    //
    //if (time == 0) {
    int time = int(abs(angle) / 90.0 * 1000);
    //}

    float velocity2 = (angle * 115 * PI) / (180.0 * time / 1000.0);  // angle is still not perfect
    int velocity = int(velocity2);

    //println("rotate by " + velocity2);

    Roomba.write(137);
    Roomba.write(velocity >> 8);
    Roomba.write(velocity);
    Roomba.write(radius >> 8);
    Roomba.write(radius);

    delay(time);
  }

  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);

  Roomba.write(164);  // start LED command
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
}

void moveRight() {

  for (int i = 0; i < reps; i++) {
    Serial.println("right rep");
    Roomba.write(164);  // start LED command
    Roomba.write(lR);
    Roomba.write(lBlank);
    Roomba.write(lBlank);
    Roomba.write(lBlank);

    int radius = 1;
    int angle = -90;
    //
    //if (time == 0) {
    int time = int(abs(angle) / 90.0 * 1000);
    //}

    float velocity2 = (angle * 115 * PI) / (180.0 * time / 1000.0);  // angle is still not perfect
    int velocity = int(velocity2);

    //println("rotate by " + velocity2);


    Roomba.write(137);
    Roomba.write(velocity >> 8);
    Roomba.write(velocity);
    Roomba.write(radius >> 8);
    Roomba.write(radius);

    delay(time);
  }
  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);

  Roomba.write(164);  // start LED command
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
}

void nudgeLeft() {

  Roomba.write(164);  // start LED command
  Roomba.write(lL);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);

  int radius = 1;
  int angle = 5;
  //
  //if (time == 0) {
  int time = int(abs(angle) / 90.0 * 1000);
  //}

  float velocity2 = (angle * 115 * PI) / (180.0 * time / 1000.0);  // angle is still not perfect
  int velocity = int(velocity2);

  //println("rotate by " + velocity2);

  Roomba.write(137);
  Roomba.write(velocity >> 8);
  Roomba.write(velocity);
  Roomba.write(radius >> 8);
  Roomba.write(radius);

  delay(time);

  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);

  Roomba.write(164);  // start LED command
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
}

void nudgeRight() {

  Roomba.write(164);  // start LED command
  Roomba.write(lR);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);

  int radius = 1;
  int angle = -5;
  //
  //if (time == 0) {
  int time = int(abs(angle) / 90.0 * 1000);
  //}

  float velocity2 = (angle * 115 * PI) / (180.0 * time / 1000.0);  // angle is still not perfect
  int velocity = int(velocity2);

  //println("rotate by " + velocity2);


  Roomba.write(137);
  Roomba.write(velocity >> 8);
  Roomba.write(velocity);
  Roomba.write(radius >> 8);
  Roomba.write(radius);

  delay(time);

  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);

  Roomba.write(164);  // start LED command
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
  Roomba.write(lBlank);
}

void moveStraight(int distance, int time) {

  //println("move straight");

  int radius = 0;

  int velocity = 200;
  if (time == 0) {
    time = distance / velocity * 1000;
    //println(time);
  }

  Roomba.write(137);
  Roomba.write(velocity >> 8);
  Roomba.write(velocity);
  Roomba.write(radius >> 8);
  Roomba.write(radius);

  delay(time);

  // stop!

  Roomba.write(137);  //drive
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
}



void rotateBy(int angle, int time) {

  int radius = 1;

  //
  if (time == 0) {
    time = int(abs(angle) / 90.0 * 1000);
  }

  float velocity2 = (angle * 115 * PI) / (180.0 * time / 1000.0);  // angle is still not perfect
  int velocity = int(velocity2);

  //println("rotate by " + velocity2);


  Roomba.write(137);
  Roomba.write(velocity >> 8);
  Roomba.write(velocity);
  Roomba.write(radius >> 8);
  Roomba.write(radius);

  delay(time);

  Roomba.write(137);  //stop it
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
  Roomba.write((byte)0x00);
}

void speller() {
  Roomba.write(164);  // start LED command

  Roomba.write(letters[0]);
  Roomba.write(letters[1]);
  Roomba.write(letters[2]);
  Roomba.write(letters[3]);
  //
  int temp = letters[0];
  for (int i = 0; i < 4; i++) {
    letters[i] = letters[i + 1];
  }

  letters[letterSize - 1] = temp;
}
