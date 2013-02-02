using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;


namespace HTTPpost
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlbase = "https://api.eveonline.com/";
            string [] urlarray = {"eve/TypeName.xml.aspx", "eve/SkillTree.xml.aspx", "eve/RefTypes.xml.aspx", "eve/FacWarTopStats.xml.aspx", "eve/FacWarStats.xml.aspx", "eve/Errorlist.xml.aspx", "eve/ConquerableStationList.xml.aspx", "eve/CharacterName.xml.aspx", "eve/CharacterInfo.xml.aspx", "eve/CharacterID.xml.aspx", "eve/ConquerableStationList.xml.aspx", "eve/CertificateTree.xml.aspx", "eve/AllianceList.xml.aspx", "map/FacWarSystems.xml.aspx", "map/jumps.xml.aspx", "map/kills.xml.aspx", "map/sovereignty.xml.aspx", "map/SovereigntyStatus.xml.aspx"};

            foreach (string urlapi in urlarray)
            {
                string url = urlbase+urlapi;
            
                string path1 = urlapi.Remove(0,4);
                string path = path1.Substring(0, path1.Length -9);

                string filename = @"c:\devtemp\" + path + ".file";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 500000;

                try
                {

                    using (WebResponse response = (HttpWebResponse)request.GetResponse())
                    {

                        using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                        {

                            byte[] bytes = ReadFully(response.GetResponseStream());
                            stream.Write(bytes, 0, bytes.Length);

                        }

                    }

                }

                catch (WebException)
                {

                    Console.WriteLine("Error Occured downloading information from "+url);

                }

            }
        }

        public static byte[] ReadFully(Stream input)
        {

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {

                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {

                    ms.Write(buffer, 0, read);

                }
                return ms.ToArray();

            }

        }
    }
}
