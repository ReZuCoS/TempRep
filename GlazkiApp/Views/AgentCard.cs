using System;
using System.Windows.Forms;
using GlazkiApp.Models;

namespace GlazkiApp.Views
{
    public partial class AgentCard : UserControl
    {
        public AgentCard(Agent agent)
        {
            InitializeComponent();

            labelName.Text = agent.AgentType.Title + " | " + agent.Title;
            labelCount.Text = agent.ProductSale.Count + " продаж за год";
            labelPhone.Text = agent.Phone;
            labelPriority.Text += agent.Priority;
            decimal sum = 0;

            if (agent.Logo != "")
            {
                pictureBox.ImageLocation = $@"{Environment.CurrentDirectory}\Resources\{agent.Logo}";
            }

            foreach (ProductSale sale in agent.ProductSale)
            {
                sum += sale.ProductCount * sale.Product.MinCostForAgent;
            }

            if(sum <= 10000)
            {
                labelPercent.Text = "0%";
            }
            else if (sum > 10000 && sum <= 50000)
            {
                labelPercent.Text = "5%";
            }
            else if (sum > 50000 && sum <= 150000)
            {
                labelPercent.Text = "10%";
            }
            else if (sum > 150000 && sum <= 500000)
            {
                labelPercent.Text = "20%";
            }
            else
            {
                labelPercent.Text = "25%";
            }
        }
    }
}
