# QuickNoteWidget
A Windows Desktop Widget, build with WPF. It's basically a little notepad, where you can store Texts for later use (Like a memo). 
You can store single-line notes, mulit-line notes (which you can also save to and load from a file) and create lists.   



# How do I control that thing??
The whole controlling of the UI goes over context menus, there's no border for a widget.
For replacing the window, simply click somewhere on the application and drag it around.

# What is a Widget?
In my definition, a widget is a small tool which helps by handling small tasks. It's a full fledget dektop application, but compared
to 'big' players (like Word) it's just a tiny pilot fish - small but useful.

# For Developers - what is the structure of the source Code?
I used WPF with MVVM for that. The MVVM Library in use is DevExpress.Mvvm (available in NuGet)
For UI Theming I used MahApps.Metro (also available through NuGet)
As you can see, the WindowStyle is set to "None". To handle all the controlling (like closing, minimizing etc)You can use the context Menus. 
There's also a TryIcon, which supports the same context menu commands as the Main UI.
For dragging the window around, I overrided the "OnMouseDrag" event (or something like that - can't remember the exact name of the event right now) for dragging the window around,
whenever you click somewhere and drag it around.
If you plan to extend the context menu, notice that you got to extend the MainWindow context menu AND the TrayIcon context menu.
I also used some code-behind for UI-specific stuff, so don't hang me for having code-behind in a MVVM system.
