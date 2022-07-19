# QuickNoteWidget
A Windows Desktop Widget, build with WPF. It's basically a little notepad, where you can store Texts for later use (Like a memo). 
You can store single line notes, multi line notes (which you can also save to and load from a text file) and create lists.

![preview](https://gitlab.com/Al_develop/QuickNoteWidget/blob/master/QuickNote%201.png)

# How to use
The whole controlling of the UI goes over context menus. 
You can drag around the Window with the gray border.
Use ctrl + mouse wheel (or ctrl + +/-) to zoom in and out of the Text area. 

![preview](https://gitlab.com/Al_develop/QuickNoteWidget/blob/master/QuickNote%202.png)

![preview](https://gitlab.com/Al_develop/QuickNoteWidget/blob/master/QuickNote%203.png)


# For Contributors - what is the structure of the source Code?
I used WPF with MVVM. The used MVVM Library is DevExpress.Mvvm (available in NuGet)
The Metro Design of the Context Menus comes from MahApps.Metro (also available through NuGet)
The Text Editor is from ICSharpCode.AvalonEdit (NuGet as well). By using AvalonEdit, we have build in options for LineNumbers and general "improvements".
The WindowStyle is set to "None". To handle all the controlling (like closing, minimizing etc), I used the context menu (right click menu). 
There is also a TryIcon, which supports the same context menu commands as the Main UI.
For moving the window around, I made the border a little bit wider with a light gray background. This is the Area which a user can click on, to drag the window around.
If you plan to extend the context menu, notice that you got to extend the MainWindow context menu AND the TrayIcon context menu.
Although this is a MVVM architecture, I have some code behind for UI specific stuff.
Keep in mind to keep the UI related stuff separated from the ViewModel.
