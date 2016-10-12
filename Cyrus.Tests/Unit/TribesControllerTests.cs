using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Cyrus.Core.DomainModels;
using Cyrus.Services;
using Cyrus.WebApi.Controllers;

namespace Cyrus.Tests.Unit
{

    [TestFixture]
    public class TribesControllerTests
    {
        private IEnumerable<Tribe> _tribes;

        [OneTimeSetUp]
        public void Setup()
        {
            _tribes = SetUpTribes();
        }

        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _tribes = null;
        }

        public static Tribe SetUpTribes(int id)
        {
            var allTribes = SetUpTribes();

            var enumerable = allTribes as Tribe[] ?? allTribes.ToArray();

            if (enumerable.Any(x => x.Id == id))
            {
                return enumerable.First(x => x.Id == id);
            }

            else return null;

        }

        private static IEnumerable<Tribe> SetUpTribes()
        {
            return new[]
            {
                new Tribe()
                {
                    Id = 1,
                    Name = "Alley Cats",
                    MemberCount = 21,
                    Description = "Scruffy, Scrappy, Cats"

                },
                new Tribe()
                {
                    Id = 2,
                    Name = "Popular Cats",
                    MemberCount = 321,
                    Description = "Scruffy, Scrappy, Cats"
                },
                new Tribe()
                {
                    Id = 3,
                    Name = "Lone Cats",
                    MemberCount = 3,
                    Description = "Lone Wolf... Cats"
                },
                new Tribe()
                {
                    Id = 4,
                    Name = "Mean Cats",
                    MemberCount = 67,
                    Description = "Some Seriously Tough Cats"
                },
                new Tribe()
                {
                    Id = 5,
                    AccountId = 1, // what is the point of this?
                    Description = "Lots of Pussies",
                    Name = "Pussy Galore",
                    IsActive = true,
                    IsPublic = true,
                    MemberCount = 3,
                    Members = new List<TribeMember>()
                    {
                        new TribeMember()
                        {
                            Id = 1,
                            ProfileId = 4,
                            IsAdmin = false,
                            IsApproved = false,
                            TribeId = 5
                        },
                        new TribeMember()
                        {
                            Id = 2,
                            ProfileId = 1,
                            IsAdmin = false,
                            IsApproved = false,
                            TribeId = 5
                        },
                        new TribeMember()
                        {
                            Id = 3,
                            ProfileId = 2,
                            IsAdmin = true,
                            IsApproved = true,
                            TribeId = 5
                        }
                    }
                }
            };

        }

        [Test]
        public void Get_ShouldReturn_AllTribes()
        {
            // Arrange:
            var tribeServiceMock = new Mock<ITribeService>();
            tribeServiceMock.Setup(service => service.GetTribes()).Returns(_tribes);

            var controller = new TribesController(tribeServiceMock.Object);

            // Act: 
            var actionResult = controller.GetTribes();

            Assert.IsNotNull(actionResult);

            var response = actionResult as OkNegotiatedContentResult<IEnumerable<Tribe>>;

            Assert.IsNotNull(response);

            var tribes = response.Content;

            // Assert:
            Assert.AreEqual(5, tribes.Count());

        }

        [Test]
        public void GetWithId_ShouldReturn_ThatTribe()
        {
            // Arrange:

            var tribe = SetUpTribes(1);

            var tribeServiceMock = new Mock<ITribeService>();
            tribeServiceMock.Setup(service => service.GetTribeByTribeId(1)).Returns(tribe);

            var controller = new TribesController(tribeServiceMock.Object);

            // Act: 
            var actionResult = controller.GetTribeByTribeId(1);
            var response = actionResult as OkNegotiatedContentResult<Tribe>;

            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Content.Id);
        }

        [Test]
        public void Put_ShouldUpdateTribe_RetrunsTribeId()
        {
            // Arrange:
            var tribeServiceMock = new Mock<ITribeService>();

            tribeServiceMock.Setup(service => service.InsertOrUpdateTribe(
                It.IsAny<Tribe>())).Returns(1);

            var controller = new TribesController(tribeServiceMock.Object);

            // Act:
            var actionResult = controller.UpdateTribe(

                new Tribe()
                {
                    Id = 1,
                    Name = "Terrible Tabbys",
                    MemberCount = 8,
                    Description = "Simply Terrible"
                });

            var response = actionResult as OkNegotiatedContentResult<int>;

            // Assert:
            Assert.IsNotNull(response);
            var tribeId = response.Content;

            Assert.AreEqual(1, tribeId);

        }

        [Test]
        public void Post_ShouldAddTribe_ReturnsRouteValResponse()
        {
            var tribeServiceMock = new Mock<ITribeService>();

            tribeServiceMock.Setup(service => service.InsertOrUpdateTribe(
                It.IsAny<Tribe>())).Returns(5);

            var controller = new TribesController(tribeServiceMock.Object);

            var tribe = SetUpTribes(5);

            var actionResult = controller.CreateTribe(tribe);

            var response = actionResult as CreatedAtRouteNegotiatedContentResult<Tribe>;

            Assert.IsNotNull(response);
            Assert.AreEqual("DefaultApi", response.RouteName);
            Assert.AreEqual(response.Content.Id, response.RouteValues["returnId"]);
        }


        [Test]
        public void Get_ShouldReturn_TribeNotFound()
        {
            // Arrange
            var tribeServiceMock = new Mock<ITribeService>();
            tribeServiceMock.Setup(service => service.GetTribes()).Returns(_tribes);

            var controller = new TribesController(tribeServiceMock.Object);

            // Act:
            var actionResult = controller.GetTribeByTribeId(998323456);
            var notFoundRes = actionResult as NotFoundResult;

            // Assert:
            Assert.IsNotNull(notFoundRes);

        }

    }

}