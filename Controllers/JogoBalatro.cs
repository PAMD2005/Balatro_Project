using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maio10.Controllers
{

    public class JogoBalatro : UIElement
    {
        private static readonly Random back_r = new Random();
        private static readonly int cardback = back_r.Next(100, 104);
        public Deck _deck;
        public string NameBoss = "";
        public int BossWidth = 0;
        public int BossHeight = 0;
        public int Health = 300;
        public int RoundNum = 0;
        int soma = 0;
        public string logtext = "Bem Vindo!";

        public int Straight_Flush_Mult8 = 10;
        public int Four_of_a_Kind7 = 9;
        public int Full_House4 = 5;
        public int Flush4 = 5;
        public int Straight4 = 5;
        public int Three_of_a_Kind3 = 4;
        public int Two_Pair2 = 3;
        public int Pair2 = 2;

        public int sumOfSelectedCardValues;
        public bool hasFlush_AndOr_Straight = false;

        public int score = 0;




        public bool GameStarted
        {
            get { return (bool)GetValue(GameStartedProperty); }
            set { SetValue(GameStartedProperty, value); }
        }

        public static readonly DependencyProperty GameStartedProperty =
            DependencyProperty.Register("GameStarted", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));


        public int DiscardAmount           
        {
            get { return (int)GetValue(DiscardAmountProperty); }
            set { SetValue(DiscardAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Montante.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiscardAmountProperty =
            DependencyProperty.Register("DiscardAmount", typeof(int), typeof(ClsJogoDados), new PropertyMetadata(4));

        public int PlaysAmount
        {
            get { return (int)GetValue(PlaysAmountProperty); }
            set { SetValue(PlaysAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Montante.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaysAmountProperty =
            DependencyProperty.Register("PlaysAmount", typeof(int), typeof(ClsJogoDados), new PropertyMetadata(4));

        public string BossSource
        {
            get { return (string)GetValue(BossSourceProperty); }
            set { SetValue(BossSourceProperty, value); }
        }

        public static readonly DependencyProperty BossSourceProperty =
            DependencyProperty.Register("BossSource", typeof(string), typeof(Controller), new PropertyMetadata(""));


        public int Card1
        {
            get { return (int)GetValue(Card1Property); }
            set { SetValue(Card1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card1Property =
            DependencyProperty.Register("Card1", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        public int Card2
        {
            get { return (int)GetValue(Card2Property); }
            set { SetValue(Card2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card2Property =
            DependencyProperty.Register("Card2", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        public int Card3
        {
            get { return (int)GetValue(Card3Property); }
            set { SetValue(Card3Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card3Property =
            DependencyProperty.Register("Card3", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        public int Card4
        {
            get { return (int)GetValue(Card4Property); }
            set { SetValue(Card4Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card4Property =
            DependencyProperty.Register("Card4", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        public int Card5
        {
            get { return (int)GetValue(Card5Property); }
            set { SetValue(Card5Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card5Property =
            DependencyProperty.Register("Card5", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        public int Card6
        {
            get { return (int)GetValue(Card6Property); }
            set { SetValue(Card6Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card6Property =
            DependencyProperty.Register("Card6", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        public int Card7
        {
            get { return (int)GetValue(Card7Property); }
            set { SetValue(Card7Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card7Property =
            DependencyProperty.Register("Card7", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));

        // --------------------------------------------------------- CardValues ------------------------------------------------------------------

        public int Card1Value
        {
            get { return (int)GetValue(Card1ValueProperty); }
            set { SetValue(Card1ValueProperty, value); }
        }

        public static readonly DependencyProperty Card1ValueProperty =
            DependencyProperty.Register("Card1Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        public int Card2Value
        {
            get { return (int)GetValue(Card2ValueProperty); }
            set { SetValue(Card2ValueProperty, value); }
        }

        public static readonly DependencyProperty Card2ValueProperty =
            DependencyProperty.Register("Card2Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        public int Card3Value
        {
            get { return (int)GetValue(Card3ValueProperty); }
            set { SetValue(Card3ValueProperty, value); }
        }

        public static readonly DependencyProperty Card3ValueProperty =
            DependencyProperty.Register("Card3Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        public int Card4Value
        {
            get { return (int)GetValue(Card4ValueProperty); }
            set { SetValue(Card4ValueProperty, value); }
        }

        public static readonly DependencyProperty Card4ValueProperty =
            DependencyProperty.Register("Card4Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        public int Card5Value
        {
            get { return (int)GetValue(Card5ValueProperty); }
            set { SetValue(Card5ValueProperty, value); }
        }

        public static readonly DependencyProperty Card5ValueProperty =
            DependencyProperty.Register("Card5Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        public int Card6Value
        {
            get { return (int)GetValue(Card6ValueProperty); }
            set { SetValue(Card6ValueProperty, value); }
        }

        public static readonly DependencyProperty Card6ValueProperty =
            DependencyProperty.Register("Card6Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        public int Card7Value
        {
            get { return (int)GetValue(Card7ValueProperty); }
            set { SetValue(Card7ValueProperty, value); }
        }

        public static readonly DependencyProperty Card7ValueProperty =
            DependencyProperty.Register("Card7Value", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        // --------------------------------------------------------- CardNaipe ------------------------------------------------------------------

        public string Card1Suit
        {
            get { return (string)GetValue(Card1SuitProperty); }
            set { SetValue(Card1SuitProperty, value); }
        }

        public static readonly DependencyProperty Card1SuitProperty =
            DependencyProperty.Register("Card1Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));


        public string Card2Suit
        {
            get { return (string)GetValue(Card2SuitProperty); }
            set { SetValue(Card2SuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card2Suit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card2SuitProperty =
            DependencyProperty.Register("Card2Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));


        public string Card3Suit
        {
            get { return (string)GetValue(Card3SuitProperty); }
            set { SetValue(Card3SuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card3Suit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card3SuitProperty =
            DependencyProperty.Register("Card3Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));


        public string Card4Suit
        {
            get { return (string)GetValue(Card4SuitProperty); }
            set { SetValue(Card4SuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card4Suit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card4SuitProperty =
            DependencyProperty.Register("Card4Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));


        public string Card5Suit
        {
            get { return (string)GetValue(Card5SuitProperty); }
            set { SetValue(Card5SuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card5Suit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card5SuitProperty =
            DependencyProperty.Register("Card5Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));


        public string Card6Suit
        {
            get { return (string)GetValue(Card6SuitProperty); }
            set { SetValue(Card6SuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card6Suit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card6SuitProperty =
            DependencyProperty.Register("Card6Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));


        public string Card7Suit
        {
            get { return (string)GetValue(Card7SuitProperty); }
            set { SetValue(Card7SuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card7Suit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Card7SuitProperty =
            DependencyProperty.Register("Card7Suit", typeof(string), typeof(JogoBalatro), new PropertyMetadata(""));










        // --------------------------------------------------------- Check\\Par\\Tripla\\Quadra ------------------------------------------------------------------

        public int EqualCardValuesCount
        {
            get { return (int)GetValue(EqualCardValuesCountProperty); }
            set { SetValue(EqualCardValuesCountProperty, value); }
        }

        public static readonly DependencyProperty EqualCardValuesCountProperty =
            DependencyProperty.Register("EqualCardValuesCount", typeof(int), typeof(JogoBalatro), new PropertyMetadata(0));

        // --------------------------------------------------------- DeckSlot ------------------------------------------------------------------

        public int DeckSlot
        {
            get { return (int)GetValue(DeckSlotProperty); }
            set { SetValue(DeckSlotProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeckSlotProperty =
            DependencyProperty.Register("DeckSlot", typeof(int), typeof(JogoBalatro), new PropertyMetadata(cardback));



        // --------------------------------------------------------- SelectedSlotCards ------------------------------------------------------------------

        public bool IsCard1Selected
        {
            get { return (bool)GetValue(IsCard1SelectedProperty); }
            set { SetValue(IsCard1SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard1SelectedProperty =
            DependencyProperty.Register("IsCard1Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));

        public bool IsCard2Selected
        {
            get { return (bool)GetValue(IsCard2SelectedProperty); }
            set { SetValue(IsCard2SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard2SelectedProperty =
            DependencyProperty.Register("IsCard2Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));

        public bool IsCard3Selected
        {
            get { return (bool)GetValue(IsCard3SelectedProperty); }
            set { SetValue(IsCard3SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard3SelectedProperty =
            DependencyProperty.Register("IsCard3Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));

        public bool IsCard4Selected
        {
            get { return (bool)GetValue(IsCard4SelectedProperty); }
            set { SetValue(IsCard4SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard4SelectedProperty =
            DependencyProperty.Register("IsCard4Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));

        public bool IsCard5Selected
        {
            get { return (bool)GetValue(IsCard5SelectedProperty); }
            set { SetValue(IsCard5SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard5SelectedProperty =
            DependencyProperty.Register("IsCard5Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));

        public bool IsCard6Selected
        {
            get { return (bool)GetValue(IsCard6SelectedProperty); }
            set { SetValue(IsCard6SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard6SelectedProperty =
            DependencyProperty.Register("IsCard6Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));

        public bool IsCard7Selected
        {
            get { return (bool)GetValue(IsCard7SelectedProperty); }
            set { SetValue(IsCard7SelectedProperty, value); }
        }

        public static readonly DependencyProperty IsCard7SelectedProperty =
            DependencyProperty.Register("IsCard7Selected", typeof(bool), typeof(JogoBalatro), new PropertyMetadata(false));


        public JogoBalatro()
        {
            _deck = new Deck();
            DiscardAmount = 4;
        }

        public void begingame()
        {

            _deck = new Deck();
            soma = 0;
            score = 0;
            BeginDraw();
            DiscardAmount = 4;
            PlaysAmount = 4;


        }

        public void BeginDraw()
        {
            
            var card1 = _deck.DrawCard();
            Card1 = card1?.Id ?? 0;
            Card1Value = card1?.Valor ?? 0;
            Card1Suit = card1?.Naipe ?? "";

            var card2 = _deck.DrawCard();
            Card2 = card2?.Id ?? 0;
            Card2Value = card2?.Valor ?? 0;
            Card2Suit = card2?.Naipe ?? "";

            var card3 = _deck.DrawCard();
            Card3 = card3?.Id ?? 0;
            Card3Value = card3?.Valor ?? 0;
            Card3Suit = card3?.Naipe ?? "";

            var card4 = _deck.DrawCard();
            Card4 = card4?.Id ?? 0;
            Card4Value = card4?.Valor ?? 0;
            Card4Suit = card4?.Naipe ?? "";

            var card5 = _deck.DrawCard();
            Card5 = card5?.Id ?? 0;
            Card5Value = card5?.Valor ?? 0;
            Card5Suit = card5?.Naipe ?? "";

            var card6 = _deck.DrawCard();
            Card6 = card6?.Id ?? 0;
            Card6Value = card6?.Valor ?? 0;
            Card6Suit = card6?.Naipe ?? "";

            var card7 = _deck.DrawCard();
            Card7 = card7?.Id ?? 0;
            Card7Value = card7?.Valor ?? 0;
            Card7Suit = card7?.Naipe ?? "";

            DiscardAmount -= 1;
            

            //Logica Antiga
            /*Card1 = _deck.DrawCard()?.Id ?? 0;
            Card2 = _deck.DrawCard()?.Id ?? 0;
            Card3 = _deck.DrawCard()?.Id ?? 0;
            Card4 = _deck.DrawCard()?.Id ?? 0;
            Card5 = _deck.DrawCard()?.Id ?? 0;
            Card6 = _deck.DrawCard()?.Id ?? 0;
            Card7 = _deck.DrawCard()?.Id ?? 0;*/
            
        }

        public void PlayCards()
        {
            soma = 0;
            if (PlaysAmount > 0)
            {
                PlaysAmount -= 1;
                CheckFlushAndStraight();

                if (hasFlush_AndOr_Straight == false)
                {
                   CheckEqualCardValues();
                }
            }
            hasFlush_AndOr_Straight = false;

            #region Trocar cartas selecionadas
            if (IsCard1Selected)
            {
                var card1 = _deck.DrawCard();
                Card1 = card1?.Id ?? 0;
                Card1Value = card1?.Valor ?? 0;
                Card1Suit = card1?.Naipe ?? "";
            }

            if (IsCard2Selected)
            {
                var card2 = _deck.DrawCard();
                Card2 = card2?.Id ?? 0;
                Card2Value = card2?.Valor ?? 0;
                Card2Suit = card2?.Naipe ?? "";
            }

            if (IsCard3Selected)
            {
                var card3 = _deck.DrawCard();
                Card3 = card3?.Id ?? 0;
                Card3Value = card3?.Valor ?? 0;
                Card3Suit = card3?.Naipe ?? "";
            }

            if (IsCard4Selected)
            {
                var card4 = _deck.DrawCard();
                Card4 = card4?.Id ?? 0;
                Card4Value = card4?.Valor ?? 0;
                Card4Suit = card4?.Naipe ?? "";
            }

            if (IsCard5Selected)
            {
                var card5 = _deck.DrawCard();
                Card5 = card5?.Id ?? 0;
                Card5Value = card5?.Valor ?? 0;
                Card5Suit = card5?.Naipe ?? "";
            }

            if (IsCard6Selected)
            {
                var card6 = _deck.DrawCard();
                Card6 = card6?.Id ?? 0;
                Card6Value = card6?.Valor ?? 0;
                Card6Suit = card6?.Naipe ?? "";
            }

            if (IsCard7Selected)
            {
                var card7 = _deck.DrawCard();
                Card7 = card7?.Id ?? 0;
                Card7Value = card7?.Valor ?? 0;
                Card7Suit = card7?.Naipe ?? "";
            }
            #endregion

        }

        public void DrawCards()
        {

            bool anyCardSelected = IsCard1Selected || IsCard2Selected || IsCard3Selected || IsCard4Selected || IsCard5Selected || IsCard6Selected || IsCard7Selected;

            if (!anyCardSelected)
            {
                logtext = "É necessário selecionar pelo menos uma carta antes de fazer o Discard.";
                return; // Retorna sem reduzir o DiscardAmount
            }

            if (IsCard1Selected)
            {
                var card1 = _deck.DrawCard();
                Card1 = card1?.Id ?? 0;
                Card1Value = card1?.Valor ?? 0;
                Card1Suit = card1?.Naipe ?? "";
            }

            if (IsCard2Selected)
            {
                var card2 = _deck.DrawCard();
                Card2 = card2?.Id ?? 0;
                Card2Value = card2?.Valor ?? 0;
                Card2Suit = card2?.Naipe ?? "";
            }

            if (IsCard3Selected)
            {
                var card3 = _deck.DrawCard();
                Card3 = card3?.Id ?? 0;
                Card3Value = card3?.Valor ?? 0;
                Card3Suit = card3?.Naipe ?? "";
            }

            if (IsCard4Selected)
            {
                var card4 = _deck.DrawCard();
                Card4 = card4?.Id ?? 0;
                Card4Value = card4?.Valor ?? 0;
                Card4Suit = card4?.Naipe ?? "";
            }

            if (IsCard5Selected)
            {
                var card5 = _deck.DrawCard();
                Card5 = card5?.Id ?? 0;
                Card5Value = card5?.Valor ?? 0;
                Card5Suit = card5?.Naipe ?? "";
            }

            if (IsCard6Selected)
            {
                var card6 = _deck.DrawCard();
                Card6 = card6?.Id ?? 0;
                Card6Value = card6?.Valor ?? 0;
                Card6Suit = card6?.Naipe ?? "";
            }

            if (IsCard7Selected)
            {
                var card7 = _deck.DrawCard();
                Card7 = card7?.Id ?? 0;
                Card7Value = card7?.Valor ?? 0;
                Card7Suit = card7?.Naipe ?? "";
            }


            DiscardAmount -= 1;

        }

        

        public void CheckFlushAndStraight()
        {
            List<int> selectedValues = new List<int>();
            List<string> selectedSuits = new List<string>();

            if (IsCard1Selected)
            {
                selectedValues.Add(Card1Value);
                selectedSuits.Add(Card1Suit);
            }
            if (IsCard2Selected)
            {
                selectedValues.Add(Card2Value);
                selectedSuits.Add(Card2Suit);
            }
            if (IsCard3Selected)
            {
                selectedValues.Add(Card3Value);
                selectedSuits.Add(Card3Suit);
            }
            if (IsCard4Selected)
            {
                selectedValues.Add(Card4Value);
                selectedSuits.Add(Card4Suit);
            }
            if (IsCard5Selected)
            {
                selectedValues.Add(Card5Value);
                selectedSuits.Add(Card5Suit);
            }
            if (IsCard6Selected)
            {
                selectedValues.Add(Card6Value);
                selectedSuits.Add(Card6Suit);
            }
            if (IsCard7Selected)
            {
                selectedValues.Add(Card7Value);
                selectedSuits.Add(Card7Suit);
            }
            if (selectedValues.Count == 5)
            {
                /*foreach (var suit in selectedSuits) // Valida o Suit das cartas selecionadas \\ Despiste apenas
                {
                    MessageBox.Show(suit);
                }*/

                // Verifica se todos os naipes são iguais
                bool hasFlush = selectedSuits.Distinct().Count() == 1;

                // Verifica se as cartas selecionadas formam um straight
                bool hasStraight = CheckStraight();


                sumOfSelectedCardValues = selectedValues.Sum();

                if (hasFlush && hasStraight)
                {
                    soma += (sumOfSelectedCardValues + 100) * Straight_Flush_Mult8;
                    score += soma;
                    logtext = ($"Temos uma Straight Flush! \nPontuação da mão: {soma} \nTotal: {score} ");
                    hasFlush_AndOr_Straight = true;
                }
                else if (hasFlush)
                {
                    soma += (sumOfSelectedCardValues + 35) * Flush4;
                    score += soma;
                    logtext = ($"Temos uma Flush! \nPontuação da mão: {soma} \nTotal: {score} ");
                    hasFlush_AndOr_Straight = true;
                }

            }



        }

        private bool CheckStraight()
        {
            List<int> selectedValues = new List<int>();

            if (IsCard1Selected)
            {
                selectedValues.Add(Card1Value);
            }
            if (IsCard2Selected)
            {
                selectedValues.Add(Card2Value);
            }
            if (IsCard3Selected)
            {
                selectedValues.Add(Card3Value);
            }
            if (IsCard4Selected)
            {
                selectedValues.Add(Card4Value);
            }
            if (IsCard5Selected)
            {
                selectedValues.Add(Card5Value);
            }
            if (IsCard6Selected)
            {
                selectedValues.Add(Card6Value);
            }
            if (IsCard7Selected)
            {
                selectedValues.Add(Card7Value);
            }

            if (selectedValues.Count < 5)
            {
                return false;
            }

            // Ordena os valores em ordem crescente
            selectedValues.Sort();


            // Verifica se os valores formam uma sequência consecutiva
            for (int i = 0; i < selectedValues.Count - 1; i++)
            {
                if (selectedValues[i + 1] != selectedValues[i] + 1)
                {
                    return false;
                }
            }
            sumOfSelectedCardValues = selectedValues.Sum();
            soma += (sumOfSelectedCardValues + 30) * Straight4;
            score += soma;
            logtext = ($"Temos uma Straight! \nPontuação da mão: {soma} \nTotal: {score} ");
            hasFlush_AndOr_Straight = true;
            return true;
        }


        public void CheckEqualCardValues()
        {
            #region Valida Cards Selected
            List<int> selectedCardValues = new List<int>();

            // Adiciona o valor da carta à lista se o respectivo checkbox estiver selecionado
            if (IsCard1Selected)
            {
                selectedCardValues.Add(Card1Value);
            }
            if (IsCard2Selected)
            {
                selectedCardValues.Add(Card2Value);
            }
            if (IsCard3Selected)
            {
                selectedCardValues.Add(Card3Value);
            }
            if (IsCard4Selected)
            {
                selectedCardValues.Add(Card4Value);
            }
            if (IsCard5Selected)
            {
                selectedCardValues.Add(Card5Value);
            }
            if (IsCard6Selected)
            {
                selectedCardValues.Add(Card6Value);
            }
            if (IsCard7Selected)
            {
                selectedCardValues.Add(Card7Value);
            }
            #endregion

            int[] cardValues = selectedCardValues.ToArray();

            //MessageBox.Show("Valores selecionados: " + string.Join(", ", cardValues)); // Apenas para despiste

            var groupedValues = cardValues.GroupBy(v => v).Where(g => g.Count() > 1);

            /*foreach (var group in groupedValues)             \\\ Apenas para despiste
            {
                MessageBox.Show($"Valor: {group.Key}, Contagem: {group.Count()}");
            }*/

            EqualCardValuesCount = groupedValues.Sum(g => g.Count());

            sumOfSelectedCardValues = selectedCardValues.Sum();

            // HAND FOUR OF A KIND \\ Verifica se há uma quadra
            if (groupedValues.Any(g => g.Count() == 4))
            {
                
                soma += (sumOfSelectedCardValues + 60) * Four_of_a_Kind7;
                score += soma;
                logtext = ($"Temos uma Quadra! \nPontuação da mão: {soma} \nTotal: {score} ");
            
            }
            // HAND FULL HOUSE \\ Verifica se há uma tripla e um par
            else if (groupedValues.Any(g => g.Count() == 3) && groupedValues.Any(g => g.Count() == 2))
            {
                soma += (sumOfSelectedCardValues + 40) * Full_House4;
                score += soma;
                logtext = ($"Temos um Full House! \nPontuação da mão: {soma} \nTotal: {score} ");
            }

            // HAND TWO PAIR \\ Verifica se há dois pares
            else if (groupedValues.Count(g => g.Count() == 2) == 2)
            {
                soma += (sumOfSelectedCardValues + 20) * Two_Pair2;
                score += soma;
                logtext = ($"Temos dois Pares! \nPontuação da mão: {soma} \nTotal: {score} ");
            }

            // HAND THREE OF A KIND \\ Verifica se há um trio
            else if (groupedValues.Any(g => g.Count() == 3))
            {
                soma += (sumOfSelectedCardValues + 30) * Three_of_a_Kind3;
                score += soma;
                logtext = ($"Temos uma Tripla! \nPontuação da mão: {soma} \nTotal: {score} ");
            }

            // HAND PAIR \\ Verifica se há um par
            else if (groupedValues.Count(g => g.Count() == 2) == 1)
            {
                soma += (sumOfSelectedCardValues + 10) * Pair2;
                score += soma;
                logtext = ($"Temos um Par! \nPontuação da mão: {soma} \nTotal: {score} ");
            }
            else
            {
                soma += sumOfSelectedCardValues;
                score += soma;
                logtext = ($"Nenhuma combinação válida. \nPontuação da mão: {soma} \nTotal: {score} ");
            }


            hasFlush_AndOr_Straight = false;
            
            

        }


    }
}
