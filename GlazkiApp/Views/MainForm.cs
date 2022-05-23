using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using GlazkiApp.Models;
using GlazkiApp.Utils;
    
namespace GlazkiApp.Views
{
    public partial class MainForm : Form
    {
        private int _currentPageStartIndex = 0;
        private int _currentPageEndIndex = 10;
        private int _pageSize = 10;

        private List<Agent> _agents = DatabaseContext.database.Agent.ToList();

        public int PageCount
        {
            get { return _agents.Count / _pageSize; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<string> agentTypes = DatabaseContext.database.AgentType.Select(type => type.Title.ToString()).ToList();
            agentTypes.Insert(0, "Все типы");
            comboBoxFilter.DataSource = agentTypes;
            comboBoxSort.SelectedIndex = 0;
            
            UpdateOrder(_agents);
        }
        
        private void UpdateOrder(List<Agent> agents)
        {
            

            if(textBoxSearch.Text != "")
            {
                agents = agents.Where(agent =>
                    agent.Title.ToLower().Contains(textBoxSearch.Text.ToLower()) ||
                    agent.Phone.Contains(textBoxSearch.Text) ||
                    agent.Email.ToLower().Contains(textBoxSearch.Text.ToLower())
                    ).ToList();
            }

            if (comboBoxFilter.SelectedIndex != 0)
            {
                agents = agents.Where(agent => agent.AgentType.Title == comboBoxFilter.Text).ToList();
            }

            switch (comboBoxSort.Text)
            {
                case "Размер скидки":

                    if (checkBox.Checked)
                    {
                        agents = agents.OrderByDescending(agent => agent.ProductSale.Count).ToList();
                    }
                    else
                    {
                        agents = agents.OrderBy(agent => agent.ProductSale.Count).ToList();
                    }
                    break;

                case "Приоритет агента":

                    if (checkBox.Checked)
                    {
                        agents = agents.OrderByDescending(agent => agent.Priority).ToList();
                    }
                    else
                    {
                        agents = agents.OrderBy(agent => agent.Priority).ToList();
                    }
                    break;

                default:
                    if (checkBox.Checked)
                    {
                        agents = agents.OrderByDescending(agent => agent.Title).ToList();
                    }
                    else
                    {
                        agents = agents.OrderBy(agent => agent.Title).ToList();
                    }
                    break;
            }

            GenerateCards(agents);
        }
        
        private void GenerateCards(List<Agent> agents)
        {
            flowLayoutPanel.Controls.Clear();

            foreach (Agent agent in agents.GetRange(_currentPageStartIndex, _currentPageEndIndex))
            {
                AgentCard card = new AgentCard(agent);
                flowLayoutPanel.Controls.Add(card);
                card.Click += new EventHandler(this.CardClick);
            }
        }

        private void CardClick(object sender, EventArgs e)
        {
            AgentCard selectedAgentCard = (AgentCard)sender;

            Agent editingAgent = (Agent)_agents.First(agent => agent.Phone == selectedAgentCard.labelPhone.Text);

            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateOrder(_agents);
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrder(_agents);
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrder(_agents);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOrder(_agents);
        }

        private void labelPrevious_Click(object sender, EventArgs e)
        {

        }

        private void labelNext_Click(object sender, EventArgs e)
        {

        }
    }
}
