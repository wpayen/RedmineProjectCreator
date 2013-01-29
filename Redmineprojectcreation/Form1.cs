using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redmineprojectcreation
{
    public partial class Window : Form
    {
        Boolean connected = false;
        Boolean clientKnown = false;
        Parser parser;
        Boolean productSelected = false;
        RedmineManager redmine;
        BindingList<Project> products = new BindingList<Project>();
        Random Ramdom = new Random();

        public Window()
        {
            parser = new Parser();
            InitializeComponent();
            urlTextBox.Text = parser.defaultURL;
            productSelector.DataSource = products;
            productSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            productSelector.DisplayMember =  "Name";
        }

        
        private void actionButton_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                if (productSelected)
                {
                    try
                    {
                        createProject();
                    }
                    catch (RedmineException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
            }
            else
            {
                if (IsValidHttpUri(urlTextBox.Text))
                {
                    urlTextBox.Font = new Font(urlTextBox.Font.FontFamily, urlTextBox.Font.Size, FontStyle.Regular);
                    if (connectRedmine(urlTextBox.Text))
                    {
                        try
                        {
                            launchCreation();
                        }
                        catch (RedmineException error)
                        {
                            MessageBox.Show(error.Message);
                        }
                    }
                    else
                    {
                        urlTextBox.Font = new Font(urlTextBox.Font.FontFamily, urlTextBox.Font.Size, FontStyle.Underline);
                    }
                }
                else
                {
                    urlTextBox.Font = new Font(urlTextBox.Font.FontFamily, urlTextBox.Font.Size, FontStyle.Underline);
                }
            }
        }

        private void launchCreation()
        {
            try
            {
                getProduct();
                productSelector.Update();
                connected = true;
                actionButton.Text = "Create";
            }
            catch(RedmineException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void createProject() 
        {
            Project newProject = new Project();
            newProject.Name = clientName.Text;
            newProject.Identifier = ((Project)productSelector.SelectedItem).Name.ToLower() + "_" + clientName.Text.ToLower();
            newProject.Description = clientName.Text;
            newProject.Id = createId();
            newProject.Parent = (IdentifiableName)productSelector.SelectedItem;

            try
            {
                redmine.CreateObject<Project>(newProject);
                parser.getIssuesFromProject(((Project)productSelector.SelectedItem));
            }
            catch(RedmineException error)
            {
                try
                {
                    var parameters = new NameValueCollection { { "name", "*" } };

                    foreach (var issue in redmine.GetObjectList<Project>(parameters))
                    {
                        if (newProject.Identifier == issue.Identifier)
                        {
                            throw new RedmineException("Projet déja existant");
                        }
                    }
                }
                catch (RedmineException error2)
                {
                    throw new RedmineException(error2.Message);
                }
            }

        }

        private void getProduct()
        {
            try
            {
                var parameters = new NameValueCollection { { "name", "*" } };

                foreach (var issue in redmine.GetObjectList<Project>(parameters))
                {
                    if (issue.Parent == null)
                    {
                        products.Add(issue);
                    }
                }
            }
            catch (RedmineException error)
            {
                throw new RedmineException(error.Message);
            }
        }

        private void productSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            productSelected = true;
        }

        private void clientName_TextChanged(object sender, EventArgs e)
        {
            clientKnown = true;
        }

        //Fonction Utilitaires
        public static bool IsValidHttpUri(string uriString)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://" + uriString);

                using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                {
                    if (rsp.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (WebException)
            {
                return false;
            }
            return false;
        }
        private int createId()
        {
            int newId = 0;
            List<int> Ids = new List<int>();

            foreach (Project item in products)
            {
                Ids.Add(item.Id);
                newId = item.Id;
            }
            while (Ids.Contains(newId))
            {
                newId = Ramdom.Next(10000);
            }
            return newId;
        }
        private Boolean connectRedmine(String url)
        {
            try
            {
                var parameters = new NameValueCollection { { "name", "*" } };
                redmine = new RedmineManager(url, parser.apiKey);
                redmine.GetObjectList<Project>(parameters);
                return true;
            }
            catch (RedmineException error)
            {
                Console.WriteLine(error);
                return false;
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Apikey frm = new Apikey();
            frm.apiKeyTextbox.Text = parser.apiKey;
            frm.setParser(parser);
            frm.Show(this);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            defaultUrl frm = new defaultUrl();
            frm.apiKeyTextbox.Text = parser.defaultURL;
            frm.setParser(parser);
            frm.Show(this);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

    }
}
