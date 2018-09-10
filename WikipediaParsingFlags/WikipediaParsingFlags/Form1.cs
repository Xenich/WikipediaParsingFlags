using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WikipediaParsingFlags
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonstartParse_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.Create("https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%B7%D0%BD%D0%B0%D0%BA%D0%BE%D0%B2_%D0%B2%D0%B0%D0%BB%D1%8E%D1%82");
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string response = reader.ReadToEnd();
            int start = response.IndexOf("<tr>", response.IndexOf("<tr>", response.IndexOf("<tr>")+3 ) +3);
            string resp1 = response.Substring(start);
            int end = resp1.IndexOf("</table>");
            string resp2 = resp1.Remove(end);
            string[] rows = resp2.Split(new string[] { "<tr>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in rows)
            {
                string[] rows1 = str.Split(new string[] { "<td>" }, StringSplitOptions.None);
                int startPNG = rows1[2].IndexOf("src=");
                int endPNG = rows1[2].IndexOf("width=",startPNG);
                string URL22 = "https:" + rows1[2].Substring(startPNG + 5, endPNG-startPNG - 7);

                int fileNameEnd = rows1[3].IndexOf("</td>");
                string fileName = (rows1[3].Substring(0,fileNameEnd)+".png").ToLower(); // имя файла - буквенное обозначение

                /*
                string URL44 = URL22.Replace("22px", "44px");
                DrawFlagToFile(URL44, "flags/Буквенное обозначение флагов/width=44/" + fileName);*/

                int fileNameR030End = rows1[4].IndexOf("</td>");
                string fileNameR030 = rows1[4].Substring(0, fileNameR030End) + ".png";   // имя файла - код R030
                //DrawFlagToFile(URL22, "flags/Обозначение в кодировке R030/width=22/" + fileNameR030);
                string URL88 = URL22.Replace("22px", "88px");
                DrawFlagToFile(URL88, "flags/Обозначение в кодировке R030/width=88/" + "f"+fileNameR030);
                /*string URL88 = URL22.Replace("22px", "88px");
                DrawFlagToFile(URL88, "flags/Обозначение в кодировке R030/width=88/" + fileNameR030);
                string URL200 = URL22.Replace("22px", "200px");
                DrawFlagToFile(URL200, "flags/Обозначение в кодировке R030/width=200/" + fileNameR030);*/
             }
        }


        /*
<td><a href="/wiki/%D0%9C%D0%B0%D0%BB%D0%B0%D0%B3%D0%B0%D1%81%D0%B8%D0%B9%D1%81%D0%BA%D0%B8%D0%B9_%D0%B0%D1%80%D0%B8%D0%B0%D1%80%D0%B8" title="Малагасийский ариари">Ариари</a></td>
<td><a href="/wiki/%D0%A4%D0%B0%D0%B9%D0%BB:Flag_of_Madagascar.svg" class="image" title="Флаг Мадагаскара"><img alt="Флаг Мадагаскара" src="//upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Madagascar.svg/22px-Flag_of_Madagascar.svg.png" width="22" height="15" class="thumbborder" srcset="//upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Madagascar.svg/33px-Flag_of_Madagascar.svg.png 1.5x, //upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Madagascar.svg/44px-Flag_of_Madagascar.svg.png 2x" data-file-width="900" data-file-height="600" /></a><br />
<a href="/wiki/%D0%9C%D0%B0%D0%B4%D0%B0%D0%B3%D0%B0%D1%81%D0%BA%D0%B0%D1%80" title="Мадагаскар">Мадагаскар</a></td>
<td>MGA</td>
<td>969</td>
<td>—</td>
<td>—</td>
<td>—</td>
<td>—</td>
<td>—</td>
<td>—</td>
<td>Ar</td>
</tr>
         * */
        private static void DrawFlagToFile(string URL, string fileName)
        {
            WebRequest req = WebRequest.Create(URL);
            WebResponse resp = req.GetResponse();
            Stream str = resp.GetResponseStream();
            byte[] buf = new byte[30000];
            MemoryStream ms = new MemoryStream(buf);
            int newByte;
            int length = 0;
            while ((newByte = str.ReadByte()) != -1)
            {
                ms.WriteByte((byte)newByte);
                length++;
            }

            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.CreateNew);
                fileStream.Write(buf, 0, length);
                fileStream.Close();
            }
            catch { }
            ms.Close();
            str.Close();
        }
    }
}
