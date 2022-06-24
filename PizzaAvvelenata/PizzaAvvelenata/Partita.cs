namespace PizzaAvvelenata
{
    public enum STATO
    {
        NUOVA = 0,
        IN_CORSO = 1,
        TERMINATA = 2,

    }
    public class Partita
    {
        public int NumeroGiocatori { get; private set; }
        public List<Giocatore> Giocatori { get; private set; } = new List<Giocatore>();
        public List<Mossa> Mosse { get; private set; } = new List<Mossa>();

        public int NumeroPizzeIniziale { get; private set; }
        public int PizzeResidue { get; private set; }

        public STATO Stato { get; private set; }

        public Partita(int numeroMassimoPizze)
        {

            this.NumeroPizzeIniziale =  new Random().Next(11, numeroMassimoPizze);
            this.NumeroGiocatori = 2;
            this.PizzeResidue = this.NumeroPizzeIniziale;
            
        }

        public bool AddGiocatore(Giocatore g, ref string messaggio)
        {
            messaggio = string.Empty;

            Partita partita = this;
            if (partita.Giocatori.Count== partita.NumeroGiocatori)
            {
                messaggio = string.Format("Impossibile aggiungere altri giocatori, raggiunto il numero di giocatori previsto [{0}]", partita.NumeroGiocatori);
                return false;
            }
            if (string.IsNullOrWhiteSpace(g.Nome))
            {
                messaggio = string.Format("Nome non valido [{0}]", g.Nome);
                return false;
            }
            if (partita.Giocatori.Any(x => x.Equals(g)))
            {
                messaggio = string.Format("Impossibile aggiungere il giocatore, nome giocatore già esistente [{0}]", g.Nome);
                return false;
            }

            partita.Giocatori.Add(g);
            if (partita.Giocatori.Count == partita.NumeroGiocatori) this.Stato = STATO.IN_CORSO;

            return true;
        }

        public bool Gioca(Mossa mossa , ref Mossa nuovaMossa, ref string messaggio,ref bool continua)
        {
            if (mossa==null || mossa.NPizze==0 || mossa.NPizze > 3 || mossa.Giocatore==null )
            {
                messaggio = string.Format("Mossa non valida");
                nuovaMossa = mossa;
                continua = true;
                return false;
            }

            if (!this.Giocatori.Any(g=>g.Equals(mossa.Giocatore))){
                messaggio = string.Format("Giocatore non assegnato alla partita [{0}]",mossa.Giocatore.Nome );
                nuovaMossa = mossa;
                continua = false;
                return false;
            }

            int pizzeResidue = this.NumeroPizzeIniziale - this.Mosse.Sum(x => x.NPizze);

            int numeroUltimaMossaAvversari = (this.Mosse.Count==0 || !this.Mosse.Any(t => !t.Giocatore.Equals(mossa.Giocatore)) )? -1 : this.Mosse.Where(t => !t.Giocatore.Equals(mossa.Giocatore)).Max(p => p.Numero);
            var ultimaMossaAvversari = numeroUltimaMossaAvversari < 0 ? null : this.Mosse.Where(x => !x.Giocatore.Equals(mossa.Giocatore) && x.Numero == numeroUltimaMossaAvversari).First();

            
            // controllo che la mossa non sia uguale a quella del giocatore precedente
            if ((ultimaMossaAvversari != null && ultimaMossaAvversari.NPizze == mossa.NPizze))
            {
                messaggio = string.Format("Mossa non valida, stessa mossa dell'avversario [{0}]", ultimaMossaAvversari.Giocatore.Nome);
                nuovaMossa = mossa;
                continua = true;
                return false;
            }

            // mossa non valida 
            if ((pizzeResidue - mossa.NPizze) < 0)
            {
                messaggio = string.Format("Mossa non valida, non abbastanza pizze in tavola. Pizze residue [{0}]", pizzeResidue - mossa.NPizze);
                nuovaMossa = mossa;
                continua = true;
                return false;
            }

            // mossa perdente (mangia pizza avvelenata) 
            if ((pizzeResidue - mossa.NPizze) == 0)
            {
                pizzeResidue = pizzeResidue - mossa.NPizze;
                this.PizzeResidue = pizzeResidue;
                messaggio = string.Format("Giocatore [{0}] hai perso!!", mossa.Giocatore.Nome);
                nuovaMossa = mossa;
                continua = false;
                this.Stato = STATO.TERMINATA;
                return false;
            }

            this.Mosse.Add(mossa);

            pizzeResidue = pizzeResidue - mossa.NPizze;
            this.PizzeResidue = pizzeResidue;
            if (ultimaMossaAvversari != null && pizzeResidue==1 && mossa.NPizze==1 )
            {
                
                messaggio = string.Format("Giocatore [{0}] hai perso!!", mossa.Giocatore.Nome);
                nuovaMossa = mossa;
                continua = false;
                this.Stato = STATO.TERMINATA;
                return false;
            }

            Giocatore gNuovaMossa = this.Giocatori.Where(s=>!s.Equals(mossa.Giocatore)).First();
            nuovaMossa = new Mossa(gNuovaMossa, 0, string.Empty, this.Mosse.Max(x => x.Numero) + 1);
            continua = true;
            return true;

        }

        private bool VerificaPresenzaMosseGiocabiliAvversario(int pizzeResidue,int ultimaMossaAvversario)
        {
            if (pizzeResidue == ultimaMossaAvversario)
            {
                return false;
            }
            else
                return true;
        }

        
    }
}