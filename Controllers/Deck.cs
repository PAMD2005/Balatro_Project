using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maio10.Controllers
{
    public class Deck
    {

        public Card[] Cards { get; private set; }
        private Random _random = new Random();

        public Deck()
        {
            Cards = new Card[52];
            string[] naipes = { "Hearts", "Diamonds", "Spades", "Clubs" };
            int[] valores = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }; // 11=J, 12=Q, 13=K, 14=A

            int id = 1;
            int index = 0;
            foreach (int valor in valores)
            {
                foreach (string naipe in naipes)
                {
                    string foto = id + ".png"; // Arquivos PNG estão nomeados de 1.png a 52.png
                    Cards[index] = new Card(id, foto, valor, naipe);
                    //MessageBox.Show("ID: " + id + "\nvalor: " + valor + "\nnaipe: " + naipe);
                    id++;
                    index++;
                    
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            for (int i = Cards.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                Card temp = Cards[i];
                Cards[i] = Cards[j];
                Cards[j] = temp;
            }
        }

        public int CurrentIndex = 0;

        public Card DrawCard()
        {
            if (CurrentIndex >= Cards.Length)
                return null;
            return Cards[CurrentIndex++];
        }
    }
}


