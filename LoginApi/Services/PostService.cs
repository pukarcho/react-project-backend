using LoginApi.Contracts.Repositories;
using LoginApi.Contracts.Services;
using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public List<Post> GetPosts(string search)
        {
            return _postRepository.GetPosts(search);
        }

        public Post GetPostById(int postId)
        {
            return _postRepository.GetPostById(postId);
        }

        public void AddPosts(Post post, string userId)
        {
            var user = _userRepository.GetUserById(userId);
            post.PostBy = user.Username;

            _postRepository.AddPosts(post);
        }

        public void EditPosts(Post post)
        {
            _postRepository.EditPosts(post);
        }

        public void RemovePost(int postId)
        {
            _postRepository.RemovePost(postId);
        }

        public void AddComment(Comment comment, string userId)
        {
            var user = _userRepository.GetUserById(userId);
            comment.CommentBy = user.Username;

            _postRepository.AddComment(comment);
        }

        public List<Comment> GetComments(int postId)
        {
            return _postRepository.GetComments(postId);
        }
    }
}
