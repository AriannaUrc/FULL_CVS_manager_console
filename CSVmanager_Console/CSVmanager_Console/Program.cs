//Console.WriteLine("Hello, World!");
using System.Text;

int recordLength = 64;
string separator = ",";

CSVDLL.csvFunctions.dati d;
d.draw_date = "test";
d.winning_numbers = "test";
d.mega_ball = 1;
d.multiplier = 1;
d.miovalore = 1;
d.cancLogic = true;

string FileName = "Lottery_Numbers.csv";
string BackUp = "BackUp.csv";


CSVDLL.csvFunctions.Format(BackUp, FileName, separator);

string choice = "0";

while(choice != "-1")
{
    Console.WriteLine("\n\nCosa vuoi fare? Inserisci -1 per uscire");
    Console.Write("0) aprire il file\n1) Visualizzare i record\n2) Conta quanti campi ci sono in un record\n3) Calcolare la lunghezza massima di un record\n4) Aggiungere un record in coda\n5) ricercare un record per campo a scelta");
    Console.WriteLine("\n6) Modificare un record\n7) Cancellare il record");
    choice = Console.ReadLine();

    switch (choice)
    {
        case "0":
            CSVDLL.csvFunctions.ExecuteCommand(FileName);
            break;

        case "1":
            visualizza();
            break;

        case "2":
            Console.WriteLine("il numero di campi é: " + CSVDLL.csvFunctions.findNumberOfFields(FileName, recordLength, d, separator));
            break;

        case "3":
            Console.WriteLine("il numero di campi é: " + CSVDLL.csvFunctions.MaxFieldLenght(FileName, recordLength, d, separator));
            break;

        case "4":
            string drawDate, winNumbers;
            int megaBall, multiplier;

            Console.WriteLine("Inserire la data di estrazione: ");
            drawDate = Console.ReadLine();
            Console.WriteLine("Inserire i numeri vincenti: ");
            winNumbers = Console.ReadLine();
            Console.WriteLine("Inserire megaball: ");
            megaBall = int.Parse(Console.ReadLine());
            Console.WriteLine("Inserire multiplier: ");
            multiplier = int.Parse(Console.ReadLine());

            CSVDLL.csvFunctions.AddFile(drawDate, winNumbers, megaBall, multiplier, d, separator, FileName);
            break;

        case "5":
            string target, value;

            Console.WriteLine("Inserire rispetto a cosa si vuole fare la ricerca:  (DrawDate, WinNumbers, MegaBall, Multiplier) ");
            target = Console.ReadLine();
            Console.WriteLine("\nInserire il valore da ricercare: ");
            value = Console.ReadLine();
            
            Console.WriteLine("il record corrispondente: " + CSVDLL.csvFunctions.FindLineInFile(target, value,FileName, recordLength, d, separator));
            break;

        case "6":
            string Modtarget, Moddate, NewValue;

            Console.WriteLine("Inserire cosa si vuole modificare:  (WinNumbers, MegaBall, Multiplier) ");
            Modtarget = Console.ReadLine();
            Console.WriteLine("\nInserire la data dell'estrazione: ");
            Moddate = Console.ReadLine();
            Console.WriteLine("\nInserire nuovo valore (WinNumbers é una stringa mentre gli altri sono int): ");
            NewValue = Console.ReadLine();

            CSVDLL.csvFunctions.ModificaFile(Moddate, Modtarget, NewValue, FileName, recordLength, d, separator);
            break;

        case "7":
            string Deltarget, Delvalue;

            Console.WriteLine("Inserire rispetto a cosa si vuole fare la ricerca:  (DrawDate, WinNumbers, MegaBall, Multiplier, MioValore) ");
            Deltarget = Console.ReadLine();
            Console.WriteLine("\nInserire il valore dell'ogetto/i da cancellare: ");
            Delvalue = Console.ReadLine();

            CSVDLL.csvFunctions.DeleteFile(Deltarget, Delvalue,FileName, recordLength,d,separator);
            break;

        case "-1":
            Console.WriteLine("\nChiudendo il programma...");
            break;

        default:
            Console.WriteLine("\nInserire un valore valido:\n");
            break;
    }
}

void visualizza()
{
    Console.WriteLine("DrawDate\t\tWinning Numbers\t\tMultiplier");
    String line;
    byte[] br;

    var f = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite);
    BinaryReader reader = new BinaryReader(f);
    BinaryWriter writer = new BinaryWriter(f);



    string[] words = new string[4];


    while (f.Position < f.Length - 2)
    {

        br = reader.ReadBytes(recordLength);
        //converte in stringa
        line = Encoding.ASCII.GetString(br, 0, br.Length);

        //estraggo dalla stringa i valori e gli inserisco il d
        d = CSVDLL.csvFunctions.FromString(line);

        if (d.cancLogic == true)
        {
            //aggiunge alla lista i dati di d
            Console.WriteLine(d.draw_date + "\t\t" + d.winning_numbers + "\t\t" + d.multiplier.ToString());
        }

    }

    writer.Close();
    reader.Close();
    f.Close();

}