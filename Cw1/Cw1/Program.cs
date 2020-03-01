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
            var emails = await getEmails(args[0]);
            foreach (var a in args)
            {
                Console.WriteLine(a);
            }

            foreach (var email in emails)
            {
                Console.WriteLine(email);
            }

        }

        static async Task<IList<string>> getEmails(string url)
        {
            var httpClient = new HttpClient();
            var listOfEmails = new List<string>();

            var response = await httpClient.GetAsync(url);

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
