using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctoral_accounting
{
    public partial class SelectViewForm : Form
    {
        public SelectViewForm(History history)
        {
            InitializeComponent();

            patientSurNameText.Text = history.PatientSurName;
            patientText.Text = history.PatientName;
            comentaryText.Text = history.Comentary;
            diagnosesText.Text = history.Diagnos;
            dateTimePicker1.Value = history.CurrentDate;
            analizesText.Text = history.Analizes;
            treatmentText.Text = history.Treatment;
            dateTimePicker2.Value = history.DateOfArive;
        }

        private void SelectViewForm_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
