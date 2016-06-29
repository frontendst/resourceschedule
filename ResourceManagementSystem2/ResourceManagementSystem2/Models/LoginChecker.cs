using ResourceManagementSystem2.Models.Exceptions;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Hosting;
using System.Xml.Linq;

namespace ResourceManagementSystem2.Models
{
    public static class LoginChecker
    {
        public static bool IsAllowedUser(string username, string password)
        {
            //return true;
            //заглушка
            const string ldapPath = "LDAP://local.st.by/DC=local,DC=st,DC=by";

            var entry = new DirectoryEntry(ldapPath, @"ST_DOMAIN\" + username, password);
            var searcher = new DirectorySearcher(entry)
            {
                Filter = "(SAMAccountName=" + username + ")"
            };

            SearchResult searchResult;
            try
            {
                searchResult = searcher.FindOne();
            }
            catch (DirectoryServicesCOMException)
            {
                throw new UserNotFoundException();
            }
            catch (COMException)
            {
                throw new IncorrectPasswordException();
            }
            if (searchResult == null) throw new UserNotFoundException();

            IEnumerable<string> allowedUsersEmails;
            try
            {
                var userDataFile = XDocument.Load(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Data/users.xml"));

                allowedUsersEmails = userDataFile.Elements("users").Elements("user").Select(node => node.Value.Trim());
            }
            catch
            {
                throw new FileNotFoundException();
            }

            var currentUserEmail = SafeExtractProperty(searchResult, "mail");

            return allowedUsersEmails.Contains(currentUserEmail);
        }

        private static string SafeExtractProperty(SearchResult item, string propertyName)
        {
            if (!item.Properties.Contains(propertyName) || item.Properties[propertyName].Count == 0) return string.Empty;

            return item.Properties[propertyName][0].ToString();
        }
    }
}