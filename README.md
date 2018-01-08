# QuickNoteWidget
A Windows Desktop Widget, build with WPF. It's basically a little notepad, where you can store Texts for later use (Like a memo). 
You can store single line notes, mulit line notes (which you can also save to and load from a text file) and create lists.

![preview](https://github.com/al-develop/QuickNoteWidget/blob/master/quicknotewidget.png)

# How do I control that thing??
The whole controlling of the UI goes over context menus, there's no border for a widget.
For replacing the window, simply click somewhere on the application and drag it around.

# What is a Widget?
In my definition, a widget is a small tool which helps by handling small tasks. It's a full fledget dektop application, but compared
to 'big' players (like Word) it's just a tiny pilot fish - small but useful.
Windows introduced "gadgets" in Windows Vista, which were very useful. Widgets can be compared to those gadgets, since they both have the same goal "to be small and leightweight, but useful".

# For Developers - what is the structure of the source Code?
I used WPF with MVVM for that. The MVVM Library in use is DevExpress.Mvvm (available in NuGet)
For UI Theming I used MahApps.Metro (also available through NuGet)
The multi line editor used to be a "normal" WPF TextBox, with mahapps.metro enchantments. I replaced it with a ICSharpCode.AvalonEdit TextEdit. This was necessary, for showing line numbers and to have the possibility to click on a line number and select the whole line (like it's common in other text editors, like notepad++ or KDE kate as well as any possible IDE)
As you can see, the WindowStyle is set to "None". To handle all the controlling (like closing, minimizing etc), I used context menu (right click menu). 
There's also a TryIcon, which supports the same context menu commands as the Main UI.
For moving the window around, I overrid the "OnMouseDrag" event. By clicking anywhere outside of a control, the widget can be moved around the desktop.
If you plan to extend the context menu, notice that you got to extend the MainWindow context menu AND the TrayIcon context menu.
Although this is a MVVM architecture, I have some code behind for UI spcific stuff.
