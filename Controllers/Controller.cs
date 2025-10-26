//using Maio10.Models;
using Maio10.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;
using Maio10.Models;

namespace Maio10.Controllers
{
    public class Controller : UIElement
    {
        DispatcherTimer timer = new DispatcherTimer();
        MainWindow main = (MainWindow)App.Current.MainWindow;
        public bool gamebegan = false;
        public bool gameend = true;
        public int playcard_allower = 5;
        public int playsleft = 4;
        public int score;
        public bool Round1Won = false;
        public bool Round2Won = false;
        public bool Round3Won = false;
        public bool Round4Won = false;  
        public bool Round5Won = false;
        public bool Impossivel = false;
        public int ControllScore { get; set; }
        public ClsJogoDados myjogo { get; set; }
        public Models.CarrosModel carrosModel { get; set; }
        public Models.CarrosModel playersModel { get; set; }
        public Cmd cmdselos { get; set; }
        public Cmd cmdsair { get; set; }
        public Cmd cmdnavega { get; set; }
        public Cmd cmdrolar { get; set; }
        public JogoBalatro mycards { get; set; }
        public Cmd cmdbegingame { get; set; }
        public Cmd cmdplaycards { get; set; }
        public Cmd cmddrawcards { get; set; }
        public Cmd cmdSacaFoto { get; set; }
        public Cmd cmdSacaFotoPlayer { get; set; }
        public Cmd cmdGravar { get; set; }
        public Cmd cmdInserir { get; set; }
        public Cmd cmdApagar { get; set; }
        public Cmd cmdModificarPlayer { get; set; }
        public Cmd cmdApagarPlayer { get; set; }
        public Cmd cmdCriarPlayer { get; set; }

        public List<player> Players { get; set; } 
        public player SelectedPlayer { get; set; }



        public Controller()
        {
            carrosModel = new Models.CarrosModel();
            playersModel = new Models.CarrosModel();
            myjogo = new ClsJogoDados();
            myjogo.OnPremio += Myjogo_OnPremio;
            cmdsair = new Cmd(Sair, (obj) => true);
            cmdnavega = new Cmd(Navega, canNavega);
            cmdselos = new Cmd(selos, canSelos);
            cmdrolar = new Cmd(Rolar, canRolar);
            mycards = new JogoBalatro();
            cmdbegingame = new Cmd(Begin, canBegin);
            cmdplaycards = new Cmd(PlayCards, canPlayCards);
            cmddrawcards = new Cmd(DrawCards, canDrawCards);
            cmdSacaFoto = new Cmd(GetFoto, (x) => true);
            cmdGravar = new Cmd(carrosModel.GravaCarro, (x) => true);
            cmdInserir = new Cmd(carrosModel.inserirCarro, (x) => true);
            cmdApagar = new Cmd(carrosModel.apagarCarro, (x) => true);

            cmdSacaFotoPlayer = new Cmd(GetFotoPlayer, (x) => true);
            cmdModificarPlayer = new Cmd(playersModel.ModificarPlayer, (x) => true);
            cmdApagarPlayer = new Cmd(playersModel.ApagarPlayer, (x) => true);
            cmdCriarPlayer = new Cmd(playersModel.CriarPlayer, (x) => true);


        }


        #region Player

        public void GetFotoPlayer(object parameter)
        {
            string path = System.Environment.CurrentDirectory;
            path = path.Substring(0, path.IndexOf("bin")) + @"imagens\";
            string fich = "";
            System.Windows.Controls.Image img1 = parameter as System.Windows.Controls.Image;
            OpenFileDialog dlg = new OpenFileDialog();
            string[] fichs = Directory.GetFiles(path);
            foreach (string f in fichs)
            {
                Regex r = new Regex(playersModel.PlayerCorrente.idplayer.ToString() + @"\.\w*");
                if (r.IsMatch(f)) File.Delete(f);
            }
            dlg.Filter = "Todos|*.*|bmp|*.bmp|png|*.png";
            if (dlg.ShowDialog() == true)
            {
                fich = playersModel.PlayerCorrente.idplayer.ToString() +
                     System.IO.Path.GetExtension(dlg.FileName);
                playersModel.PlayerCorrente.fotopath = fich;
                path += fich;
                System.IO.File.Copy(dlg.FileName, path, true);
                playersModel.ModificarPlayer(null);
            }
            img1.GetBindingExpression(System.Windows.Controls.Image.SourceProperty).UpdateSource();
            img1.GetBindingExpression(System.Windows.Controls.Image.SourceProperty).UpdateTarget();


        }

