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
    public partial class CreateEmployee : Form
    {
        private readonly IMediator _mediator;

        public CreateEmployee(IMediator mediator)
        {
            InitializeComponent();
            _mediator = mediator;

            // initialize comboboxes for gender and status
            UiHelper.FillGendersCombobox(cmbGender);
            UiHelper.FillStatusesCombobox(cmbStatus);
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            var entity = new Employee
            {
                Email = txtEmail.Text.Trim(),
                Gender = cmbGender.Text,
                Name = txtName.Text.Trim(),
                Status = cmbStatus.Text
            };

            try
            {
                var response = await _mediator.Send(new CreateEmployeeCommand(entity));
                if(response.Code == 201)
                {
                    UiHelper.ShowLabel(lblSuccess, "Successfully added");
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
