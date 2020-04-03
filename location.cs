using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace location
{
    public class Whois
    {
        static void Main(string[] args)
        {
            try
            {
                /// Setting the default serverName, Port, and protocol 
                /// (if not specified by args supplied) via ACW specification.
                string protocol = "whois";
                string serverName = "whois.net.dcs.hull.ac.uk";
                int port = 43;

                /// Assign starting user info values to null. 
                string userName = null;
                string locationInput = null;

                // For client optional features.
                bool debug = false;
                int timeout = 1000;

                /// Launches a WinForms UI if no client args provided (as specified in ACW).
                /// Saves UI input data to variables in the main method.
                if (args.Length == 0)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    LocationForm Form = new LocationForm();
                    Application.Run(Form);

                    /// When the form submit button is clicked, the application exits, and then reaches here.
                    Console.WriteLine("Form/ UI input has been received now.");

                    /// Assigning the UI form input to the variables.
                    if (Form.m_protocol == "HTTP/0.9")
                    {
                        protocol = "-h9";
                    }
                    else if (Form.m_protocol == "HTTP/1.0")
                    {
                        protocol = "-h0";
                    }
                    else if (Form.m_protocol == "HTTP/1.1")
                    {
                        protocol = "-h1";
                    }
                    else
                    {
                        protocol = "whois";
                    }

                    if (Form.m_serverName != "" || Form.m_serverName.Trim() != "")
                    {
                        serverName = Form.m_serverName;
                    }

                    port = Form.m_port; // Error checking done in form.
                    userName = Form.m_userName;
                    locationInput = Form.m_location;

                    if (Form.m_debug == true)
                    {
                        debug = true;
                    }

                    timeout = Form.m_timeout; // Error checking done in form.
                }
                else
                {
                    int i = 0;
                    /// Runs through the entire input args string, and extracts out the info needed,
                    /// before connecting to the server. This includes the protocol specified, the 
                    /// serverName specified, and also the port number specified.
                    while (i < args.Length)
                    {
                        // NOTE: more cases need adding for the optional extras in the ACW.
                        switch (args[i])
                        {
                            case "-h":
                                //Increment to next value, before assignment, to get the specified serverName.
                                serverName = args[++i];
                                break;

                            case "-p":
                                /// Increment to next value, before assignment, to get the specified port number.
                                /// A conversion is needed from string input to integer port number.
                                port = Convert.ToInt32(args[++i]);
                                break;

                            case "-d":
                                debug = true;
                                break;

                            case "-t":
                                timeout = Convert.ToInt32(args[++i]);
                                break;

                            case "-h9":
                                protocol = args[i];
                                break;

                            case "-h0":
                                protocol = args[i];
                                break;

                            case "-h1":
                                protocol = args[i];
                                break;

                            default:
                                if (userName == null)
                                {
                                    userName = args[i];
                                }
                                else if (locationInput == null)
                                {
                                    locationInput = args[i];
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Invalid argument structure (input).");
                                }
                                break;
                        }
                        i++;
                    }
                }

                /// Optional feature: Debug. It shows what parameters have been passed in, if you include '-d' in the args.
                if (debug == true)
                {
                    Console.WriteLine($"User Name = {userName}, The Location Input = {locationInput}, Protocol = {protocol}, Server Name = {serverName}, Port Number = {port}");
                }

                /// Setting up the connection now that we have read the input args for a possible specified
                /// serverName and Port input. If no input, client connects to default server and port.
                TcpClient client = new TcpClient();

                /// Connect using the supplied info from args. If not supplied, use default values (specified in ACW).
                client.Connect(serverName, port);

                /// Default timeout is 1 second. If no response from server after 1 second, exit out.
                /// Timeout can be set by the args, as an optional feature added (as specified in ACW).
                client.ReceiveTimeout = timeout;
                client.SendTimeout = timeout;

                /// Initialising the streamWriters and streamReader for communication to server,
                /// and receiving request results back from the server.
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());

                /// if, else if, conditions to test which protocol was requested by the input args.
                /// For each protocol, a different request structure needs to be output to the server.
                if (protocol == "whois")
                {

                    /// If the locationInput is still null, then an update was not requested by the user's string input (args).
                    /// Therefore, only a look-up of the student's location was requested from args.
                    if (locationInput == null)
                    {
                        sw.WriteLine(userName);  //WriteLine adds <CR><LF> itself.
                        sw.Flush();
                        string response = sr.ReadToEnd();
                        if (response == "ERROR: no entries found\r\n")
                        {
                            Console.WriteLine(response);
                        }
                        else
                        {
                            Console.WriteLine(userName + " is " + response);
                        }
                    }

                    /// If both variables have been changed (not null), then an update request was made.
                    else if (userName != null && locationInput != null)
                    {
                        sw.WriteLine(userName + " " + locationInput);  //args[1] covers all the argument in the " ".
                        sw.Flush();
                        string response = sr.ReadToEnd();
                        if (response == "OK\r\n")  // The \r\n is ignored.
                        {
                            // Manipulated response to give the proper response format string (like in ACW).
                            Console.WriteLine(userName + " location changed to be " + locationInput);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Server error occured: " + response);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: 'whois' protocol argument error");
                    }
                }

                else if (protocol == "-h9")
                {
                    // Look-up request in HTTP/0.9 format.
                    if (locationInput == null)
                    {
                        // ACW specified structure for a HTTP/0.9 look-up. 
                        sw.WriteLine("GET" + " " + "/" + userName); //WriteLine adds <CR><LF> itself.
                        sw.Flush();
                        string response = sr.ReadToEnd().Trim();

                        /// If the server response gives an error, show error to client interface.
                        if (response == "HTTP/0.9 404 Not Found\r\nContent-Type: text/plain")
                        {
                            Console.WriteLine(response);
                        }
                        else
                        {
                            /// string manipulation to retrieve the Location from the server response.
                            string[] responseSections = response.Split('\n');
                            string locationMatch = responseSections[3].Trim();
                            Console.WriteLine(userName + " is " + locationMatch);
                        }
                    }

                    /// Update request in HTTP/0.9 format.
                    else if (userName != null && locationInput != null)
                    {
                        // ACW specified structure for a HTTP/0.9 Update query.
                        sw.Write("PUT" + " " + "/" + userName + "\r\n" + "\r\n" + locationInput + "\r\n");
                        sw.Flush();

                        string response = sr.ReadToEnd().Trim();

                        if (response == "HTTP/0.9 200 OK\r\nContent-Type: text/plain")
                        {
                            // Manipulated response to give the proper response format string (like in ACW).
                            Console.WriteLine(userName + " location changed to be " + locationInput);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Server error occured: " + response);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: 'HTTP/0.9' protocol argument error");
                    }
                }

                else if (protocol == "-h0")
                {
                    // Look-up request in HTTP/1.0 format.
                    if (locationInput == null)
                    {
                        /// ACW specified structure for a HTTP/1.0 look-up.
                        /// The '<optional header lines>' don't need to be sent by the client to the server.
                        sw.Write("GET" + " " + "/?" + userName + " " + "HTTP/1.0" + "\r\n" + "\r\n");
                        sw.Flush();

                        string response = sr.ReadToEnd().Trim();

                        /// If the server response gives an error, show error to client interface.
                        if (response == "HTTP/1.0 404 Not Found\r\nContent-Type: text/plain")
                        {
                            Console.WriteLine(response);
                        }
                        else
                        {
                            /// string manipulation to retrieve the Location from the server response.
                            string[] responseSections = response.Split('\n');
                            string locationMatch = responseSections[3].Trim();
                            Console.WriteLine(userName + " is " + locationMatch);
                        }
                    }

                    /// Update request in HTTP/1.0 format.
                    else if (userName != null && locationInput != null)
                    {
                        /// ACW specified structure for a HTTP/1.0 Update query.
                        /// The '<optional header lines>' don't need to be sent by the client to the server.
                        sw.Write("POST" + " " + "/" + userName + " " + "HTTP/1.0" + "\r\n" + "Content-Length:"
                            + " " + locationInput.Length + "\r\n" + "\r\n" + locationInput);
                        sw.Flush();

                        string response = sr.ReadToEnd().Trim();
                        if (response == "HTTP/1.0 200 OK\r\nContent-Type: text/plain")
                        {
                            // Manipulated response to give the proper response format string (like in ACW).
                            Console.WriteLine(userName + " location changed to be " + locationInput);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Server error occured: " + response);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: 'HTTP/1.0' protocol argument error");
                    }
                }
                // Web server interaction will only be tested on this protocol.
                else if (protocol == "-h1")
                {
                    // Look-up request in HTTP/1.1 format.
                    if (locationInput == null)
                    {
                        /// ACW specified structure for a HTTP/1.1 look-up.
                        /// The '<optional header lines>' don't need to be sent by the client to the server.
                        sw.Write("GET" + " " + "/?name=" + userName + " " + "HTTP/1.1" + "\r\n"
                            + "Host:" + " " + serverName + "\r\n" + "\r\n");
                        sw.Flush();

                        /// To read WebServer input structure (to ignore the optional header lines).
                        string line = sr.ReadLine().Trim();
                        while ((line != "") == true)
                        {
                            //skip optional header lines
                            line = sr.ReadLine().Trim();
                        }

                        Console.Write(userName + " is ");
                        try
                        {
                            int c;
                            while ((c = sr.Read()) > 0)
                            {
                                Console.Write((char)c);
                            }
                        }
                        catch
                        {
                            //Ignore faults. NOTE: OR, do we show the exception since I commented out the code below 'finally'???
                        }
                        finally
                        {
                            sr.Close(); sw.Close(); client.Close();
                        }
                    }

                    /// Update request in HTTP/1.1 format.
                    else if (userName != null && locationInput != null)
                    {
                        /// ACW specified structure for a HTTP/1.1 Update query.
                        /// The '<optional header lines>' don't need to be sent by the client to the server.
                        string nameLocationStr = "name=" + userName + "&" + "location=" + locationInput;
                        int lengthOfStr = nameLocationStr.Length;

                        sw.Write("POST" + " " + "/" + " " + "HTTP/1.1" + "\r\n" + "Host:"
                            + " " + serverName + "\r\n" + "Content-Length:" + " " + lengthOfStr
                            + "\r\n" + "\r\n" + nameLocationStr);
                        sw.Flush();

                        string response = sr.ReadToEnd().Trim();
                        if (response == "HTTP/1.1 200 OK\r\nContent-Type: text/plain")
                        {
                            // Manipulated response to give the proper response format string (like in ACW).
                            Console.WriteLine(userName + " location changed to be " + locationInput);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: 'HTTP/1.1' protocol argument error");
                    }
                }

                else
                {
                    Console.WriteLine("ERROR: Unknown protocol");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected issue occured: " + e);
            }
        }
    }
}