        #endregion

        #region Stand
        public void GetFoto(object parameter)
        {
            string path = System.Environment.CurrentDirectory;
            path = path.Substring(0, path.IndexOf("bin")) + @"imagens\";
            string fich = "";
            System.Windows.Controls.Image img = parameter as System.Windows.Controls.Image;
            OpenFileDialog dlg = new OpenFileDialog();
            string[] fichs = Directory.GetFiles(path);
            foreach (string f in fichs)
            {
                Regex r = new Regex(carrosModel.CarroCorrente.idcar.ToString() + @"\.\w*");
                if (r.IsMatch(f)) File.Delete(f);
            }
            dlg.Filter = "Todos|*.*|bmp|*.bmp|png|*.png";
            if (dlg.ShowDialog() == true)
            {
                fich = carrosModel.CarroCorrente.idcar.ToString() +
                     System.IO.Path.GetExtension(dlg.FileName);
                carrosModel.CarroCorrente.fotopath = fich;
                path += fich;
                System.IO.File.Copy(dlg.FileName, path, true);
                carrosModel.GravaCarro(null);
            }
            img.GetBindingExpression(System.Windows.Controls.Image.SourceProperty).UpdateSource();
            img.GetBindingExpression(System.Windows.Controls.Image.SourceProperty).UpdateTarget();


        }


        private void Myjogo_OnChangeMontante(string str)
        {
            ((JogoDados)main.frame.Content).tv.Text += str;
        }
        #endregion

        #region BeginGame
        public bool canBegin(Object parameter)
        {

            if (playersModel.CanComecarJogo(null) == true && !gamebegan || playsleft == 0) 
            {
                return true;
            }
            else
            {
                return false;
            }

            
            
        }

