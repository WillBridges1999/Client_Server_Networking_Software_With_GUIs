# HTTP-Client-Server-Networking-Project-With-GUIs

An HTTP and WHOIS request type, client-server Networking project written in C#.Net; WinForms GUIs are present for both the client and server side (must be run on WindowsOS for WinForms to show).
The 'location.exe' is the client program, and the 'locationserver.exe' is the server program. There is a 2nd Readme.txt file in the project which offers more information too.

To run the project properly, run the 'locationserver.exe' first, via a terminal window, and pass the argument '-w' like this 'C:\*AddFilepathToDebugFileHere*\> locationserver.exe -w'. By passing this '-w' argument in, it opens a WinForms UI where you can alter the functionality of the server. Set your serverName to 'localhost' (in the GUI) to run on your own network (for it to work), then click the submit button. 

Once you've done that, the server should be listening for an incoming connection from a client on the port you suggested (port 80 is the default), and will show progress/ state feedback in the console window. 
Conversely, you could run the 'locationserver.exe' without the UI, and just pass arguments in via the command line terminal. The way to do this is detailed in the specification I've added to the project (from University).

Once the server is running and listening on a port number, you can then run the 'location.exe' client. The project supports multi-threaded behaviour/ concurrency and can therefore run multiple clients talking to the same server. You can do this by running multiple 'location.exe.' clients at the same time. 

This time, to run the 'location.exe' with a UI, don't pass any arguments into the command line. Just run it like this 'C:\*AddFilepathToDebugFileHere*\> location.exe'. This will open a client GUI, where you can alter the client input to send to the server and also decide which HTTP request type you want. The 'Whois' request type is the default if no UI data is found. Again, set the Server Name as 'localhost' and the Port as the same you input in the server UI (port 80 is suggested). Then press submit and inspect the console window loging to see what oocured.

More advanced features are present for the 'locationserver.exe'. To save the loging information (which is in the common loging format) to a text file, you can input your file path into the UI and when the project is run now, my code will create a new text file at that file path and save all the loging information that occurs during the client-server interaction. The loging information can also be seen via the console window. 

Also, my solution utilises a Hashtable/ dictionary to store usernames with their respective location (Key/Value data structure). If you want to save changes to a text file so you can re-load the previous sessions Hashtable/ database data, then you can specify a file path to store this text file (as explained above).

If you need any more help/ guidance please refer to the specification. If you still need help, please email me.
