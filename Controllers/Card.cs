using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maio10.Controllers
{
    public class Card
    {
        public int Id { get; set; } // ID da carta
        public string Foto { get; set; } // Nome do arquivo PNG associado à carta
        public int Valor { get; set; } // Valor da carta (2-14, onde 11=J, 12=Q, 13=K, 14=A)
        public string Naipe { get; set; } // Naipe da carta (Suit)

        public Card(int id, string foto, int valor, string naipe)
        {
            Id = id;
            Foto = foto;
            Valor = valor;
            Naipe = naipe;
        }
    }
}
