using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maio10.Controllers
{
    public class ClsJogoDados : UIElement
    {
        public int Montante
        {
            get { return (int)GetValue(MontanteProperty); }
            set { SetValue(MontanteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Montante.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MontanteProperty =
            DependencyProperty.Register("Montante", typeof(int), typeof(ClsJogoDados), new PropertyMetadata(1000));



        public int Dado1
        {
            get { return (int)GetValue(Dado1Property); }
            set { SetValue(Dado1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Dado1Property =
            DependencyProperty.Register("Dado1", typeof(int), typeof(ClsJogoDados), new PropertyMetadata(3));

        public int Dado2
        {
            get { return (int)GetValue(Dado2Property); }
            set { SetValue(Dado2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Dado2Property =
            DependencyProperty.Register("Dado2", typeof(int), typeof(ClsJogoDados), new PropertyMetadata(5));

        public void rolar(int aposta)
        {
            if (Montante >= aposta) Montante -= aposta;
            else if (Montante > 0) { aposta = Montante; Montante = 0; }
            else aposta = 0;
            if (aposta > 0)
            {
                Random r = new Random();
                Dado1 = r.Next(1, 7);
                Dado2 = r.Next(1, 7);
                if (Dado1 == Dado2) RaiseOnPremio(aposta);
            }
        }
        private static readonly RoutedEvent OnPremioEvent = EventManager.RegisterRoutedEvent("OnPremio", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ClsJogoDados));

        public event RoutedEventHandler OnPremio
        {
            add { AddHandler(OnPremioEvent,value); }
            remove { RemoveHandler(OnPremioEvent,value); }
        }

        public class OnPremioRoutedEventArgs : RoutedEventArgs
        {
            public int Aposta;

            public OnPremioRoutedEventArgs(int aposta):base(OnPremioEvent)
            {
                Aposta = aposta;
            }
        }

        public void RaiseOnPremio(int aposta)
        {
            OnPremioRoutedEventArgs args = new OnPremioRoutedEventArgs(aposta);
            RaiseEvent(args);
        }


    }
}
