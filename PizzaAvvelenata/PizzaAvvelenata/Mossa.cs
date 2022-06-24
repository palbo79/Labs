using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAvvelenata
{
    public class Mossa
    {
        
        public int NPizze { get; set; }
        public Giocatore Giocatore { get; private set;  }

        public int Numero { get; private set; }
        public string Nota { get; private set; }
        
        public Mossa(Giocatore giocatore,int pizzeMangiate,string nota, int numero)
        {
            this.NPizze = pizzeMangiate;
            this.Nota = nota;
            this.Giocatore = giocatore;
            this.Numero = numero;
        }
    }
}
