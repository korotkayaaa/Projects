#include "U8glib.h"
#include <OneWire.h>
#include <DallasTemperature.h>
#include <IRremote.hpp>
#include "iarduino_RTC.h"
#define ONE_WIRE_BUS 12
#define IR_RECEIVE_PIN A3
#define PIN_LED2 7
#define PIN_LED3 4
#define PIN_LED1 6
#define PIN_LED4 5
#define PIN_PHOTO_SENSOR A2
OneWire oneWire(ONE_WIRE_BUS);
iarduino_RTC time(RTC_DS1307);
DallasTemperature sensors(&oneWire);
U8GLIB_SSD1306_128X64 display(13, 11, 10, 9, 8);
#define LINE_MAX 128
#define ROW_MAX 64
uint8_t screen[ROW_MAX][LINE_MAX];
byte autoLighting=0;
byte autoVentilation=0;
byte autoHeating=0;
byte enablingTime=0;
byte printTime=0;
String Hour="00";
String Minut="00";
byte inputMinut=0;
byte inputHour=0;
void clear_screen(void) {
uint8_t i, j;
for( i = 0; i < ROW_MAX; i++ )
for( j = 0; j < LINE_MAX; j++ )
screen[i][j] = 0;
}
void drawTime(String hour, String minut) {
  display.firstPage();
  do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20, 15);
display.print("Time Lamp: ");
display.setPrintPos(40, 35);
display.print(hour+":"+minut);}while( display.nextPage() );
}

void setup() {
pinMode(4,OUTPUT);
digitalWrite(4,HIGH);
//Serial.begin(9600);
time.begin();
IrReceiver.begin(IR_RECEIVE_PIN);
pinMode(PIN_LED1, OUTPUT);
pinMode(PIN_LED2, OUTPUT);
pinMode(PIN_LED3, OUTPUT);
pinMode(PIN_LED4, OUTPUT);
digitalWrite(PIN_LED1, HIGH);
digitalWrite(PIN_LED2, HIGH);
digitalWrite(PIN_LED3, HIGH);
digitalWrite(PIN_LED4, HIGH);
display.firstPage();
  do {
    display.setFont(u8g_font_unifont);
    clear_screen();
  } while( display.nextPage() );
  
}
void loop() {
if (IrReceiver.decode()) {
IrReceiver.resume();
//Serial.println(IrReceiver.decodedIRData.command);
if(IrReceiver.decodedIRData.command==20)
{
display.firstPage();
do {display.setFont(u8g_font_unifont);
display.setPrintPos(40,35);
display.print("Hello!");
}
 while( display.nextPage() );
 enablingTime=0;
autoLighting=0;
autoVentilation=0;
autoHeating=0;
printTime=0;
}
if(IrReceiver.decodedIRData.command==23)
{  
 display.firstPage();
  do {
    display.setFont(u8g_font_unifont);
    clear_screen();
  } while( display.nextPage() );
  printTime=0;
}
if(IrReceiver.decodedIRData.command==1)
{
  digitalWrite(PIN_LED2, LOW);
  enablingTime=0;
  printTime=0;
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(5,35);
display.print("Lighting is on");
}
 while( display.nextPage() );
}
if(IrReceiver.decodedIRData.command==2)
{
  enablingTime=0;
  autoLighting=0;
  printTime=0;
  digitalWrite(PIN_LED2, HIGH);
  display.firstPage();
  do {
    display.setFont(u8g_font_unifont);
    clear_screen();
  } while( display.nextPage() );
}
if(IrReceiver.decodedIRData.command==15)
{
  printTime=0;
 drawTime(Hour,Minut);
}
if(IrReceiver.decodedIRData.command==16)
{
if(inputHour!=23)inputHour+=1;else inputHour=0;
  if(inputMinut<10){Minut="0"+String(inputMinut);}else Minut=String(inputMinut); 
  if(inputHour<10){Hour="0"+String(inputHour);}else Hour=String(inputHour);
  drawTime(Hour,Minut); 
  }    
if(IrReceiver.decodedIRData.command==17)
{
if(inputHour!=0)inputHour-=1;else inputHour=23;
  if(inputMinut<10){Minut="0"+String(inputMinut);}else Minut=String(inputMinut); if(inputHour<10){Hour="0"+String(inputHour);}else Hour=String(inputHour);
  drawTime(Hour,Minut); 
  }
if(IrReceiver.decodedIRData.command==18)
{
if(inputMinut!=59)inputMinut+=1;else inputMinut=0;
  if(inputMinut<10){Minut="0"+String(inputMinut);}else Minut=String(inputMinut); if(inputHour<10){Hour="0"+String(inputHour);}else Hour=String(inputHour);
  drawTime(Hour,Minut); 
  }
if(IrReceiver.decodedIRData.command==19)
{
if(inputMinut!=0)inputMinut-=1;else inputMinut=59;
  if(inputMinut<10){Minut="0"+String(inputMinut);}else Minut=String(inputMinut); if(inputHour<10){Hour="0"+String(inputHour);}else Hour=String(inputHour);
  drawTime(Hour,Minut); 
   }
if(IrReceiver.decodedIRData.command==21){enablingTime=2;
}
if(IrReceiver.decodedIRData.command==11){
  sensors.requestTemperatures();
  printTime=0;
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20, 15);
display.print("Temperature ");
display.setPrintPos(40, 35);
display.print(sensors.getTempCByIndex(0));
display.drawCircle(82,27,2);
display.print(" C");
} while( display.nextPage() ); 
}
if(IrReceiver.decodedIRData.command==26)
{autoLighting=1; autoHeating=1; autoVentilation=1;printTime=0;
display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20,15);
display.print("Auto control");
display.setPrintPos(40,35);
display.print("is on");
}
 while( display.nextPage() );
}
if(IrReceiver.decodedIRData.command==14)
{autoLighting=0;autoHeating=0; autoVentilation=0;printTime=0;
display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20,15);
display.print("Auto control");
display.setPrintPos(40,35);
display.print("is off");
}
 while( display.nextPage() );
}
if(IrReceiver.decodedIRData.command==3)
{digitalWrite(PIN_LED3, LOW);
  enablingTime=0;
  autoHeating=0;
  printTime=0;
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(35,15);
display.print("Heating");
display.setPrintPos(40,35);
display.print("is on");
}
 while( display.nextPage() );}
