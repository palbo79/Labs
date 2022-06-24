using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAvvelenata
{
    public class Giocatore
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; }    
        public Giocatore(string nome)
        {
            this.Id = Guid.NewGuid();
            this.Nome = nome;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Giocatore g = (Giocatore)obj;
                return ( Nome==g.Nome);
            }
        }
    }
}
