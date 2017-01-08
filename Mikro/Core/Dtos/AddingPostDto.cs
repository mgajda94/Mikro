using System.ComponentModel.DataAnnotations;

namespace Mikro.Core.Dtos
{
    public class AddingPostDto
    {
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}