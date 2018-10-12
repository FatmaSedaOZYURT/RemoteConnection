using System.ComponentModel.DataAnnotations;
namespace ConnectApp.Data
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string SurName { get; set; }
        
    }
}
