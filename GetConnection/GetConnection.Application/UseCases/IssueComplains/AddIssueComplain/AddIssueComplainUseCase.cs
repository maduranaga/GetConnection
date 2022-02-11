using AutoMapper;
using GetConnection.Application.UseCases.IssueComplains.AddHrIssue;
using GetConnection.Application.UseCases.IssueComplains.GetHrIssue;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.FirbaseNotifications;
using GetConnection.Core.Repositories.IssueComplains;
using GetConnection.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.AddIssueComplain
{
   public  class AddIssueComplainUseCase : IRequest<AddIssueComplainResponse>
    {
    
        public AddIssueComplainRequest  AddIssueComplainRequest  { get; set; }

    }
    public class Handler : IRequestHandler<AddIssueComplainUseCase, AddIssueComplainResponse>
    {
        private readonly IIssueComplainWriteOnlyRepository _issueComplainWriteOnlyRepository;
        private readonly IMapper _mapper;
        private IBlobService _blobService;
        private IFirbaseNotificationWriteOnlyRepository _firbaseNotificationWriteOnlyRepository;


        public Handler(IFirbaseNotificationWriteOnlyRepository firbaseNotificationWriteOnlyRepository, IBlobService blobService, IIssueComplainWriteOnlyRepository issueComplainWriteOnlyRepository, IMapper mapper)
        {
            _issueComplainWriteOnlyRepository = issueComplainWriteOnlyRepository;
            _mapper = mapper;
            _blobService = blobService;
            _firbaseNotificationWriteOnlyRepository = firbaseNotificationWriteOnlyRepository;

        }
        public async Task<AddIssueComplainResponse> Handle(AddIssueComplainUseCase request, CancellationToken cancellationToken)
        {

            if (request.AddIssueComplainRequest.UserDescription !=null && request.AddIssueComplainRequest.IssueTypeID !=0)
            {
                var orgnResponse = _mapper.Map<AddIssueComplainRequest, IssueComplain>(request.AddIssueComplainRequest);

                var orgn = await _issueComplainWriteOnlyRepository.AddComplainIssue(orgnResponse);

                AddIssueComplainResponse d = new AddIssueComplainResponse();
                if (orgn != null)
                {
                    if (request.AddIssueComplainRequest.Images !=null) { 
                    IssueImage data = new IssueImage();
                    foreach (var formFile in request.AddIssueComplainRequest.Images)
                    {
                            if (formFile.Length > 0)
                            {
                                string uniqueFileName = Guid.NewGuid().ToString() + formFile.FileName;


                                var result = await _blobService.UploadFileBlobAsync(
                                             "getconnection",
                                             formFile.OpenReadStream(),
                                             formFile.ContentType,
                                             uniqueFileName);


                                data.IssueId = orgn.Id;
                                data.ImagePath = "https://mankiwwa.blob.core.windows.net/getconnection/" + uniqueFileName;


                                var imagepost = await _issueComplainWriteOnlyRepository.AddIssueIamge(data);
                            }

                        }
                    }

                    

                    FirbaseNotification not = new FirbaseNotification();
                    not.DeviceToken = "";
                    not.Title ="";
                    not.Body ="Your Issue Complain , Inform The Deparmnet, we will inform you after;";
                    not.DateTime = DateTime.Now;


                    var x = _firbaseNotificationWriteOnlyRepository.AdNotification(not);
                    d.Status_code = 201;
                    d.Status = "Success";
                    d.Error = "";
                    d.Data = null;
                    return await Task.FromResult(d);
                }
                else
                {
                    d.Status_code = 402;
                    d.Status = "Not Found";
                    d.Error = "Not Found";
                    d.Data = null;
                    return await Task.FromResult(d);
                }
            }
            else
            {
                AddIssueComplainResponse d = new AddIssueComplainResponse();
                d.Status_code = 422;
                d.Status = "Required fields not supplied";
                d.Error = "Required fields not supplied";
                d.Data = null;
                return await Task.FromResult(d);
            }
        }
    }
}