        public void Begin(Object parameter)
        {
            Balatro jogoBalatro = (Balatro)main.frame.Content;

            if (playersModel.ComecarJogo(null) == false)
            {
                MessageBox.Show("Escolha um Player com dinheiro");
                return;
            }
            int remainingHealth = mycards.Health - mycards.score;

            #region Inimigos e Rounds
            void DevilIntro()
            {
                string path = @"..\\..\\audios\\Devil_Intro.wav";
                SoundPlayer player = new SoundPlayer(path);
                player.Load();
                player.PlayLooping();
            }

            void KingDiceIntro()
            {
                string path = @"..\\..\\audios\\KingDiceintro.wav";
                SoundPlayer player = new SoundPlayer(path);
                player.Load();
                player.PlayLooping();
            }
            void BeppiIntro()
            {
                string path = @"..\\..\\audios\\Carnival.wav";
                SoundPlayer player = new SoundPlayer(path);
                player.Load();
                player.PlayLooping();
            }

            void HopusIntro()
            {
                string path = @"..\\..\\audios\\Rabbit.wav";
                SoundPlayer player = new SoundPlayer(path);
                player.Load();
                player.PlayLooping();
            }

            if (Round5Won == true)
            {
                mycards.NameBoss = "D¿E?¿V?I¿L:";
                mycards.BossSource = "/inimigos/devil.png";
                mycards.BossWidth = 360;
                mycards.BossHeight = 259;
                DevilIntro();
                if (remainingHealth > 0)
                {
                    jogoBalatro.logbox.Text = "D¿E?¿V?I¿L está pronto!";

                }
            }
            else if (Round4Won == true)
            {
                mycards.NameBoss = "DEVIL:";
                mycards.BossSource = "/inimigos/devilphase2.png";
                mycards.BossWidth = 320;
                mycards.BossHeight = 239;
                DevilIntro();
                if (remainingHealth > 0)
                {
                    jogoBalatro.logbox.Text = "DEVIL está pronto!";

                }

            }
            else if (Round3Won == true)
            {
                mycards.NameBoss = "King Dice:";
                mycards.BossSource = "/inimigos/kingdice.png";
                mycards.BossWidth = 480;
                mycards.BossHeight = 319;
                KingDiceIntro();
                if (remainingHealth > 0)
                {
                    jogoBalatro.logbox.Text = "King Dice está pronto!";


                }

            }
            else if (Round2Won == true)
            {
                mycards.NameBoss = "Hopus Pocus:";
                mycards.BossSource = "/inimigos/hopuspocus.png";
                mycards.BossWidth = 320;
                mycards.BossHeight = 239;
                HopusIntro();
                if (remainingHealth > 0)
                {
                    jogoBalatro.logbox.Text = "Hopus Pocus está pronto!";

                }

            }
            else if (Round1Won == true)
            {
                mycards.NameBoss = "Pirouletta:";
                mycards.BossSource = "/inimigos/pirouletta.png";
                mycards.BossWidth = 320;
                mycards.BossHeight = 239;
                BeppiIntro();
                if (remainingHealth > 0)
                {
                    jogoBalatro.logbox.Text = "Pirouletta está pronto!";

                }

            }
            else
            {
                mycards.NameBoss = "Beppi The Clown:";
                mycards.BossSource = "/inimigos/beppitheclown.png";
                mycards.BossWidth = 320;
                mycards.BossHeight = 239;
                BeppiIntro();
                if (remainingHealth > 0)
                {
                    jogoBalatro.logbox.Text = "Beppi The Clown está pronto!";
                }


            }
            #endregion

            jogoBalatro.PlayerSelectionText.Visibility = Visibility.Collapsed;

            gameend = false;
            mycards.begingame();
            gamebegan = true;
            
            jogoBalatro.drawamount.Text = mycards.DiscardAmount.ToString();
            jogoBalatro.playsamount.Text = mycards.PlaysAmount.ToString();
            jogoBalatro.roundnum.Text = mycards.RoundNum.ToString();
            
            
            int remainingCards = mycards._deck.Cards.Length - mycards._deck.CurrentIndex;
            remainingHealth = mycards.Health - mycards.score;
            jogoBalatro.boss.Width = mycards.BossWidth;
            jogoBalatro.boss.Height = mycards.BossHeight;
            jogoBalatro.kinghealth.Text = remainingHealth + "/" + mycards.Health;
            jogoBalatro.kinghealth_name.Text = mycards.NameBoss;
            jogoBalatro.kinghealth_name.Visibility = Visibility.Visible;
            jogoBalatro.PlayerList.Visibility = Visibility.Hidden;
            jogoBalatro.deck_amount.Text = remainingCards + "/52";
            jogoBalatro.boss.Source = new BitmapImage(new Uri(mycards.BossSource, UriKind.Relative));



            playsleft = 4;
            mycards.GameStarted = true;
            
        }
        #endregion

        #region PlayCards
        public bool canPlayCards(Object parameter)
        {
            if (playsleft == 0 || gameend == true)
            {
                return false;
            }
            else if (gamebegan == true || playcard_allower <= 0)
            {
                return true;
            }
            return false;
        }

