using Azure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using URL.Model;

namespace URL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        [EnableCors("AllowAngularFrontend")]
    public class UrlController : ControllerBase
    {



        private readonly Urlcontext _context;
        //  private readonly IHttpContextAccessor _configuration;
        public UrlController(Urlcontext context)
        {
            _context = context;
            //  _configuration= httpContextAccessor;
        }

        // private const string BaseUrl = "https://e";
        private const string AllowedCharacters = "abcdefghijklmnopqrstuvwzyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int ShortUrlLength = 6;
        [HttpPost]
        [Route("GetLongUrl")]
        public async Task<ActionResult<string>> GetLongUrl(Request url)
        {
            // shirt url

            var url1 = await _context.Urls.FirstOrDefaultAsync(u => u.shortUrl ==  url.shortUrl);
            if (url1 == null)
            {
                return NotFound();
            }
            var response = new LongUrlResponse
            {
                LongUrl = url1.OriginalUrl
            };

            return Ok(response);
           // return Redirect(url1.OriginalUrl);
            ////orginalUrl from shorturl

            // Response.Redirect(orginalUrl);
            //  return Ok();
        }

        

        //[HttpPost]
        //[Route("ShortenUrl")]
        //public async Task<ActionResult<string>> ShortenUrl(RequestPayload orginalUrl)
        //{
        //    var url = new Url { OriginalUrl = orginalUrl.OriginalUrl.OriginalString };

        //    // await _context.SaveChangesAsync();
        //    var shortUrl = GenerateShortUrl();
        //    url.shortUrl = shortUrl;
        //    _context.Urls.Add(url);
        //    await _context.SaveChangesAsync();
        //    var response = new ShortURlResponse
        //    {
        //        ShortUrl = shortUrl
        //    };
        //    // return Ok("{\"shortUrl\":\""+shortUrl+"\"}");
        //    return Ok(response);
        //    // var x = orginalUrl.Host;
        //    // return Ok(x);

        //}
        //private static string GenerateShortUrl()
        //{

        //    var random = new Random();
        //    var shortUrl = new StringBuilder();
        //    for (int i = 0; i < ShortUrlLength; i++)
        //    // var x = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
        //    {
        //        int randomIndex = random.Next(0, AllowedCharacters.Length);
        //        shortUrl.Append(AllowedCharacters[randomIndex]);
        //    }
        //    return shortUrl.ToString();
        //}


        [HttpPost]
        [Route("ShortenUrl")]
        public async Task<ActionResult<string>> ShortenUrl(RequestPayload orginalUrl)
        {
            //url
            //  applicationUrl
            var url = new Url { OriginalUrl = orginalUrl.OriginalUrl.OriginalString };
            _context.Urls.Add(url);

            // await _context.SaveChangesAsync();

            var shortUrl = GenerateShortUrl(orginalUrl.OriginalUrl.Scheme + "://" + orginalUrl.OriginalUrl.Host);
            //  var shortUrl = GenerateShortUrl(orginalUrl.Scheme +);
            url.shortUrl = shortUrl;
            await _context.SaveChangesAsync();
            var response = new ShortURlResponse
            {
                ShortUrl = shortUrl
            };
            return Ok(response);
            // var x = orginalUrl.Host;
            // return Ok(x);

        }
        private string GenerateShortUrl(string BaseUrl)
        {

            var random = new Random();
            var shortUrl = new StringBuilder();
            var x = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            // HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            for (int i = 0; i < ShortUrlLength; i++)
            {
                int randomIndex = random.Next(0, AllowedCharacters.Length);
                shortUrl.Append(AllowedCharacters[randomIndex]);
            }
            return x + "/" + shortUrl.ToString();
        }


    }

    internal class ShortURlResponse
    {
        public string ShortUrl { get; set; }
       
    }
    internal class LongUrlResponse
    {
        public string LongUrl { get; set; }
    }
}
