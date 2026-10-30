# Kajens Cykeluthyrning 

## VG - Analys och åtgärd av problem 

Under testning av programmet upptäcktes en större bugg vid återlämning av cyklar. Exempel på det som skedde:

Person 1 (Adam) 
1. Hyr cykel 
2. Lämnar tillbaka cykel
3. Får bekräftelse att "Adam" lämnat tillbaka cykeln. 

Person 2 (Carlos)
1. Hyr cykel 
2. Lämnar tillbaka cykel 
3. Får bekräftelse att "Adam" lämnat tillbaka cykeln.

### Orsak till buggen

I ReturnBike() använde vi en foreach som gick igenom listan rentals från början. 
Då Adams uthyrning låg först i listan så hittade programmet hans tidigare uthyrning och avslutade
sökningen med return; Därför visades den första personen som hade hyrt och lämnat tillbaka cykeln på alla 
efterföljande återlämningsbekräftelser för samma cykel.

#### Lösning till buggen

I ReturnBike() och på foreach-loopen lades en Reverse() till som läser listan bakifrån istället. 
Detta resulterar i att den behöver loopa från senast skapad uthyrning till första och läsa av det på det sättet. 
Så vid återlämning av cykel så läser den av listan nedifrån upp istället. 

Efter ändringen testade jag att hyra ut cyklar till två olika kunder. När dessa sedan återlämnades, visade båda 
återlämningarna rätt kund i bekräftelsen. 

##### Förbättringsförslag

En förbättring jag skulle vilja göra är att begränsa hur länge en kund kan hyra en cykel. 

Just nu kontrollerar programmet bara att hyrestiden är större än 0. Vilket resulterar i att en 
kund kan hyra en cykel i hur många timmar dom vill. Vilket inte hade varit ett problem för en vanlig cykel 
utan batteri, men dom eldrivna är de drabbade. 

Detta skulle kunna förbättras genom att lägga till ett nytt villkor i RentBike() som kontrollerar att hyrestiden
inte överstiger exempelvis 8 timmar. 

Skulle kunden ange en längre hyrestid, skulle ett felmeddelande dyka upp med en ny rekommendation och
därmed neka uthyrningen. 

Detta skulle göra uthyrningssystemet mer realistiskt, undvika orimligt höga kostnader på kvittot och batterier som inte
håller måttet. 



