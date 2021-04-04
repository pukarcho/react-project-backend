using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Contracts.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetPosts(string search);

        Post GetPostById(int postId);

        void AddPosts(Post post);

        void EditPosts(Post post);

        void RemovePost(int postId);

        void AddComment(Comment comment);

        List<Comment> GetComments(int postId);
    }
}
