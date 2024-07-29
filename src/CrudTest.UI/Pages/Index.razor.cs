using CrudTestMicroservice.Protobuf.Protos.Customer;
using MudBlazor;

namespace CrudTest.UI.Pages;

public partial class Index
{
    private int _isShowSuccessOnAddNew = 0;
    private string _error ="Noting";
    private MudDataGrid<GetAllCustomerByFilterResponseModel> mudDataGrid ;
    private List<string> _events = new();

    private async Task<GridData<GetAllCustomerByFilterResponseModel>> GetServerData(GridState<GetAllCustomerByFilterResponseModel> state)
    {
        var apiResult = await CustomerContract.GetAllCustomerByFilterAsync(new GetAllCustomerByFilterRequest()
        {
            PaginationState = new()
            {
                PageNumber = state.Page,
                PageSize = state.PageSize
            }
        });


        return new GridData<GetAllCustomerByFilterResponseModel>() { TotalItems = (int)apiResult.MetaData.TotalCount, Items = apiResult.Models.ToList() };
    }

    private async Task OnEditClick(GetAllCustomerByFilterResponseModel model)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true , MaxWidth = MaxWidth.Medium};
        var parameters = new DialogParameters<EditCustomerDialog> { { x => x.UpdateCustomerRequest, new UpdateCustomerRequest()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Id = model.Id,
            BankAccountNumber = model.BankAccountNumber,
            DateOfBirth = model.DateOfBirth
        } } };
        var dialogResult=await (await DialogService.ShowAsync<EditCustomerDialog>("Edit Dialog",parameters, options)).Result;
        if (!dialogResult.Canceled)
        {
            var addNewResult = (KeyValuePair<bool, string>)dialogResult.Data;
            if (addNewResult.Key)
            {
                _isShowSuccessOnAddNew = 1;
                await mudDataGrid.ReloadServerData();
            }
            else
            {
                _error = addNewResult.Value;
                _isShowSuccessOnAddNew = 2;
            }
        }
    }
    
    private async Task OnDeleteClick(long Id)
    {
        try
        {
         await CustomerContract.DeleteCustomerAsync(new DeleteCustomerRequest(){Id = Id});
        _isShowSuccessOnAddNew = 1;
        await mudDataGrid.ReloadServerData();
        }
        catch (Exception e)
        {
            _error = e.Message;
            _isShowSuccessOnAddNew = 2;
        }
    }
   
    private async Task OpenDialogAsync()
    {    
        var options = new DialogOptions { CloseOnEscapeKey = true , MaxWidth = MaxWidth.Medium};

        var dialogResult=await (await DialogService.ShowAsync<AddNewCustomerDialog>("Add New Dialog", options)).Result;
        if (!dialogResult.Canceled)
        {
            var addNewResult = (KeyValuePair<bool, string>)dialogResult.Data;
            if (addNewResult.Key)
            {
                _isShowSuccessOnAddNew = 1;
               await mudDataGrid.ReloadServerData();
            }
            else
            {
                _error = addNewResult.Value;
                _isShowSuccessOnAddNew = 2;
            }
        }
    }
}