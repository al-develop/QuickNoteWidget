# QuickNoteWidget
A Windows Desktop Application, build with WPF. It's basically a little notepad, where you can store texts for later use (Like a memo). 

![preview](https://github.com/al-develop/QuickNoteWidget/blob/master/QuickNote%201.png)

# How to use
The whole controlling of the UI goes over context menus. 
You can drag the window around the screen by dragging the gray border around the text.
Use ctrl + mouse wheel (or ctrl + +/-) to change the font size

Over the context menu, you get several options to control the behaviour of the application.
This includes: 
- Control whether the app should stay above all other windows.

- Control whether the app should occupy space in the taskbar (note: if you decide to uncheck this option, you can always bring the application back ontop though a right-click on the notification/Tryicon).

- Control about the appearance of the app: Dark/Light Themes, several accent colors and the Font can be changed.

- Control whether to display additional information under the text, such as the Word count and also a slider to control the transparency of the application.

- Reset all settings back to the default values.


![preview](https://github.com/al-develop/QuickNoteWidget/blob/master/QuickNote%202.png)

![preview](https://github.com/al-develop/QuickNoteWidget/blob/master/QuickNote%203.png)


# For Contributors - what is the structure of the source Code?
I used WPF with MVVM. The used MVVM Library is DevExpress.Mvvm (available in NuGet)
The Metro Design (including themes and accents) are from MahApps.Metro (also available through NuGet)
The Text Editor is from ICSharpCode.AvalonEdit (NuGet as well). With using AvalonEdit comes several improvements over the WPF build-in Textbox or Richtextbox, such as Line Numbers or a Bindable Rich-Text.
The WindowStyle is set to "None". To handle all the controlling (like closing, minimizing etc), I used the context menu (right click menu). 
There is also a TryIcon, which supports the same context menu commands as the Main UI.
For moving the window around, the border got to be a little bit wider with a light gray background. This is the area which a user can click on, to drag the window around.
The context menu is located within the Window.Resources of the MainWindow.
Although this is a MVVM architecture, there is also some code behind for the UI specific stuff (such as dragging the window around the screen).
Keep in mind to keep the UI related stuff separated from the ViewModel.