if(IrReceiver.decodedIRData.command==4)
{ enablingTime=0;
  autoHeating=0;
  printTime=0;
  digitalWrite(PIN_LED3, HIGH);
  display.firstPage();
  do {
    display.setFont(u8g_font_unifont);
    clear_screen();
  } while( display.nextPage() );}
  if(IrReceiver.decodedIRData.command==5)
{digitalWrite(PIN_LED1, LOW);
  enablingTime=0;
  autoVentilation=0;
  printTime=0;
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20,15);
display.print("Ventilation");
display.setPrintPos(40,35);
display.print("is on");
}
 while( display.nextPage() );}
if(IrReceiver.decodedIRData.command==6)
{ enablingTime=0;
  autoVentilation=0;
  printTime=0;
  digitalWrite(PIN_LED1, HIGH);
  display.firstPage();
  do {
    display.setFont(u8g_font_unifont);
    clear_screen();
  } while( display.nextPage() );}
  if(IrReceiver.decodedIRData.command==7)
{digitalWrite(PIN_LED4, LOW);
  enablingTime=0;
  printTime=0;
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20,15);
display.print("The socket");
display.setPrintPos(40,35);
display.print("is on");
}
 while( display.nextPage() );}
if(IrReceiver.decodedIRData.command==8)
{ enablingTime=0;
printTime=0;
  digitalWrite(PIN_LED4, HIGH);
  display.firstPage();
  do {
    display.setFont(u8g_font_unifont);
    clear_screen();
  } while( display.nextPage() );}
if(IrReceiver.decodedIRData.command==31){
  autoLighting=2;
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20,15);
display.print("Time saved");
display.setPrintPos(40,35);
display.print("Enabling");
}
 while( display.nextPage() );}
if(IrReceiver.decodedIRData.command==30){autoLighting=3;
display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(25,15);
display.print("Time saved");
display.setPrintPos(40,35);
display.print("Shutdown");
}
 while( display.nextPage() );}
if(IrReceiver.decodedIRData.command==24)
{
 printTime=1;
}
}
sensors.requestTemperatures();
if(autoHeating==1){
if(sensors.getTempCByIndex(0)<=15){
  digitalWrite(PIN_LED3, LOW);
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(35,15);
display.print("Heating");
display.setPrintPos(40,35);
display.print("is on");
}while( display.nextPage() );
printTime=0;
enablingTime=0;
}
if(sensors.getTempCByIndex(0)>15){
  digitalWrite(PIN_LED3, HIGH);
}}
if(autoVentilation==1){
if(sensors.getTempCByIndex(0)>=27){
  digitalWrite(PIN_LED1, LOW);
  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(20,15);
display.print("Ventilation");
display.setPrintPos(40,35);
display.print("is on");
}while( display.nextPage() );
printTime=0;
enablingTime=0;
}
if(sensors.getTempCByIndex(0)<27){
  digitalWrite(PIN_LED1, HIGH); 
}}
if(autoLighting==1){
int val = analogRead(PIN_PHOTO_SENSOR);
//Serial.println(val);
  if (val <= 500) { digitalWrite(PIN_LED2, LOW);} else {digitalWrite(PIN_LED2, HIGH);
  }
}
if(enablingTime==2){
  if(String(time.gettime("H"))==Hour && String(time.gettime("i"))==Minut  && String(time.gettime("s"))=="00"){
    if(autoLighting==2){ digitalWrite(PIN_LED2,LOW);  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(5,35);
display.print("Lighting is on");
}
 while( display.nextPage() );
  } else{digitalWrite(PIN_LED2,HIGH);  display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(5,35);
display.print("Lighting is off");
}
 while( display.nextPage() );
  } }
}

if(printTime==1){
display.firstPage();
do {
display.setFont(u8g_font_unifont);
display.setPrintPos(25,20);
display.print(time.gettime("d-m-Y"));
display.setPrintPos(25,40);
display.print(time.gettime(" H:i:s"));
display.setPrintPos(50,60);
display.print(time.gettime("D"));
}while( display.nextPage() );delay(1);   
}
}
