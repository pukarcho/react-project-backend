using LoginApi.Contracts.Repositories;
using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        static List<Post> posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Name = "Бургер",
                Description = "майонеза - 1/2 чаша чесън - 2 скилдки телешко филе - 4х120 г бекон - 4 слайса питки - 4 бр. сирене - 4 резена зелена салата червено цвекло - 1 чаша мариновано",
                PostBy = "admin",
                Images = new List<string>() {
                    "https://media1.s-nbcnews.com/i/newscms/2019_21/2870431/190524-classic-american-cheeseburger-ew-207p_d9270c5c545b30ea094084c7f2342eb4.jpg",
                    "https://cdn.vox-cdn.com/thumbor/RzabiOl7nXsT1uVOlO0SxhSNDhg=/0x0:1117x521/1200x800/filters:focal(481x215:659x393)/cdn.vox-cdn.com/uploads/chorus_image/image/68758039/McPlant_Burger.0.png"
                }
            },
            new Post
            {
                Id = 2,
                Name = "Сандвич",
                Description = "Две филии хляб и посредата трета",
                PostBy = "БедниятСтудент",
                Images = new List<string>() {
                    "https://media.kaufland.com/images/PPIM/AP_Content_1010/std.lang.all/13/76/Asset_1671376.jpg",
                    "https://m4.netinfo.bg/media/images/15625/15625336/960-540-bon-apeti.jpg",
                    "https://img.haskovo.net/uploads/recipes/2015/04/03/15333.jpg",
                    "https://craftlog.com/m/i/8626646=s1280=h960"
                }
            }
        };

        static List<Comment> comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                PostId = 1,
                CommentBy = "admin",
                CommentText = "Вкусно!"
            }
        };

        public List<Post> GetPosts(string search)
        {
            if (search == string.Empty)
            {
                return posts;
            }
            else
            {
                return posts.Where(a => a.Name.ToLower().Contains(search.ToLower())).ToList();
            }
        }

        public Post GetPostById(int postId)
        {
            return posts.Where(a => a.Id == postId).FirstOrDefault();
        }

        public void AddPosts(Post post)
        {
            post.Id = posts[posts.Count - 1].Id + 1;

            posts.Add(post);
        }

        public void EditPosts(Post post)
        {
            var index = posts.FindIndex(a => a.Id == post.Id);

            posts[index].Name = post.Name;
            posts[index].Description = post.Description;
        }

        public void RemovePost(int postId)
        {
            var postToRemove = posts.Single(a => a.Id == postId);
            posts.Remove(postToRemove);
        }

        public void AddComment(Comment comment)
        {
            comment.Id = comments[comments.Count - 1].Id + 1;

            comments.Add(comment);
        }

        public List<Comment> GetComments(int postId)
        {
            return comments.Where(a => a.PostId == postId).ToList();
        }
    }
}
