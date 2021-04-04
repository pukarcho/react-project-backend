using IdentityModel;
using LoginApi.Contracts.Services;
using LoginApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginApi.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [Route("get_all")]
        [HttpPost]
        public List<Post> GetPosts([FromBody]string search)
        {
            return _postService.GetPosts(search);
        }

        [Route("get_by_id")]
        [HttpPost]
        public Post GetPostById([FromBody] int postId)
        {
            return _postService.GetPostById(postId);
        }

        [Route("add")]
        [HttpPost]
        public void AddPosts(Post post)
        {
            var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);

            _postService.AddPosts(post, userId);
        }

        [Route("edit")]
        [HttpPost]
        public void EditPosts(Post post)
        {
            _postService.EditPosts(post);
        }

        [Route("remove")]
        [HttpPost]
        public void RemovePost([FromBody] int postId)
        {
            _postService.RemovePost(postId);
        }

        [Route("add_comment")]
        [HttpPost]
        public void AddComment(Comment comment)
        {
            var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);

            _postService.AddComment(comment, userId);
        }

        [AllowAnonymous]
        [Route("get_comments")]
        [HttpPost]
        public List<Comment> GetComments([FromBody]int postId)
        {
            return _postService.GetComments(postId);
        }
    }
}
