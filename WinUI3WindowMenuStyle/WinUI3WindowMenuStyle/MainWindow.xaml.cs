using GetStoreApp.Helpers.Root;
using GetStoreApp.WindowsAPI.PInvoke.Comctl32;
using GetStoreApp.WindowsAPI.PInvoke.User32;
using Microsoft.UI.Content;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3WindowMenuStyle
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly SUBCLASSPROC mainWindowSubClassProc;
        private readonly SUBCLASSPROC inputNonClientPointerSourceSubClassProc;
        private readonly ContentCoordinateConverter contentCoordinateConverter;
        private readonly OverlappedPresenter overlappedPresenter;

        private bool _isWindowMaximized;

        public bool IsWindowMaximized
        {
            get { return _isWindowMaximized; }

            set
            {
                if (!Equals(_isWindowMaximized, value))
                {
                    _isWindowMaximized = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWindowMaximized)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            overlappedPresenter = AppWindow.Presenter as OverlappedPresenter;
            ExtendsContentIntoTitleBar = true;
            SystemBackdrop = new MicaBackdrop();
            IsWindowMaximized = overlappedPresenter.State is OverlappedPresenterState.Maximized;
            contentCoordinateConverter = ContentCoordinateConverter.CreateForWindowId(AppWindow.Id);

            // ΪӦ����������Ӵ��ڹ���
            mainWindowSubClassProc = new SUBCLASSPROC(MainWindowSubClassProc);
            Comctl32Library.SetWindowSubclass((IntPtr)AppWindow.Id.Value, Marshal.GetFunctionPointerForDelegate(mainWindowSubClassProc), 0, IntPtr.Zero);

            // Ϊ�ǹ������Ĵ���������Ӧ�Ĵ�����ʽ����Ӵ��ڹ���
            IntPtr inputNonClientPointerSourceHandle = User32Library.FindWindowEx((IntPtr)AppWindow.Id.Value, IntPtr.Zero, "InputNonClientPointerSource", null);

            if (inputNonClientPointerSourceHandle != IntPtr.Zero)
            {
                inputNonClientPointerSourceSubClassProc = new SUBCLASSPROC(InputNonClientPointerSourceSubClassProc);
                Comctl32Library.SetWindowSubclass((IntPtr)AppWindow.Id.Value, Marshal.GetFunctionPointerForDelegate(inputNonClientPointerSourceSubClassProc), 0, IntPtr.Zero);
            }
        }

        private void myButton_Click(object sender, RoutedEventArgs args)
        {
            myButton.Content = "Clicked";
        }

        /// <summary>
        /// ���ڻ�ԭ
        /// </summary>
        private void OnRestoreClicked(object sender, RoutedEventArgs args)
        {
            overlappedPresenter.Restore();
        }

        /// <summary>
        /// �����ƶ�
        /// </summary>
        private void OnMoveClicked(object sender, RoutedEventArgs args)
        {
            MenuFlyoutItem menuItem = sender as MenuFlyoutItem;
            if (menuItem.Tag is not null)
            {
                ((MenuFlyout)menuItem.Tag).Hide();
                User32Library.SendMessage((IntPtr)AppWindow.Id.Value, WindowMessage.WM_SYSCOMMAND, 0xF010, 0);
            }
        }

        /// <summary>
        /// ���ڴ�С
        /// </summary>
        private void OnSizeClicked(object sender, RoutedEventArgs args)
        {
            MenuFlyoutItem menuItem = sender as MenuFlyoutItem;
            if (menuItem.Tag is not null)
            {
                ((MenuFlyout)menuItem.Tag).Hide();
                User32Library.SendMessage((IntPtr)AppWindow.Id.Value, WindowMessage.WM_SYSCOMMAND, 0xF000, 0);
            }
        }

        /// <summary>
        /// ������С��
        /// </summary>
        private void OnMinimizeClicked(object sender, RoutedEventArgs args)
        {
            overlappedPresenter.Minimize();
        }

        /// <summary>
        /// �������
        /// </summary>
        private void OnMaximizeClicked(object sender, RoutedEventArgs args)
        {
            overlappedPresenter.Maximize();
        }

        /// <summary>
        /// ���ڹر�
        /// </summary>
        private void OnCloseClicked(object sender, RoutedEventArgs args)
        {
            Application.Current.Exit();
        }

        /// <summary>
        /// Ӧ����������Ϣ����
        /// </summary>
        private IntPtr MainWindowSubClassProc(IntPtr hWnd, WindowMessage Msg, UIntPtr wParam, IntPtr lParam, uint uIdSubclass, IntPtr dwRefData)
        {
            if (Msg is WindowMessage.WM_SYSCOMMAND)
            {
                // ѡ�񴰿��Ҽ��˵�����Ŀʱ���յ�����Ϣ

                SYSTEMCOMMAND sysCommand = (SYSTEMCOMMAND)(wParam.ToUInt32() & 0xFFF0);

                if (sysCommand is SYSTEMCOMMAND.SC_MOUSEMENU)
                {
                    FlyoutShowOptions options = new()
                    {
                        Position = new Point(0, 15),
                        ShowMode = FlyoutShowMode.Standard
                    };
                    TitlebarMenuFlyout.ShowAt(null, options);
                    return 0;
                }
                else if (sysCommand is SYSTEMCOMMAND.SC_KEYMENU)
                {
                    FlyoutShowOptions options = new()
                    {
                        Position = new Point(0, 45),
                        ShowMode = FlyoutShowMode.Standard
                    };
                    TitlebarMenuFlyout.ShowAt(null, options);
                    return 0;
                }
            }

            return Comctl32Library.DefSubclassProc(hWnd, Msg, wParam, lParam);
        }

        /// <summary>
        /// Ӧ����ק���򴰿���Ϣ����
        /// </summary>
        private IntPtr InputNonClientPointerSourceSubClassProc(IntPtr hWnd, WindowMessage Msg, UIntPtr wParam, IntPtr lParam, uint uIdSubclass, IntPtr dwRefData)
        {
            switch (Msg)
            {
                // ���û�����������ʱ�����λ�ڴ��ڵķǹ������ڵ���Ϣ
                case WindowMessage.WM_NCLBUTTONDOWN:
                    {
                        if (TitlebarMenuFlyout.IsOpen)
                        {
                            TitlebarMenuFlyout.Hide();
                        }
                        break;
                    }
                // ���û���������Ҽ����ͷ�ʱ�����λ�ڴ��ڵķǹ������ڵ���Ϣ
                case WindowMessage.WM_NCRBUTTONUP:
                    {
                        if (Content is not null && Content.XamlRoot is not null)
                        {
                            PointInt32 screenPoint = new(lParam.ToInt32() & 0xFFFF, lParam.ToInt32() >> 16);
                            Point localPoint = contentCoordinateConverter.ConvertScreenToLocal(screenPoint);

                            FlyoutShowOptions options = new()
                            {
                                ShowMode = FlyoutShowMode.Standard,
                                Position = InfoHelper.SystemVersion.Build >= 22000 ? new Point(localPoint.X / Content.XamlRoot.RasterizationScale, localPoint.Y / Content.XamlRoot.RasterizationScale) : new Point(localPoint.X, localPoint.Y)
                            };

                            TitlebarMenuFlyout.ShowAt(null, options);
                        }
                        return 0;
                    }
            }
            return Comctl32Library.DefSubclassProc(hWnd, Msg, wParam, lParam);
        }
    }
}