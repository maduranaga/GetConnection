using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.SuportTokens;
using GetConnection.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.SuportToken.AddSuportToken
{
    public class AddSuportTicketUseCase : IRequest<AddSuportTicketResponse>
    {
        public AddSuportTicketRequest AddSuportTicketRequest  { get; set; }
    }
    public class Handler : IRequestHandler<AddSuportTicketUseCase,AddSuportTicketResponse>
    {
        private readonly ISuportTokenWriteOnlyRepository _suportTokenWriteOnlyRepository;
        private readonly IMapper _mapper;
        private IBlobService _blobService;


        public Handler(IBlobService  blobService, ISuportTokenWriteOnlyRepository suportTokenWriteOnlyRepository, IMapper mapper)
        {
            _suportTokenWriteOnlyRepository =  suportTokenWriteOnlyRepository;
            _mapper = mapper;
            _blobService = blobService;

        }
        public async Task<AddSuportTicketResponse>Handle(AddSuportTicketUseCase request, CancellationToken cancellationToken)
        {


            if (request.AddSuportTicketRequest.Attachment != null)
            {

                foreach (var formFile in request.AddSuportTicketRequest.Attachment)
                {
                    if (formFile.Length > 0)
                    {
                        SuportTokenAttachemnt dataset = new SuportTokenAttachemnt();
                        string uniqueFileName = Guid.NewGuid().ToString() + formFile.FileName;


                        var result = await _blobService.UploadFileBlobAsync(
                                     "getconnection",
                                     formFile.OpenReadStream(),
                                     formFile.ContentType,
                                     uniqueFileName);

                        if (request.AddSuportTicketRequest.Attachment1Path ==null)
                        {
                            request.AddSuportTicketRequest.Attachment1Path= "https://mankiwwa.blob.core.windows.net/getconnection/" + uniqueFileName; 
                        }
                        else if(request.AddSuportTicketRequest.Attachment2Path ==null)
                        {
                            request.AddSuportTicketRequest.Attachment2Path= "https://mankiwwa.blob.core.windows.net/getconnection/" + uniqueFileName;
                        }
                        else
                        {
                            request.AddSuportTicketRequest.Attachment3Path= "https://mankiwwa.blob.core.windows.net/getconnection/" + uniqueFileName;
                        }
                    }
                }
         
            }


            if (request.AddSuportTicketRequest.IssueId !=0 && request.AddSuportTicketRequest.Text !=null) {

                var data = _mapper.Map<AddSuportTicketRequest, SupportToken>(request.AddSuportTicketRequest);

                SupportToken orgn = new SupportToken();

                orgn = await _suportTokenWriteOnlyRepository.AddSupportToken(data);

                AddSuportTicketResponse res = new AddSuportTicketResponse();

                if (orgn != null) {

                    res.Status_code = 201;

                    res.Status = "Sucess";
                    res.Error = "";
                    res.Data = _mapper.Map<SupportToken, AddTicket>(orgn);
                    return await Task.FromResult(res);
                }
                else {

                    res.Status_code = 404;
                    res.Status = "UnSucess";
                    res.Error = "UnSucees";
                    res.Data = null;

                    return await Task.FromResult(res);

                }
            }
            else
            {
                AddSuportTicketResponse res = new AddSuportTicketResponse();
                res.Status_code = 422;
                res.Status = "Required fields not supplied";
                res.Error = "Required fields not supplied";
                res.Data = null;

                return await Task.FromResult(res);


            }

        }
    }
}