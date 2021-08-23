using Autofac;
using EmloyeeManagement.Business.Commands;
using EmloyeeManagement.Business.Queries;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using EmloyeeManagement.WinformsUi.Helper;
using EmloyeeManagement.WinformsUi.IoC.AutoFac;
using MediatR;
using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmloyeeManagement.WinformsUi
{
    public partial class ListPage : Form
    {
        public static IContainer container;

        private readonly IMediator _mediator;
        private int PAGE_NUMBER = 1;
        private int MAX_PAGE_NUMBER = 1;
        private string SEARCH_TEXT = "";

        public ListPage(IMediator mediator)
        {
            InitializeComponent();

            _mediator = mediator;
        }

        private async void ListPage_Load(object sender, EventArgs e)
        {
            await GetList();
        }

        #region Button Events

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            PAGE_NUMBER = 1;
            txtSeach.Text = "";
            SEARCH_TEXT = "";

            await GetList();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            PAGE_NUMBER = 1;
            SEARCH_TEXT = txtSeach.Text.Trim();

            await GetList();
        }

        private async void btnClearSearch_Click(object sender, EventArgs e)
        {
            PAGE_NUMBER = 1;
            txtSeach.Text = "";
            SEARCH_TEXT = "";

            await GetList();
        }

        private async void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (PAGE_NUMBER == 1)
            {
                return;
            }

            PAGE_NUMBER -= 1;

            await GetList();
        }

        private async void btnNextPage_Click(object sender, EventArgs e)
        {
            if (PAGE_NUMBER == MAX_PAGE_NUMBER)
            {
                return;
            }

            PAGE_NUMBER += 1;

            await GetList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            container = ContainerConfiguration.Configure();

            var createEmployeeForm = new CreateEmployee(container.Resolve<IMediator>());
            createEmployeeForm.Show();
        }

        private void bntEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                var item = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;

                container = ContainerConfiguration.Configure();

                var editEmployeeForm = new EditEmployee(container.Resolve<IMediator>(), item);
                editEmployeeForm.Show();
            }
            else
            {
                UiHelper.ShowLabel(lblErrorDescription, "Row should be selected");
            }
        }

        private async void bntDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                var item = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;

                DialogResult result = UiHelper.ShowDialog("Employee will be deleted", $"Are you sure want to delete {item.Name} ?");
                if (result == DialogResult.OK)
                {
                    var response = await _mediator.Send(new DeleteEmployeeCommand(item.Id));
                    if (response.Code == 204)
                    {
                        await GetList();
                    }
                    else
                    {
                        var errorMessage = UiHelper.CreateErrorMessage(response.Data);
                        UiHelper.ShowLabel(lblErrorDescription, errorMessage);
                    }
                }
            }
            else
            {
                UiHelper.ShowLabel(lblErrorDescription, "Row should be selected");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelHelper.ExportExcel(dgvEmployees);
            }
            catch (Exception ex)
            {

                UiHelper.ShowLabel(lblErrorDescription, ex.Message);
            }
        }

        #endregion


        // Global variables are used for query parameters
        private async Task<ApiResponse> GetList()
        {
            var query = $"?page={PAGE_NUMBER}";

            if (SEARCH_TEXT != "")
            {
                query += $"&name={SEARCH_TEXT}";
            }

            var response = await _mediator.Send(new GetEmployeeListWithQuery(query));
            var model = response.Data.Select(x => new Employee
            {
                Email = x.Email,
                Gender = x.Gender,
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();
            UiHelper.FillDataGridView(dgvEmployees, model);

            MAX_PAGE_NUMBER = response.Meta.Pagination.Pages;
            UiHelper.SetPaginationLabel(lblPageNumber, response);

            return response;
        }
    }
}
