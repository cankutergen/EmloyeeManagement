using EmloyeeManagement.Business.Commands;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.WinformsUi.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmloyeeManagement.WinformsUi
{
    public partial class EditEmployee : Form
    {
        private readonly IMediator _mediator;
        private readonly Employee _employee;

        public EditEmployee(IMediator mediator, Employee employee)
        {
            InitializeComponent();
            _mediator = mediator;
            _employee = employee;

            // initialize comboboxes for gender and status
            UiHelper.FillGendersCombobox(cmbGender);
            UiHelper.FillStatusesCombobox(cmbStatus);
        }

        private void EditEmployee_Load(object sender, EventArgs e)
        {
            cmbGender.SelectedText = _employee.Gender;
            cmbStatus.SelectedText = _employee.Status;
            txtEmail.Text = _employee.Email;
            txtName.Text = _employee.Name;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            var entity = new Employee
            {
                Id = _employee.Id,
                Email = txtEmail.Text.Trim(),
                Gender = cmbGender.Text,
                Name = txtName.Text.Trim(),
                Status = cmbStatus.Text
            };

            try
            {
                var response = await _mediator.Send(new UpdateEmployeeCommand(entity));
                if (response.Code == 200)
                {
                    UiHelper.ShowLabel(lblSuccess, "Successfully updated");
                }
                else
                {
                    var errorMessage = UiHelper.CreateErrorMessage(response.Data);
                    UiHelper.ShowLabel(lblErrorDescription, errorMessage);
                }
            }
            catch (Exception ex)
            {
                UiHelper.ShowLabel(lblErrorDescription, ex.Message);
            }
        }
    }
}
