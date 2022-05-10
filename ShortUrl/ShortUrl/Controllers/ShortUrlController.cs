using System;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Models;
using ShortUrl.Utility;
using System.Web;

namespace ShortUrl.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private IConfiguration configuration;

        public ShortUrlController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet("{longUrl}")]
        public IActionResult GetShortUrl(string longUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(longUrl))
                    throw new ArgumentException("The original url must not be null or empty. Please enter a valid non-empty url");

                string decodedUrl = HttpUtility.UrlDecode(longUrl); //decode the speacial characters in the url


                if (!UriValidator.ValidateUri(decodedUrl)) // //validate BaseUrl here
                    throw new FormatException("The format of the original url is incorrect. Please correct the url and try again");


                var settings = configuration.GetSection("ShrinkUrlSettings").Get<ShrinkUrlSettings>();

                if (settings.BaseUrl != null)
                {
                    Uri baseUri = new Uri(settings.BaseUrl);
                    string encryptedUrl = Cryptography.EncryptUrl(decodedUrl, settings.MaxLength);

                    if (encryptedUrl != null)
                    {
                        Uri shortUri = UriValidator.CombineUri(baseUri, encryptedUrl);
                        return Ok(shortUri);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent(); //httpStatus204
        }
    }
}
