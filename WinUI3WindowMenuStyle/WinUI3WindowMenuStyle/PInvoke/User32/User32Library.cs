﻿using System;
using System.Runtime.InteropServices;

namespace GetStoreApp.WindowsAPI.PInvoke.User32
{
    /// <summary>
    /// User32.dll 函数库
    /// </summary>
    public static partial class User32Library
    {
        private const string User32 = "user32.dll";

        /// <summary>
        /// 检索一个窗口的句柄，该窗口的类名和窗口名称与指定的字符串匹配。 该函数搜索子窗口，从指定子窗口后面的子窗口开始。 此函数不执行区分大小写的搜索。
        /// </summary>
        /// <param name="hWndParent">要搜索其子窗口的父窗口的句柄。如果 hwndParent 为 NULL，则该函数使用桌面窗口作为父窗口。 函数在桌面的子窗口之间搜索。 如果 hwndParent 为HWND_MESSAGE，则函数将搜索所有 仅消息窗口。</param>
        /// <param name="hWndChildAfter">子窗口的句柄。 搜索从 Z 顺序中的下一个子窗口开始。 子窗口必须是 hwndParent 的直接子窗口，而不仅仅是子窗口。 如果 hwndChildAfter 为 NULL，则搜索从 hwndParent 的第一个子窗口开始。请注意，如果 hwndParent 和 hwndChildAfter 均为 NULL，则该函数将搜索所有顶级窗口和仅消息窗口。</param>
        /// <param name="lpszClass">类名或上一次对 RegisterClass 或 RegisterClassEx 函数的调用创建的类名或类原子。 原子必须置于 lpszClass 的低序单词中;高阶单词必须为零。如果 lpszClass 是字符串，则指定窗口类名。 类名可以是注册到 RegisterClass 或 RegisterClassEx 的任何名称，也可以是预定义的控件类名称，也可以是 MAKEINTATOM(0x8000)。 在此后一种情况下，0x8000是菜单类的原子。 </param>
        /// <param name="lpszWindow">窗口名称 (窗口的标题) 。 如果此参数为 NULL，则所有窗口名称都匹配。</param>
        /// <returns>如果函数成功，则返回值是具有指定类和窗口名称的窗口的句柄。如果函数失败，则返回值为 NULL。 要获得更多的错误信息，请调用 GetLastError。</returns>
        [LibraryImport(User32, EntryPoint = "FindWindowExW", SetLastError = false, StringMarshalling = StringMarshalling.Utf16)]
        public static partial IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 将指定的消息发送到窗口或窗口。 SendMessage 函数调用指定窗口的窗口过程，在窗口过程处理消息之前不会返回。
        /// </summary>
        /// <param name="hWnd">
        /// 窗口过程的句柄将接收消息。 如果此参数 HWND_BROADCAST ( (HWND) 0xffff) ，则会将消息发送到系统中的所有顶级窗口，
        /// 包括已禁用或不可见的未所有者窗口、重叠窗口和弹出窗口;但消息不会发送到子窗口。消息发送受 UIPI 的约束。
        /// 进程的线程只能将消息发送到较低或等于完整性级别的线程的消息队列。
        /// </param>
        /// <param name="wMsg">要发送的消息。</param>
        /// <param name="wParam">其他的消息特定信息。</param>
        /// <param name="lParam">其他的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果;这取决于发送的消息。</returns>
        [LibraryImport(User32, EntryPoint = "SendMessageW", SetLastError = false)]
        public static partial IntPtr SendMessage(IntPtr hWnd, WindowMessage wMsg, int wParam, IntPtr lParam);
    }
}