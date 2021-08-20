using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmloyeeManagement.WinformsUi.Helper
{
    public class UiHelper
    {
        public static void ShowLabel(Label label, string message)
        {
            label.Show();
            label.Text = message;

            var timer = new Timer();
            timer.Interval = 2000; // it will Tick in 3 seconds
            timer.Tick += (s, e) =>
            {
                label.Hide();

                timer.Stop();
            };

            timer.Start();
        }

        public static DialogResult ShowDialog(string caption, string message)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OKCancel);
        }

        public static Employee GetSelectedRow(DataGridView dataGridView)
        {
            return dataGridView.SelectedRows[0].DataBoundItem as Employee;
        }

        public static string CreateErrorMessage(List<DataResponse> dataResponse)
        {
            var errorMessage = "";
            foreach (var item in dataResponse)
            {
                errorMessage += $"{item.Field} {item.Message} {Environment.NewLine}";
            }

            return errorMessage;
        }

        public static void FillGendersCombobox(ComboBox cmbGender)
        {
            cmbGender.Items.Add(new { gender = "female" });
            cmbGender.Items.Add(new { gender = "male" });
            cmbGender.DisplayMember = "gender";
            cmbGender.ValueMember = "gender";
        }

        public static void FillStatusesCombobox(ComboBox cmbStatus)
        {
            cmbStatus.Items.Add(new { status = "active" });
            cmbStatus.Items.Add(new { status = "inactive" });
            cmbStatus.DisplayMember = "status";
            cmbStatus.ValueMember = "status";
        }

        public static void SetPaginationLabel(Label label, ApiResponse response)
        {
            label.Text = $"Page {response.Meta.Pagination.Page} / {response.Meta.Pagination.Pages}";
        }

        public static void FillDataGridView(DataGridView dataGridView, List<Employee> employees)
        {
            dataGridView.DataSource = employees;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ClearSelection();
        }
    }
}
