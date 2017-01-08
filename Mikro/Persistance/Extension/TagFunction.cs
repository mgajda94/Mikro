using System.Collections.Generic;
using Mikro.Core;
using Mikro.Core.Models;

namespace Mikro.Persistance.Extension
{
    public class TagFunction
    {
        public string TagToUrl(string content, IEnumerable<string> tagsList)
        {
            string name = "";
            string output = content;

            if (tagsList == null)
                return content;

            foreach (var item in tagsList)
            {
                name = item.Replace("#", "");
                output = content
                    .Replace(item, "<a href='/tag/" + name + "'>" + item + "</a>");
            }

            return output;
        }

        public void IsExist(IEnumerable<string> tags, IUnitOfWork uow, Post post)
        {
            string name;
            var isExist = false;

            foreach (var item in tags)
            {
                name = item.Replace("#", "");

                var existingTag = uow.Tags.GetTagByName(name);
                if (existingTag == null)
                {
                    var tag = new Tag { Name = name };
                    uow.Tags.Add(tag);

                    var postTag = new PostTag
                    {
                        Post = post,
                        Tag = tag
                    };
                    uow.Tags.Add(tag);
                    uow.PostTags.Add(postTag);
                }
                else
                {
                    var postTags = uow.PostTags.GetPostTagsByTagId(existingTag.Id);
                    foreach (var tag in postTags)
                    {
                        if (tag.Post == post)
                            isExist = true;
                    }
                    if (isExist == false)
                    {
                        var postTag = new PostTag
                        {
                            Post = post,
                            Tag = existingTag
                        };
                        uow.PostTags.Add(postTag);
                    }                  
                }              
            }
        }
    }
}