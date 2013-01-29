using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Redmineprojectcreation
{
    public class Parser
    {
       public String apiKey;
       public String defaultURL;
       public List<Issue> issues;

        XmlDocument xDoc;
        
        public Parser()
        {
            xDoc = new XmlDocument();
            issues = new List<Issue>();
            getDefaultData();
        }

        public void getDefaultData()
        {
            try
            {
                xDoc.Load("parameter.xml");
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("Paramètre non trouvé");
            }

            apiKey = xDoc.GetElementsByTagName("apiKey")[0].InnerText;
            defaultURL = xDoc.GetElementsByTagName("url")[0].InnerText;
        }

        public List<Issue> getIssuesFromProject(Project project)
        {
            XmlNodeList tmp = xDoc.GetElementsByTagName("template");
            if (!(tmp == null))
            {
                foreach (XmlNode i in tmp)
                {
                    if (i.Attributes["id"].Value == project.Name)
                    {
                        foreach (XmlNode j in i.ChildNodes)
                        {
                            DateTime f;
                            DateTime.TryParse(j.Attributes["start_date"].Value,out f);
                                issues.Add(new Issue()
                                {
                                    Project = project,
                                    AssignedTo = new IdentifiableName() { Name = j.Attributes["assigned"].Value },
                                    Description = j.Attributes["description"].Value,
                                    Subject = j.Attributes["name"].Value,
                                    StartDate = f,
                                });
                            int index = issues.Count();
                                foreach (XmlNode k in j.ChildNodes)
                                {
                                    DateTime t;
                                    DateTime.TryParse(j.Attributes["start_date"].Value, out t);
                                    issues.Add(new Issue()
                                    {
                                        Project = project,
                                        AssignedTo = new IdentifiableName() { Name = k.Attributes["assigned"].Value },
                                        Description = k.Attributes["description"].Value,
                                        Subject = k.Attributes["name"].Value,
                                        StartDate = t,
                                        ParentIssue = new IdentifiableName() { Name = issues[index-1].Subject, Id = issues[index-1].Id, },
                                    });

                                }
                        }
                    }
                    Issue b = new Issue();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Aucun Template trouvé");
            }

            return null;

        }

        public void changeApiKey(string temp)
        {
            xDoc.GetElementsByTagName("apiKey")[0].InnerText = temp;
            xDoc.Save("parameter.xml");
        }

        internal void changeDefaultUrl(string p)
        {
            xDoc.GetElementsByTagName("url")[0].InnerText = p;
            xDoc.Save("parameter.xml");
        }
    }
}
