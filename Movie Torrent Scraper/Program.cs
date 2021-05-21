using System;
using Leaf.xNet;
using System.Net;
using System.Threading;
using System.Security.Authentication;

namespace Movie_Torrent_Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tomatopaste";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Please insert rotton tomatoes list url: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string tomatoUrl = Console.ReadLine();
            string[] rarurl;
            string[] ml = { "" };
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nScraping list titles...\n");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            using (var req = new HttpRequest())
            {

                req.EnableEncodingContent = true;
                req.IgnoreInvalidCookie = true;
                req.IgnoreProtocolErrors = true;
                req.Reconnect = true;
                req.ReconnectDelay = 3;
                req.ReconnectLimit = 3;
                req.SslProtocols = SslProtocols.Tls12;
                req.SslCertificateValidatorCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                req.UseCookies = true;
                req.ConnectTimeout = 5000;
                req.ReadWriteTimeout = 5000;
                req.Cookies = new CookieStorage();
                req.AllowAutoRedirect = true;
                req.MaximumAutomaticRedirections = 10;
                req.UserAgentRandomize();
                req.Cookies = new CookieStorage();
                Cookie usercookie = new Cookie("skt", "PLACE YOUR COOKIE VALUE FOR rarbg.to HERE", "/", "rarbg.to");
                req.Cookies.Add(usercookie);

                string tomatoPage = req.Get(tomatoUrl).ToString();
               
                tomatoPage = tomatoPage.Substring("<td class=\"bold\">1.</td>", "Certified Fresh In Theaters");
                
                ml = tomatoPage.Replace("</a>", "~").Split('~');


                for (int i = 0; i < ml.Length; i++) {
                    ml[i] = ml[i].Substring("articleLink\">", ")");
                    if(i != (ml.Length-1))
                    {
                        ml[i] = ml[i].Remove(0, 13);
                        ml[i] = ml[i] + ")";
                        Console.WriteLine((i+1) + ". " + ml[i]);
                    }
                }

                string[] resultpage = ml;
                rarurl = ml;
                Random rnd = new Random();
                
                double perc = 0.0;
                double percvalue = 100.0 / (ml.Length-1) / 2.0;

                for (int i = 0; i < rarurl.Length; i++)
                {
                    if (i != (resultpage.Length - 1))
                    {
                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Searching " + ml[i] + "...");
                        
                        ml[i] = ml[i].Replace("(", "%28");
                        ml[i] = ml[i].Replace(")", "%28=9");
                        ml[i] = ml[i].Replace(" ", "+");
                        string resp = req.Get("https://rarbg.to/torrents.php?search=" + ml[i] + "&category%5B%5D=44").ToString();
                        perc = perc + percvalue;
                        Console.Title = ("Tomatopaste " + perc.ToString() + "%");
                        if (!resp.Contains("div,dl,dt,em,fieldset,figcaption,figure,footer,form,h1,h2,h3,h4,h5,h6,header,hgroup,html,i,iframe,img,ins,kbd,label,legend,li,mark,menu,nav,object,ol,p,pre,q,s,samp,section,small,span,strike,strong,sub,summary"))
                        {
                            resp = resp.Substring("class=\"lista2\">", "align=\"center\"><div");
                            Console.WriteLine("Got Response!!");
                            if (!string.IsNullOrEmpty(resp))
                            {
                                resp = resp.Replace("<td align=\"left\" class=\"lista\" width=\"48\" style=\"width: 48px;\"", "~");
                                resp = resp.Substring("href=\"/torrent/", "\" title");
                                rarurl[i] = ("https://rarbg.to/torrent/" + resp);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("https://rarbg.to/torrent/" + resp + "\n\n");
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("No Results On Title Search\n");
                                Console.ForegroundColor = ConsoleColor.Green;
                            }

                            int RandomWaitTime = rnd.Next(500, 1500);
                            Thread.Sleep(RandomWaitTime);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Failed to access query (please update you cookies)\n\n");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                    }
                }
                int counter = 1;
                for (int i = 0; i < rarurl.Length; i++)
                {
                    int RandomWaitTime = rnd.Next(500, 1500);
                    Thread.Sleep(RandomWaitTime);
                    try
                    {
                        string movpage = req.Get(rarurl[i]).ToString();
                        movpage = movpage.Substring("href=\"magnet", "\">");
                        movpage = "magnet" + movpage;
                        System.Diagnostics.Process.Start(movpage);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(counter + ". " + ml[i] + " Done!");
                        counter++;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    catch { 
                    
                    }
                    perc += percvalue;
                    Console.Title = ("Tomatopaste " + (perc - percvalue).ToString() + "%");
                }
                Console.WriteLine("\nMovie Scan Completed!");
                Console.ReadLine();
            }
            
        }
    }
}
