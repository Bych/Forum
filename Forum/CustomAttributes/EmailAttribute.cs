using System.ComponentModel.DataAnnotations;

namespace Forum.CustomAttributes
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute()
            : base(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")
        {
            ErrorMessage = "Please provide a valid email address";
        }
    }
}