using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using N5NowChallengue.BusinessService;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.BusinessService.Interfaces;
using N5NowChallengue.Config;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;
using Nest;
using NUnit.Framework;
using Serilog;

namespace N5NowTest
{
    [TestFixture]
    public class PermissionBusinessServiceUnitTest
    {
        private IPermissionBusinessService _permissionBusinessService;
        private IUnitOfWork unitOfWork;
        private IElasticClient elasticClient;
        private IKafkaProducer kafkaProducer;
        private IMapper mapper;
        private List<Permission> fakeBD;

        [SetUp]
        public virtual void Setup()
        {
            var mockKafkaProducer = new Mock<IKafkaProducer>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockElasticClient = new Mock<IElasticClient>();

            fakeBD = new List<Permission>();

            mockMapper.Setup(e => e.Map<Permission>(It.IsAny<PermissionDto>()))
                .Returns(new Permission
                {
                    PermissionId = 1, PermissionCode = "RTL354", PermissionName = "Login", Method = "POST",
                    Status = true
                });
            mockMapper.Setup(e => e.Map<PermissionDto>(It.IsAny<Permission>()))
                .Returns(new PermissionDto
                    { PermissionCode = "RTL354", PermissionName = "Login", Method = "POST", Status = true });
            mapper = mockMapper.Object;

            List<PermissionDto> permissionDtos = new List<PermissionDto>
            {
                new PermissionDto
                    { PermissionCode = "RTL357", PermissionName = "Login", Method = "POST", Status = true },
                new PermissionDto
                    { PermissionCode = "RTL358", PermissionName = "Enroll", Method = "POST", Status = true }
            };
            mockMapper.Setup(e => e.Map<List<PermissionDto>>(It.IsAny<List<Permission>>()))
                .Returns(permissionDtos);
            mapper = mockMapper.Object;

            mockUnitOfWork.Setup(e => e.Permissions.Add(It.IsAny<Permission>()))
                .Returns<Permission>(permission =>
                {
                    fakeBD.Add(permission);
                    return Task.CompletedTask;
                });
            IEnumerable<Permission> items = new List<Permission>
            {
                new Permission
                {
                    PermissionId = 1, PermissionCode = "RTL354", PermissionName = "Login", Method = "POST",
                    Status = true
                },
                new Permission
                {
                    PermissionId = 2, PermissionCode = "RTU856", PermissionName = "Enroll", Method = "POST",
                    Status = true
                }
            };
            mockUnitOfWork.Setup(e => e.Permissions.GetAll()).ReturnsAsync(items);

            Permission item = new Permission
            {
                PermissionId = 1, PermissionCode = "RTL354", PermissionName = "Login", Method = "POST",
                Status = true
            };
            mockUnitOfWork.Setup(e => e.Permissions.Get(It.IsAny<int>())).ReturnsAsync(item);

            mockUnitOfWork.Setup(e => e.Permissions.Update(It.IsAny<Permission>()))
                .Callback<Permission>(e =>
                {
                    fakeBD.RemoveAll(permission => permission.PermissionId == 1);
                    fakeBD.Add(e);
                });

            mockUnitOfWork.Setup(e => e.Complete()).Returns(1);
            mockUnitOfWork.Setup(e => e.Permissions.Add(It.IsAny<Permission>())).Returns<Permission>(permission =>
            {
                fakeBD.Add(permission);
                return Task.CompletedTask;
            });
            unitOfWork = mockUnitOfWork.Object;

            var mockIndexResponse = new Mock<IndexResponse>();
            mockIndexResponse.SetupGet(r => r.IsValid).Returns(true);

            mockElasticClient
                .Setup(client => client.IndexDocument(It.IsAny<object>())).Returns(mockIndexResponse.Object);
            elasticClient = mockElasticClient.Object;

            mockKafkaProducer
                .Setup(producer => producer.ProduceObject(It.IsAny<string>(), It.IsAny<KafkaDto>()))
                .Callback<string, KafkaDto>((topic, kafkaDto) =>
                {
                    if (true)
                    {
                        Log.Information($"The kafkaDto with Id {kafkaDto.Id} was successfully persisted");
                    }
                });
            kafkaProducer = mockKafkaProducer.Object;

            _permissionBusinessService =
                new PermissionBusinessService(kafkaProducer, elasticClient, mapper, unitOfWork);
        }

        [Test]
        public void GetAll_Should_Return_ListOfPermissionsNotNull()
        {
            // Arrange

            // Act
            var result = _permissionBusinessService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetById_Should_Return_NotNull()
        {
            // Arrange

            // Act
            var result = _permissionBusinessService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdatePermission_Should_Suceed()
        {
            // Arrange
            _permissionBusinessService.AddPermission(new PermissionDto
                { PermissionCode = "RTL354", PermissionName = "Login", Method = "POST", Status = true });

            // Act
            var result = _permissionBusinessService.UpdatePermission(new Permission
            {
                PermissionName = "Login2", PermissionId = 1, Method = "POST", Status = true, PermissionCode = "RTL546"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.message, "The permission was updated successfully");
            Assert.AreEqual(fakeBD.Find(e => e.PermissionId == 1).PermissionCode, "RTL546");
        }
        
        [Test]
        public void AddPermission_Should_Suceed()
        {
            // Arrange
            

            // Act
            var result = _permissionBusinessService.AddPermission(new PermissionDto
                { PermissionCode = "RTL354", PermissionName = "Login", Method = "POST", Status = true });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.message, "The permission was successfully saved");
            Assert.AreEqual(fakeBD.Find(e => e.PermissionId == 1).PermissionCode, "RTL354");
        }
    }
}