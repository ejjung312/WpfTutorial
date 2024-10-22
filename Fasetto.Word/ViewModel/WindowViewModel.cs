using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Fasetto.Word
{
    public class WindowViewModel : BaseViewModel
    {
        private Window mWindow;

        private int mOuterMarginSize = 10;
        private int mWindowRadius = 10;
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get; set; } = 800;
        public double WindowMinimumHeight { get; set; } = 500;

        public bool Borderless { get {
                return (mWindow.WindowState == WindowState.Maximized
                    || mDockPosition != WindowDockPosition.Undocked);
            } }

        public int ResizeBorder { get { return Borderless ? 0 : 6; } }

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public int OuterMarginSize 
        {
            get
            {
                return Borderless ? 0 : mOuterMarginSize;
            } 
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } } 

        public int WindowRadius
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder ); } }

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Chat;

        public RelayCommand MinimizeCommand { get; set; }

        public RelayCommand MaximizeCommand { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand MenuCommand { get; set; }

        public WindowViewModel(Window window)
        {
            mWindow = window;

            mWindow.StateChanged += (sender, e) =>
            {
                Debug.Write("changed");
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() =>
            {
                mWindow.WindowState = WindowState.Minimized;
            });

            MaximizeCommand = new RelayCommand(() =>
            {
                mWindow.WindowState ^= WindowState.Maximized;
            });

            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);
        }

        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }
    }
}
