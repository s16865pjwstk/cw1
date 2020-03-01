using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // wymaganie 1
            if (args.Length != 1 || args[0] == "")
                throw new ArgumentNullException("ArgumentNullException message");

            // wymaganie 2
            if (!Uri.IsWellFormedUriString(args[0], UriKind.Absolute))
                throw new ArgumentException("ArgumentException message");

           
            var emails = await getEmails(args[0]);

            // wymaganie 5
            if (emails.Count == 0)
            {
                Console.WriteLine("Nie znaleziono adresów e-mail");
            } else
            {
                foreach (var a in args)
                {
                    Console.WriteLine(a);
                }

                // wymaganie 6
                var unique_items = new HashSet<string>(emails);
                foreach (var email in unique_items)
                {
                    Console.WriteLine(email);
                }
            }
        }

        static async Task<IList<string>> getEmails(string url)
        {
            var httpClient = new HttpClient();
            var listOfEmails = new List<string>();

            // wmaganie 4
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.GetAsync(url);

                // wymaganie 3
                httpClient.Dispose();
            } catch (Exception )
            {
                Console.WriteLine("Błąd w czasie pobierania strony");

                // wymaganie 3
                httpClient.Dispose();
                return listOfEmails;
            }

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);

            MatchCollection emailmatches = emailRegex.Matches(response.Content.ReadAsStringAsync().Result);

            foreach (var emailMatch in emailmatches)
            {
                listOfEmails.Add(emailMatch.ToString());
            }



            return listOfEmails;

        }

    }


}
