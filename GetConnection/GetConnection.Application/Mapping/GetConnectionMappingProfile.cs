using AutoMapper;
using GetConnection.Application.UseCases.IssueComplains.AddHrIssue;
using GetConnection.Application.UseCases.IssueComplains.GetHrIssue;
using GetConnection.Application.UseCases.IssueComplains.UpdateIssueComplain;
using GetConnection.Application.UseCases.Organiztions.GellAllOrganizations;
using GetConnection.Application.UseCases.Organiztions.InsertOraganization;
using GetConnection.Application.UseCases.SuportToken.AddSuportToken;
using GetConnection.Application.UseCases.SuportToken.GetSuportTokenById;
using GetConnection.Application.UseCases.Users.GetUserById;
using GetConnection.Application.UseCases.Users.InsertDeviceToken;
using GetConnection.Application.UseCases.Users.RegisterUser;
using GetConnection.Application.UseCases.Users.UpdateUser;
using GetConnection.Core.Entities;
using GetConnection.Core.ResponseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.Mapping
{
    
    public class GetConnectionMappingProfile : Profile
    {
        public GetConnectionMappingProfile()
        {
            CreateMap<GetConnection.Core.Entities.Organiztion,GetAllOrganiztionsResponse>().ReverseMap();
            CreateMap<InsertOrganizationRequest, Organiztion>().ReverseMap();
            CreateMap<InsertUserResponse, User>().ReverseMap();
            CreateMap<InsertUserRequest, User>().ReverseMap();
            CreateMap<InsertDeviceTokenRequest, DeviceToken>().ReverseMap();
            CreateMap<AddIssueComplainRequest, IssueComplain>().ReverseMap();
            CreateMap<UpdateIssueComplainRequest,IssueComplain>().ReverseMap();
            CreateMap<IssueComplainResponse, IssueComplain>().ReverseMap();
            CreateMap<UpdateUserRequest,User>().ReverseMap();
            /*Support Token*/
            CreateMap<AddSuportTicketRequest,SupportToken>().ReverseMap();
            CreateMap<SupportToken,Token>().ReverseMap();
            CreateMap<SupportToken,AddTicket>().ReverseMap();

            /*updateUser*/
            CreateMap<User, UpdateUser>().ReverseMap();

            //CreateMap<>.ReverseMap();

            //getUerById
            CreateMap<User,UserResponseGetByID>().ReverseMap();


            //suportToken
            CreateMap<SuportTokenGetById,Token>().ReverseMap();

        }
    }
}
