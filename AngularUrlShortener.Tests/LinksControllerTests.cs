using AngularUrlShortener.Controllers;
using AngularUrlShortener.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularUrlShortener.Tests
{
    public class LinksControllerTests
    {
        [Fact]
        public void CanGetUrlFromShortLink()
        {
            // Arrange
            string expectedShort = "http://localhost:4200/asf4ss";
            string expectedUrl = "https://website.com/signup";

            Mock<ILinksRepository> linkRepo = new Mock<ILinksRepository>();
            linkRepo.Setup(l => l.Get("asf4ss"))
                .Returns(new Link { Id = 1, ShortUrl = "asf4ss", Url = "https://website.com/signup" });

            Mock<ISettingsRepository> settingsRepo = new Mock<ISettingsRepository>();
            settingsRepo.Setup(s => s.Get())
                .Returns(new Settings { Hostname = "http://localhost:4200" });

            LinksController linksController = new LinksController(null, linkRepo.Object, null, settingsRepo.Object);
            // Act
            var link = (linksController.Get("asf4ss") as OkObjectResult).Value as Link;

            // Assert
            Assert.NotNull(link);
            Assert.Equal(expectedShort, link.ShortUrl);
            Assert.Equal(expectedUrl, link.Url);
        }
    }
}
