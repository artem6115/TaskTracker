using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public static class CommentService
    {
        public static async Task<List<Comment>> GetComments(long TaskId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Comment/{TaskId}");
            List<Comment> coments = await AuthService.SendAsync<List<Comment>>(request);
            if(coments != null)
                coments.ForEach(comment => 
                comment.IsMyComment = (comment.User.Id == AuthService.User.Id));
            
            return coments;
        }
        public static async Task<bool> DeleteComment(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Comment/{Id}");
            var result = await AuthService.SendAsync(request);
            return result is not null && result.IsSuccessStatusCode;
        }
        public static async Task<Comment> CreateComment(Comment comment)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Comment");
            request.Content = JsonContent.Create(comment);
            var newComment = await AuthService.SendAsync<Comment>(request);
            if(newComment is not null)newComment.IsMyComment = true;
            return newComment;
        }
        public static async Task<Comment> UpdateComment(Comment comment)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Comment");
            request.Content = JsonContent.Create(comment);
            var newComment = await AuthService.SendAsync<Comment>(request);
            if (newComment is not null) newComment.IsMyComment = true;
            return newComment;
        }


    }
}