        public void PlayCards(Object parameter)
        {
            Balatro jogoBalatro = (Balatro)main.frame.Content;
            
            int remainingCards = mycards._deck.Cards.Length - mycards._deck.CurrentIndex;
            jogoBalatro.kinghealth_name.Visibility = Visibility.Visible;
            jogoBalatro.roundnum.Text = mycards.RoundNum.ToString();
            jogoBalatro.logbox.Text = mycards.logtext.ToString();
            jogoBalatro.boss.Width = mycards.BossWidth;
            jogoBalatro.boss.Height = mycards.BossHeight;
            jogoBalatro.kinghealth_name.Text = mycards.NameBoss;
            jogoBalatro.PlayerSelectionText.Visibility = Visibility.Collapsed;
            bool Selectedslot1 = jogoBalatro.selected_card1.IsChecked == true;
            bool Selectedslot2 = jogoBalatro.selected_card2.IsChecked == true;
            bool Selectedslot3 = jogoBalatro.selected_card3.IsChecked == true;
            bool Selectedslot4 = jogoBalatro.selected_card4.IsChecked == true;
            bool Selectedslot5 = jogoBalatro.selected_card5.IsChecked == true;
            bool Selectedslot6 = jogoBalatro.selected_card6.IsChecked == true;
            bool Selectedslot7 = jogoBalatro.selected_card7.IsChecked == true;

            int selectedCount = 0;
            if (Selectedslot1) selectedCount++;
            if (Selectedslot2) selectedCount++;
            if (Selectedslot3) selectedCount++;
            if (Selectedslot4) selectedCount++;
            if (Selectedslot5) selectedCount++;
            if (Selectedslot6) selectedCount++;
            if (Selectedslot7) selectedCount++;


            if (selectedCount <= 5 && selectedCount != 0)
            {
                mycards.PlayCards();
                remainingCards = mycards._deck.Cards.Length - mycards._deck.CurrentIndex;
                jogoBalatro.playsamount.Text = mycards.PlaysAmount.ToString();
                jogoBalatro.deck_amount.Text = remainingCards + "/52";
                jogoBalatro.logbox.Text = mycards.logtext.ToString();
                

                void Knockout()
                {
                    string path = @"..\\..\\audios\\knockout.wav";
                    SoundPlayer player = new SoundPlayer(path);
                    player.Load();
                    player.Play();
                }

                void DevilKnock()
                {
                    string path = @"..\\..\\audios\\DevilOminous.wav";
                    SoundPlayer player = new SoundPlayer(path);
                    player.Load();
                    player.Play();
                }

                int remainingHealth = mycards.Health - mycards.score;
                if (remainingHealth < 0)
                {
                    jogoBalatro.PlayerList.Visibility = Visibility.Visible;
                    jogoBalatro.kinghealth.Text = 0 + "/" + mycards.Health;
                    gameend = true;
                    gamebegan = false;
                    if (Impossivel == true)
                    {
                        mycards.logtext = "Fizeste o impossivel. O Devil foi derrotado. Ganhas-te 10000€!";
                        Knockout();
                        playersModel.WinBalatro(6);
                    }
                    else if (Round4Won == true)
                    {
                        mycards.Health = 9999;
                        mycards.RoundNum = 9;
                        Round5Won = true;
                        Impossivel = true;
                        mycards.logtext = "O Devil é demasiado forte, terá que ser derrotado novamente.";
                        DevilKnock();
                        playersModel.WinBalatro(5);
                    }
                    else if (Round3Won == true)
                    {
                        mycards.Health = 1333;
                        Round4Won = true;
                        mycards.RoundNum = 3;
                        mycards.logtext = "O King Dice foi derrotado.\nGanhas-te 1500€!";
                        Knockout();
                        playersModel.WinBalatro(4);
                    }
                    else if (Round2Won == true)
                    {
                        mycards.Health = 1000;
                        Round3Won = true;
                        mycards.RoundNum = 2;
                        mycards.logtext = "O Hopus Pocus foi derrotado.\nGanhas-te 1000€!";
                        Knockout();
                        playersModel.WinBalatro(3);
                    }
                    else if (Round1Won == true)
                    {
                        mycards.Health = 800;
                        Round2Won = true;
                        mycards.RoundNum = 1;
                        mycards.logtext = "O Pirouletta foi derrotado.\nGanhas-te 800€!";
                        Knockout();
                        playersModel.WinBalatro(2);
                    }
                    else
                    {
                        mycards.Health = 600;
                        Round1Won = true;
                        mycards.RoundNum = 1;
                        mycards.logtext = "O Beppi The Clown foi derrotado.\nGanhas-te 600€!";
                        Knockout();
                        playersModel.WinBalatro(1);

                    }

                }
                else
                {
                    jogoBalatro.kinghealth.Text = remainingHealth + "/" + mycards.Health;
                }



                playsleft = mycards.PlaysAmount;
                if (playsleft == 0 && remainingHealth != 0)
                {
                    gameend = true;
                    gamebegan = false;
                    mycards.logtext = "GAME OVER!";
                    jogoBalatro.PlayerList.Visibility = Visibility.Visible;
                }

            }
            else if (selectedCount == 0)
            {
                mycards.logtext = "É necessário selecionar pelo uma carta.";
                int remainingHealth = mycards.Health - mycards.score;
                jogoBalatro.kinghealth.Text = remainingHealth + "/" + mycards.Health;
            }
            else
            {
                mycards.logtext = "Jogada inválida. Foram selecionadas mais de 5 cartas.";
                int remainingHealth = mycards.Health - mycards.score;
                jogoBalatro.kinghealth.Text = remainingHealth + "/" + mycards.Health;
            }

            

            // Mete todas as checkbox devolta a desselecionado
            jogoBalatro.selected_card1.IsChecked = false;
            jogoBalatro.selected_card2.IsChecked = false;
            jogoBalatro.selected_card3.IsChecked = false;
            jogoBalatro.selected_card4.IsChecked = false;
            jogoBalatro.selected_card5.IsChecked = false;
            jogoBalatro.selected_card6.IsChecked = false;
            jogoBalatro.selected_card7.IsChecked = false;

            

            
            jogoBalatro.logbox.Text = mycards.logtext.ToString();

            playersModel.HighscoreHand(mycards.score);
        }

