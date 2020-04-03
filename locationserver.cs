using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locationserver
{
    /// <summary>
    /// Name: locationserver
    /// This is a server for the Client and Server Networking Assignment
    /// Author: 579440
    /// History: Created 04/02/2020
    /// Version: 1
    /// </summary>
    class locationserver
    {
        /// <summary>
        /// Hash table to store the users: Student Identifier (Key) with a Location (Value).
        /// </summary>
        static Hashtable userInfoHash;

        public static Logging Log;

        /// <summary>
        /// This is the main program/ running point. It is invoked with an array
        /// of strings that were received on the command line (via args[]). This
        /// will be useful for adding optional features and also the UI commands.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        static void Main(string[] args)
        {
            /// Initialising a 'database' to store student locations. '-f- optional feature
            /// has been added, so this 'database' can be saved to a specified text file and
            /// it can also be re-loaded (from the text file) when the server starts up again.
            userInfoHash = new Hashtable();

            string filename = null;
            int timeout = 1000;
            bool debug = false;
            string hashtableFilename = null;
            bool isUserInterfaceRequested = false;

            try
            {
                /// This iterates through the cmd args, and is used to determine what options
                /// have been requested by the user. It also checks if the UI wants to be shown.
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-w":
                            isUserInterfaceRequested = true;
                            break;
                        case "-l":
                            filename = args[++i];
                            break;

                        case "-t":
                            timeout = Convert.ToInt32(args[++i]);
                            break;

                        case "-d":
                            debug = true;
                            break;

                        case "-f":
                            hashtableFilename = args[++i];
                            break;

                        default:
                            Console.WriteLine("Unknown option: " + args[i]);
                            break;
                    }
                }

                /// Checks if a UI is wanted. If it is, then launch the UI form. Also, as specified
                /// in the ACW, if other intial arguments were received along side the '-w', then assign 
                /// these pre-set values to the textboxes in the form. They can also be altered in the form.
                if (isUserInterfaceRequested == true)
                {
                    // Could run 2 forms in here, by adding a 'Admin interface' form to show the logging file contents.
                    // Re-load the form each time the contents change remember (look at OOP panopto for help).
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    /// Sending the pre-set values to the form, so they can be displayed when it opens.
                    LocationServerForm Form = new LocationServerForm(filename, timeout, debug, hashtableFilename);
                    Application.Run(Form);

                    Console.WriteLine("Form/ UI input has been received now.");

                    /// Assigning the UI form input to the variables. Error prevention done in form code.
                    if (Form.m_logFilename.Trim() == "")
                    {
                        /// No logging was wanted in this case.
                        filename = null;
                    }
                    else
                    {
                        filename = Form.m_logFilename;
                    }
                    timeout = Form.m_timeout;
                    debug = Form.m_debug;

                    if (Form.m_databaseFilename.Trim() == "")
                    {
                        // No hashtable save and re-load wanted.
                        hashtableFilename = null;
                    }
                    else
                    {
                        hashtableFilename = Form.m_databaseFilename;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Arguments error: Invalid argument structure input.");
            }

            /// Setting up a Server Log (in "Common Log Format").
            Log = new Logging(filename);

            /// Properly starting the server, since all options have been set now.
            runServer(timeout, debug, hashtableFilename);
        }

        /// <summary>
        /// This method is called by the Main Program to create and run a server.
        /// It is in a separate method to facilitate multi-threading with Thread-safe Logging.
        /// </summary>
        /// <param name="timeout">Optional feature set by intial args. It's default is 1000ms.</param>
        /// <param name="debug">Optional feature set by intial args. Shows debugging info.</param>
        /// <param name="hashtableFilename">File location & name, to save & re-load database (if requested).</param>
        static void runServer(int timeout, bool debug, string hashtableFilename)
        {
            TcpListener listener;
            Socket connection;
            Handler RequestHandler;
            try
            {
                /// Creating a TCP socket to listen on port 43 for incoming client requests
                listener = new TcpListener(IPAddress.Any, 43);

                // Then start listening for requests
                listener.Start();
                Console.WriteLine("Server has started listening on port 43");

                /// Loop forever handling all incoming requests to the server (from client).
                while (true)
                {
                    /// When a request is received, create a socket and handle it. Invoke RequestHandler.DoRequest() 
                    /// to handle the stream details, and allow multi-threading via Thread class.
                    connection = listener.AcceptSocket();
                    RequestHandler = new Handler();
                    Thread t = new Thread(() => RequestHandler.DoRequest(connection, Log, timeout, debug, hashtableFilename));
                    t.Start();
                    //Thread.Sleep();
                }
            }
            catch (Exception e)
            {
                /// If there was an error in the server processing, catch it and note the error details.
                Console.WriteLine("ERROR: " + e.ToString());
            }
        }

        /// <summary>
        /// Thread handler class, allowing multi-threading due to different class instances possible.
        /// </summary>
        class Handler
        {
            /// <summary>
            /// This method is called after the server receives a connection on a listener.
            /// It processes the lines received as a request in the desired protocol and
            /// sends back appropriate replies.
            /// </summary>
            public void DoRequest(Socket connection, Logging Log, int timeout, bool debug, string hashtableFilename)
            {
                string Host = ((IPEndPoint)connection.RemoteEndPoint).Address.ToString();
                string firstInputLine = null;
                string status = "OK";
                NetworkStream socketStream;
                socketStream = new NetworkStream(connection);
                Console.WriteLine("Connection received via port 43");

                // For reading and writing hashtable data via a file location specified.
                var binformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                try
                {
                    string protocol = null;

                    // Used to tell the program whether the client request is for a look-up or update.
                    string clientRequest = null;
                    string userName = null;
                    string locationInput = null;

                    // Timeout for failed responses- can be changed via optional initial args.
                    socketStream.ReadTimeout = timeout;
                    socketStream.WriteTimeout = timeout;

                    /// Creating some streamReaders to handle the socket (43) I/O.
                    /// ACW mentions ASCII text structure, so this is better than using byte arrays.
                    StreamWriter sw = new StreamWriter(socketStream);
                    StreamReader sr = new StreamReader(socketStream);

                    /// Read the first line of the client request. Store this as may
                    /// be needed later for new switch statements.
                    firstInputLine = sr.ReadLine();

                    /// Re-load hashtable data from provided location, ONLY if this was requested in args.
                    if (hashtableFilename != null)
                    {
                        try
                        {
                            /// Code found at: https://stackoverflow.com/questions/32283868/how-to-write-a-hashtable-into-the-file
                            Hashtable userInfoHashDeserialized = null;
                            using (var fs = File.Open(hashtableFilename, FileMode.Open))
                            {
                                userInfoHashDeserialized = (Hashtable)binformatter.Deserialize(fs);
                                userInfoHash = userInfoHashDeserialized;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Hashtable data save to file error.");
                        }
                    }

                    /// Split sections, and assign a maximum split of 2 sections. Used mainly for whois
                    /// functionality and differentiating between possible protocol styles sent by the client.
                    String[] sections = firstInputLine.Split(new char[] { ' ' }, 2);

                    /// Only the Student Indentifier supplied if section.Length == 1. Therefore a Whois look-up was requested.
                    if (sections.Length == 1)
                    {
                        /// If there's a match in the Hashtable, send back look-up request data.
                        if (userInfoHash.ContainsKey(sections[0]))
                        {
                            /// Gets the associated value that matches the supplied student identifier and sends it back to client.
                            sw.WriteLine(userInfoHash[sections[0]]);
                            sw.Flush();
                        }
                        else
                        {
                            status = "UNKNOWN";
                            /// Give the appropriote error for a look-up miss, as specified in ACW.
                            sw.WriteLine("ERROR: no entries found");
                            sw.Flush();
                        }
                    }
                    /// If the sections.Length == 2, a Whois update query was requested by the 
                    /// client, OR, a HTTP look-up or update request was made by the client.
                    else if (sections.Length == 2)
                    {
                        /// If this is true, a HTTP look-up or update request was made.
                        if (sections[1].StartsWith("/"))
                        {
                            /// Split the original input again, but this time, no section limit is applied.
                            /// The server functionality for the whois protocol has already been handled.
                            string[] newSections = firstInputLine.Split(new char[] { ' ' });

                            /// Runs through the client request input and saves the specific protocol
                            /// used, as well as the request type (look-up or update). Using this later.
                            switch (newSections[0])
                            {
                                /// If GET, then we have a HTTP look-up. Now it finds the specific protocol used.
                                case "GET":
                                    clientRequest = "look-up";

                                    if (newSections.Length == 2)
                                    {
                                        protocol = "HTTP/0.9";
                                        userName = newSections[1].TrimStart('/');
                                    }
                                    else if (newSections[2] == "HTTP/1.0")
                                    {
                                        protocol = "HTTP/1.0";
                                        userName = newSections[1].TrimStart('/', '?');

                                        /// Reading in the optional header lines, and ignoring them.
                                        string optionalHeaderLines = sr.ReadLine();
                                    }
                                    else if (newSections[2] == "HTTP/1.1")
                                    {
                                        protocol = "HTTP/1.1";
                                        string[] nameSplit = newSections[1].Split('=');
                                        userName = nameSplit[1];
                                        /// Just reading the extra information sent by the HTTP/1.1 request and ignoring them.
                                        string hostNameLine = sr.ReadLine();
                                        string optionalHeaderLines = sr.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR: Incorrect GET request format received.");
                                        status = "EXCEPTION";
                                    }
                                    break;

                                /// If PUT, then the only matching protocol and request type is: HTTP/0.9 update request.
                                case "PUT":
                                    clientRequest = "update";
                                    protocol = "HTTP/0.9";
                                    userName = newSections[1].TrimStart('/');

                                    /// If there are more characters to read from the stream, read them.
                                    if (sr.Peek() >= 0)
                                    {
                                        /// Ignoring the \r\n from request format, on the 2nd input line.
                                        string secondInputLine = sr.ReadLine();

                                        /// Now, reading the final input line that contains the location.
                                        string finalInputLine = sr.ReadLine();
                                        locationInput = finalInputLine.Trim();
                                    }
                                    /// If there are no characters left to read, it's a whois request instead.
                                    else
                                    {
                                        protocol = "whois";
                                    }
                                    break;

                                /// If POST, the protocol could be either HTTP/1.0 or /1.1. The request type is an update though.
                                case "POST":
                                    clientRequest = "update";
                                    try
                                    {
                                        if (newSections[2] == "HTTP/1.0")
                                        {
                                            protocol = "HTTP/1.0";
                                            userName = newSections[1].TrimStart('/');

                                            /// Getting the location from the request format now. To get this, we have to get
                                            /// the content length so we know how many stream characters to read for 'location'.
                                            string[] contentLengthLineSplit = sr.ReadLine().Split(' ');
                                            int contentLength = Convert.ToInt32(contentLengthLineSplit[1].Trim());

                                            /// Ignoring the optional header lines.
                                            string optionalHeaderLines = sr.ReadLine();

                                            /// Using the content length value, we can read characters until the end of 'location'.
                                            try
                                            {
                                                int charsLeft = 0;
                                                while (charsLeft < contentLength)
                                                {
                                                    int c = sr.Read();
                                                    locationInput = locationInput + (char)c;
                                                    charsLeft++;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Reading HTTP/1.0 location error");
                                                status = "EXCEPTION";
                                            }
                                        }
                                        else if (newSections[2] == "HTTP/1.1")
                                        {
                                            protocol = "HTTP/1.1";
                                            /// Reading a line that we want to ignore.
                                            string hostNameLine = sr.ReadLine();

                                            /// Again, we have to read the content length so that we know how many chars to read
                                            /// so that we can pull out the userName and location from the request format. But,
                                            /// this time, the content length of this: 'name=<name>&location=<location>'.
                                            string[] contentLengthLineSplit = sr.ReadLine().Split(' ');
                                            int contentLength = Convert.ToInt32(contentLengthLineSplit[1].Trim());

                                            /// Now, reading the optional header lines and ignoring them...
                                            string optionalHeaderLines = sr.ReadLine();

                                            /// Using the content length value, we read all the chars left. These chars 
                                            /// contain the name and location information, as well as other unwanted strings.
                                            string nameAndLocationLineInput = "";
                                            try
                                            {
                                                int charsLeft = 0;
                                                while (charsLeft < contentLength)
                                                {
                                                    int c = sr.Read();
                                                    nameAndLocationLineInput = nameAndLocationLineInput + (char)c;
                                                    charsLeft++;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Reading HTTP/1.1 content error");
                                                status = "EXCEPTION";
                                            }
                                            /// Now, we need to split the line input up, so we can extract the name and location.
                                            string[] lineSplit = nameAndLocationLineInput.Split('&');
                                            string[] nameSplit = lineSplit[0].Split('=');
                                            userName = nameSplit[1];

                                            string[] locationSplit = lineSplit[1].Split('=');
                                            locationInput = locationSplit[1];
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR: Incorrect POST request format received.");
                                            status = "UNKNOWN";
                                        }
                                    }
                                    catch
                                    {
                                        protocol = "whois";
                                    }
                                    break;

                                default:
                                    Console.WriteLine("HTTP protocol error.");
                                    status = "EXCEPTION";
                                    break;
                            }

                            /// The debug optional feature. It allows you to see all the current information sent.
                            if (debug == true)
                            {
                                Console.WriteLine($"User Name = {userName}, The Location Input = {locationInput}, Protocol = {protocol}, " +
                                    $"Client Request type = {clientRequest}, Timeout set as: {timeout}, Host Name = {Host}");
                            }


                            /// We can do the HTTP request code functionality, by using the assigned variables I 
                            /// set in the switch statement (on the first section of the split clientInput).
                            /// We have variables 'protocol' and 'clientRequest' set, allowing the code to 
                            /// specifically deal with each input, now we know what protocol arrived and
                            /// we also know the request type sent e.g. update or look-up. We also have the
                            /// userName and location, as it was pulled out the request in the switch above.
                            if (protocol == "HTTP/0.9")
                            {
                                if (clientRequest == "look-up")
                                {
                                    if (userInfoHash.ContainsKey(userName))
                                    {
                                        string response = "HTTP/0.9 200 OK\r\nContent-Type: text/plain" +
                                            "\r\n\r\n" + userInfoHash[userName] + "\r\n";
                                        sw.Write(response);
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        string response = "HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n";
                                        status = "UNKNOWN";
                                        sw.Write(response);
                                        sw.Flush();
                                    }
                                }
                                else if (clientRequest == "update")
                                {
                                    if (userInfoHash.ContainsKey(userName))
                                    {
                                        userInfoHash[userName] = locationInput;

                                        sw.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Student identifier: " + userName + " location cannot be updated as no match was found.");
                                        Console.WriteLine("Creating new user data for student: " + userName);

                                        userInfoHash.Add(userName, locationInput);
                                        Console.WriteLine("New user data (Key Value pair) created succesfully for: " + userName +
                                            ", and supplied data was inputted: " + locationInput);

                                        sw.Write("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                        sw.Flush();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: HTTP/0.9 protocol error, unrecognised client request.");
                                    status = "UNKNOWN";
                                }
                            }
                            // Do the processing for a HTTP 1.0 request type.
                            else if (protocol == "HTTP/1.0")
                            {
                                if (clientRequest == "look-up")
                                {
                                    if (userInfoHash.ContainsKey(userName))
                                    {
                                        string response = "HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n"
                                            + userInfoHash[userName] + "\r\n";
                                        sw.Write(response);
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        string response = "HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n";
                                        status = "UNKNOWN";
                                        sw.Write(response);
                                        sw.Flush();
                                    }
                                }
                                else if (clientRequest == "update")
                                {
                                    if (userInfoHash.ContainsKey(userName))
                                    {
                                        userInfoHash[userName] = locationInput;

                                        sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Student identifier: " + userName + " location cannot be updated as no match was found.");
                                        Console.WriteLine("Creating new user data for student: " + userName);

                                        /// Add a new Key, Value pair for the supplied student identifier.
                                        /// Key = student identifier
                                        /// Value = location string given after the: student identifier and a space.
                                        userInfoHash.Add(userName, locationInput);
                                        Console.WriteLine("New user data (Key Value pair) created succesfully for: " + userName +
                                            ", and supplied data was inputted: " + locationInput);

                                        sw.Write("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                        sw.Flush();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: HTTP/1.0 protocol error, unrecognised client request.");
                                    status = "UNKNOWN";
                                }
                            }
                            // Do the processing for a HTTP 1.1 request type.
                            else if (protocol == "HTTP/1.1")
                            {
                                if (clientRequest == "look-up")
                                {
                                    if (userInfoHash.ContainsKey(userName))
                                    {
                                        string response = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n"
                                            + userInfoHash[userName] + "\r\n";
                                        sw.Write(response);
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        string response = "HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n";
                                        status = "UNKNOWN";
                                        sw.Write(response);
                                        sw.Flush();
                                    }
                                }

                                else if (clientRequest == "update")
                                {
                                    if (userInfoHash.ContainsKey(userName))
                                    {
                                        userInfoHash[userName] = locationInput;

                                        sw.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Student identifier: " + userName + " location cannot be updated as no match was found.");
                                        Console.WriteLine("Creating new user data for student: " + userName);

                                        /// Add a new Key, Value pair for the supplied student identifier.
                                        /// Key = student identifier
                                        /// Value = location string given after the: student identifier and a space.
                                        userInfoHash.Add(userName, locationInput);
                                        Console.WriteLine("New user data (Key Value pair) created succesfully for: " + userName +
                                            ", and supplied data was inputted: " + locationInput);

                                        sw.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                                        sw.Flush();
                                    }
                                }
                            }
                            else if (protocol == "whois")
                            {
                                if (clientRequest == "look-up")
                                {
                                    /// If there's a match in the Hashtable, send back look-up request data.
                                    if (userInfoHash.ContainsKey(sections[0]))
                                    {
                                        /// Gets the associated value that matches the supplied student identifier and sends it back to client.
                                        sw.WriteLine(userInfoHash[sections[0]]);
                                        sw.Flush();
                                    }
                                    else
                                    {
                                        status = "UNKNOWN";
                                        /// Give the appropriote error for a look-up miss, as specified in ACW.
                                        sw.WriteLine("ERROR: no entries found");
                                        sw.Flush();
                                    }
                                }
                                else if (clientRequest == "update")
                                {
                                    if (userInfoHash.ContainsKey(sections[0]))
                                    {
                                        /// From the Key match, we update the value. The value is the location string supplied by the client.
                                        /// Trimming any \r\n that are present at the end of a string/ last string.
                                        userInfoHash[sections[0]] = sections[1];

                                        /// Giving the server response that the ACW specifies. Shows an update completed.
                                        sw.WriteLine("OK");
                                        sw.Flush();
                                    }

                                    /// If the user didn't already exist, we create a new Key Value pair and input the supplied data,
                                    /// and send back the required server response, as specified in the ACW.
                                    else
                                    {
                                        Console.WriteLine("Student identifier: " + sections[0] + " location cannot be updated as no match was found.");
                                        Console.WriteLine("Creating new user data for student: " + sections[0]);

                                        /// Add a new Key, Value pair for the supplied student identifier.
                                        /// Key = student identifier
                                        /// Value = location string given after the: student identifier and a space.
                                        userInfoHash.Add(sections[0], sections[1]);
                                        Console.WriteLine("New user data (Key Value pair) created succesfully for: " + sections[0] +
                                            ", and supplied data was inputted: " + sections[1]);

                                        /// Giving the server response that the ACW specifies. Shows an update completed.
                                        sw.WriteLine("OK");
                                        sw.Flush();
                                    }
                                }
                            }
                        }

                        // This performs a Whois update request response and flushes TCP packet back to client.
                        else
                        {
                            /// If the user already exists in the Hashtable, we update the location (Value) and
                            /// send back the required server response, as specified in the ACW.
                            if (userInfoHash.ContainsKey(sections[0]))
                            {
                                /// From the Key match, we update the value. The value is the location string supplied by the client.
                                /// Trimming any \r\n that are present at the end of a string/ last string.
                                userInfoHash[sections[0]] = sections[1];

                                /// Giving the server response that the ACW specifies. Shows an update completed.
                                sw.WriteLine("OK");
                                sw.Flush();
                            }

                            /// If the user didn't already exist, we create a new Key Value pair and input the supplied data,
                            /// and send back the required server response, as specified in the ACW.
                            else
                            {
                                Console.WriteLine("Student identifier: " + sections[0] + " location cannot be updated as no match was found.");
                                Console.WriteLine("Creating new user data for student: " + sections[0]);

                                /// Add a new Key, Value pair for the supplied student identifier.
                                /// Key = student identifier
                                /// Value = location string given after the: student identifier and a space.
                                userInfoHash.Add(sections[0], sections[1]);
                                Console.WriteLine("New user data (Key Value pair) created succesfully for: " + sections[0] +
                                    ", and supplied data was inputted: " + sections[1]);

                                /// Giving the server response that the ACW specifies. Shows an update completed.
                                sw.WriteLine("OK");
                                sw.Flush();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: No client request made/ received.");
                        status = "EXCEPTION";
                    }
                }
                catch (Exception e)
                {
                    /// This is a catch all- it prevents the server crashing from unexpected errors.
                    Console.WriteLine("Unexpected issue occured.");
                    status = "EXCEPTION";
                }
                finally
                {
                    /// Now the request is complete, close the socket and connection as they're no longer needed.
                    socketStream.Close();
                    connection.Close();

                    /// If filename was passed to save hashtable data to file, then save the data to the file location.
                    if (hashtableFilename != null)
                    {
                        try
                        {
                            /// Helpful code found at: https://stackoverflow.com/questions/32283868/how-to-write-a-hashtable-into-the-file
                            /// Creating a file to store hashtable data.
                            using (var fs = File.Create(hashtableFilename))
                            {
                                binformatter.Serialize(fs, userInfoHash);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Hashtable/ data save to file error."); ;
                        }
                    }

                    /// Finally, Log what occured during this whole connection process.
                    Log.WriteToLog(Host, firstInputLine, status);
                }
            }
        }

        /// <summary>
        /// This is a logging example from https://stackoverflow.com/questions/2954900/simple-multithread-safe-log-class
        /// This is a Thread-safe method of writing to a Log file (Common Log Format).
        /// </summary>
        public class Logging
        {
            // Initially the Log file is null, meaning write to the console.
            public static string logFile = null;

            // This creates a Log file at specified name.
            public Logging(string fileName)
            {
                logFile = fileName;
            }

            private static readonly object locker = new object();

            /// <summary>
            /// This writes a Log entry to the console and optionally to a file, 
            /// trapping file write errors.
            /// </summary>
            /// <param name="hostName">name of host.</param>
            /// <param name="message">first input line sent from the client, holding useful info.</param>
            /// <param name="status">States if each attempt was OK or not.</param>
            public void WriteToLog(string hostName, string message, string status)
            {
                // Creates a line in the Common Log Format.
                string line = hostName + " - - " + DateTime.Now.ToString("'['dd'/'MM'/'yyyy':'HH':'mm':'ss zz00']'") + " \"" + message + " \"" + status;

                // Lock the file write to prevent concurrent threaded writes.
                lock (locker)
                {
                    Console.WriteLine(line);
                    if (logFile == null)
                    {
                        return;
                    }

                    // If no Log file exists after writing to console.
                    try
                    {
                        StreamWriter SW;
                        SW = File.AppendText(logFile);
                        SW.WriteLine(line);
                        SW.Close();
                    }
                    catch
                    {
                        // Catch any file write errors.
                        Console.WriteLine("Unable to write to the Log File: " + logFile);
                    }
                }
            }
        }
    }
}
