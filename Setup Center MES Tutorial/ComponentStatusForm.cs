using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace OIB.Tutorial
{
    public partial class ComponentStatusForm : Form
    {
        public ComponentStatusForm()
        {
            InitializeComponent();

            dataGridViewComponentStatus.DataSource = MesContext.ComponentStatusDataSet.Tables[0];
        }

    }
}
