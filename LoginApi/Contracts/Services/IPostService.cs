using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Contracts.Services
{
    public interface IPostService
    {
        List<Post> GetPosts(string search);

        Post GetPostById(int postId);

        void AddPosts(Post post, string userId);

        void EditPosts(Post post);

        void RemovePost(int postId);

        void AddComment(Comment comment, string userId);

        List<Comment> GetComments(int postId);
    }
}
