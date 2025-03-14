using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AbstractApp
{
    public class Eintrag : Grid
    {
        private int heightKontrolle = 24; 
        public SmartTextBox TextBox { get; private set; }
        public Kontrollleiste Kontrolle { get; private set; }

        private ProjektDetails pDetail; 


        public double X
        {
            get => Margin.Left;
            set => Margin = new Thickness(value, Y, 0, 0);
        }

        public double Y
        {
            get => Margin.Top;
            set => Margin = new Thickness(X, value, 0, 0);
        }

        public Eintrag(Point position, ProjektDetails pDetail)
        {
            InitializeLayout(position);
            SetupEvents();
            this.pDetail = pDetail;
            pDetail.eintragRegister(this);
        }

        private void InitializeLayout(Point position)
        {   
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); 

            TextBox = new SmartTextBox(new Point(0, 0))
            {
                Margin = new Thickness(0) 
            };

            Kontrolle = new Kontrollleiste(heightKontrolle)
            {
                HorizontalAlignment = HorizontalAlignment.Left, 
                Margin = new Thickness(0, 0, 0, 5) 
            };

            Grid.SetRow(TextBox, 1); 
            Grid.SetRow(Kontrolle, 0); 

            Children.Add(TextBox);
            Children.Add(Kontrolle);
        }

        private void SetupEvents()
        {
            Kontrolle.LargerClicked += (s, e) => largerClickHandler();
            Kontrolle.SmallerClicked += (s, e) => smallerClickHandler();
            Kontrolle.CheckClicked += (s, e) => checkedHandler();
        }

        private void largerClickHandler()
        {
            TextBox.increaseFontSize();
            TextBox.fontSizeChangeReceiver();
        }

        private void smallerClickHandler()
        {
            TextBox.decreaseFontSize();
            TextBox.fontSizeChangeReceiver();
        }

        private void checkedHandler()
        {
            TextBox.RemoveFocus();
            HideKontrolleiste();
        }

        private void HideKontrolleiste()
        {
            Kontrolle.Visibility = Visibility.Collapsed;
            TextBox.IsReadOnly = true;

        }

        public void ShowKontrolleiste()
        {
            Kontrolle.Visibility = Visibility.Visible;
            TextBox.IsReadOnly = false;
        }

        public void notifyOnStateUpdate()
        {
            if (pDetail.currentLayerMode == ProjektDetails.LayerMode.DeleteMode)
            {
                Kontrolle.setCurrentMode(Kontrollleiste.ControlMode.DeleteMode);
                Kontrolle.Visibility = Visibility.Visible;
            }
            else if (pDetail.currentLayerMode == ProjektDetails.LayerMode.TranslateMode)
            {
                Kontrolle.setCurrentMode(Kontrollleiste.ControlMode.TranslateMode);
                Kontrolle.Visibility = Visibility.Visible;
            }
            else if (pDetail.currentLayerMode == ProjektDetails.LayerMode.EditMode)
            {
                Kontrolle.setCurrentMode(Kontrollleiste.ControlMode.EditMode);
                Kontrolle.Visibility = Visibility.Visible;
            }
            else
            {
                Kontrolle.Visibility = Visibility.Collapsed;
            }
        }

        public void Delete()
        {
            Children.Remove(TextBox);
            Children.Remove(Kontrolle);
            if (pDetail != null)
            {
                pDetail.eintraege.Remove(this);
            }
            TextBox = null;
            Kontrolle = null;
        }

    }
}