        #endregion

        #region DrawCards
        public bool canDrawCards(Object parameter)
        {

            if (parameter == null) return false;
            else if (int.TryParse(parameter.ToString(), out int drawamount) && gamebegan == true && playsleft != 0)
            {
                if (drawamount > 0)
                {
                    return true;
                }
               

            }
            return false;


        }

        public void DrawCards(Object parameter)
        {
            Balatro jogoBalatro = (Balatro)main.frame.Content;
            
            int remainingCards = mycards._deck.Cards.Length - mycards._deck.CurrentIndex;
            jogoBalatro.kinghealth_name.Visibility = Visibility.Visible;
            jogoBalatro.roundnum.Text = mycards.RoundNum.ToString();
            jogoBalatro.logbox.Text = mycards.logtext.ToString();
            jogoBalatro.boss.Width = mycards.BossWidth;
            jogoBalatro.boss.Height = mycards.BossHeight;
            jogoBalatro.kinghealth_name.Text = mycards.NameBoss;
            int remainingHealth = mycards.Health - mycards.score;
            jogoBalatro.kinghealth.Text = remainingHealth + "/" + mycards.Health;
            jogoBalatro.PlayerSelectionText.Visibility = Visibility.Collapsed;
            bool Selectedslot1 = jogoBalatro.selected_card1.IsChecked == true;
            bool Selectedslot2 = jogoBalatro.selected_card2.IsChecked == true;
            bool Selectedslot3 = jogoBalatro.selected_card3.IsChecked == true;
            bool Selectedslot4 = jogoBalatro.selected_card4.IsChecked == true;
            bool Selectedslot5 = jogoBalatro.selected_card5.IsChecked == true;
            bool Selectedslot6 = jogoBalatro.selected_card6.IsChecked == true;
            bool Selectedslot7 = jogoBalatro.selected_card7.IsChecked == true;

            int selectedCount = 0;
            if (Selectedslot1) selectedCount++;
            if (Selectedslot2) selectedCount++;
            if (Selectedslot3) selectedCount++;
            if (Selectedslot4) selectedCount++;
            if (Selectedslot5) selectedCount++;
            if (Selectedslot6) selectedCount++;
            if (Selectedslot7) selectedCount++;

            if (selectedCount <= 5 && selectedCount != 0)
            {
                playcard_allower = playcard_allower - 1;
                mycards.DrawCards();
                jogoBalatro.drawamount.Text = mycards.DiscardAmount.ToString();

                
                
            }
            else if (selectedCount == 0)
            {
                mycards.logtext = "É necessário selecionar pelo uma carta.";
            }
            else
            {
                mycards.logtext = "Jogada inválida. Foram selecionadas mais de 5 cartas.";
            }
            jogoBalatro.selected_card1.IsChecked = false;
            jogoBalatro.selected_card2.IsChecked = false;
            jogoBalatro.selected_card3.IsChecked = false;
            jogoBalatro.selected_card4.IsChecked = false;
            jogoBalatro.selected_card5.IsChecked = false;
            jogoBalatro.selected_card6.IsChecked = false;
            jogoBalatro.selected_card7.IsChecked = false;
            remainingCards = mycards._deck.Cards.Length - mycards._deck.CurrentIndex;
            jogoBalatro.deck_amount.Text = remainingCards + "/52";
            jogoBalatro.logbox.Text = mycards.logtext.ToString();

        }
        #endregion

        #region Premio
        private void Myjogo_OnPremio(Object sender, RoutedEventArgs e)
        {
            ClsJogoDados jogo = (ClsJogoDados)sender;
            ClsJogoDados.OnPremioRoutedEventArgs args = (ClsJogoDados.OnPremioRoutedEventArgs)e;
            int premio = jogo.Dado1 * 2 * args.Aposta;
            jogo.Montante += premio;
            StringBuilder str = new StringBuilder();
            str.Append($"Parabéns \r\nganhou €{premio}");

            JogoDados jogoDados = (JogoDados)main.frame.Content;
            jogoDados.tv.Text = str.ToString();
        }
        #endregion

