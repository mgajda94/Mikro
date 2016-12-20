using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Mikro.Models;
using Mikro.Repository;
using Mikro.Controllers;

namespace Mikro.Extension
{
    public class TagFunction
    {
        public string TagToUrl(string content, IEnumerable<string> tagsList)
        {
            string name = "";
            string output = "";

            foreach (var item in tagsList)
            {
                name = item.Replace("#", "");
                output = content
                    .Replace(item, "<a href='/tag/" + name + "'>" + item + "</a>");
            }

            return output;
        }

        public void IsExist(IEnumerable<string> tags, UnitOfWork uow, Post post)
        {
            string name;
            var isExist = false;

            foreach (var item in tags)
            {
                name = item.Replace("#", "");

                var existingTag = uow.Repository<Tag>().Select(x => x.Name == name);
                if (existingTag == null)
                {
                    var tag = new Tag { Name = name };
                    uow.Repository<Tag>().Add(tag);

                    var postTag = new PostTag
                    {
                        Post = post,
                        Tag = tag
                    };
                    uow.Repository<Tag>().Add(tag);
                    uow.Repository<PostTag>().Add(postTag);
                }
                else
                {
                    var postTags = uow.Repository<PostTag>().GetOverview(x => x.Tag == existingTag);
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
                        uow.Repository<PostTag>().Add(postTag);
                    }                  
                }              
            }
        }
    }
}