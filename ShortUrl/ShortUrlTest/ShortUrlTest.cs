using Xunit;
using ShortUrl.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ShortUrlTest;

public class ShortUrlControllerTests
{

    [Fact]
    public void GetShortUrl_Returns_The_Correct_Short_Url()
    {
        //Arrange

        //BaseUrl
        var configurationMock = new Mock<IConfiguration>();

        var configurationSectionBaseUrlMock = new Mock<IConfigurationSection>();

        configurationSectionBaseUrlMock
           .Setup(x => x.Value)
           .Returns("https://example.co");

        configurationMock
           .Setup(x => x.GetSection("ShrinkUrlSettings:BaseUrl"))
           .Returns(configurationSectionBaseUrlMock.Object);

        //maxlength

        var configurationSectionMaxUrlLengthMock = new Mock<IConfigurationSection>();
        configurationSectionMaxUrlLengthMock
           .Setup(x => x.Value)
           .Returns("6");

        configurationMock
           .Setup(x => x.GetSection("ShrinkUrlSettings:MaxLength"))
           .Returns(configurationSectionMaxUrlLengthMock.Object);


        ShortUrlController controller = new ShortUrlController((IConfiguration)configurationMock);
       

        var testUrl = "https://www.finning.com/welcome/canada/monitor/validate";


        //Act
        var result = controller.GetShortUrl(testUrl);
        var okResult = result as OkObjectResult;


        //Assert
        Assert.NotNull(okResult);

        Assert.True(okResult is OkObjectResult);

        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);



    }
}