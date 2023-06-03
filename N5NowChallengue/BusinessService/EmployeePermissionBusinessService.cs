using AutoMapper;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.BusinessService.Interfaces;
using N5NowChallengue.Config;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;
using N5NowChallengue.DataService.Models.Types;
using N5NowChallengue.ErrorHandler;
using Nest;
using Serilog;

namespace N5NowChallengue.BusinessService
{
    public class EmployeePermissionBusinessService : IEmployeePermissionsBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IKafkaProducer _kafkaProducer;

        public EmployeePermissionBusinessService(IUnitOfWork unitOfWork, IMapper mapper, IElasticClient elasticClient,
            IKafkaProducer kafkaProducer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _elasticClient = elasticClient;
            _kafkaProducer = kafkaProducer;
        }

        public MessageDto RequestPermission(RequestPermissionDto requestPermissionDto)
        {
            Log.Information("EmployeePermission - RequestPermission");
            var existsEmployeePermission =
                _unitOfWork.EmployeePermissions.Get(requestPermissionDto.EmployeeId, requestPermissionDto.PermissionId)
                    .Result;
            var existsEmployee = _unitOfWork.Employees.Get(requestPermissionDto.EmployeeId).Result;
            var existsPermission = _unitOfWork.Permissions.Get(requestPermissionDto.PermissionId).Result;
            if (existsEmployee != null && existsPermission != null)
            {
                if (existsEmployeePermission == null)
                {
                    var employeePermission = _mapper.Map<EmployeePermission>(requestPermissionDto);
                    employeePermission.RequestedStatus = StatusEmployeePermission.Requested;
                    _unitOfWork.EmployeePermissions.Add(employeePermission);

                    var indexReponse = _elasticClient.IndexDocument(employeePermission);
                    if (indexReponse.IsValid)
                    {
                        Log.Information("The request permission was saved successfully");
                    }
                    else
                    {
                        Log.Error("There was a problem when tried to save in elastic search");
                        throw new BaseException("There was a problem when tried to save in elastic search");
                    }

                    KafkaDto kafkaDto = new KafkaDto() { NameOperation = "Request Permission" };
                    _kafkaProducer.ProduceObject("registry", kafkaDto);

                    if (_unitOfWork.Complete() != 0)
                    {
                        return new MessageDto() { message = "The requested was successfully saved" };
                    }

                    Log.Error("Error saved the requested");
                    throw new BaseException("Error saved the requested", 404);
                }

                return new MessageDto() { message = "Permission has already been requested" };
            }
            throw new BaseException("Not found", 404);
        }
    }
}