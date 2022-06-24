// See https://aka.ms/new-console-template for more information



using PizzaAvvelenata;

Partita partita = new Partita(20);
Console.WriteLine("Nuova partita, numero pizze: [{0}]",partita.NumeroPizzeIniziale);

bool primoGiocatoreInserito = false;
string messaggio;
Giocatore g1 = null;
Giocatore g2 = null;

do
{
    messaggio = string.Empty;
    Console.WriteLine("Inserisci nome primo giocatore");
    string nomeG1 = Console.ReadLine();

    g1 = new Giocatore(nomeG1);
    primoGiocatoreInserito = partita.AddGiocatore(g1, ref messaggio);
    if (!primoGiocatoreInserito)
        Console.WriteLine(messaggio);
    Console.WriteLine("\n");
}
while (!primoGiocatoreInserito);

bool secondoGiocatoreInserito = false;
do
{
    messaggio = string.Empty;
    Console.WriteLine("Inserisci nome secondo giocatore");
    string nomeG2 = Console.ReadLine();

    g2 = new Giocatore(nomeG2);
    secondoGiocatoreInserito = partita.AddGiocatore(g2, ref messaggio);
    if (!secondoGiocatoreInserito)
        Console.WriteLine(messaggio);
    Console.WriteLine("\n");
}
while (!secondoGiocatoreInserito);

// prima mossa
int nPizze;
bool esitoAcquisizione = false;
do
{
    Console.WriteLine("[{0}] inserisci numero pizze da mangiare: ", g1.Nome);
    string input = Console.ReadLine();
    esitoAcquisizione = Int32.TryParse(input, out nPizze);


} while (!esitoAcquisizione);
Mossa mossa = new Mossa(g1, nPizze, string.Empty, 1);

Mossa prossimaMossa = null;
string messaggioDiGioco = string.Empty;
while(partita.Stato==STATO.IN_CORSO)
{
    bool continua = true;
    bool esitoMossa= partita.Gioca(mossa, ref prossimaMossa, ref messaggioDiGioco,ref continua);
    Console.WriteLine("Pizze residue [{0}], {1}",partita.PizzeResidue, messaggioDiGioco);
    if (continua)
    {
        messaggioDiGioco = String.Empty;
        mossa = prossimaMossa;
        do
        {
            Console.WriteLine("{0} inserisci numero pizze da mangiare: ", mossa.Giocatore.Nome);
            string input = Console.ReadLine();
            esitoAcquisizione = Int32.TryParse(input, out nPizze);


        } while (!esitoAcquisizione);
        mossa.NPizze = nPizze;
    }

    
}
Console.WriteLine("PARTITA TERMINATA, premi un tasto per chiudere l'applicazione");
var a = Console.ReadLine();