        #region Rolar Dados
        public bool canRolar(Object parameter)
        {
            if (parameter == null) return false;
            else if (int.TryParse(parameter.ToString(), out int aposta))
            {
                if (aposta > 0) return true;

            }
            return false;
        }

        public void Rolar(Object parameter)
        {
            JogoDados jogoDados = (JogoDados)main.frame.Content;
            jogoDados.tv.Text = "";
            if (int.TryParse(parameter.ToString(), out int aposta))
            {
                myjogo.rolar(aposta);

                if (jogoDados.txtmontante.Text == "0") jogoDados.slider.Value = 0;
            }

        }
        #endregion

        #region Selos
        public bool canSelos(Object parameter)
        {
            if (String.IsNullOrEmpty(parameter.ToString())) return false;
            if (int.Parse(parameter.ToString()) == 0) return false;
            return true;
        }

        public void selos(Object parameter)
        {
            int euros;
            Selos pagselos = (Selos)main.frame.Content;
            if (int.TryParse(parameter.ToString(), out euros))
            {


                pagselos.txtresult.Text = trocaselos(euros);
            }
            else
            {

                pagselos.txtresult.Text = "Erro - quantia inválida";
            }

        }

        public string trocaselos(int euros)
        {
            String resposta = "";
            int s5 = 0, s3 = 0, quoc, r;
            if (euros >= 8)
            {
                quoc = euros / 8;
                r = euros % 8;
                switch (r)
                {
                    case 0: s5 = quoc; s3 = quoc; break;
                    case 1: s5 = quoc - 1; s3 = quoc + 2; break;
                    case 2: s5 = quoc + 1; s3 = quoc - 1; break;
                    case 3: s5 = quoc; s3 = quoc + 1; break;
                    case 4: s5 = quoc - 1; s3 = quoc + 3; break;
                    case 5: s5 = quoc + 1; s3 = quoc; break;
                    case 6: s5 = quoc; s3 = quoc + 2; break;
                    case 7: s5 = quoc + 2; s3 = quoc - 1; break;

                }
            }
            else
            {
                if (euros == 3) { s5 = 0; s3 = 1; }
                else if (euros == 5) { s5 = 1; s3 = 0; }
                else if (euros == 6) { s5 = 0; s3 = 2; }
                else resposta = "Devolução da quantia ";
            }
            resposta = resposta + $"Selos de cinco {s5} selos de três {s3} ";
            return resposta;

        }

        #endregion

        #region Navegar
        public bool canNavega(Object parameter)
        {
            string destino = parameter.ToString();
            if (String.IsNullOrEmpty(destino)) return true;
            String pagcorrente = main.frame.Source.ToString();
            if (pagcorrente.Contains(destino)) return false;
            return true;
        }
        public void Navega(Object parameter)
        {
            String destino = parameter.ToString();
            switch (destino)
            {
                case "Inicio":
                    main.frame.Source = new Uri("/Views/Inicio.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                case "Selos":
                    main.frame.Source = new Uri("/Views/Selos.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                case "JogoDados":
                    main.frame.Source = new Uri("/Views/JogoDados.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                case "Stand":
                    main.frame.Source = new Uri("/Views/Stand.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                case "Balatro":
                    main.frame.Source = new Uri("/Views/Balatro.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                case "Regras":
                    main.frame.Source = new Uri("/Views/Regras.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                case "PokerHands":
                    main.frame.Source = new Uri("/Views/PokerHands.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 1120;
                    System.Windows.Application.Current.MainWindow.Height = 950;
                    break;
                case "AddPlayer":
                    main.frame.Source = new Uri("/Views/AddPlayer.xaml", UriKind.Relative);
                    System.Windows.Application.Current.MainWindow.Width = 800;
                    System.Windows.Application.Current.MainWindow.Height = 650;
                    break;
                default:
                    main.frame.Source = new Uri("/Views/Inicio.xaml", UriKind.Relative);
                    break;
            }
        }

        #endregion

        #region Sair
        public void Sair(Object parameter)
        {
            App.Current.Shutdown();
        }
        #endregion



    }
}
