using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responces;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    public class PostController : Controller
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;  
        }




        [HttpGet(ApiRoute.Post.GetAll)]
        public async Task<IActionResult> Get()
        {
            var posts = await  _postService.GetAllAsync();
            return Ok(posts);
        }


        [HttpGet(ApiRoute.Post.Get)]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            var post =  await _postService.GetByIdAsync(postId);
            if (post is null)
            {
                return NotFound();
            }
            return Ok(post);
        }



        [HttpPut(ApiRoute.Post.Update)]
        public async Task<IActionResult> UpdatePost(Guid postId,[FromBody] UpdatePostRequest request)
        {
            if (postId != null)
            {
                var newPost = new Post { Id = postId, Name = request.Name };

                var updated = await _postService.UpdatePostAsync(newPost);
                if (updated)
                {
                    return Ok(newPost);
                }
                else
                    return BadRequest();
            }

            return NotFound();
          
        }



        [HttpPost(ApiRoute.Post.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {

            if (ModelState.IsValid)
            {
                var post = new Post { Name = postRequest.Name };
                var created = await _postService.CreatePostAsync(post);
                var location = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var baseUri = location + "/" + ApiRoute.Post.Get.Replace("{postId}", post.Id.ToString());
                if (created)
                {
                    var response = new CreateResponce { Name = postRequest.Name};
                    return Created(baseUri, response);
                }
            }
            return BadRequest();            
        }



        [HttpDelete(ApiRoute.Post.Delete)]
        public async  Task<IActionResult> Delete(Guid postId)
        {
          var deleted = await  _postService.DeletePostAsync(postId);

            if (deleted)
                return NoContent();
            else
                return NotFound();
        }
    }
}
