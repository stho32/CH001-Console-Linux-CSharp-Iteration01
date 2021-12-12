if (args.Length == 0) {
    Console.WriteLine("Random stuff generator - usage");

    Console.WriteLine("random number");
    Console.WriteLine("random number min max");
    Console.WriteLine("random password");
    Console.WriteLine("random password 14 [+numbers CountOfNumbers] [+special CountOfSpecialChars]");
    Console.WriteLine("random animal");
}

if (args.Length == 1 && args[0] == "number") {
    Console.WriteLine(Zufallszahl(1,10));
}

if (args.Length == 3 && args[0] == "number") {
    var min = Int32.Parse(args[1]);
    var max = Int32.Parse(args[2]);

    Console.WriteLine(Zufallszahl(min, max));
}

// password
// password 14
// password 14 +special 3
// password 14 +numbers 3
// password 14 +numbers 0
// password 14 +numbers 2 +special 3
if (args.Length >= 1 && args[0] == "password") {

    var getLength = false;
    var getSpecial = false;
    var getNumbers = false;

    var length = 14;
    var specialChars = 4;
    var numbers = 3;

    foreach (var arg in args) {
        if (getLength) {
            length = Int32.Parse(arg);
            getLength = false;
            continue;
        }

        if (getSpecial) {
            specialChars = Int32.Parse(arg);
            getSpecial = false;
            continue;
        }

        if (getNumbers) {
            numbers = Int32.Parse(arg);
            getNumbers = false;
        }

        if (arg == "password") {
            getLength = true;
        }

        if (arg == "+special") {
            getSpecial = true;
        }

        if (arg == "+numbers") {
            getNumbers = true;
        }
    }

    Console.WriteLine(Passwort(length, specialChars, numbers));
}

// animal
if (args.Length == 1 && args[0] == "animal") {
    Console.WriteLine(Zufallstier());
}

string Zufallstier()
{
    var adjektive = new string[] {"lustig", "listig", "passiv", "schielend", "wütend"};
    var verben = new string[] {"schnappender", "guckender", "fokussierender", "rennender", "seufzender", "trampelnder"};
    var tiere = new string[] {"Elefant", "Löwe", "Lurch", "Wolf", "Käfer"};

    var random = new Random();
    var ergebnis = 
        adjektive[random.Next(adjektive.Length)] + "-" +
        verben[random.Next(verben.Length)] + "-" +
        tiere[random.Next(tiere.Length)];

    return ergebnis;
}

int Zufallszahl(int min, int max)
{
    var random = new Random();
    return random.Next(min, max+1);
}

string Passwort(int length, int numberOfSpecialChars, int numberOfNumbers) {
    var random = new Random();
    
    var numberOfChars = length - numberOfSpecialChars - numberOfNumbers;
    if (numberOfChars < 0) {
        throw new ArgumentOutOfRangeException("numberOfChars");
    }

    var possibleChars = "qwertzuiopasdfghjklyxcvbnmn";
    possibleChars += possibleChars.ToUpper();

    var passwort = ""; 
    
    for (int i = 0; i < numberOfChars; i++) {
        passwort += possibleChars[random.Next(0, possibleChars.Length)];
    }

    possibleChars = "1234567890";
    for (int i = 0; i < numberOfNumbers; i++ ) {
        passwort += possibleChars[random.Next(0, possibleChars.Length)];
    }

    possibleChars = "!\"§$%&/()=?{[]}\\#+*-";
    for (int i = 0; i < numberOfSpecialChars; i++ ) {
        passwort += possibleChars[random.Next(0, possibleChars.Length)];
    }

    passwort = Shuffle(passwort);

    return passwort;
}

string Shuffle(string passwort)
{
    var random = new Random();
    var arrayOfChars = passwort.ToArray();

    for ( int i = 0; i < arrayOfChars.Length; i++ ) {
        var randomPosition = random.Next(arrayOfChars.Length);
        var temp = arrayOfChars[i];
        arrayOfChars[i] = arrayOfChars[randomPosition];
        arrayOfChars[randomPosition] = temp;
    }

    return new String(arrayOfChars);
}