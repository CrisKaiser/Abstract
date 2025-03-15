using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AbstractApp
{
    public class ControlBar : Border
    {
        public event RoutedEventHandler LargerClicked;
        public event RoutedEventHandler SmallerClicked;
        public event RoutedEventHandler CheckClicked;
        public event RoutedEventHandler TranslateClicked;
        public event RoutedEventHandler DeleteClicked;

        private readonly int _controlBarHeight;
        private const int EditModeWidth = 80;
        private const int OtherModeWidth = 24;

        public enum ControlMode
        {
            EditMode,
            TranslateMode,
            DeleteMode
        }
        private ControlMode _currentControlMode = ControlMode.EditMode;

        public ControlBar(int controlBarHeight)
        {
            _controlBarHeight = controlBarHeight;
            InitializeControlBarBaseUI(); 
        }

        public void SetCurrentControlMode(ControlMode mode)
        {
            _currentControlMode = mode;
            UpdateControlBarUI();
        }

        private void InitializeControlBarBaseUI()
        {
            Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            CornerRadius = new CornerRadius(3);
            Padding = new Thickness(3);
            Width = EditModeWidth;
            Height = _controlBarHeight;
            UpdateControlBarUI();
        }

        private void UpdateControlBarUI()
        {
            switch (_currentControlMode)
            {
                case ControlMode.EditMode:
                    Child = SetUpEditModeButtons();
                    break;
                case ControlMode.TranslateMode:
                    Child = SetUpTranslateModeButton();
                    break;
                case ControlMode.DeleteMode:
                    Child = SetUpDeleteModeButton();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateControlBarWidth(int width)
        {
            Width = width;
        }

        private UIElement SetUpEditModeButtons()
        {
            var grid = ControlBarUIFactory.CreateEditModeGrid();

            var btnLarger = ControlBarUIFactory.CreateButtonLarger();
            var btnSmaller = ControlBarUIFactory.CreateButtonSmaller();
            var btnCheck = ControlBarUIFactory.CreateButtonCheck();

            btnLarger.Click += (s, e) => LargerClicked?.Invoke(this, e);
            btnSmaller.Click += (s, e) => SmallerClicked?.Invoke(this, e);
            btnCheck.Click += (s, e) => CheckClicked?.Invoke(this, e);

            Grid.SetColumn(btnSmaller, 0);
            Grid.SetColumn(btnLarger, 1);
            Grid.SetColumn(btnCheck, 2);

            grid.Children.Add(btnSmaller);
            grid.Children.Add(btnLarger);
            grid.Children.Add(btnCheck);

            UpdateControlBarWidth(EditModeWidth);

            return grid;
        }

        private UIElement SetUpTranslateModeButton()
        {
            var button = ControlBarUIFactory.CreateButtonTranslate();
            button.PreviewMouseLeftButtonDown += (s, e) =>
            {
                TranslateClicked?.Invoke(this, e);
                e.Handled = true;
            };

            UpdateControlBarWidth(OtherModeWidth);

            return button;
        }

        private UIElement SetUpDeleteModeButton()
        {
            var button = ControlBarUIFactory.CreateButtonDelete();
            button.Click += (s, e) => DeleteClicked?.Invoke(this, e);

            UpdateControlBarWidth(OtherModeWidth);

            return button;
        }

    }
}