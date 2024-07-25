# WinUI3WindowMenuStyle

----------

### 基于 WinUI 风格的窗口菜单实现

### Based on WinUI style window menu implementation

----------

#### 目前在 WinUI 3 框架中，我们可以通过 ExtendsContentIntoTitlebar 属性来自定义标题栏，然而在自定义标题栏后，点击窗口右键，窗口菜单还是 Win32 样式的。所以在本项目中，我们提供了一种基于 WinUI 风格的窗口菜单实现。

#### At present in the frame of the WinUI 3, we can through ExtendsContentIntoTitlebar properties to customize the title bar, however after a custom title bar, right click on the window, the window menu or Win32 style. So in this project, we provide a WinUI-style window menu implementation.

----------

#### 其他说明

##### 1.需要设置 ExtendsContentIntoTitlebar = true 才能正常启用 WinUI 风格的窗口菜单

##### 2.显示窗口菜单时，不仅需要处理鼠标或触摸屏/笔发出的操作，还需要处理键盘 Alt + Space 的按键操作

----------

#### Other instructions

##### 1. You need to set up ExtendsContentIntoTitlebar = true to enable properly WinUI style window menu

##### 2. When displaying the window menu, you need to process not only the operation of the mouse or touch screen/pen, but also the operation of the Alt + Space key on the keyboard

----------

#### 参考资料（Reference）

> * [使用菜单（Using menus）](https://learn.microsoft.com/windows/win32/menurc/using-menus)&emsp;
> * [菜单和上下文菜单（Menus and context menus）](https://learn.microsoft.com/windows/apps/design/controls/menus-and-context-menus)&emsp;
> * [关于菜单（About menus）](https://learn.microsoft.com/windows/win32/menurc/about-menus)&emsp;
> * [标题栏自定义（Titlebar customization）](https://learn.microsoft.com/zh-cn/windows/apps/develop/title-bar)&emsp;

----------

#### 示例图（Demo）

##### 浅色模式（Light Mode）

![image](https://github.com/user-attachments/assets/40fb7ce3-c0a1-44f6-b433-a0e12c48bead)

##### 深色模式（Dark Mode）

![image](https://github.com/user-attachments/assets/9e1b49d5-bb13-4506-9276-cb7704e4cf10)
