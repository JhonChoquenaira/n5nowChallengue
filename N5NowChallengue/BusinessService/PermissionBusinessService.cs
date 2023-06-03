using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.BusinessService.Interfaces;
using N5NowChallengue.Config;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;
using N5NowChallengue.ErrorHandler;
using Nest;
using Serilog;

namespace N5NowChallengue.BusinessService
{
    public class PermissionBusinessService : IPermissionBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IKafkaProducer _kafkaProducer;

        public PermissionBusinessService(IKafkaProducer kafkaProducer, IElasticClient elasticClient, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _kafkaProducer = kafkaProducer;
            _elasticClient = elasticClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<PermissionDto> GetAll()
        {
            Log.Information("Permission - GetAll");
            var listOfPermissions = _unitOfWork.Permissions.GetAll().Result.ToList();
            var listOfPermissionsDtos = _mapper.Map<List<PermissionDto>>(listOfPermissions);
            if (listOfPermissions.Count != 0)
            {
                var indexReponse = _elasticClient.IndexDocument(listOfPermissions[0]);
                if (indexReponse.IsValid)
                {
                    Log.Information("The permission was saved successfully");
                }
                else
                {
                    Log.Error("There was a problem when tried to save in elastic search");
                    throw new BaseException("There was a problem when tried to save in elastic search");
                }
            }

            KafkaDto kafkaDto = new KafkaDto() { NameOperation = "Get All Permissions" };
            _kafkaProducer.ProduceObject("registry", kafkaDto);
            return listOfPermissionsDtos;
        }

        public PermissionDto GetById(int permissionId)
        {
            Log.Information("Permission - GetById");
            var permission = _unitOfWork.Permissions.Get(permissionId).Result;
            if (permission != null)
            {
                var indexReponse = _elasticClient.IndexDocument(permission);
                if (indexReponse.IsValid)
                {
                    Log.Information("The permission was saved successfully");
                }
                else
                {
                    Log.Error("There was a problem when tried to save in elastic search");
                    throw new BaseException("There was a problem when tried to save in elastic search");
                }

                KafkaDto kafkaDto = new KafkaDto() { NameOperation = "Get Permission By Id" };
                _kafkaProducer.ProduceObject("registry", kafkaDto);
                return _mapper.Map<PermissionDto>(permission);
            }

            throw new BaseException("Not Found", 404);
        }

        public MessageDto UpdatePermission(Permission permissionUpdated)
        {
            Log.Information("Permission - UpdatePermission");
            var permission = _unitOfWork.Permissions.Get(permissionUpdated.PermissionId).Result;
            if (permission != null)
            {
                permission.PermissionCode = permissionUpdated.PermissionCode;
                permission.PermissionName = permissionUpdated.PermissionName;
                permission.Method = permissionUpdated.Method;
                permission.Status = permissionUpdated.Status;
                _unitOfWork.Permissions.Update(permission);
                if (_unitOfWork.Complete() != 0)
                {
                    var indexReponse = _elasticClient.IndexDocument(permissionUpdated);
                    if (indexReponse.IsValid)
                    {
                        Log.Information("The permission was saved successfully");
                    }
                    else
                    {
                        Log.Error("There was a problem when tried to save in elastic search");
                        throw new BaseException("There was a problem when tried to save in elastic search");
                    }

                    KafkaDto kafkaDto = new KafkaDto() { NameOperation = "Update Permission" };
                    _kafkaProducer.ProduceObject("registry", kafkaDto);
                    return new MessageDto() { message = "The permission was updated successfully" };
                }
                else
                {
                    Log.Error("There was an error updating the permission");
                    throw new BaseException("There was an error updating the permission", 404);
                }
            }

            Log.Error("Permission to update was not found");
            throw new BaseException("Permission to update was not found", 404);
        }

        public MessageDto AddPermission(PermissionDto newPermissionDto)
        {
            Log.Information("Permission - Add Permission");
            _unitOfWork.Permissions.Add(_mapper.Map<Permission>(newPermissionDto));
            var indexReponse = _elasticClient.IndexDocument(_mapper.Map<Permission>(newPermissionDto));
            if (indexReponse.IsValid)
            {
                Log.Information("The permission was saved successfully");
            }
            else
            {
                Log.Error("There was a problem when tried to save in elastic search");
                throw new BaseException("There was a problem when tried to save in elastic search");
            }

            KafkaDto kafkaDto = new KafkaDto() { NameOperation = "Add Permission" };
            _kafkaProducer.ProduceObject("registry", kafkaDto);
            if (_unitOfWork.Complete() != 0)
            {
                return new MessageDto() { message = "The permission was successfully saved" };
            }
            Log.Error("Error saving the permission");
            throw new BaseException("Error saving the permission", 404);
        }
    }
}