# SwedishIdentifier - Svensk personidentifierare

[![NuGet](https://img.shields.io/nuget/v/SwedishIdentifier.svg)](https://www.nuget.org/packages/SwedishIdentifier/)

Tolkar och validerar svenska personnummer, samordningsnummer, nationella reservnummer och (några) lokala reservnummer.

Generera nya personnummer, samordningsnummer, nationella reservnummer och (några) lokala reservnummer delvis eller helt slumpade. (OBS! Endast för att användas i testsyfte ;-)

### Tolka 
Genom att använda `PersonIdentifier.Parse(string identifier)` kan personnummer, samordningsnummer och nationella reservnummer tolkas och valideras. Lokala reservnummer kan inte toklas denna väg.

### Ladda
Genom att använda `PersonIdentifier.Load(string oid, string identifier)` kan alla kända typer av personidentifierare laddas. 
Identifieraren valideras mot den typ av identifierare som oid pekar på.

### Skapa
Genom att använda `PersonIdentifierBuilder` kan alla typer av identifierare skapas, mer eller mindre slumpmässigt. 
Valfritt att ange födelseår, födelsemånad, födelsedag och kön. De värden som inte anges slumpas. Korrekt kontrollsiffra enligt Luhn räknas fram.

## Personnummer 

### Tolka

```cSharp

PersonalNumberIdentifier identifier1 = PersonIdentifier.Parse("191212121212");

//Det går även att ange "+" eller "-". Om århundrade är med spelar tecknet ingen roll.
PersonalNumberIdentifier identifier2 = PersonIdentifier.Parse("19121212-1212");
PersonalNumberIdentifier identifier3 = PersonIdentifier.Parse("19121212+1212");

//När århundrade inte är med krävs avdelningstecken. + eller - beroende på ålder.
PersonalNumberIdentifier identifier4 = PersonIdentifier.Parse("121212+1212");
```

### Ladda
```cSharp
PersonalNumberIdentifier identifier = PersonIdentifier.Load(PersonalNumberIdentifier.Oid, "191212121212");
```

### Skapa

````cSharp
PersonalNumberIdentifier created = new PersonIdentifierBuilder()
	.BornYear(1979)
	.BornMonth(11)
	.BornDay(9)
	.AsFemale
	.BuildPersonalNumber()
`````
Ger ett personnummer ungefär 19791109-9427 (4 sista blir olika varje gång...)

## Samordningsnummer

### Tolka

````cSharp
CoordinationNumberIdentifier identifier = PersonIdentifier.Parse("196206703974");
//(Samma regler gäller som för personnummer)
`````

### Ladda
```cSharp
CoordinationNumberIdentifier identifier = PersonIdentifier.Load(CoordinationNumberIdentifier.Oid, "196206703974");
```

### Skapa

````cSharp
CoordinationNumberIdentifier created = new PersonIdentifierBuilder()
	.BornYear(1979)
	.BornMonth(11)
	.BornDay(9)
	.AsFemale
	.BuildCoordinationNumber()
`````
Ger ett samordningsnummer ungefär 19791169-8905 (4 sista blir olika varje gång...)

## Nationellt reservnummer

### Tolka

````cSharp
// Med känd ålder och kön (kvinna född 19950606)
NationalReserveNumberIdentifier identifier1 = PersonIdentifier.Parse("22950606-FH20");

// Med känd ålder men okänt kön (född 19780404)
NationalReserveNumberIdentifier identifier2 = PersonIdentifier.Parse("25780404KHD5");

// Med okänd ålder men känt kön (man)
NationalReserveNumberIdentifier identifier3 = PersonIdentifier.Parse("00342145-BZ31");

// Varken ålder eller kön är känt
NationalReserveNumberIdentifier identifier4 = PersonIdentifier.Parse("00749852BZK0");
`````

### Ladda
```cSharp
NationalReserveNumberIdentifier identifier = PersonIdentifier.Load(NationalReserveNumberIdentifier.Oid, "00342145BZ31");
```

### Skapa
````cSharp
NationalReserveNumberIdentifier created = new PersonIdentifierBuilder()
	.BornYear(1979)
	.BornMonth(11)
	.BornDay(9)
	.AsFemale
	.BuildNationalReserveNumber()
`````
Ger ett nationellt reservnummer ungefär 22791109-JF40, dvs med känd ålder och kön.

````cSharp
NationalReserveNumberIdentifier created = new PersonIdentifierBuilder().BuildNationalReserveNumber()
`````
Ger ett nationellt reservnummer ungefär 00917250-SRC1, dvs med okänd ålder och okänt kön.